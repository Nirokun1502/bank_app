using BANK_APP.Server.Data;
using BANK_APP.Server.DTOs;
using BANK_APP.Server.Interfaces;
using BANK_APP.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace BANK_APP.Server.Services
{
    public class AuthenticationService : IAuthenticationService // có thể thêm nhiều interface ở đây để DI
    {
       
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IConfiguration configuration, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Account?> Register (AccountDTO request)
        {
           
            if (request.Username == "" || request.Password == "")
            {
                return null;
            }

            if(await _context.ACCOUNT.AnyAsync(a => a.Username == request.Username))
            {
                return null;
            }
            
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
            
            
            Role ?existedRole = await _context.ROLE.FirstOrDefaultAsync(a => a.Role_Name == request.Role);

            if (existedRole is null)
            {
                existedRole = await _context.ROLE.FirstOrDefaultAsync(a => a.Role_Name == "ACTIVE_USER");
            }

            Account account = new Account();
            account.Roles = new List<Role>();

            account.Username = request.Username;
            account.Password = passwordHash;
            account.Roles.Add(existedRole);

            _context.ACCOUNT.Add(account);
            await _context.SaveChangesAsync();
            
            return account;      
        }

        public async Task<string?> ChangePassword (ChangePasswordRequestDTO request)
        {     
            var account = await _context.ACCOUNT
                .Where(c => c.Username == request.Account.Username)
                .FirstOrDefaultAsync();
            if (account is null ||
               !BCrypt.Net.BCrypt.Verify(request.Account.Password, account.Password))
            {
                return null;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            account.Password = passwordHash;
            await _context.SaveChangesAsync();
            return "successfully changing password";
        }

        public async Task<string?> Login (AccountDTO request)
        {
            var account = await _context.ACCOUNT
                .Where(c => c.Username == request.Username)
                .Include(c => c.Roles)
                    .ThenInclude(r => r.Permissions)
                .FirstOrDefaultAsync();

            if (account is null || 
               !BCrypt.Net.BCrypt.Verify(request.Password, account.Password)) 
            {
                return null; 
            }

            bool hasLoginPermission = account.Roles
           .SelectMany(r => r.Permissions)
           .Any(p => p.Permission_Name == "LOGIN");

            if (!hasLoginPermission) 
            {
                return "1";
            }

            string token = CreateToken(account);

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true, // Ngăn chặn truy cập từ mã JavaScript
                Secure = true,   // Chỉ gửi qua HTTPS
                SameSite = SameSiteMode.Strict // Ngăn chặn CSRF attacks
            });

            return token;
        }

        public async Task Logout()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("access_token");
        }

        public async Task<object?> GetAccountInfo()
        {   
            if (_httpContextAccessor.HttpContext is null)
            {
                return null;
            }

            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            var roles = _httpContextAccessor.HttpContext?
                        .User
                        .Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList();

            var permissions = _httpContextAccessor.HttpContext?
                              .User
                              .Claims
                              .Where(c => c.Type == "Permission")
                              .Select(c => c.Value)
                              .ToList();

            return new { userName, roles, permissions };
        }

        private string CreateToken(Account account)
        {   
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, account.Username),
                //new Claim(ClaimTypes.Role, "Admin")       
            };     
            if (account.Roles.Any())
            {
                claims.AddRange(account.Roles.Select(role => new Claim(ClaimTypes.Role, role.Role_Name)));
            }
            var permissions = account.Roles
                .SelectMany(role => role.Permissions)
                .Select(permission => permission.Permission_Name)
                .Distinct()
                .ToList();
            if (permissions.Any()) 
            {
                claims.AddRange(permissions.Select(permission => new Claim("Permission", permission)));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!  ));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
