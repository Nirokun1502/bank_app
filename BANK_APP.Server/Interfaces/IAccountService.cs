using BANK_APP.Server.DTOs;
using BANK_APP.Server.Model;

namespace BANK_APP.Server.Interfaces
{
    public interface IAccountService
    {
        Task<List<Object>> GetAllAccounts();
        Task<Account?> GetAccountById(int id);
        Task<Account?> AddAccount(AccountDTO request);
        Task<List<Account>?> UpdateAccount(int id, Account request);
        Task<List<Account>?> DeleteAccount(int id);
        Task<Account?> AddAccountRole(Account_Roles request);
        Task<Account?> RemoveAccountRole(Account_Roles request);

    }
}
