using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using PriceComparing.AuthModels;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Services
{
    public class UserService : IUserServices
    {
        private readonly UnitOfWOrks unitOfWork;

        private readonly UserManager<AuthUser> userManager;

        public UserService(UnitOfWOrks _unitOfWork, UserManager<AuthUser> _userManager)
        {
            unitOfWork= _unitOfWork;
            userManager = _userManager; 
        }

        
       

        public async Task<UpdateUserModel> UpdateUserAsync(UpdateUserDTO user, string id)
        {
            string Message = string.Empty;
            if (id == null) return new UpdateUserModel { message = "NO Id was Sent" } ;
            var OldUser = await userManager.FindByIdAsync(id);
            if (OldUser == null) return new UpdateUserModel { message = "NOT exist Such User"};

           

                if (await userManager.GetEmailAsync(OldUser) != user.Email){ 
                    if(await userManager.FindByEmailAsync(user.Email) != null)
                    {
                        return new UpdateUserModel { message = "User Email already Exist" };
                    }
                }

                if (OldUser.UserName != user.UserName)
                {
                    if (await userManager.FindByNameAsync(user.UserName) != null)
                    {
                        return new UpdateUserModel { message = "User Name already Exist" };
                    }
                }

           await unitOfWork.UserRepoNonGenric.UpdateUserAsync(OldUser, user); 
            return new UpdateUserModel { message = "Update Successfully",IsUpdated =true };
        }

        public async Task<List<ProductDTO>> getUserFavProd(string id)
        {
            var Auser = await userManager.FindByIdAsync(id); 
            if (Auser == null) return new List<ProductDTO>();
            var user = Auser.User;
            if (user == null) return new List<ProductDTO>();

            var FavProd = await unitOfWork.UserFavProdRepo.SelectAll();

            var userFavProds = FavProd.Where(a => a.UserID == user.Id).Select(x => x.Product).ToList();
            List<ProductDTO> products = new List<ProductDTO>();
            foreach (var product in userFavProds)
            {
                products.Add(new ProductDTO() { Id = product.Id ,Name_Global= product.Name_Global}); 
            }

            return products;
        }


        public async Task AddUserFavProd(int id, string Userid)
        {
            var Auser = await userManager.FindByIdAsync(Userid);
            var user = Auser.User;
            if (user == null)
            {
                user = new User() { AuthUserID = Auser.Id };
                await unitOfWork.WebUserRepository.Add(user);
            }

            var userFavProdExists = await unitOfWork.UserFavProdRepo.SelectAll();
            var newUserFavProd = new UserFavProd() { ProductId = id, UserID = user.Id };
            await unitOfWork.UserFavProdRepo.Add(newUserFavProd);
            if (userFavProdExists.Count==0)
            {
                //var newUserFavProd = new UserFavProd() { ProductId = id, UserID = user.Id };
                //await unitOfWork.UserFavProdRepo.Add(newUserFavProd);
                
            }


        }


    }
}
