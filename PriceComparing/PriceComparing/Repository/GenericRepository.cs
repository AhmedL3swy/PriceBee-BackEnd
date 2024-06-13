using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace PriceComparing.Repository
{
	public class GenericRepository<TEntity> where TEntity : class
	{
		private readonly DatabaseContext _db;

		public GenericRepository(DatabaseContext db)
		{
			_db = db;
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

        public async Task<TEntity?> SelectById(int id)
		{
			return await _db.Set<TEntity>().FindAsync(id);
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

        internal async Task DeleteRange(object Item)
        {
            TEntity? obj = await _db.Set<TEntity>().FindAsync(Item);
            if (obj != null)
            {
                _db.Set<TEntity>().RemoveRange(obj);
                await _db.SaveChangesAsync();
            }
        }


    }
}
