using AppMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMVC.BLL.Interfaces
{
	public interface IDepartmentRepository
	{
		IEnumerable<Employee> GetAllDepartment();

		Employee GetDepartmentById(int id);

		int AddDepartment(Employee entity);

		int UpdateDepartment(Employee entity);

		int DeleteDepartment(Employee entity);




	}
}
