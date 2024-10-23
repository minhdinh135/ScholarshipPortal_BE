﻿using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<IEnumerable<Account>> GetAllWithRole();
}