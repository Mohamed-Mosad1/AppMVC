using AppMVC.BLL.Interfaces;
using AppMVC.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace AppMVC.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo; 
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employeeRepo, IWebHostEnvironment env)
        {
            _employeeRepo = employeeRepo;
            _env = env;
        }
         
        public IActionResult Index()
        {
            // 1. ViewData

            ViewData["Message"] = "Hello ViewData";

            // 2. ViewBag

            ViewBag.Message = "Hello ViewBag";

            var emp = _employeeRepo.GetAll();

            return View(emp);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid) 
            {
                var count = _employeeRepo.Add(emp);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(emp);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }

            var emp = _employeeRepo.GetById(id.Value);

            if (emp == null)
            {
                return NotFound(); // 404
            }
            return View(viewName, emp);
        }

        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee emp)
        {
            if (id != emp.Id)
               return BadRequest();
            
            if (!ModelState.IsValid)
               return View(emp);
            

            try
            {
                _employeeRepo.Update(emp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");

                return View(emp);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee emp)
        {
            try
            {
                _employeeRepo.Delete(emp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");

                return View(emp);
            }
        }
    }
}
