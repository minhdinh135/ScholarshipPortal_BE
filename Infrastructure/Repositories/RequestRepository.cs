﻿using Application.Interfaces.IRepositories;
using Domain.DTOs.Request;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RequestRepository : GenericRepository<Request>, IRequestRepository
{
    public RequestRepository(ScholarshipContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<IEnumerable<Request>> GetAllRequests()
    {
        var requests = await _dbContext.Requests
            .AsNoTracking()
            .AsSplitQuery()
            .Include(r => r.RequestDetails)
            .ToListAsync();

        return requests;
    }

    public async Task<Request> GetRequestById(int id)
    {
        var request = await _dbContext.Requests
            .AsSplitQuery()
            .Include(r => r.RequestDetails)
            .FirstOrDefaultAsync(r => r.Id == id);

        return request;
    }
}