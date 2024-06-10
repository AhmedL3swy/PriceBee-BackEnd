using DataAccess;
using DataAccess.Models;
using PriceComparing.Repository;

namespace PriceComparing.UnitOfWork
{
    public class UnitOfWOrks
    {
        DatabaseContext _db;
        //WE Will change code Here After the Models is Done 

        GenericRepository<Brand> brandRepository;
        GenericRepository<Category> categoryRepository;
        GenericRepository<Domain> domainRepository;
        GenericRepository<PriceHistory> priceHistoryRepository;
        GenericRepository<Product> productRepository;
        GenericRepository<ProductDetail> productDetailRepository;
        GenericRepository<ProductImage> productImageRepository;
        GenericRepository<ProductLink> productLinkRepository;
        GenericRepository<ProductSponsored> productSponsoredRepository;
        GenericRepository<SearchValue> searchValueRepository;
        GenericRepository<SubCategory> subCategoryRepository;

        GenericRepository<User> userRepository;

        ProductRepository productRepo;



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

        public GenericRepository<Brand> BrandRepository
        {
            get
            {
                if (brandRepository == null)
                {
                    brandRepository = new GenericRepository<Brand>(_db);
                }
                return brandRepository;
            }
        }

        // categoryRepository
        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new GenericRepository<Category>(_db);
                }
                return categoryRepository;
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
		public GenericRepository<ProductLink> ProductLinkRepository
		{
			get
			{
				if (productLinkRepository == null)
				{
					productLinkRepository = new GenericRepository<ProductLink>(_db);
				}
				return productLinkRepository;
			}
		}
		public GenericRepository<ProductDetail> ProductDetailRepository
		{
			get
			{
				if (productDetailRepository == null)
				{
					productDetailRepository = new GenericRepository<ProductDetail>(_db);
				}
				return productDetailRepository;
			}
		}
		public GenericRepository<ProductSponsored> ProductSponsoredRepository
		{
			get
			{
				if (productSponsoredRepository == null)
				{
					productSponsoredRepository = new GenericRepository<ProductSponsored>(_db);
				}
				return productSponsoredRepository;
			}
		}
		public GenericRepository<PriceHistory> PriceHistoryRepository
		{
			get
			{
				if (priceHistoryRepository == null)
				{
					priceHistoryRepository = new GenericRepository<PriceHistory>(_db);
				}
				return priceHistoryRepository;
			}
		}
		public ProductRepository ProductRepo
		{
            get
			{
                if (productRepo == null)
				{
                    productRepo = new ProductRepository(_db);
                }
                return productRepo;
            }
        }

		public void savechanges()
        {
            _db.SaveChanges();
        }
    }
}
