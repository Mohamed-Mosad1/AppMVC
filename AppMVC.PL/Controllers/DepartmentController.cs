using AppMVC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppMVC.PL.Controllers
{
	// Inhertiance : DepartmentController is Controller
	// Composition : DepartmentController has a DepartmentRepository
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _departmentRepo; // NULL

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
			_departmentRepo = departmentRepo;
		}

        // /Department/Index
        public IActionResult Index()
		{
			//var department = _departmentRepo.GetAllDepartment();

			return View();
		}
	}
}
