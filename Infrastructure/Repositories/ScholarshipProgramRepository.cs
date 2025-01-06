using Application.Exceptions;
using Application.Helper;
using Application.Interfaces.IRepositories;
using Domain.Constants;
using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NinjaNye.SearchExtensions;

namespace Infrastructure.Repositories;

public class ScholarshipProgramRepository : GenericRepository<ScholarshipProgram>, IScholarshipProgramRepository
{
    public async Task<PaginatedList<ScholarshipProgram>> GetAllScholarshipPrograms(ListOptions listOptions)
    {
        var query = _dbContext.ScholarshipPrograms
            .AsNoTracking()
            .AsSplitQuery()
            .Include(sp => sp.Category)
            .Include(sp => sp.University).ThenInclude(u => u.Country)
            .Include(sp => sp.ScholarshipProgramCertificates).ThenInclude(c => c.Certificate)
            .Include(sp => sp.Major).ThenInclude(m => m.MajorSkills).ThenInclude(ms => ms.Skill)
            .Include(sp => sp.Criteria)
            .Include(sp => sp.Documents)
            .Include(sp => sp.ReviewMilestones)
            .Include(sp => sp.AwardMilestones)
            .OrderByDescending(sp => sp.UpdatedAt);

        if (!string.IsNullOrEmpty(listOptions.SortBy))
        {
            var orderByExpression = ExpressionUtils.GetOrderByExpression<ScholarshipProgram>(listOptions.SortBy);
            query = listOptions.IsDescending
                ? query.OrderByDescending(orderByExpression)
                : query.OrderBy(orderByExpression);
        }

        var totalCount = await query.CountAsync();

        if (!listOptions.IsPaging)
        {
            var allItems = await query.ToListAsync();
            return new PaginatedList<ScholarshipProgram>(allItems, totalCount, 1, totalCount);
        }

        var result = await query
            .Skip((listOptions.PageIndex - 1) * listOptions.PageSize)
            .Take(listOptions.PageSize)
            .ToListAsync();

        return new PaginatedList<ScholarshipProgram>(result, totalCount, listOptions.PageIndex,
            listOptions.PageSize);
    }

	public async Task DeleteScholarshipProgram(int scholarshipProgramId)
	{
        var scholarshipProgram = await _dbContext.ScholarshipPrograms
            .Include(sp => sp.Criteria)
            .Include(sp => sp.ScholarshipProgramCertificates)
            .Include(sp => sp.Documents)
			.Include(sp => sp.ReviewMilestones)
			.Include(sp => sp.AwardMilestones)
            .ThenInclude(sp => sp.AwardMilestoneDocuments)
			.FirstOrDefaultAsync(sp => sp.Id == scholarshipProgramId);

		if (scholarshipProgram == null)
		{
			throw new NotFoundException($"Scholarship Program with ID {scholarshipProgramId} not found.");
		}

		// Remove related entities
		_dbContext.Criteria.RemoveRange(scholarshipProgram.Criteria);
        _dbContext.Documents.RemoveRange(scholarshipProgram.Documents);
        _dbContext.ScholarshipProgramCertificates.RemoveRange(scholarshipProgram.ScholarshipProgramCertificates);
		_dbContext.ReviewMilestones.RemoveRange(scholarshipProgram.ReviewMilestones);
        foreach( var item in scholarshipProgram.AwardMilestones)
        {
			_dbContext.AwardMilestoneDocuments.RemoveRange(item.AwardMilestoneDocuments);
		}
		_dbContext.AwardMilestones.RemoveRange(scholarshipProgram.AwardMilestones);

		// Remove the scholarship program
		_dbContext.ScholarshipPrograms.Remove(scholarshipProgram);

		await _dbContext.SaveChangesAsync();
	}

	public async Task<PaginatedList<ScholarshipProgram>> GetExpertAssignedPrograms(ListOptions listOptions,
        int expertId)
    {
        var query = _dbContext.ScholarshipPrograms
            .AsNoTracking()
            .AsSplitQuery()
            .Include(sp => sp.Category)
            .Include(sp => sp.University).ThenInclude(u => u.Country)
            .Include(sp => sp.ScholarshipProgramCertificates).ThenInclude(c => c.Certificate)
            .Include(sp => sp.Major).ThenInclude(m => m.MajorSkills).ThenInclude(ms => ms.Skill)
            .Include(sp => sp.Criteria)
            .Include(sp => sp.Documents)
            .Include(sp => sp.ReviewMilestones)
            .Include(sp => sp.AwardMilestones)
            .Where(sp => sp.AssignedExperts.Select(expertForProgram => expertForProgram.ExpertId).Contains(expertId))
            .OrderByDescending(sp => sp.UpdatedAt);

        if (!string.IsNullOrEmpty(listOptions.SortBy))
        {
            var orderByExpression = ExpressionUtils.GetOrderByExpression<ScholarshipProgram>(listOptions.SortBy);
            query = listOptions.IsDescending
                ? query.OrderByDescending(orderByExpression)
                : query.OrderBy(orderByExpression);
        }

        var totalCount = await query.CountAsync();

        if (!listOptions.IsPaging)
        {
            var allItems = await query.ToListAsync();
            return new PaginatedList<ScholarshipProgram>(allItems, totalCount, 1, totalCount);
        }

        var result = await query
            .Skip((listOptions.PageIndex - 1) * listOptions.PageSize)
            .Take(listOptions.PageSize)
            .ToListAsync();

        return new PaginatedList<ScholarshipProgram>(result, totalCount, listOptions.PageIndex,
            listOptions.PageSize);
    }

    public async Task<PaginatedList<ScholarshipProgram>> GetScholarshipProgramsByFunderId(ListOptions listOptions,
        int funderId)
    {
        var query = _dbContext.ScholarshipPrograms
            .AsNoTracking()
            .AsSplitQuery()
            .Include(sp => sp.Category)
            .Include(sp => sp.University).ThenInclude(u => u.Country)
            .Include(sp => sp.ScholarshipProgramCertificates).ThenInclude(c => c.Certificate)
            .Include(sp => sp.Major).ThenInclude(m => m.MajorSkills).ThenInclude(ms => ms.Skill)
            .Include(sp => sp.Criteria)
            .Include(sp => sp.Documents)
            .Include(sp => sp.ReviewMilestones)
            .Include(sp => sp.AwardMilestones)
            .Where(sp => sp.FunderId == funderId)
            .OrderByDescending(sp => sp.UpdatedAt);

        if (!string.IsNullOrEmpty(listOptions.SortBy))
        {
            var orderByExpression = ExpressionUtils.GetOrderByExpression<ScholarshipProgram>(listOptions.SortBy);
            query = listOptions.IsDescending
                ? query.OrderByDescending(orderByExpression)
                : query.OrderBy(orderByExpression);
        }

        var totalCount = await query.CountAsync();

        if (!listOptions.IsPaging)
        {
            var allItems = await query.ToListAsync();
            return new PaginatedList<ScholarshipProgram>(allItems, totalCount, 1, totalCount);
        }

        var result = await query
            .Skip((listOptions.PageIndex - 1) * listOptions.PageSize)
            .Take(listOptions.PageSize)
            .ToListAsync();

        return new PaginatedList<ScholarshipProgram>(result, totalCount, listOptions.PageIndex,
            listOptions.PageSize);
    }

    public async Task<ScholarshipProgram> GetScholarsipProgramById(int id)
    {
        var scholarshipProgram = await _dbContext.ScholarshipPrograms
            .AsSplitQuery()
            .Include(sp => sp.Criteria)
            .Include(sp => sp.Documents)
            .Include(sp => sp.ReviewMilestones)
            .Include(sp => sp.AwardMilestones)
            .ThenInclude(a => a.AwardMilestoneDocuments)
            .Include(sp => sp.Category)
            .Include(sp => sp.University)
            .ThenInclude(u => u.Country)
            .Include(sp => sp.Major)
            .ThenInclude(m => m.MajorSkills)
            .ThenInclude(ms => ms.Skill)
            .Include(sp => sp.ScholarshipProgramCertificates)
            .ThenInclude(spc => spc.Certificate)
            .Include(sp => sp.AssignedExperts)
            .ThenInclude(sp => sp.Expert)
            .ThenInclude(expert => expert.ExpertProfile)
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
        await _dbContext.ScholarshipProgramCertificates
            .Where(spc => spc.ScholarshipProgramId == scholarshipProgram.Id)
            .ExecuteDeleteAsync();
    }

    public async Task RemoveExpertsFromScholarshipProgram(int scholarshipProgramId, List<int> expertIds)
    {
        await _dbContext.ExpertForPrograms.Where(programExpert =>
                programExpert.ScholarshipProgramId == scholarshipProgramId &&
                expertIds.Contains(programExpert.ExpertId))
            .ExecuteDeleteAsync();
    }
}