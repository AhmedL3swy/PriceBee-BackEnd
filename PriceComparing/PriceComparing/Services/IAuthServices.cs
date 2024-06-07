using PriceComparing.AuthModels;

namespace PriceComparing.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> Register(RegisteredModel model);
        Task<AuthModel> Login(LoginModel model);
        Task<string> AssignRole(RoleModel role);
    }
}
