﻿using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public void CreateAccount(Account account,int customerId)
        {
            account.CustomerId = customerId;
            Create(account);
        }

        public async Task<Account> GetAccountAsync(int id, bool trackChanges)
        {
            return await FindByCondition(u => u.AccountId == id, trackChanges)
                .Include(c=>c.Customer)
                .Include(t=>t.Transactions)
                .FirstOrDefaultAsync();
        }
        public async Task<Account> GetAccountByCustomerId(int customerId, bool trackChanges)
        {
            return await FindByCondition(u => u.CustomerId == customerId, trackChanges)
             .Include(c => c.Customer)
             .Include(t => t.Transactions)
             .FirstOrDefaultAsync();
        }
    }
}