using Microsoft.AspNetCore.Mvc;

namespace _14_December.Controllers
{
	public class CategoriesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
