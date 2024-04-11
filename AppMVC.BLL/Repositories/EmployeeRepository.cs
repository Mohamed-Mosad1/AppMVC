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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return _dbContext.Employees.Where(E => E.Address.ToLower() == address.ToLower());
        }

        public IQueryable<Employee> SearchEmployeeByName(string name)
        {
            return _dbContext.Employees.Where(E => E.Name.ToLower().Contains(name));
        }
    }
}
