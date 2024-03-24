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
		IEnumerable<Department> GetAllDepartment();

		Department GetDepartmentById(int id);

		int AddDepartment(Department entity);

		int UpdateDepartment(Department entity);

		int DeleteDepartment(Department entity);




	}
}
