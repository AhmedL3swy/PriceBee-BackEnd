using DTO;
using PriceComparing.AuthModels;

namespace PriceComparing.Services
{
    public interface IUserServices
    {
        Task<UpdateUserModel> UpdateUserAsync(UpdateUserDTO user, string id);
        Task<List<UserFavProdDTO>> getUserFavProd(string id);

        Task<string> AddUserFavProd(int id, string Userid);
        Task<List<UserProdHistoryDto>> getUserHistoryProd(string id);

        Task<string> AddUserHistoryProd(int id, string Userid);
        Task<List<UserAlertProdDTO>> getUserAlert(string id);

        Task<string> AddUserAlertProd(int id, string Userid);

       

        Task RemoveUserAlertProd(int id, string userId);

        Task RemoveUserFavProd(int id, string Userid);

        Task RemoveUserHisProd(int id, string Userid);
    }
}
