using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PriceComparing.Repository
{
	public class GenericRepository<TEntity> where TEntity : class
	{
		private readonly DatabaseContext _db;
        private readonly UserManager<AuthUser> userManager;

        public GenericRepository(DatabaseContext db)
		{
			_db = db;

		}

		public IQueryable<TEntity> SelectAllProduct()
		{
			return _db.Set<TEntity>().AsNoTracking();
		}
		public async Task<List<TEntity>> SelectAll()
		{
			return await _db.Set<TEntity>().AsNoTracking().ToListAsync();
		}

        public async Task<List<TEntity>> SelectAllIgnoringFiltersAsync()
        {
            return await _db.Set<TEntity>()
                .AsNoTracking()
                .IgnoreQueryFilters()
                .ToListAsync();
        }
        internal async Task<List<TEntity>> SelectAllSoftDeletedAsync()
        {
            // throw new NotImplementedException();
            return await _db.Set<TEntity>()
                .AsNoTracking()
                .IgnoreQueryFilters()
                // .Where(e => (bool)e.GetType().GetProperty("IsDeleted").GetValue(e))
                .Where(s => EF.Property<bool>(s, "IsDeleted"))
                .ToListAsync();

        }


        public async Task<TEntity?> SelectById(int id)
		{
			return await _db.Set<TEntity>().FindAsync(id);
		}

        public async Task<TEntity?> SelectUserById(string id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> SelectUserByEmail(string email)
        {
            return await _db.Set<TEntity>().FindAsync(email);
        }

        public async Task Add(TEntity entity)
        // Get by id ignoring filters
        public async Task<TEntity?> SelectByIdIgnoringFiltersAsync(int id)
        {
            var entities = await _db.Set<TEntity>()
                .AsNoTracking()
                .IgnoreQueryFilters()
                .ToListAsync();

            return entities.FirstOrDefault(e => (int)e.GetType().GetProperty("Id").GetValue(e) == id);
        }


        public async Task Add(TEntity entity)
		{
			await _db.Set<TEntity>().AddAsync(entity);
			await _db.SaveChangesAsync();
		}

		public async Task UpdateAsync(TEntity entity)
		{
			_db.Entry(entity).State = EntityState.Modified;
			await _db.SaveChangesAsync();
		}

       

        public async Task Delete(int id)
		{
			TEntity? obj = await _db.Set<TEntity>().FindAsync(id);
			if (obj != null)
			{
				_db.Set<TEntity>().Remove(obj);
				await _db.SaveChangesAsync();
			}
		}

		// Soft Delete using shadow property
		public async Task SoftDelete(int id)
        {
            TEntity? obj = await _db.Set<TEntity>().FindAsync(id);
            if (obj != null)
            {
                _db.Entry(obj).Property("IsDeleted").CurrentValue = true;
                await _db.SaveChangesAsync();
            }
        }

        internal async Task DeleteRange(IEnumerable<object> entities)
        {
            //TEntity? obj = await _db.Set<TEntity>().FindAsync(entities);
            _db.Set<TEntity>().RemoveRange(entities.Cast<TEntity>());
            await _db.SaveChangesAsync();
            //foreach (var item in Items)
            //{
            //}
        }


        internal async Task RestoreAsync(TEntity entity)
        {
            // check if Attached 
            if (_db.Entry(entity).State == EntityState.Detached)
                _db.Set<TEntity>().Attach(entity);
            // update the state to modified
            _db.Entry(entity).State = EntityState.Modified;
            // set the IsDeleted property to false
            _db.Entry(entity).Property("IsDeleted").CurrentValue = false;
            // save the changes
            await _db.SaveChangesAsync();
        }

    }
}
