using AppMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMVC.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployee();

        Employee GetEmployeeById(int id);

        int AddEmployee(Employee entity);

        int UpdateEmployee(Employee entity);

        int DeleteEmployee(Employee entity);
    }
}
