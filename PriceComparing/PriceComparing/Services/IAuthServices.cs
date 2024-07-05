using DTO;
using PriceComparing.AuthModels;

namespace PriceComparing.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> Register(RegsiterUserDTO model);
        Task<AuthModel> Login(LoginUserDTO model);
        Task<string> AssignRole(RoleModel role);

        Task<string> AssignAdminRole(string ID);

        Task<string> AssignUserRoleAgain(string ID);
    }
}
