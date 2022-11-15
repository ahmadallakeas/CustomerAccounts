using Application.Interfaces.IRepository;
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

        public void CreateAccount(Account account)
        {
            Create(account);
        }

        public async Task<Account> GetAccountAsync(int id, bool trackChanges)
        {
            return await FindByCondition(u => u.AccountId == id, trackChanges)
                .FirstOrDefaultAsync();
        }
    }
}
