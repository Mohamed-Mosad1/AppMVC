using AppMVC.BLL.Interfaces;
using AppMVC.DAL.Data;
using AppMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMVC.BLL.Repositories
{
	internal class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext) // Ask CLR for creating object from "ApplicationDbContext"
		{
			_dbContext = dbContext;
        }

        public int AddDepartment(Department entity)
		{
			_dbContext.Departments.Add(entity);
			return _dbContext.SaveChanges();
		}

		public int DeleteDepartment(Department entity)
		{
			_dbContext.Departments.Remove(entity);
			return _dbContext.SaveChanges();
		}

		public IEnumerable<Department> GetAllDepartment()
		{
			return _dbContext.Departments.AsNoTracking().ToList();
		}

		public Department GetDepartmentById(int id)
		{
			///var department = _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
			///if (department == null)
			///	department = _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();
			///return department;

			//return _dbContext.Departments.Find(id);

			return _dbContext.Find<Department>(id); // EF core 3.1 NEW Features
		}

		public int UpdateDepartment(Department entity)
		{
			_dbContext.Departments.Update(entity);
			return _dbContext.SaveChanges();
		}
	}
}
