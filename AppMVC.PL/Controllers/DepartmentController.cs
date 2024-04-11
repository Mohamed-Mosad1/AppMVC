using AppMVC.BLL.Interfaces;
using AppMVC.BLL.Repositories;
using AppMVC.DAL.Models;
using AppMVC.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.PL.Controllers
{
    // Inhertiance : DepartmentController is Controller
    // Composition : DepartmentController has a DepartmentRepository
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        //private readonly IDepartmentRepository _departmentRepo; // NULL

        public DepartmentController(
            //IDepartmentRepository departmentRepo, 
            IUnitOfWork unitOfWork,
            IMapper mapper, 
            IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_departmentRepo = departmentRepo;
            _env = env;
        }

        // /Department/Index
        public async Task<IActionResult> Index(string searchInput)
        {

            var department = Enumerable.Empty<Department>();
            var departmentRepo = _unitOfWork.Repository<Department>() as DepartmentRepository;

            if (string.IsNullOrEmpty(searchInput))
            {
                department = await departmentRepo.GetAllAsync();
            }
            else
            {
                department = departmentRepo.SearchEmployeeByName(searchInput.ToLower());
            }

            var mappedEmp = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(department);


            return View(mappedEmp);

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var mappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repository<Department>().Add(mappedEmp);

                var count = await _unitOfWork.Complete();

                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(departmentVM);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }

            var department = await _unitOfWork.Repository<Department>().GetByIdAsync(id.Value);

            var mappedEmp = _mapper.Map<Department, DepartmentViewModel>(department);

            if (department == null)
            {
                return NotFound(); // 404
            }
            return View(viewName, mappedEmp);
        }

        public async Task<IActionResult> Edit(int? id)
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

            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,DepartmentViewModel departmentVM)
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

                _unitOfWork.Repository<Department>().Update(mappedEmp);
                await _unitOfWork.Complete();
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

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                _unitOfWork.Repository<Department>().Delete(mappedEmp);
                await _unitOfWork.Complete();
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
