using BANK_APP.Server.DTOs;
using BANK_APP.Server.Model;

namespace BANK_APP.Server.Interfaces
{
    public interface IPermissionService
    {
        Task<List<Object>> GetAllPermissions();
        Task<Permission?> GetPermissionById(int id);
        Task<Permission> AddPermission(PermissionDTO request);
        Task<Permission?> UpdatePermission(int id, PermissionDTO request);
        Task<List<Permission>?> DeletePermission(int id);
        Task<List<String>> GetAllPermissionsByAccountId(int id);
    }
}
