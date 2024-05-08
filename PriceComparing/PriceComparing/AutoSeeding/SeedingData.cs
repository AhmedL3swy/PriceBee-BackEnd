using DataAccess;
namespace PriceComparing.Models
{
	public class SeedingData
	{
		public static void InitializeDataBase(DBContext context)
		{
			context.Database.EnsureCreated();

			if (context.Products.Any())
			{
				return;
			}

			context.Categories.AddRange(
				new Category
				{
					Name_Local = "الكترونيات",
					Name_Global = "Electronics",
				},
				new Category
				{
					Name_Local = "2الكترونيات",
					Name_Global = "Electronics2",
				},
				new Category
				{
					Name_Local = "الكترونيات3",
					Name_Global = "Electronics3",				}
				);

			//context.Products.AddRange(
			//					new Product
			//					{
			//						Id = 1,
			//						Name_Local = "تليفون سامسونج",
			//						Name_Global = "Samsung Phone",
			//						Description_Local = "تليفون سامسونج",
			//						Description_Global = "Samsung Phone",
			//					}) ;
			context.SaveChanges();

		}
	}
}
