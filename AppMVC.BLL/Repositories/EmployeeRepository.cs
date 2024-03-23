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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) // Ask CLR for creating object from "ApplicationDbContext"
        {
            _dbContext = dbContext;
        }

        public int AddEmployee(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int DeleteEmployee(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _dbContext.Employees.AsNoTracking().ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            ///var Employee = _dbContext.Employees.Local.Where(D => D.Id == id).FirstOrDefault();
            ///if (Employee == null)
            ///	Employee = _dbContext.Employees.Where(D => D.Id == id).FirstOrDefault();
            ///return Employee;

            //return _dbContext.Employees.Find(id);

            return _dbContext.Find<Employee>(id); // EF core 3.1 NEW Features
        }

        public int UpdateEmployee(Employee entity)
        {
            _dbContext.Employees.Update(entity);
            return _dbContext.SaveChanges();
        }
    
    }
}
