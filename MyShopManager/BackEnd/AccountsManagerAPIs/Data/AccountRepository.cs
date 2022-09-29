using AccountsManagerAPIs.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountsManagerAPIs.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAccount(Accounts accountDetails)
        {
            if (accountDetails != null)
            {
                await _context.Accounts.AddAsync(accountDetails);
            }
            else
            {
                throw new ArgumentNullException(nameof(accountDetails));
            }
        }

        public void DeleteAccount(Accounts accountDetails)
        {
            if (accountDetails == null)
            {
                throw new ArgumentNullException(nameof(accountDetails));
            }
            _context.Accounts.Remove(accountDetails);
        }

        public async Task<Accounts?> GetAccountDetailsById(Guid id)
        {
            return await _context.Accounts.Where(acc => acc.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Accounts>> GetAllAccounts()
        {
            var ss = await _context.Accounts.ToListAsync();
            return ss;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
