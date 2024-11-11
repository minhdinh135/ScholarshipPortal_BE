using Application.Interfaces.IRepositories;
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
            q => q.Include(sp => sp.ScholarshipProgramUniversities).ThenInclude(u => u.University),
            q => q.Include(sp => sp.ScholarshipProgramCertificates).ThenInclude(c => c.Certificate),
            q => q.Include(sp => sp.MajorSkills).ThenInclude(ms => ms.Major),
            q => q.Include(sp => sp.MajorSkills).ThenInclude(ms => ms.Skill)
        };
        var scholarshipPrograms = await GetPaginatedList(includes, listOptions);

        return scholarshipPrograms;
    }

    public async Task<ScholarshipProgram> GetScholarsipProgramById(int id)
    {
        var scholarshipProgram = await _dbContext.ScholarshipPrograms
            // .AsNoTracking()
            .AsSplitQuery()
            .Include(sp => sp.Category)
            .Include(sp => sp.ScholarshipProgramUniversities)
            .ThenInclude(spm => spm.University)
            .Include(sp => sp.MajorSkills)
            .ThenInclude(spm => spm.Major)
            .Include(sp => sp.MajorSkills)
            .ThenInclude(spm => spm.Skill)
            .Include(sp => sp.ScholarshipProgramCertificates)
            .ThenInclude(spc => spc.Certificate)
            .FirstOrDefaultAsync(sp => sp.Id == id);

        return scholarshipProgram;
    }

    public async Task<IEnumerable<ScholarshipProgram>> GetScholarshipProgramByMajorId(int majorId)
    {
        var scholarshipPrograms = await _dbContext.ScholarshipPrograms
            .AsSplitQuery()
            .Include(sp => sp.MajorSkills)
            .ThenInclude(ms => ms.Major)
            .Where(sp => sp.MajorSkills.Any(ms => ms.MajorId == majorId))
            .ToListAsync();

        return scholarshipPrograms;
    }

    public async Task DeleteRelatedInformation(ScholarshipProgram scholarshipProgram)
    {
        await _dbContext.MajorSkills.Where(ms => ms.ScholarshipProgramId == scholarshipProgram.Id).ExecuteDeleteAsync();
        await _dbContext.ScholarshipProgramUniversities.Where(spu => spu.ScholarshipProgramId == scholarshipProgram.Id)
            .ExecuteDeleteAsync();
        await _dbContext.ScholarshipProgramCertificates.Where(spc => spc.ScholarshipProgramId == scholarshipProgram.Id)
            .ExecuteDeleteAsync();
    }
}