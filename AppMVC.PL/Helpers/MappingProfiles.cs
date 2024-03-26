using AppMVC.DAL.Models;
using AppMVC.PL.ViewModels;
using AutoMapper;

namespace AppMVC.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
