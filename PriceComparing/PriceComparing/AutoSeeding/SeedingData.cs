using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace PriceComparing.Models
{
    public class SeedingData
    {

        public static void InitializeDataBase(DatabaseContext db)
        {
            // Delete the database if it exists
            //db.Database.EnsureDeleted();

            // Create the database if it does not exist
            db.Database.EnsureCreated();

            if (db.Products.Any())
            {
                return;
            }
            // flag to enable/disable seeding data
            // Categories
            db.Categories.AddRange(
                new Category
                {
                    Name_Local = "الكترونيات",
                    Name_Global = "Electronics",
                },
                new Category
                {
                    Name_Local = "أدوات مكتبيه",
                    Name_Global = "Office Supplies",
                },
                new Category
                {
                    Name_Local = "أدوات منزليه",
                    Name_Global = "Home Supplies",
                }
                );
            db.SaveChanges();

            // SubCategories
            db.SubCategories.AddRange(
                new SubCategory
                {
                    Name_Local = "تليفونات",
                    Name_Global = "Phones",
                    CategoryId = 1
                },
                new SubCategory
                {
                    Name_Local = "لاب توب",
                    Name_Global = "Laptops",
                    CategoryId = 1
                },
                new SubCategory
                {
                    Name_Local = "طابعات",
                    Name_Global = "Printers",
                    CategoryId = 1
                },
                new SubCategory
                {
                    Name_Local = "أقلام",
                    Name_Global = "Pens",
                    CategoryId = 2
                },
                new SubCategory
                {
                    Name_Local = "ورق",
                    Name_Global = "Paper",
                    CategoryId = 2
                },
                new SubCategory
                {
                    Name_Local = "كتب",
                    Name_Global = "Books",
                    CategoryId = 2
                },
                new SubCategory
                {
                    Name_Local = "أواني",
                    Name_Global = "Dishes",
                    CategoryId = 3
                },
                new SubCategory
                {
                    Name_Local = "أدوات تنظيف",
                    Name_Global = "Cleaning Tools",
                    CategoryId = 3
                },
                new SubCategory
                {
                    Name_Local = "أدوات طبخ",
                    Name_Global = "Cooking Tools",
                    CategoryId = 3
                }
                );
            db.SaveChanges();

            // Brands
            db.Brands.AddRange(
                // Electronics:Phones
                new Brand
                {
                    // Id = 1,
                    Name_Local = "سامسونج",
                    Name_Global = "Samsung",
                    Description_Local = "سامسونج",
                    Description_Global = "Samsung",
                    Logo = "samsung.jpg",
					LogoUrl = "samsung",
					CategoryId = 1
                },
                new Brand
                {
                    // Id = 2,
                    Name_Local = "ابل",
                    Name_Global = "Apple",
                    Description_Local = "ابل",
                    Description_Global = "Apple",
					Logo = "apple.jpg",
					LogoUrl = "samsung",

					CategoryId = 1
                },
                new Brand
                {
                    // Id = 3,
                    Name_Local = "اتش بي",
                    Name_Global = "HP",
                    Description_Local = "اتش بي",
                    Description_Global = "HP",
					Logo = "hp.jpg",
					LogoUrl = "samsung",

					CategoryId = 1
                },
                // Office Supplies:Pens
                new Brand
                {
                    // Id = 4,
                    Name_Local = "بيك",
                    Name_Global = "Bic",
                    Description_Local = "بيك",
                    Description_Global = "Bic",
                    Logo = "bic.jpg",
					LogoUrl = "samsung",

					CategoryId = 2
                },
                new Brand
                {
                    // Id = 5,
                    Name_Local = "ماكس",
                    Name_Global = "Max",
                    Description_Local = "ماكس",
                    Description_Global = "Max",
					Logo = "max.jpg",
					LogoUrl = "samsung",

					CategoryId = 2
                },
                new Brand
                {
                    // Id = 6,
                    Name_Local = "دار النشر",
                    Name_Global = "Dar El Nashr",
                    Description_Local = "دار النشر",
                    Description_Global = "Dar El Nashr",
					Logo = "dar_el_nashr.jpg",
					LogoUrl = "samsung",

					CategoryId = 2
                },
                new Brand
                {
                    // Id = 7,
                    Name_Local = "بيركس",
                    Name_Global = "Pyrex",
                    Description_Local = "بيركس",
                    Description_Global = "Pyrex",
					Logo = "pyrex.jpg",
					LogoUrl = "samsung",

					CategoryId = 3
                },
                new Brand
                {
                    // Id = 8,
                    Name_Local = "فيك",
                    Name_Global = "Vic",
                    Description_Local = "فيك",
                    Description_Global = "Vic",
					Logo = "vic.jpg",
					LogoUrl = "samsung",

					CategoryId = 3
                },
                new Brand
                {
                    // Id = 9,
                    Name_Local = "تيفال",
                    Name_Global = "Tefal",
                    Description_Local = "تيفال",
                    Description_Global = "Tefal",
					Logo = "tefal.jpg",
					LogoUrl = "samsung",

					CategoryId = 3
                }


                );
            db.ProductDetails.AddRange(
                );
            db.SaveChanges();

            // Products 
            db.Products.AddRange(
                // Electronics:Phones
                new Product
                {
                    Name_Local = "تليفون سامسونج",
                    Name_Global = "Samsung Phone",
                    Description_Local = "تليفون سامسونج",
                    Description_Global = "Samsung Phone",
                    SubCategoryId = 1,
                    BrandId = 1
                },
                new Product
                {
                    Name_Local = "تليفون ابل",
                    Name_Global = "Apple Phone",
                    Description_Local = "تليفون ابل",
                    Description_Global = "Apple Phone",
                    SubCategoryId = 1,
                    BrandId = 2
                },
                // Electronics:Laptops
                new Product
                {
                    Name_Local = "لاب توب اتش بي",
                    Name_Global = "HP Laptop",
                    Description_Local = "لاب توب اتش بي",
                    Description_Global = "HP Laptop",
                    SubCategoryId = 2,
                    BrandId = 3
                },
                // Electronics:Printers
                new Product
                {
                    Name_Local = "طابعة اتش بي",
                    Name_Global = "HP Printer",
                    Description_Local = "طابعة اتش بي",
                    Description_Global = "HP Printer",
                    SubCategoryId = 3,
                    BrandId = 3
                },
                // Office Supplies:Pens
                new Product
                {
                    Name_Local = "قلم جاف",
                    Name_Global = "Ballpoint Pen",
                    Description_Local = "قلم جاف",
                    Description_Global = "Ballpoint Pen",
                    SubCategoryId = 4,
                    BrandId = 4
                },
                // Office Supplies:Paper
                new Product
                {
                    Name_Local = "ورق A4",
                    Name_Global = "A4 Paper",
                    Description_Local = "ورق A4",
                    Description_Global = "A4 Paper",
                    SubCategoryId = 5,
                    BrandId = 5
                },
                // Office Supplies:Books
                new Product
                {
                    Name_Local = "كتاب رواية",
                    Name_Global = "Novel Book",
                    Description_Local = "كتاب رواية",
                    Description_Global = "Novel Book",
                    SubCategoryId = 6,
                    BrandId = 6
                },
                // Home Supplies:Dishes
                new Product
                {
                    Name_Local = "طقم أواني",
                    Name_Global = "Dishes Set",
                    Description_Local = "طقم أواني",
                    Description_Global = "Dishes Set",
                    SubCategoryId = 7,
                    BrandId = 7
                },
                // Home Supplies:Cleaning Tools
                new Product
                {
                    Name_Local = "مسحوق غسيل",
                    Name_Global = "Detergent",
                    Description_Local = "مسحوق غسيل",
                    Description_Global = "Detergent",
                    SubCategoryId = 8,
                    BrandId = 8
                },
                // Home Supplies:Cooking Tools
                new Product
                {
                    Name_Local = "مقلاة",
                    Name_Global = "Frying Pan",
                    Description_Local = "مقلاة",
                    Description_Global = "Frying Pan",
                    SubCategoryId = 9,
                    BrandId = 9

                }
                );
            db.SaveChanges();

            // Prices History
            db.PriceHistories.AddRange(
                                new PriceHistory
                                {
                                    ProdId = 1,
                                    Price = 1000,
                                    Date = DateTime.Now
                                },
                                new PriceHistory
                                {
                                    ProdId = 1,
                                    Price = 1100,
                                    Date = DateTime.Now.AddDays(-1)
                                },
                                new PriceHistory
                                {
                                    ProdId = 2,
                                    Price = 2000,
                                    Date = DateTime.Now
                                },
                                new PriceHistory
                                {
                                    ProdId = 2,
                                    Price = 2100,
                                    Date = DateTime.Now.AddDays(-1)
                                },
                                new PriceHistory
                                {
                                    ProdId = 3,
                                    Price = 3000,
                                    Date = DateTime.Now
                                },
                                new PriceHistory
                                {
                                    ProdId = 3,
                                    Price = 3100,
                                    Date = DateTime.Now.AddDays(-1)
                                },
                                new PriceHistory
                                {
                                    ProdId = 4,
                                    Price = 4000,
                                    Date = DateTime.Now
                                },
                                new PriceHistory
                                {
                                    ProdId = 4,
                                    Price = 4100,
                                    Date = DateTime.Now.AddDays(-1)
                                },
                                new PriceHistory
                                {
                                    ProdId = 5,
                                    Price = 5000,
                                    Date = DateTime.Now
                                },
                                new PriceHistory
                                {
                                    ProdId = 5,
                                    Price = 5100,
                                    Date = DateTime.Now.AddDays(-1)
                                },
                                new PriceHistory
                                {
                                    ProdId = 6,
                                    Price = 6000,
                                    Date = DateTime.Now
                                },
                                new PriceHistory
                                {
                                    ProdId = 6,
                                    Price = 6100,
                                    Date = DateTime.Now.AddDays(-1)
                                },
                                new PriceHistory
                                {
                                    ProdId = 7,
                                    Price = 7000,
                                    Date = DateTime.Now
                                },
                                new PriceHistory
                                {
                                    ProdId = 7,
                                    Price = 7100,
                                    Date = DateTime.Now.AddDays(-1)
                                }
            );
            db.SaveChanges();



            var passwordHasher = new PasswordHasher<AuthUser>();


            var authUser = new AuthUser
            {
                UserName = "AhmedMostafa",
                NormalizedUserName = "AHMEDMOSTAFA",
                Email = "AhmedMostafa@gmail.com",
                NormalizedEmail = "AHMEDMOSTAFA@GMAIL.COM",
                EmailConfirmed = true,
                FName = "Ahmed",
                LName = "Mostafa",
                Gender = "Male",
                Country = "Egypt",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
                PhoneCode = "+20",
                PhoneNumber = "1149147981",
                Image = "path_to_image",
                PasswordHash = passwordHasher.HashPassword(null, "password"),
            };

            var authUser2 = new AuthUser
            {
                UserName = "EslamMohmeh",
                NormalizedUserName = "ESLAMMOHMEH",
                Email = "EslamMohmeh@gmail.com",
                NormalizedEmail = "ESLAMMOHMEH@GMAIL.COM",
                EmailConfirmed = true,
                FName = "Eslam",
                LName = "Mohmeh",
                Gender = "Male",
                Country = "Egypt",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1992, 1, 1)),
                PhoneCode = "+20",
                PhoneNumber = "1149147982",
                Image = "path_to_image",
                PasswordHash = passwordHasher.HashPassword(null, "kafarelsheikha"),
            };

            var authUser3 = new AuthUser
            {
                UserName = "AhmedAli",
                NormalizedUserName = "AHMEDALI",
                Email = "AhmedAli@gmail.com",
                NormalizedEmail = "AHMEDALI@GMAIL.COM",
                EmailConfirmed = true,
                FName = "Ahmed",
                LName = "Ali",
                Gender = "Male",
                Country = "Egypt",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1999, 2, 1)),
                PhoneCode = "+20",
                PhoneNumber = "1099662282",
                Image = "path_to_image",
                PasswordHash = passwordHasher.HashPassword(null, "Ahmed@12345678"),
            };

            var authUser4 = new AuthUser
            {
                UserName = "MohamedSamy",
                NormalizedUserName = "MOHAMEDSAMY",
                Email = "MohamedSamy@gmail.com",
                NormalizedEmail = "MOHAMEDSAMY@GMAIL.COM",
                EmailConfirmed = true,
                FName = "Mohamed",
                LName = "Samy",
                Gender = "Male",
                Country = "Egypt",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1999, 2, 1)),
                PhoneCode = "+20",
                PhoneNumber = "1099662282",
                Image = "path_to_image",
                PasswordHash = passwordHasher.HashPassword(null, "Mohamed@12345678"),
            };

            var authUser5 = new AuthUser
            {
                UserName = "MostafaMourad",
                NormalizedUserName = "MOSTAFAMOURAD",
                Email = "MostafaMourad@gmail.com",
                NormalizedEmail = "MOSTAFAMOURAD@GMAIL.COM",
                EmailConfirmed = true,
                FName = "Mostafa",
                LName = "Mourad",
                Gender = "Male",
                Country = "Egypt",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1980, 2, 1)),
                PhoneCode = "+20",
                PhoneNumber = "1088775566",
                Image = "path_to_image",
                PasswordHash = passwordHasher.HashPassword(null, "Mourad@12345678"),
            };

            List<AuthUser> users = new List<AuthUser>();
            users.Add(authUser);
            users.Add(authUser2);
            users.Add(authUser3);
            users.Add(authUser4);
            users.Add(authUser5);




            db.Users.AddRange(users);
            db.SaveChanges();
            //add user fav products

            var productIds = new List<int> { 1, 2, 3 };

          
            var products = db.Products
                .Where(p => productIds.Contains(p.Id))
                .Include(p => p.ProductImages) 
                .ToList();

            

            var UserWebsite = new User()
            {
                AuthenticatedUser = authUser2,
                ProdFavUser = products
            };

            db.websiteUsers.Add(UserWebsite);
            db.SaveChanges();

            List<UserFavProd> FavProd = new List<UserFavProd>() {
             new UserFavProd()  {ProductId =1,UserID=UserWebsite.Id },
             new UserFavProd()  {ProductId =2,UserID=UserWebsite.Id },
             new UserFavProd()  {ProductId =3,UserID=UserWebsite.Id },

        };

            db.SDUserFavProds.AddRange(FavProd);
            db.SaveChanges();

            List<UserHistoryProd> ClickedProd = new List<UserHistoryProd>() {
             new UserHistoryProd()  {ProductId =1,UserID=UserWebsite.Id },
             new UserHistoryProd()  {ProductId =2,UserID=UserWebsite.Id },
             new UserHistoryProd()  {ProductId =3,UserID=UserWebsite.Id },

        };

            db.UserHistoryProds.AddRange(ClickedProd);
            db.SaveChanges();



            List<UserAlertProd> AlertProd = new List<UserAlertProd>() {
             new UserAlertProd()  {ProductId =3,UserID=UserWebsite.Id },
             new UserAlertProd()  {ProductId =4,UserID=UserWebsite.Id },

        };

            db.UserAlertProds.AddRange(AlertProd);
            db.SaveChanges();




            // Add Role
            db.Roles.AddRange(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "User".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });

            db.SaveChanges();



            db.UserRoles.Add(
            new IdentityUserRole<string>
            {
                UserId = authUser.Id,
                RoleId = db.Roles.FirstOrDefault(a => a.Name == "Admin").Id
            });

            db.UserRoles.Add(
            new IdentityUserRole<string>
            {
                UserId = authUser2.Id,
                RoleId = db.Roles.FirstOrDefault(a => a.Name == "User").Id
            });

            db.UserRoles.Add(
            new IdentityUserRole<string>
            {
                UserId = authUser3.Id,
                RoleId = db.Roles.FirstOrDefault(a => a.Name == "User").Id
            });


            db.UserRoles.Add(
            new IdentityUserRole<string>
            {
                UserId = authUser4.Id,
                RoleId = db.Roles.FirstOrDefault(a => a.Name == "User").Id
            });

            db.UserRoles.Add(
            new IdentityUserRole<string>
            {
                UserId = authUser5.Id,
                RoleId = db.Roles.FirstOrDefault(a => a.Name == "User").Id
            });


            db.SaveChanges();

            // Domains seeding
            db.Domains.AddRange(
                new Domain
                {
                    //Id = 1,
                    Name_Local = "متجر الكترونيات مصري",
                    Name_Global = "Egyptian Electronics Store",
                    Description_Local = "متجر الكترونيات مصري",
                    Description_Global = "Egyptian Electronics Store",
                    Url = "http://www.egyptianelectronics.com",
                    Logo = "logo1.jpg"
                },
                new Domain
                {
                    //Id = 2,
                    Name_Local = "متجر الكترونيات سعودي",
                    Name_Global = "Saudi Electronics Store",
                    Description_Local = "متجر الكترونيات سعودي",
                    Description_Global = "Saudi Electronics Store",
                    Url = "http://www.saudielectronics.com",
                    Logo = "logo2.jpg"
                },
                new Domain
                {
                    //Id = 3,
                    Name_Local = "متجر الكترونيات اماراتي",
                    Name_Global = "Emirati Electronics Store",
                    Description_Local = "متجر الكترونيات اماراتي",
                    Description_Global = "Emirati Electronics Store",
                    Url = "http://www.emiratielectronics.com",
                    Logo = "logo3.jpg"
                }
            );
            db.SaveChanges();

            // ProductLinks seeding
            db.ProductLinks.AddRange(
                new ProductLink
                {
                    //Id = 1,
                    ProdId = 1,
                    DomainId = 1,
                    ProductLink1 = "https://www.amazon.eg/-/en/Apple-iPhone-11-FaceTime-SIM/dp/B07Y3L5KWP/ref=asc_df_B07Y3L5KWP/?tag=egoshpadde-21&linkCode=df0&hvadid=545070945026&hvpos=&hvnetw=g&hvrand=12806753509304419907&hvpone=&hvptwo=&hvqmt=&hvdev=c&hvdvcmdl=&hvlocint=&hvlocphy=1005393&hvtargid=pla-823486245761&mcid=775299f28eed37cea33eaf9f40e75ca2&th=1",
                    Status = "In Stock",
                    LastUpdated = DateTime.Now,
                    LastScraped = DateTime.Now
                },
                new ProductLink
                {
                    //Id = 2,
                    ProdId = 1,
                    DomainId = 2,
                    ProductLink1 = "https://www.jarir.com/apple-iphone-11-smartphones-557620.html",
                    Status = "Out of Stock",
                    LastUpdated = DateTime.Now,
                    LastScraped = DateTime.Now
                },
                new ProductLink
                {

                    //Id= 3,
                    ProdId = 2,
                    DomainId = 2,
                    ProductLink1 = "http://www.saudielectronics.com/product/2",
                    Status = "In Stock",
                    LastUpdated = DateTime.Now,
                    LastScraped = DateTime.Now
                },
                 new ProductLink
                 {

                     //Id= 4,
                     ProdId = 2,
                     DomainId = 1,
                     ProductLink1 = "http://www.emirateselectronics.com/product/2",
                     Status = "In Stock",
                     LastUpdated = DateTime.Now,
                     LastScraped = DateTime.Now
                 },
                 new ProductLink
                    {
    
                        //Id= 5,
                        ProdId = 4,
                        DomainId = 3,
                        ProductLink1 = "http://www.emirateselectronics.com/product/3",
                        Status = "In Stock",
                        LastUpdated = DateTime.Now,
                        LastScraped = DateTime.Now
                    },
                 new ProductLink
                        {
        
                            //Id= 6,
                            ProdId = 4,
                            DomainId = 1,
                            ProductLink1 = "http://www.emirateselectronics.com/product/3",
                            Status = "In Stock",
                            LastUpdated = DateTime.Now,
                            LastScraped = DateTime.Now
                        }
            );
            db.SaveChanges();

            // ProductDetails seeding
            db.ProductDetails.AddRange(
                new ProductDetail
                {
                    Id = 1,
                    Name_Local = "تليفون سامسونج",
                    Name_Global = "Samsung Phone",
                    Description_Local = "تليفون سامسونج",
                    Description_Global = "Samsung Phone",
                    Price = 1000,
                    Rating = 4.5m,
                    Brand = "Samsung"
                },
                new ProductDetail
                {

                    Id = 2,
                    Name_Local = "تليفون ابل",
                    Name_Global = "Apple Phone",
                    Description_Local = "تليفون ابل",
                    Description_Global = "Apple Phone",
                    Price = 1500,
                    Rating = 4.7m,
                    Brand = "Apple"
                },
                new ProductDetail
                {

                    Id = 3,
                    Name_Local = "تليفون شاومى",
                    Name_Global = "xiaomi Phone",
                    Description_Local = "تليفون شاومى",
                    Description_Global = "xiaomi Phone",
                    Price = 1000,
                    Rating = 4.5m,
                    Brand = "xiaomi"
                },
                 new ProductDetail
                 {

                     Id = 4,
                     Name_Local = "تليفون شاومى",
                     Name_Global = "xiaomi Phone",
                     Description_Local = "تليفون شاومى",
                     Description_Global = "xiaomi Phone",
                     Price = 800,
                     Rating = 4.3m,
                     Brand = "samsung"
                 },
                 new ProductDetail
                    {
    
                        Id = 5,
                        Name_Local = "88تليفون شاومى",
                        Name_Global = "88xiaomi Phone",
                        Description_Local = "تليفون شاومى",
                        Description_Global = "xiaomi Phone",
                        Price = 900,
                        Rating = 4.4m,
                        Brand = "samsung"
                    }

            );
            db.SaveChanges();
            db.ProductImages.AddRange(
             new ProductImage
            {
                ProdId = 1, // Assuming this corresponds to a "Samsung Phone"
                Image = "https://f.nooncdn.com/p/pnsku/N70029832V/45/_/1706601417/c68be273-1532-4459-a8fc-19392b1b3521.jpg?format=avif&width=240"
             },
            new ProductImage
            {
                ProdId = 1, // Multiple images for the same product
                Image = "https://f.nooncdn.com/p/pnsku/N70029832V/45/_/1706601420/5cbd768c-b32c-4445-8653-1cb453c2da45.jpg?format=avif&width=240"
            },
            new ProductImage
            {
                ProdId = 2, // Assuming this corresponds to an "Apple Phone"
                Image = "https://f.nooncdn.com/p/v1687521109/N53393905A_1.jpg?format=avif&width=240"
            },
            new ProductImage
            {
                ProdId = 3, // Assuming this corresponds to a "HP Laptop"
                Image = "https://f.nooncdn.com/p/v1656599539/N53259583A_1.jpg?format=avif&width=240"
            },
            new ProductImage
            {
                ProdId = 4, // Assuming this corresponds to a "HP Printer"
                Image = "https://f.nooncdn.com/p/v1668778811/N31082847A_1.jpg?format=avif&width=240"
            },
            new ProductImage
            {
                ProdId = 5, // Assuming this corresponds to a "Ballpoint Pen"
                Image = "https://f.nooncdn.com/p/pzsku/ZF0766B35C43A3199B987Z/45/_/1648049845/6f23b0d0-2eb3-47fa-9086-b53ac2a505c9.jpg?format=avif&width=240"
            },
            new ProductImage
            {
                ProdId = 6, // Assuming this corresponds to "A4 Paper"
                Image = "https://f.nooncdn.com/p/v1666971960/N14159731A_1.jpg?format=avif&width=240"
            },
            new ProductImage
            {
                ProdId = 7, // Assuming this corresponds to a "Novel Book"
                Image = "https://f.nooncdn.com/p/v1561099741/N26857366A_1.jpg?format=avif&width=240"
            },
            new ProductImage
            {
                ProdId = 8, // Assuming this corresponds to a "Dishes Set"
                Image = "https://f.nooncdn.com/p/pzsku/ZF7D4E63461E9F320EF8DZ/45/_/1703788112/6dee7961-4305-410e-8499-8ec3857b4408.jpg?format=avif&width=240"
            },
            new ProductImage
            {
                ProdId = 9, // Assuming this corresponds to a "Frying Pan"
                Image = "https://f.nooncdn.com/p/pnsku/N16319317A/45/_/1717011397/fa606981-bd92-474f-bf35-930d030622f7.jpg?format=avif&width=240"
            }
            );
            db.SaveChanges();

            //ProductSponsored seeding
            db.ProductSponsoreds.AddRange(
                new ProductSponsored
                {
                    Cost = 1000,
                    StartDate = DateTime.Now,
                    Duration = 30,
                    ProdDetId = 4
                }
            );
            db.SaveChanges();




            // Users
            // user has
            // Id, FName, LName, Email, Password, Gender, Country, JoinDate, PhoneCode, PhoneNumber, DateOfBirth, Image, Role, SearchValues, Prods, Prods1, ProdsNavigation
            //db.Users.AddRange(
            //					new User
            //					{
            //						FName = "Ahmed",
            //						LName = "Ali",
            //						Email = "AhmedAli@gmail.com",
            //						Password = "123456",
            //						Gender = "Male",
            //						Country = "Egypt",
            //						JoinDate = new DateOnly(2021, 1, 1),
            //						PhoneCode = "+20",
            //						PhoneNumber = "123456789",
            //						DateOfBirth = new DateOnly(1990, 1, 1),
            //						Image = "AhmedAli.jpg",
            //						Role = "User"
            //					},
            //					new User
            //					{
            //						FName = "Mohamed",
            //						LName = "Ali",
            //						Email = ""
            //					}
            //					);

        }
    }
}



// Users
// user has
// Id, FName, LName, Email, Password, Gender, Country, JoinDate, PhoneCode, PhoneNumber, DateOfBirth, Image, Role, SearchValues, Prods, Prods1, ProdsNavigation
//db.Users.AddRange(
//					new User
//					{
//						FName = "Ahmed",
//						LName = "Ali",
//						Email = "AhmedAli@gmail.com",
//						Password = "123456",
//						Gender = "Male",
//						Country = "Egypt",
//						JoinDate = new DateOnly(2021, 1, 1),
//						PhoneCode = "+20",
//						PhoneNumber = "123456789",
//						DateOfBirth = new DateOnly(1990, 1, 1),
//						Image = "AhmedAli.jpg",
//						Role = "User"
//					},
//					new User
//					{
//						FName = "Mohamed",
//						LName = "Ali",
//						Email = ""
//					}
//					);





/*
1   الكترونيات Electronics	1	تليفونات	Phones	1
1	الكترونيات	Electronics	2	لاب توب	Laptops	1
1	الكترونيات	Electronics	3	طابعات	Printers	1
2	أدوات مكتبيه	Office Supplies	4	أقلام	Pens	2
2	أدوات مكتبيه	Office Supplies	5	ورق	Paper	2
2	أدوات مكتبيه	Office Supplies	6	مكتبات	Books	2
3	أدوات منزليه	Home Supplies	7	أواني	Dishes	3
3	أدوات منزليه	Home Supplies	8	أدوات تنظيف	Cleaning Tools	3
3	أدوات منزليه	Home Supplies	9	أدوات طبخ	Cooking Tools	3
*/

