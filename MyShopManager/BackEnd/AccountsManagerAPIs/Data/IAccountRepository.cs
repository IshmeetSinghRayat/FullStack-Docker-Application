using AccountsManagerAPIs.Models;

namespace AccountsManagerAPIs.Data
{
    public interface IAccountRepository
    {
        Task SaveChanges();
        void DeleteAccount(Accounts accountDetails);
        Task<Accounts?> GetAccountDetailsById(Guid id);
        Task<IEnumerable<Accounts>> GetAllAccounts();
        Task CreateAccount(Accounts accountDetails);
    }
}
