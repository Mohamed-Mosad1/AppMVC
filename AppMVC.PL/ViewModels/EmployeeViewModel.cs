using AppMVC.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace AppMVC.PL.ViewModels
{
    // Model
    public class EmployeeViewModel
    {

        #region Data

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(50, ErrorMessage = "Max length of name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min length of name is 5 chars")]
        public string Name { get; set; }

        [Range(22, 30)]
        public int Age { get; set; }
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
            , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]

        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmpType EmpType { get; set; }



        #endregion

        public int? DepartmentId { get; set; } // FK
        public Department Department { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

    }
}
