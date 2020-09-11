using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Report;
using Microsoft.AspNetCore.Mvc;
using NETCORE.Models;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Client.Controllers
{
    public class ReportsController : Controller
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44384/api/")
        };

        public ActionResult DivisionPdf(Division division)
        {
            DivisionReport divisionReport = new DivisionReport();
            byte[] abytes = divisionReport.PrepareReport(GetDivisions());
            return File(abytes, "application/pdf");
        }

        public ActionResult DivisionExcel(Division division)
        {
            var ColumnHeaders = new string[]
            {
                "No",
                "Name",
                "Department",
                "Created At",
                "Updated At"
            };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Division List");
                using (var cells = workSheet.Cells[1, 1, 1, 5])
                {
                    cells.Style.Font.Bold = true;
                }

                for(var i = 0; i < ColumnHeaders.Count(); i++)
                {
                    workSheet.Cells[1, i + 1].Value = ColumnHeaders[i];
                }

                var j = 2;
                var index = 1;
                foreach (var data in GetDivisions())
                {
                    workSheet.Cells["A" + j].Value = index++;
                    workSheet.Cells["B" + j].Value = data.Name;
                    workSheet.Cells["C" + j].Value = data.Department.Name;
                    workSheet.Cells["D" + j].Value = data.CreatedDate.ToString("yyyy MMM dd HH:mm:ss");
                    workSheet.Cells["E" + j].Value = data.UpdatedDate.Year < 2000 ? "Not Updated yet" : data.UpdatedDate.ToString("yyyy MMM dd HH:mm:ss");

                    j++;
                }
                result = package.GetAsByteArray();
            }
            return File(result, "application/ms-excel", $"Divisions.xlsx");
        }

        public List<Division> GetDivisions()
        {
            List<Division> divisions = null;
            var readTask = client.GetAsync("Divisions/");

            readTask.Wait();

            var result = readTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var output = result.Content.ReadAsStringAsync().Result;
                divisions = JsonConvert.DeserializeObject<List<Division>>(output);
            }
            return divisions;
        }
    }
}