using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Models;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class ChartsController : Controller
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44384/api/")
        };

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                return View();
            }
            return Redirect("./NotFound");
        }

        public async Task<JsonResult> Load()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            IEnumerable<Division> divisions = null;
            var readTask = client.GetAsync("Divisions/");
            readTask.Wait();

            var result = readTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                divisions = JsonConvert.DeserializeObject<List<Division>>(output);

                var departmentCounts = from di in divisions
                                       group di by di.Department.Name into de
                                       select new { department = de.Key, departmentCounts = de.Count() };
                return Json(departmentCounts);
            }

            return Json(divisions);

        }
    }
}