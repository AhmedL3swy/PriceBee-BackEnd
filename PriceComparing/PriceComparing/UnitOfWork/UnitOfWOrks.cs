using DataAccess;
using DataAccess.Models;
using PriceComparing.Controllers;
using PriceComparing.Repository;
using PriceComparing.Services;

namespace PriceComparing.UnitOfWork
{
    public class UnitOfWOrks
    {
        DatabaseContext _db;
        ScrapingService _scrapingService;
        //WE Will change code Here After the Models is Done 

		GenericRepository<Product> productRepository;
		GenericRepository<ProductImage> productImageRepository;
		GenericRepository<Domain> domainRepository;
		GenericRepository<ProductLink> productLinkRepository;
		GenericRepository<ProductDetail> productDetailRepository;
		GenericRepository<ProductSponsored> productSponsoredRepository;
		GenericRepository<Brand> brandRepository;
		GenericRepository<Category> categoryRepository;
		GenericRepository<SubCategory> subCategoryRepository;
		GenericRepository<PriceHistory> priceHistoryRepository;
		GenericRepository<AuthUser> authUserRepository;
		GenericRepository<UserAlertProd> userAlertProdRepository;
        GenericRepository<UserFavProd> userFavProdRepo;
        ProductRepository productRepo;
        UserRepoNonGenric userRepoNonGenric;
        GenericRepository<User> webUserRepository;





        public UnitOfWOrks(DatabaseContext db, ScrapingService scrapingService)
        {
            _db = db;
            _scrapingService = scrapingService;
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

        public GenericRepository<User> WebUserRepository
        {
            get
            {
                if (webUserRepository == null)
                {
                    webUserRepository = new GenericRepository<User>(_db);
                }
                return webUserRepository;
            }
        }

        public UserRepoNonGenric UserRepoNonGenric
        {
            get
            {
                if (userRepoNonGenric == null)
                {
                    userRepoNonGenric = new UserRepoNonGenric(_db);
                }
                return userRepoNonGenric;
            }
        }

        public GenericRepository<AuthUser> AuthUserRepository
        {
            get
            {
                if (authUserRepository == null)
                {
                    authUserRepository = new GenericRepository<AuthUser>(_db);
                }
                return authUserRepository;
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
		public GenericRepository<SubCategory> SubCategoryRepository
			{
            get
            {
                if (subCategoryRepository == null)
                {
                    subCategoryRepository = new GenericRepository<SubCategory>(_db);
                }
                return subCategoryRepository;
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
                    productRepo = new ProductRepository(_db, _scrapingService);
                }
                return productRepo;
            }
        }

        // UserAlertProd
        public GenericRepository<UserAlertProd> UserAlertProdRepo
        {
            get
            {
                if (userAlertProdRepository == null)
                {
                    userAlertProdRepository = new GenericRepository<UserAlertProd>(_db);
                }
                return userAlertProdRepository;
            }
        }

        // UserFavProd
        public GenericRepository<UserFavProd> UserFavProdRepo
        {
            get
            {
                if (userFavProdRepo == null)
                {
                    userFavProdRepo = new GenericRepository<UserFavProd>(_db);
                }
                return userFavProdRepo;
            }
        }

        // 


        public void savechanges()
        {
            _db.SaveChanges();
        }
    }

}
