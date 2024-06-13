﻿using DataAccess;
using DataAccess.Models;
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
					CategoryId = 1
				},
				new Brand
				{
					// Id = 2,
					Name_Local = "ابل",
					Name_Global = "Apple",
					Description_Local = "ابل",
					Description_Global = "Apple",
					CategoryId = 1
				},
				new Brand
				{
					// Id = 3,
					Name_Local = "اتش بي",
					Name_Global = "HP",
					Description_Local = "اتش بي",
					Description_Global = "HP",
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
					CategoryId = 2
				},
				new Brand
				{
					// Id = 5,
					Name_Local = "ماكس",
					Name_Global = "Max",
					Description_Local = "ماكس",
					Description_Global = "Max",
					CategoryId = 2
				},
				new Brand
				{
					// Id = 6,
					Name_Local = "دار النشر",
					Name_Global = "Dar El Nashr",
					Description_Local = "دار النشر",
					Description_Global = "Dar El Nashr",
					CategoryId = 2
				},
				new Brand
				{
					// Id = 7,
					Name_Local = "بيركس",
					Name_Global = "Pyrex",
					Description_Local = "بيركس",
					Description_Global = "Pyrex",
					CategoryId = 3
				},
				new Brand
				{
					// Id = 8,
					Name_Local = "فيك",
					Name_Global = "Vic",
					Description_Local = "فيك",
					Description_Global = "Vic",
					CategoryId = 3
				},
				new Brand
				{
					// Id = 9,
					Name_Local = "تيفال",
					Name_Global = "Tefal",
					Description_Local = "تيفال",
					Description_Global = "Tefal",
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
                    ProductLink1 = "http://www.egyptianelectronics.com/product/1",
                    Status = "In Stock",
                    LastUpdated = DateTime.Now,
                    LastScraped = DateTime.Now
                },
                new ProductLink
                {
					//Id = 2,
                    ProdId = 1,
                    DomainId = 2,
                    ProductLink1 = "http://www.saudielectronics.com/product/1",
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
                }
            );
            db.SaveChanges();

            // ProductDetails seeding
            db.ProductDetails.AddRange(
                new ProductDetail
                {
					Id=1,
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

					Id= 2,
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

					Id	= 3,
                    Name_Local = "تليفون شاومى",
                    Name_Global = "xiaomi Phone",
                    Description_Local = "تليفون شاومى",
                    Description_Global = "xiaomi Phone",
                    Price = 1000,
                    Rating = 4.5m,
                    Brand = "xiaomi"
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

