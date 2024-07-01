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
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }




        public async Task<UpdateUserModel> UpdateUserAsync(UpdateUserDTO user, string id)
        {
            string Message = string.Empty;
            if (id == null) return new UpdateUserModel { message = "NO Id was Sent" };
            var OldUser = await userManager.FindByIdAsync(id);
            if (OldUser == null) return new UpdateUserModel { message = "NOT exist Such User" };



            if (await userManager.GetEmailAsync(OldUser) != user.Email) {
                if (await userManager.FindByEmailAsync(user.Email) != null)
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
            return new UpdateUserModel { message = "Update Successfully", IsUpdated = true };
        }

        public async Task<List<UserFavProdDTO>> getUserFavProd(string id)
        {
            var Auser = await userManager.FindByIdAsync(id);
            if (Auser == null) return new List<UserFavProdDTO>();
            var user = Auser.User;
            if (user == null) return new List<UserFavProdDTO>();

            var FavProd = await unitOfWork.UserFavProdRepo.SelectAll();

            var userFavProds = FavProd.Where(a => a.UserID == user.Id).Select(x => x.Product).ToList();
            List<UserFavProdDTO> products = new List<UserFavProdDTO>();
            List<ProductImage> ProductImages = await unitOfWork.ProductImageRepository.SelectAll();
            foreach (var product in userFavProds)
            {
                var productImage = ProductImages.Where(a => a.ProdId == product.Id).FirstOrDefault()?.Image;

                products.Add(new UserFavProdDTO()
                {
                    UserId = id,
                    ProductId = product.Id,
                    ProductName = product.Name_Global,
                    ProductImage = productImage ?? string.Empty
                });
            }

            return products;
        }


        public async Task<string> AddUserFavProd(int id, string Userid)
        {
            var Auser = await userManager.FindByIdAsync(Userid);
            var user = Auser.User;
            if (user == null)
            {
                user = new User() { AuthUserID = Auser.Id };
             //   user.ProdFavUser = new List<UserFavProd>(); 
                await unitOfWork.WebUserRepository.Add(user);
            }

            var userFavProds= (await unitOfWork.UserFavProdRepo.SelectAll()).Where(a=>a.UserID==user.Id);
            if(userFavProds==null)
            { 
               // userFavProds=new List<UserFavProd>() ;
            }
            if (userFavProds.FirstOrDefault(a=>a.ProductId==id)==null)
            {
                var newUserFavProd = new UserFavProd() { ProductId = id, UserID = user.Id };
                await unitOfWork.UserFavProdRepo.Add(newUserFavProd);
                return "Added Successfully";
            }
            else { 
                return "Already Exists";
            }

        }

        public async Task<string> AddUserHistoryProd(int id, string Userid)
        {
            var Auser = await userManager.FindByIdAsync(Userid);
            var user = Auser.User;
            if (user == null)
            {
                user = new User() { AuthUserID = Auser.Id };
                await unitOfWork.WebUserRepository.Add(user);
            }

            var userHistProd = (await unitOfWork.UserHisProdRepo.SelectAll()).Where(a => a.UserID == user.Id);
            if (userHistProd == null)
            {
              //  userHistProd = new List<UserHistoryProd>();
            }
            if (userHistProd.FirstOrDefault(a => a.ProductId == id && a.UserID == user.Id) == null)
            {
                var newUserHisProd = new UserHistoryProd() { ProductId = id, UserID = user.Id };
                await unitOfWork.UserHisProdRepo.Add(newUserHisProd);
                return "Added Successfully";
            }
            else
            {
                return "Already Exists";
            }
        }

        public async Task<string> AddUserAlertProd(int id, string Userid)
        {
            var Auser = await userManager.FindByIdAsync(Userid);
            var user = Auser.User;
            if (user == null)
            {
                user = new User() { AuthUserID = Auser.Id };
                await unitOfWork.WebUserRepository.Add(user);
            }

            var userAlertProds = await unitOfWork.UserAlertProdRepo.SelectAll();
            if (userAlertProds.FirstOrDefault(a => a.ProductId == id && a.UserID == user.Id) == null)
            {
                var newUserAlertProd = new UserAlertProd() { ProductId = id, UserID = user.Id };
                 await unitOfWork.UserAlertProdRepo.Add(newUserAlertProd);
                return "Added Successfully";
            }
            else
            {
                return "Already Exists";
            }
        }


        public async Task<List<UserProdHistoryDto>> getUserHistoryProd(string id)
        {
            var Auser = await userManager.FindByIdAsync(id);
            if (Auser == null) return new List<UserProdHistoryDto>();
            var user = Auser.User;
            if (user == null) return new List<UserProdHistoryDto>();

            var HistoryProd = await unitOfWork.UserHisProdRepo.SelectAll();

            var userHistProds = HistoryProd.Where(a => a.UserID == user.Id).Select(x => x.Product).ToList();
            List<UserProdHistoryDto> products = new List<UserProdHistoryDto>();
            List<ProductImage> ProductImages = await unitOfWork.ProductImageRepository.SelectAll();
            foreach (var product in userHistProds)
            {
                var productImage = ProductImages.Where(a => a.ProdId == product.Id).FirstOrDefault()?.Image;
                products.Add(new UserProdHistoryDto()
                {
                    UserId = id,
                    ProductId = product.Id,
                    ProductName = product.Name_Global,
                    ProductImage = productImage ?? string.Empty
                });
            }

            return products;
        }


      


        public async Task<List<UserAlertProdDTO>> getUserAlert(string id)
        {
            var Auser = await userManager.FindByIdAsync(id);
            if (Auser == null) return new List<UserAlertProdDTO>();
            var user = Auser.User;
            if (user == null) return new List<UserAlertProdDTO>();

            var AlertProd = await unitOfWork.UserAlertProdRepo.SelectAll();

            var userAlertProds = AlertProd.Where(a => a.UserID == user.Id ).Select(x => x.Product ).ToList();
            List<UserAlertProdDTO> products = new List<UserAlertProdDTO>();
            List<ProductImage> ProductImages = await unitOfWork.ProductImageRepository.SelectAll();

            foreach (var product in userAlertProds)
            {
                var productImage = ProductImages.Where(a => a.ProdId == product.Id).FirstOrDefault()?.Image;
                products.Add(new UserAlertProdDTO()
                {
                    UserID = id,
                    ProdId = product.Id,
                    ProductName = product.Name_Global,
                    ProductImage = productImage ?? string.Empty
                });
            }

            return products;
        }



       


        public async Task RemoveUserAlertProd(int id, string Userid)
        {
            var Auser = await userManager.FindByIdAsync(Userid);
            if (Auser == null)
                return;
            var user = Auser.User;
            if (user == null)
                return;
            var AlertProduct = await unitOfWork.UserAlertProdRepo.SelectAll();
            var userAlertProd = AlertProduct.FirstOrDefault(a => a.UserID == user.Id && a.ProductId == id);
            
            if (userAlertProd != null)
            {
                await unitOfWork.UserRepoNonGenric.SoftDelete<UserAlertProd>(userAlertProd.ProductId,userAlertProd.UserID);
            }
          
        }


        public async Task RemoveUserFavProd(int id, string Userid)
        {
            var Auser = await userManager.FindByIdAsync(Userid);
            if (Auser == null)
                return;
            var user = Auser.User;
            if (user == null)
                return;
            var AlertProduct = await unitOfWork.UserFavProdRepo.SelectAll();
            var userAlertProd = AlertProduct.FirstOrDefault(a => a.UserID == user.Id && a.ProductId == id);

            if (userAlertProd != null)
            {
                await unitOfWork.UserRepoNonGenric.SoftDelete<UserFavProd>(userAlertProd.ProductId, userAlertProd.UserID);
            }

        }


        public async Task RemoveUserHisProd(int id, string Userid)
        {
            var Auser = await userManager.FindByIdAsync(Userid);
            if (Auser == null)
                return;
            var user = Auser.User;
            if (user == null)
                return;
            var AlertProduct = await unitOfWork.UserHisProdRepo.SelectAll();
            var userAlertProd = AlertProduct.FirstOrDefault(a => a.UserID == user.Id && a.ProductId == id);

            if (userAlertProd != null)
            {
                await unitOfWork.UserRepoNonGenric.SoftDelete<UserHistoryProd>(userAlertProd.ProductId, userAlertProd.UserID);
            }

        }

       







    }
}
