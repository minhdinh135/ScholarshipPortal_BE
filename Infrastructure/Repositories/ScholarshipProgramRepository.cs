using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ScholarshipProgramRepository : GenericRepository<ScholarshipProgram>, IScholarshipProgramRepository
{
    public ScholarshipProgramRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }


    public async Task<IEnumerable<ScholarshipProgram>> GetAllScholarshipPrograms()
    {
        var scholarshipPrograms = await _dbContext.ScholarshipPrograms
            .Include(sp => sp.Category)
            .Include(sp => sp.ScholarshipProgramUniversities)
            .ThenInclude(spu => spu.University)
            .Include(sp => sp.ScholarshipProgramMajors)
            .ThenInclude(spm => spm.Major)
            .ThenInclude(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .Include(sp => sp.ScholarshipProgramCertificates)
            .ThenInclude(spc => spc.Certificate)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

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
            .Include(sp => sp.ScholarshipProgramMajors)
            .ThenInclude(spm => spm.Major)
            .ThenInclude(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .Include(sp => sp.ScholarshipProgramCertificates)
            .ThenInclude(spc => spc.Certificate)
            .FirstOrDefaultAsync(sp => sp.Id == id);

        return scholarshipProgram;
    }

    public async Task DeleteRelatedInformation(ScholarshipProgram scholarshipProgram)
    {
        await _dbContext.ScholarshipProgramMajors.Where(spm => spm.ScholarshipProgramId == scholarshipProgram.Id)
            .ExecuteDeleteAsync();
        await _dbContext.ScholarshipProgramUniversities.Where(spu => spu.ScholarshipProgramId == scholarshipProgram.Id)
            .ExecuteDeleteAsync();
        await _dbContext.ScholarshipProgramCertificates.Where(spc => spc.ScholarshipProgramId == scholarshipProgram.Id)
            .ExecuteDeleteAsync();
    }
}