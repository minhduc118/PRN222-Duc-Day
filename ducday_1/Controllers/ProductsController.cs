using ducday_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace ducday_1.Controllers
{
    public class ProductsController : Controller
    {

        private static List<Product> _products = new List<Product>
        {
         new Product { Id = 1, Name = "Iphone 15", Price=25000000, Quantity = 50},
         new Product { Id = 2, Name = "Samsung", Price= 30000000, Quantity= 30 },
         new Product { Id = 3, Name= "Xiaomi 14", Price=150000, Quantity=20},
        };
        public IActionResult Index()
        {

            return View(_products);
        }

        public IActionResult Details(int id)
        {
            Product product = _products[id];
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(string name, decimal price)
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            Product? product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingProduct = _products.FirstOrDefault(p => p.Id == id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Price = product.Price;
                    existingProduct.Quantity = product.Quantity;
                    existingProduct.IsActive = product.IsActive;
                }

                TempData["Success"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            Product? product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Product? product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
                TempData["Success"] = "Xóa sản phẩm thành công!";
            }
            return RedirectToAction("Index");
        }
    }
}
