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

        public async Task<TEntity?> SelectById(int id)
		{
			return await _db.Set<TEntity>().FindAsync(id);
		}

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

    }
}
