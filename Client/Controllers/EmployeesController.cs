using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class EmployeesController : Controller
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
            return Redirect("/NotFound");
        }

        public async Task<JsonResult> Load(string id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            IEnumerable<ProfileViewModel> profiles = null;
            ProfileViewModel profile = null;

            var readTask = client.GetAsync("registers/" + id);

            readTask.Wait();

            var result = readTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                if (id != null)
                {
                    profile = JsonConvert.DeserializeObject<ProfileViewModel>(output);
                    return Json(profile);
                }
                profiles = JsonConvert.DeserializeObject<List<ProfileViewModel>>(output);

            }
            return Json(profiles);
        }

        public async Task<JsonResult> Delete(string id)
        {
            var deleteTask = client.DeleteAsync("registers/" + id);
            deleteTask.Wait();

            var result = deleteTask.Result;
            return Json(new { success = result.IsSuccessStatusCode });
        }
    }
}