using AppMVC.BLL.Interfaces;
using AppMVC.DAL.Models;
using AppMVC.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMVC.PL.Controllers
{
    // Inhertiance : DepartmentController is Controller
    // Composition : DepartmentController has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepo; // NULL
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IMapper mapper , IDepartmentRepository departmentRepo, IWebHostEnvironment env)
        {
            _mapper = mapper;
            _departmentRepo = departmentRepo;
            _env = env;
        }

        // /Department/Index
        public IActionResult Index(string searchInput)
        {

            var department = Enumerable.Empty<Department>();
            if (string.IsNullOrEmpty(searchInput))
            {
                department = _departmentRepo.GetAll();
            }
            else
            {
                department = _departmentRepo.SearchEmployeeByName(searchInput.ToLower());
            }

            var mappedEmp = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(department);


            return View(mappedEmp);

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var mappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                var count = _departmentRepo.Add(mappedEmp);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(departmentVM);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }

            var department = _departmentRepo.GetById(id.Value);

            var mappedEmp = _mapper.Map<Department, DepartmentViewModel>(department);

            if (department == null)
            {
                return NotFound(); // 404
            }
            return View(viewName, mappedEmp);
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
        public IActionResult Edit([FromRoute]int id,DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }

            try
            {
                var mappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _departmentRepo.Update(mappedEmp);
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

                return View(departmentVM);
            }
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _departmentRepo.Delete(mappedEmp);
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

                return View(departmentVM);
            }
        }







    }
}
