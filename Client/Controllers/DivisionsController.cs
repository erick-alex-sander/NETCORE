using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Models;
using Newtonsoft.Json;

namespace Client.Controllers
{

    public class DivisionsController : Controller
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

        public async Task<JsonResult> Load(int? id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            IEnumerable<Division> divisions = null;
            Division division = null;

            var readTask = client.GetAsync("Divisions/" + id);

            readTask.Wait();

            var result = readTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                if (id != null)
                {
                    division = JsonConvert.DeserializeObject<Division>(output);
                    return Json(division);
                }
                divisions = JsonConvert.DeserializeObject<List<Division>>(output);

            }
            return Json(divisions);
        }

        public async Task<JsonResult> Insert(int? id, Division division)
        {
            var item = JsonConvert.SerializeObject(division);

            if (id != null)
            {
                var postTask = client.PutAsync("divisions/" + id, new StringContent(item, Encoding.UTF8, "application/json"));
                postTask.Wait();
                var result = postTask.Result;
                return Json(new { success = result.IsSuccessStatusCode });
            }
            else
            {
                var postTask = client.PostAsync("divisions", new StringContent(item, Encoding.UTF8, "application/json"));
                postTask.Wait();
                var result = postTask.Result;
                var oncom = result.Content.ReadAsStringAsync().Result;
                return Json(new { success = result.IsSuccessStatusCode });
            }
        }

        public async Task<JsonResult> Delete(int id)
        {
            var deleteTask = client.DeleteAsync("divisions/" + id);
            deleteTask.Wait();

            var result = deleteTask.Result;
            return Json(new { success = result.IsSuccessStatusCode });
        }
    }
}