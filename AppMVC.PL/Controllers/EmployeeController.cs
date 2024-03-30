using AppMVC.BLL.Interfaces;
using AppMVC.DAL.Models;
using AppMVC.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppMVC.PL.Controllers
{
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

        public IActionResult Index(string searchInput)
        {

            var emp = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInput))
            {
                emp = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                emp = _unitOfWork.EmployeeRepository.SearchEmployeeByName(searchInput.ToLower());
            }

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp);


            return View(mappedEmp);

        }


        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel empVM)
        {
            if (ModelState.IsValid)
            {
                // Manual Mapping

                ///var mappedEmp = new Employee()
                ///{
                ///    Name = emp.Name,
                ///    Age = emp.Age,
                ///    Address = emp.Address,
                ///    Salary  = emp.Salary,
                ///    EmailAddress = emp.EmailAddress,
                ///    PhoneNumber = emp.PhoneNumber,
                ///    IsActive = emp.IsActive,
                ///    HiringDate = emp.HiringDate,
                ///};

                //Employee mappedEmp = (Employee) empVM;

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);

                _unitOfWork.EmployeeRepository.Add(mappedEmp);

                var count = _unitOfWork.Complete();

                if (count > 0)
                    TempData["Message"] = "Employee is Created Successfully";

                else
                    TempData["Message"] = "An Error Has Occured, Employee Not Created";

                return RedirectToAction(nameof(Index));
            }
            return View(empVM);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); // 400
            }

            var emp = _unitOfWork.EmployeeRepository.GetById(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);

            if (emp == null)
            {
                return NotFound(); // 404
            }
            return View(viewName, mappedEmp);
        }

        public IActionResult Edit(int? id)
        {
            //ViewBag.Departments = _departmentRepository.GetAll();

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel empVM)
        {
            if (id != empVM.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(empVM);


            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);

                _unitOfWork.EmployeeRepository.Update(mappedEmp);
                _unitOfWork.Complete();
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

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel empVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);

                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
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
    }
}
