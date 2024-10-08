﻿using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ScholarshipProgramRepository : GenericRepository<ScholarshipProgram>, IScholarshipProgramRepository
{
    private readonly ScholarshipContext _dbContext;
    
    public ScholarshipProgramRepository(ScholarshipContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<ScholarshipProgram>> GetAllScholarshipPrograms()
    {
        var scholarshipPrograms = await _dbContext.ScholarshipPrograms
            .Include(sp => sp.ScholarshipProgramCategories)
            .ThenInclude(spc => spc.Category)
            .Include(sp => sp.ScholarshipProgramUniversities)
            .ThenInclude(spu => spu.University)
            .Include(sp => sp.ScholarshipProgramMajors)
            .ThenInclude(spm => spm.Major)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

        return scholarshipPrograms;
    }

    public async Task<ScholarshipProgram> GetScholarsipProgramById(int id)
    {
        var scholarshipProgram = await _dbContext.ScholarshipPrograms
            .Include(sp => sp.ScholarshipProgramCategories)
            .Include(sp => sp.ScholarshipProgramUniversities)
            .Include(sp => sp.ScholarshipProgramMajors)
            .FirstOrDefaultAsync(sp => sp.Id == id);

        return scholarshipProgram;
    }

    public async Task ClearExistingCategories(ScholarshipProgram scholarshipProgram)
    {
        var existingCategories = scholarshipProgram.ScholarshipProgramCategories.ToList();
        
        foreach (var existingCategory in existingCategories)
        {
            _dbContext.ScholarshipProgramCategories.Remove(existingCategory);
        }
    }

    public async Task ClearExistingUniversities(ScholarshipProgram scholarshipProgram)
    {
        var existingUniversities = scholarshipProgram.ScholarshipProgramUniversities.ToList();
        
        foreach (var existingUniversity in existingUniversities)
        {
            _dbContext.ScholarshipProgramUniversities.Remove(existingUniversity);
        }
    }

    public async Task ClearExistingMajors(ScholarshipProgram scholarshipProgram)
    {
        var existingMajors = scholarshipProgram.ScholarshipProgramMajors.ToList();
        
        foreach (var existingMajor in existingMajors)
        {
            _dbContext.ScholarshipProgramMajors.Remove(existingMajor);
        }
    }
}