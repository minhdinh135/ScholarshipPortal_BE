﻿using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IApplicantRepository : IGenericRepository<ApplicantProfile>
{
    Task<ApplicantProfile> GetByApplicantId(int applicantId);
}