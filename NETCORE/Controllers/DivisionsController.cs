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
    public class DivisionsController : BaseController<Division, DivisionRepository>
    {
        private readonly DivisionRepository _repo;

        public DivisionsController(DivisionRepository divisionRepository) : base(divisionRepository)
        {
            _repo = divisionRepository;
        }
    }
}