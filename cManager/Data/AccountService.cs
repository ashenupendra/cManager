using cManager.Shared.Accounting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cManager.Data
{
    public class AccountService
    {
        public readonly ApplicationDbContext dBContext;

        public AccountService(ApplicationDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await dBContext.Accounts.ToListAsync();
        }

        public Account GetAccount(int accountId)
        {
            return dBContext.Accounts.FirstOrDefault(e => e.Id == accountId);
        }

        public bool SaveAccount(Account account)
        {
            if (account.Id == 0)
            {
                dBContext.Add(account);
            }
            else
            {
                dBContext.Update(account);
            }
            dBContext.SaveChanges();
            return true;
        }

        public bool DeleteAccount(int accountId)
        {
            var account = GetAccount(accountId);
            account.IsActive = false;
            return SaveAccount(account);
        }
    }
}
