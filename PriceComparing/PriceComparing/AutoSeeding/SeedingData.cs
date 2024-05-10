using DataAccess;
using DataAccess.Models;
namespace PriceComparing.Models
{
	public class SeedingData
	{
		public static void InitializeDataBase(DatabaseContext db)
		{
			// Delete the database if it exists
			db.Database.EnsureDeleted();

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
			// Products ... 
			db.Products.AddRange(
				// Electronics:Pones
				new Product
				{
					Name_Local = "تليفون سامسونج",
					Name_Global = "Samsung Phone",
					Description_Local = "تليفون سامسونج",
					Description_Global = "Samsung Phone",
					SubCategoryId = 1
				},
				new Product
				{
					Name_Local = "تليفون ابل",
					Name_Global = "Apple Phone",
					Description_Local = "تليفون ابل",
					Description_Global = "Apple Phone",
					SubCategoryId = 1
				},
				// Electronics:Laptops
				new Product
				{
					Name_Local = "لاب توب اتش بي",
					Name_Global = "HP Laptop",
					Description_Local = "لاب توب اتش بي",
					Description_Global = "HP Laptop",
					SubCategoryId = 2
				},
				// Electronics:Printers
				new Product
				{
					Name_Local = "طابعة اتش بي",
					Name_Global = "HP Printer",
					Description_Local = "طابعة اتش بي",
					Description_Global = "HP Printer",
					SubCategoryId = 3
				},
				// Office Supplies:Pens
				new Product
				{
					Name_Local = "قلم جاف",
					Name_Global = "Ballpoint Pen",
					Description_Local = "قلم جاف",
					Description_Global = "Ballpoint Pen",
					SubCategoryId = 4
				},
				// Office Supplies:Paper
				new Product
				{
					Name_Local = "ورق A4",
					Name_Global = "A4 Paper",
					Description_Local = "ورق A4",
					Description_Global = "A4 Paper",
					SubCategoryId = 5
				},
				// Office Supplies:Books
				new Product
				{
					Name_Local = "كتاب رواية",
					Name_Global = "Novel Book",
					Description_Local = "كتاب رواية",
					Description_Global = "Novel Book",
					SubCategoryId = 6
				},
				// Home Supplies:Dishes
				new Product
				{
					Name_Local = "طقم أواني",
					Name_Global = "Dishes Set",
					Description_Local = "طقم أواني",
					Description_Global = "Dishes Set",
					SubCategoryId = 7
				},
				// Home Supplies:Cleaning Tools
				new Product
				{
					Name_Local = "مسحوق غسيل",
					Name_Global = "Detergent",
					Description_Local = "مسحوق غسيل",
					Description_Global = "Detergent",
					SubCategoryId = 8
				},
				// Home Supplies:Cooking Tools
				new Product
				{
					Name_Local = "مقلاة",
					Name_Global = "Frying Pan",
					Description_Local = "مقلاة",
					Description_Global = "Frying Pan",
					SubCategoryId = 9
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

			// 


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