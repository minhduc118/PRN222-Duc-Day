using ducday_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ducday_1.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult ShowPage()
        {
            return View(); //Views/Demo/ShowPage.cshtml
        }

        public IActionResult ShowOtherPage()
        {
            return View("CustomView");
        }

        public IActionResult ShowProduct() 
        {
            var product = new Product { Name = "Iphone", Price = 1000 };
            return View(product);
        }

        public IActionResult GoToIndex()
        {
            return RedirectToAction("Index");
        }

        public IActionResult ShowText() 
        {
          return Content("Hello from DemoController!");
        }

        public IActionResult GetJson()
        { 
           var data = new { Name = "Alice", Age = 30 };
            return Json(data);
        }





    }
}
