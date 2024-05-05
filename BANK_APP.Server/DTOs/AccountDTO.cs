using BANK_APP.Server.Model;

namespace BANK_APP.Server.DTOs
{
    public class AccountDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class ChangePasswordRequestDTO 
    {
        public AccountDTO Account { get; set; } = new AccountDTO();
        public string NewPassword { get; set; } = string.Empty;
    }
}
