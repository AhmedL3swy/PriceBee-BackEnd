﻿using DataAccess;
using DataAccess.Models;
using PriceComparing.Repository;

namespace PriceComparing.UnitOfWork
{
    public class UnitOfWOrks
    {
		DatabaseContext _db;
        //WE Will change code Here After the Models is Done 

         //GenericRepository<Student> studentsRepository;
         //GenericRepository<Department> departmentRepository;

        public UnitOfWOrks(DatabaseContext db)
        {
            _db = db;
        }

        //public GenericRepository<Student> StudentsRepository
        //{
        //    get
        //    {
        //        if (studentsRepository == null)
        //        {
        //            studentsRepository = new GenericRepository<Student>(_db);

        //        }
        //        return studentsRepository;
        //    }
        //}

        //public GenericRepository<Department> DepartmentRepository
        //{
        //    get
        //    {
        //        if(departmentRepository == null)
        //        {
        //            departmentRepository = new GenericRepository<Department>(_db);
        //        }
        //        return departmentRepository;
        //    }
        //}
        public void savechanges()
        {
            _db.SaveChanges();
        }
    }
}
