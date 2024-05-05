

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BANK_APP.Server.Model
{
    public class Account
    {
        [Key]   
        public int Account_Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

     
        public List<Role>? Roles { get; set; }
    }
}
