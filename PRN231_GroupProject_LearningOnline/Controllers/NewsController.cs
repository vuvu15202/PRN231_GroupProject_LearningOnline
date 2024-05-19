using Microsoft.AspNetCore.Mvc;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
	public class NewsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
