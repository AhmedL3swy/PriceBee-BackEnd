using DataAccess;
using DataAccess.Models;
using PriceComparing.Repository;

namespace PriceComparing.UnitOfWork
{
    public class UnitOfWOrks
    {
		DatabaseContext _db;
		//WE Will change code Here After the Models is Done 

		GenericRepository<Product> productRepository;
		GenericRepository<ProductImage> productImageRepository;
		GenericRepository<Domain> domainRepository;



		public UnitOfWOrks(DatabaseContext db)
        {
            _db = db;
        }

		public GenericRepository<Product> ProductRepository
		{
			get
			{
				if (productRepository == null)
				{
					productRepository = new GenericRepository<Product>(_db);
				}
				return productRepository;
			}
		}

		public GenericRepository<ProductImage> ProductImageRepository
		{
			get
			{
				if (productImageRepository == null)
				{
					productImageRepository = new GenericRepository<ProductImage>(_db);
				}
				return productImageRepository;
			}
		}

		public GenericRepository<Domain> DomainRepository
		{
			get
			{
				if (domainRepository == null)
				{
					domainRepository = new GenericRepository<Domain>(_db);
				}
				return domainRepository;
			}
		}

		public void savechanges()
        {
            _db.SaveChanges();
        }
    }
}
