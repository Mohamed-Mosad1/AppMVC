using AppMVC.BLL.Interfaces;
using AppMVC.DAL.Data;
using AppMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMVC.BLL.Repositories
{
	public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
	{
        public DepartmentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
    }
}
