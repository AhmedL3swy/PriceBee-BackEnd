using DTO;
using PriceComparing.AuthModels;

namespace PriceComparing.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> Register(RegsiterUserDTO model);
        Task<AuthModel> Login(LoginUserDTO model);
        Task<string> AssignRole(RoleModel role);
    }
}
