using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Base;
using NETCORE.Models;
using NETCORE.Repositories;

namespace NETCORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department, DepartmentRepository>
    {
        private readonly DepartmentRepository _repo;

        public DepartmentsController(DepartmentRepository departmentRepository) : base (departmentRepository)
        {
            _repo = departmentRepository;
        }
    }
}