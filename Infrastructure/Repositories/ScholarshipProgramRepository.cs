using Application.Interfaces.IRepositories;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;

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
            q => q.Include(sp => sp.University).ThenInclude(u => u.Country),
            q => q.Include(sp => sp.ScholarshipProgramCertificates).ThenInclude(c => c.Certificate),
            q => q.Include(sp => sp.Major).ThenInclude(m => m.MajorSkills).ThenInclude(ms => ms.Skill),
            q => q.Include(sp => sp.Criteria),
            q => q.Include(sp => sp.Documents),
            q => q.Include(sp => sp.ReviewMilestones),
            q => q.Include(sp => sp.AwardMilestones)
        };
        var scholarshipPrograms = await GetPaginatedList(includes, listOptions);

        return scholarshipPrograms;
    }

    public async Task<ScholarshipProgram> GetScholarsipProgramById(int id)
    {
        var scholarshipProgram = await _dbContext.ScholarshipPrograms
            .AsSplitQuery()
            .Include(sp => sp.Criteria)
            .Include(sp => sp.Documents)
            .Include(sp => sp.ReviewMilestones)
            .Include(sp => sp.AwardMilestones)
            .Include(sp => sp.Category)
            .Include(sp => sp.University)
            .ThenInclude(u => u.Country)
            .Include(sp => sp.Major)
            .ThenInclude(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .Include(sp => sp.ScholarshipProgramCertificates)
            .ThenInclude(spc => spc.Certificate)
            .FirstOrDefaultAsync(sp => sp.Id == id);

        return scholarshipProgram;
    }

    public async Task<IEnumerable<ScholarshipProgram>> SearchScholarshipPrograms(
        ScholarshipSearchOptions scholarshipSearchOptions)
    {
        var scholarshipPrograms = await _dbContext.ScholarshipPrograms
            .AsNoTracking()
            .AsSplitQuery()
            .Include(sp => sp.Category)
            .Include(sp => sp.University).ThenInclude(u => u.Country)
            .Include(sp => sp.ScholarshipProgramCertificates).ThenInclude(spc => spc.Certificate)
            .Include(sp => sp.Major).ThenInclude(m => m.MajorSkills).ThenInclude(ms => ms.Skill)
            .Include(sp => sp.Criteria)
            .Include(sp => sp.ReviewMilestones)
            .Include(sp => sp.AwardMilestones)
            .ToListAsync();

        return scholarshipPrograms
            .Search(s => s.Name)
            .SetCulture(StringComparison.OrdinalIgnoreCase)
            .Containing(scholarshipSearchOptions.Name)
            .Search(s => s.Category.Name)
            .EqualTo(scholarshipSearchOptions.CategoryName)
            .Search(s => s.ScholarshipAmount)
            .Between(scholarshipSearchOptions.ScholarshipMinAmount, scholarshipSearchOptions.ScholarshipMaxAmount)
            .Search(s => s.Deadline)
            .LessThanOrEqualTo(scholarshipSearchOptions.Deadline);
    }

    public async Task<IEnumerable<ScholarshipProgram>> GetScholarshipProgramByMajorId(int majorId)
    {
        var scholarshipPrograms = await _dbContext.ScholarshipPrograms
            .AsSplitQuery()
            .Include(sp => sp.University)
            .ThenInclude(u => u.Country)
            .Include(sp => sp.Category)
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

    public async Task DeleteScholarshipCertificates(ScholarshipProgram scholarshipProgram)
    {
        await _dbContext.ScholarshipProgramCertificates.Where(spc => spc.ScholarshipProgramId == scholarshipProgram.Id)
            .ExecuteDeleteAsync();
    }
}