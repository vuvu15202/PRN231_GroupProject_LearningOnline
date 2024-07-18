using Microsoft.AspNetCore.Mvc;
using Project_Client.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Project_Client.Controllers
{
	public class CourseController : Controller
	{
		private readonly HttpClient _client;

		public CourseController()
		{
			_client = new HttpClient();
		}

		public IActionResult Index(int? selectCategory, string? keySearch,int page = 1,int pageSize = 4)
		{
			

			//lấy danh sách phân trang courses

			var request = new
			{
				page = page,
				pageSize = pageSize,
				keySearch = keySearch,
				categoryId = selectCategory
			};

			var jsonString = JsonSerializer.Serialize(request);

			StringContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

			var courseResponse = _client.PostAsync("https://localhost:5000/api/Courses/SearchCourse",content).Result;
			if (courseResponse.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var jsonResponse =  courseResponse.Content.ReadAsStringAsync().Result;
				ViewData["courses"] = JsonSerializer.Deserialize<PaginationResult<List<CourseResponse>>>(jsonResponse);
			}


			//Lấy danh sách category
			var CateResponse = _client.GetAsync("https://localhost:5000/api/Categories").Result;
			if (CateResponse.StatusCode == System.Net.HttpStatusCode.OK)
			{

				ViewData["categories"] = CateResponse.Content.ReadFromJsonAsync<List<CategoryResponse>>().Result;
				ViewData["selectCategory"] = selectCategory;
				ViewData["keySearch"] = keySearch;
			}



			return View("ListCourse");
		}

		public IActionResult DetailCourse(int courseId)
		{
			var courseResponse = _client.GetAsync($"https://localhost:5000/api/Courses/{courseId}").Result;

			if (courseResponse.StatusCode == System.Net.HttpStatusCode.NotFound) { return NotFound(); }

			if (courseResponse.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["course"] = courseResponse.Content.ReadFromJsonAsync<CourseDTO>().Result;
			}

			return View();
		}

		/// <summary>
		/// Chức năng danh sách course đã join
		/// </summary>
		/// <returns></returns>
		public IActionResult MyCourse(int page = 1, int pageSize = 3)
		{
			if (CheckUser() == false) return Unauthorized(); 
			HttpClient client = new HttpClient();
			client = AddTokenToHTTPCLient(client);

            var request = new
            {
                page = page,
                pageSize = pageSize
            };

            //var jsonString = JsonSerializer.Serialize(request);

            StringContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var courseResponse = _client.PostAsync("https://localhost:5000/api/Courses/GetMyCourse", content).Result;
            if (courseResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonResponse = courseResponse.Content.ReadAsStringAsync().Result;
                ViewData["courses"] = JsonSerializer.Deserialize<PaginationResult<List<EnrolledCourseDTO>>>(jsonResponse);
            }


            return View();
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

		public bool CheckUser()
		{
			var userCookie = Request.Cookies["user"];
			var userSession = HttpContext.Session.GetString("user");
			if(userCookie == null && userSession == null) return false;

			return true;
		}

        #endregion
    }
}
