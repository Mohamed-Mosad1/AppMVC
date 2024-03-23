using AppMVC.BLL.Interfaces;
using AppMVC.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace AppMVC.PL.Controllers
{
    // Inhertiance : DepartmentController is Controller
    // Composition : DepartmentController has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo; // NULL
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepository departmentRepo, IWebHostEnvironment env)
        {
            _departmentRepo = departmentRepo;
            _env = env;
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
        public IActionResult Create(Employee department)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var count = _departmentRepo.AddDepartment(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }

            var department = _departmentRepo.GetDepartmentById(id.Value);

            if (department == null)
            {
                return NotFound(); // 404
            }
            return View(viewName, department);
        }

        public IActionResult Edit(int? id)
        {
            ///if (!id.HasValue)
            ///{
            ///    return BadRequest(); // 400
            ///}
            ///var department = _departmentRepo.GetDepartmentById(id.Value);
            ///if (department == null)
            ///{
            ///    return NotFound(); // 404
            ///}
            ///return View(department);

            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,Employee department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            try
            {
                _departmentRepo.UpdateDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department");

                return View(department);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee department)
        {
            try
            {
                _departmentRepo.DeleteDepartment(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Department");

                return View(department);
            }
        }







    }
}
