using AppMVC.BLL.Interfaces;
using AppMVC.DAL.Models;
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
			var department = _departmentRepo.GetAllDepartment();

			return View(department);
		}

		
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) // Server Side Validation
			{
				var count = _departmentRepo.AddDepartment(department);
				if(count > 0)
				{
					return RedirectToAction(nameof(Index));
				}
			}
			return View(department);
        }

		public IActionResult Details(int? id)
		{
            if (!id.HasValue)
            {
				return BadRequest(); // 400
            }

			var department = _departmentRepo.GetDepartmentById(id.Value);

			if(department == null)
			{
				return NotFound(); // 404
			}
			return View(department);
        }
    }
}
