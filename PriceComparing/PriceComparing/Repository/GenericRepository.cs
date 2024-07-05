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

        // 1- Get all active entities
        // when using this function global filtration is applied
        public async Task<List<TEntity>> SelectAll()
		{
			return await _db.Set<TEntity>().AsNoTracking().ToListAsync();
		}
        // sync version
        public IQueryable<TEntity> SelectAllSync()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }


        // 2- Get all entities ignoring filters
        // when using this function global filtration not is applied
        public async Task<List<TEntity>> SelectAllIgnoringFiltersAsync()
        {
            return await _db.Set<TEntity>()
                .AsNoTracking()
                .IgnoreQueryFilters()
                .ToListAsync();
        }

        // 3- Get all soft deleted entities
        // when using this function only softdeleted is returned
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

        // 4- Get entity by id
        public async Task<TEntity?> SelectById(int id)
		{
			return await _db.Set<TEntity>().FindAsync(id);
		}
        // not in use //
        public TEntity SelectionById(int id)
        {
            //return _db.Set<TEntity>().Where(entity => EF.Property<int>(entity, "Id") == id);
            //return find by id 
            // return _db.Set<TEntity>().Where(entity => (int)entity.GetType().GetProperty("Id").GetValue(entity) == id);
            return _db.Set<TEntity>().Find(id);
        }

        // 5- Get by id ignoring filters
        // Get by id ignoring filters
        public async Task<TEntity?> SelectByIdIgnoringFiltersAsync(int id)
        {
            var entities = await _db.Set<TEntity>()
                .AsNoTracking()
                .IgnoreQueryFilters()
                .ToListAsync();

            return entities.FirstOrDefault(e => (int)e.GetType().GetProperty("Id").GetValue(e) == id);
        }

        // 6- Add entity

        public async Task Add(TEntity entity)
		{
			await _db.Set<TEntity>().AddAsync(entity);
			await _db.SaveChangesAsync();
		}

        // 7- Update entity
        public async Task UpdateAsync(TEntity entity)
		{
			_db.Entry(entity).State = EntityState.Modified;
			await _db.SaveChangesAsync();
		}


        // 8- Delete entity
        // Delete entity by id
        public async Task Delete(int id)
		{
			TEntity? obj = await _db.Set<TEntity>().FindAsync(id);
			if (obj != null)
			{
				_db.Set<TEntity>().Remove(obj);
				await _db.SaveChangesAsync();
			}
		}

        // 9- Soft Delete using shadow property
        public async Task SoftDelete(int id)
        {
            TEntity? obj = await _db.Set<TEntity>().FindAsync(id);
            if (obj != null)
            {
                _db.Entry(obj).Property("IsDeleted").CurrentValue = true;
                await _db.SaveChangesAsync();
            }
        }

        // 10- Restore soft deleted entity
        internal async Task Restore(TEntity entity)
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

        // 11- Delete range of entities

        internal async Task DeleteRange(IEnumerable<object> entities)
        {
            _db.Set<TEntity>().RemoveRange(entities.Cast<TEntity>());
            await _db.SaveChangesAsync();
        }

        #region User
        // 1- get user by id: string
        public async Task<TEntity?> SelectUserById(string id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        // 2- get user by email: string
        public async Task<TEntity?> SelectUserByEmail(string email)
        {
            return await _db.Set<TEntity>().FindAsync(email);
        }
        #endregion
    }
}
