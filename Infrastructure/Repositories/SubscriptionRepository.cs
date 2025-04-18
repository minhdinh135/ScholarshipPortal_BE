﻿using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
{
	public async Task<Subscription?> GetSubscriptionByProviderId(int providerId)
	{
		return await _dbContext.Subscriptions
			.Include(s => s.Accounts)
			.FirstOrDefaultAsync(s => s.Accounts.Any(a => a.Id == providerId));
	}
}