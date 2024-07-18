using Microsoft.AspNetCore.Mvc;
using Project_Client.Models;
using Project_Client.Services;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace Project_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private readonly MyCookieService _cookieService;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            var userJson = HttpContext.Session.GetString("user");

            HttpClient _client = new HttpClient();

            var CateResponse = _client.GetAsync("https://localhost:5000/api/Categories").Result;
            if (CateResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                
                ViewData["categories"] = CateResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>().Result;
            }

            var CourseResponse = _client.GetAsync("https://localhost:5000/api/Courses/GetTop3").Result;
            if (CateResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                
                ViewData["Course"] = CourseResponse.Content.ReadFromJsonAsync<List<CourseResponse>>().Result;

            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password) 
        {
            bool rememberMe = Request.Form["rememberMe"] == "on";

            var _client = new HttpClient();
            var req = new
            {
                userName = userName,
                password = password,
            };

            var content = new StringContent(JsonSerializer.Serialize(req), Encoding.UTF8, "application/json");


            var loginResponse = _client.PostAsync("https://localhost:5000/api/auth/Authenticate", content).Result;
            if (loginResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonUser = loginResponse.Content.ReadAsStringAsync().Result;

                if (rememberMe == true)
                {
                    Response.Cookies.Append("user", jsonUser, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7)
                    });

                    var userJson = Request.Cookies["user"];
				}
                else
                {
                    HttpContext.Session.SetString("user", jsonUser);

                    
                }
                var userSession = HttpContext.Session.GetString("user");

                return RedirectToAction("Index");
            }
            else
            {
                ViewData["errorMessage"] = "Wrong email or password";

                return View();
            }

				
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region PrivateWork

        public HttpClient AddTokenToHTTPCLient(HttpClient client)
        {

            //check cookie
            var userCookie = Request.Cookies["user"];
            if (userCookie != null)
            {
                var user = JsonSerializer.Deserialize<LoginResponse>(userCookie);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);
                return client;

            }
            //check session
            var userSession = HttpContext.Session.GetString("user");
            if (userSession != null)
            {
                var user = JsonSerializer.Deserialize<LoginResponse>(userSession);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.JwtToken);
                return client;
            }

            return client;
        }

        #endregion
    }
}
