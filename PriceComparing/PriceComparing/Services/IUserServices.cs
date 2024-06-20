using DTO;
using PriceComparing.AuthModels;

namespace PriceComparing.Services
{
    public interface IUserServices
    {
        Task<UpdateUserModel> UpdateUserAsync(UpdateUserDTO user, string id);
        Task<List<ProductDTO>> getUserFavProd(string id);

        Task AddUserFavProd(int id, string Userid);
    }
}
