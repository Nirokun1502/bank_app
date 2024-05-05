using BANK_APP.Server.DTOs;
using BANK_APP.Server.Model;

namespace BANK_APP.Server.Interfaces
{
    public interface IRoleService
    {
        Task<List<Object>> GetAllRoles();
        Task<Role?> GetRoleById(int id);
        Task<Role?> AddRole(RoleDTO request);
        Task<Role?> UpdateRole(int id, RoleDTO request);
        Task<List<Role>?> DeleteRole(int id);
        Task<Role?> AddRolePermission(Role_Permissions request);
        Task<Role?> RemoveRolePermission(Role_Permissions request);
    }
}
