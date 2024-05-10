using Microsoft.AspNetCore.Mvc;
using DataAccess;
using DataAccess.Models;

namespace PriceComparing.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
		DatabaseContext _db;

        public GenericRepository(DatabaseContext db)
        {
            _db = db;
        }

        public List<TEntity> selectall()
        {
            return _db.Set<TEntity>().ToList();
        }

        public TEntity? selectbyid(int id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public void add(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
          
        }

        public void update(TEntity entity)
        {
            _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

		public void delete(int id)
		{
			TEntity? obj = _db.Set<TEntity>().Find(id);
			if (obj != null)
			{
				_db.Set<TEntity>().Remove(obj);
			}
		}

		public void save()
        {
            _db.SaveChanges();
        }

        #region Pagination
        //Cast it to Generic
        //[HttpGet("/api/pagination")]
        //public IActionResult Getpagination([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        //{
        //    var students = _db.Students.Include(x => x.Dept).Include(x => x.St_superNavigation).ToList();
        //    var studentsDTO = students.Select(s => new StudentDTO
        //    {
        //        Id = s.St_Id,
        //        FullName = s.St_Fname + " " + s.St_Lname,
        //        Address = s.St_Address,
        //        DepartmentName = s.Dept != null ? s.Dept.Dept_Name : "",
        //        Supervisor = s.St_superNavigation != null ? s.St_superNavigation.St_Fname + " " + s.St_superNavigation.St_Lname : ""
        //    });
        //    var totalCount = students.Count();
        //    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        //    studentsDTO = studentsDTO.Skip((page - 1) * pageSize).Take(pageSize);
        //    return Ok(studentsDTO);
        //}
        #endregion

        #region Search
        //Cast it to Generic
        //[HttpGet("/api/search")]
        //public IActionResult Search([FromQuery] string name)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        return BadRequest("Search query cannot be empty");
        //    }
        //    var students = _db.Students.Include(x => x.Dept).Include(x => x.St_superNavigation).Where(s => s.St_Fname.Contains(name) || s.St_Lname.Contains(name)).ToList();
        //    var studentsDTO = students.Select(s => new StudentDTO
        //    {
        //        Id = s.St_Id,
        //        FullName = s.St_Fname + " " + s.St_Lname,
        //        Address = s.St_Address,
        //        DepartmentName = s.Dept != null ? s.Dept.Dept_Name : "",
        //        Supervisor = s.St_superNavigation != null ? s.St_superNavigation.St_Fname + " " + s.St_superNavigation.St_Lname : ""
        //    });
        //    return Ok(studentsDTO);
        //}
        #endregion


    }
}
