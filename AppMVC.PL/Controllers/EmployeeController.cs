using AppMVC.BLL.Interfaces;
using AppMVC.BLL.Repositories;
using AppMVC.DAL.Models;
using AppMVC.PL.Helpers;
using AppMVC.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppMVC.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        //private readonly IEmployeeRepository _employeeRepo;
        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(
            IUnitOfWork unitOfWork,
            //IEmployeeRepository employeeRepo, 
            //IDepartmentRepository departmentRepository, 
            IMapper mapper,
            IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;

            //_employeeRepo = employeeRepo;
            //_departmentRepository = departmentRepository;
        }

        public async Task<IActionResult> Index(string searchInput)
        {

            var emp = Enumerable.Empty<Employee>();
            var empRepo = _unitOfWork.Repository<Employee>() as EmployeeRepository;
            if (string.IsNullOrEmpty(searchInput))
            {
                emp = await empRepo.GetAllAsync();
            }
            else
            {
                emp = empRepo.SearchEmployeeByName(searchInput.ToLower());
            }

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp);


            return View(mappedEmp);

        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel empVM)
        {
            if (ModelState.IsValid)
            {
                empVM.ImageName =await DocumentSettings.UploadFile(empVM.Image, "images");

                //Employee mappedEmp = (Employee) empVM;

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);

                _unitOfWork.Repository<Employee>().Add(mappedEmp);

                var count =await _unitOfWork.Complete();

                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created Successfully";
                }
                else
                {
                    TempData["Message"] = "An Error Has Occured, Employee Not Created";

                }

                return RedirectToAction(nameof(Index));
            }
            return View(empVM);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }

            var emp = await _unitOfWork.Repository<Employee>().GetByIdAsync(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            if (emp == null)
            {
                return NotFound(); // 404
            }

            if (viewName.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = emp.ImageName;

            return View(viewName, mappedEmp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel empVM)
        {
            if (id != empVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(empVM);


            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);

                _unitOfWork.Repository<Employee>().Update(mappedEmp);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");

                return View(empVM);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel empVM)
        {
            try
            {
                empVM.ImageName = TempData["ImageName"] as string;

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);

                _unitOfWork.Repository<Employee>().Delete(mappedEmp);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                {
                    DocumentSettings.DeleteFile(empVM.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
                return View(empVM);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Has Occurred during Updating the Employee");

                return View(empVM);
            }
        }
    }
}
