using BANK_APP.Server.DTOs;
using BANK_APP.Server.Model;

namespace BANK_APP.Server.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Account?> Register(AccountDTO request);
        Task<string?> ChangePassword(ChangePasswordRequestDTO request);
        Task<string?> Login(AccountDTO request);
        Task Logout();
        Task<object?> GetAccountInfo();
    }
}
