﻿using Domain.DTOs.Common;
using Domain.DTOs.ScholarshipProgram;
using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IScholarshipProgramRepository : IGenericRepository<ScholarshipProgram>
{
    Task<PaginatedList<ScholarshipProgram>> GetAllScholarshipPrograms(ListOptions listOptions);
    Task<ScholarshipProgram> GetScholarsipProgramById(int id);
    Task<IEnumerable<ScholarshipProgram>> SearchScholarshipPrograms(ScholarshipSearchOptions scholarshipSearchOptions);
    Task<IEnumerable<ScholarshipProgram>> GetScholarshipProgramByMajorId(int majorId);
    Task<IEnumerable<ScholarshipProgram>> GetOpenScholarshipPrograms();
    Task DeleteScholarshipCertificates(ScholarshipProgram scholarshipProgram);
}