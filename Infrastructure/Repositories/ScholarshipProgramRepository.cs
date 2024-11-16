using Application.Interfaces.IRepositories;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ScholarshipProgramRepository : GenericRepository<ScholarshipProgram>, IScholarshipProgramRepository
{
    public ScholarshipProgramRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }

    public async Task<PaginatedList<ScholarshipProgram>> GetAllScholarshipPrograms(ListOptions listOptions)
    {
        var includes = new Func<IQueryable<ScholarshipProgram>, IQueryable<ScholarshipProgram>>[]
        {
            q => q.Include(sp => sp.Category),
            q => q.Include(sp => sp.University),
            q => q.Include(sp => sp.ScholarshipProgramCertificates).ThenInclude(c => c.Certificate),
            q => q.Include(sp => sp.Major).ThenInclude(m => m.MajorSkills).ThenInclude(ms => ms.Skill),
        };
        var scholarshipPrograms = await GetPaginatedList(includes, listOptions);

        return scholarshipPrograms;
    }

    public async Task<ScholarshipProgram> GetScholarsipProgramById(int id)
    {
        var scholarshipProgram = await _dbContext.ScholarshipPrograms
            .AsSplitQuery()
            .Include(sp => sp.Category)
            .Include(sp => sp.University)
            .Include(sp => sp.Major)
            .ThenInclude(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .Include(sp => sp.ScholarshipProgramCertificates)
            .ThenInclude(spc => spc.Certificate)
            .FirstOrDefaultAsync(sp => sp.Id == id);

        return scholarshipProgram;
    }

    public async Task<IEnumerable<ScholarshipProgram>> GetScholarshipProgramByMajorId(int majorId)
    {
        var scholarshipPrograms = await _dbContext.ScholarshipPrograms
            .AsSplitQuery()
            .Include(sp => sp.Major)
            .ThenInclude(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .Where(sp => sp.MajorId == majorId)
            .ToListAsync();

        return scholarshipPrograms;
    }

    public async Task<IEnumerable<ScholarshipProgram>> GetOpenScholarshipPrograms()
    {
        var scholarshipPrograms = await _dbContext.ScholarshipPrograms
            .Where(s => s.Status == ScholarshipProgramStatusEnum.Open.ToString())
            .ToListAsync();

        return scholarshipPrograms;
    }

    public async Task DeleteRelatedInformation(ScholarshipProgram scholarshipProgram)
    {
        await _dbContext.ScholarshipProgramCertificates.Where(spc => spc.ScholarshipProgramId == scholarshipProgram.Id)
            .ExecuteDeleteAsync();
    }
}