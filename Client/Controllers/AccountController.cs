using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCORE;
using NETCORE.ViewModel;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44384/api/")
        };

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var content = JsonConvert.SerializeObject(login);

                var postTask = client.PostAsync("Logins", new StringContent(content, Encoding.UTF8, "application/json"));
                postTask.Wait();
                var result = postTask.Result;
                if (result.StatusCode.ToString() == "NotFound")
                {
                    ModelState.AddModelError("UserName", "Username not found");
                    return View();
                }
                else if (result.StatusCode.ToString() == "BadRequest")
                {
                    ModelState.AddModelError("Password", "Wrong Password");
                    return View();
                }
                else
                {
                    var stream = result.Content.ReadAsStringAsync().Result;
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stream);
                    var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
                    //var data = tokenS.Claims.ToList();
                    var userName = tokenS.Claims.First(claim => claim.Type == "UserName").Value;
                    var role = tokenS.Claims.First(claim => claim.Type == "Role").Value;
                    var firstName = tokenS.Claims.First(claim => claim.Type == "FirstName").Value;
                    var lastName = tokenS.Claims.First(claim => claim.Type == "LastName").Value;

                    var auth = "Bearer " + stream;

                    //HttpContext.Session.SetString("Id", account.Id);
                    HttpContext.Session.SetString("UserName", userName);
                    //HttpContext.Session.SetString("Email", account.Email);
                    HttpContext.Session.SetString("Role", role);
                    HttpContext.Session.SetString("FirstName", firstName);
                    HttpContext.Session.SetString("LastName", lastName);
                    HttpContext.Session.SetString("token", auth);
                    //HttpContext.Session.SetString("Phone", account.Phone);

                    return Redirect("../Home");
                }
            }
            return View();  
            
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["register"] = JsonConvert.SerializeObject(registerViewModel);
                return RedirectToAction("Verification");
            }
            return View();
        }

        public IActionResult Verification()
        {
            var otp = GenerateOTP();
            RegisterViewModel register = JsonConvert.DeserializeObject<RegisterViewModel>((string) TempData["register"]);

            SmtpClient smtp = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("someone@domain.com", "yourpassword"),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            MailMessage message = new MailMessage
            {
                From = new MailAddress("eacoldmelon72@gmail.com", "donotreplyplz@admin.com"),
                IsBodyHtml = true,
                Subject = "Registration Verification at: " + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")
            };
            message.Body = MessageBody(otp);
            message.To.Add(new MailAddress(register.Email));
            smtp.Send(message);
            TempData["OTP"] = otp;
            TempData["registerParse"] = JsonConvert.SerializeObject(register);
            return View();
        }

        [HttpPost]
        public IActionResult Verification(IFormCollection form)
        {
            RegisterViewModel register = JsonConvert.DeserializeObject<RegisterViewModel>((string)TempData["registerParse"]);
            string verification = TempData["OTP"].ToString();
            string code = form["OTP"].ToString();
            if (code == verification)
            {
                var content = JsonConvert.SerializeObject(register);

                var postTask = client.PostAsync("Registers/", new StringContent(content, Encoding.UTF8, "application/json"));
                postTask.Wait();
                var result = postTask.Result;
                return Redirect("./Login");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Clear();
            return Redirect("./Login");
        }

        public static string GenerateOTP()
        {
            Random random = new Random();

            string number = "";
            for (int i = 0; i < 6; i++)
            {
                int randomNum = random.Next(10);
                number += randomNum;
            }

            return number;
        }

        public static string MessageBody(string otp)
        {
            string messageBody = "<h3>Confirmation Message</h3>";
            messageBody += "<br />";
            messageBody += "<p>Your OTP code is " + otp + "</p>";
            messageBody += "<br />";
            messageBody += "<b><i>Please don't tell anyone about this code</i></b>";
            return messageBody;
        }
    }
}
