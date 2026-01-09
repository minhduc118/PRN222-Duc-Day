# 📚 BÀI 3: CONTROLLERS VÀ ROUTING

> **Thời gian**: 2-3 giờ  
> **Mục tiêu**: Hiểu cách Controller xử lý request và cấu hình Routing

---

## 1️⃣ CONTROLLER LÀ GÌ?

Controller là "bộ não" của MVC, chịu trách nhiệm:
- Nhận request từ người dùng
- Xử lý logic nghiệp vụ
- Quyết định trả về View nào

### Quy tắc đặt tên

| Convention | Ví dụ |
|------------|-------|
| Tên kết thúc bằng `Controller` | `HomeController`, `ProductsController` |
| Kế thừa từ `Controller` | `public class HomeController : Controller` |
| Action là public method | `public IActionResult Index()` |

---

## 2️⃣ TẠO CONTROLLER

### 2.1 Controller cơ bản

```csharp
// Controllers/ProductsController.cs

using Microsoft.AspNetCore.Mvc;

namespace MyFirstMvc.Controllers
{
    public class ProductsController : Controller
    {
        // GET: /Products hoặc /Products/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Products/Details/5
        public IActionResult Details(int id)
        {
            // id được lấy từ URL
            ViewData["ProductId"] = id;
            return View();
        }

        // GET: /Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        [HttpPost]
        public IActionResult Create(string name, decimal price)
        {
            // Xử lý tạo sản phẩm
            // Redirect về trang Index sau khi tạo
            return RedirectToAction("Index");
        }
    }
}
```

---

## 3️⃣ ACTION METHODS

### 3.1 Các loại trả về (Action Results)

```csharp
public class DemoController : Controller
{
    // 1. View() - Trả về View tương ứng
    public IActionResult ShowPage()
    {
        return View();  // Views/Demo/ShowPage.cshtml
    }

    // 2. View(viewName) - Trả về View cụ thể
    public IActionResult ShowOtherPage()
    {
        return View("CustomView");  // Views/Demo/CustomView.cshtml
    }

    // 3. View(model) - Trả về View với data
    public IActionResult ShowProduct()
    {
        var product = new Product { Name = "iPhone", Price = 1000 };
        return View(product);
    }

    // 4. RedirectToAction() - Chuyển hướng đến action khác
    public IActionResult GoToIndex()
    {
        return RedirectToAction("Index");
        // hoặc
        return RedirectToAction("Index", "Home");  // controller khác
        // hoặc với tham số
        return RedirectToAction("Details", new { id = 5 });
    }

    // 5. Redirect() - Chuyển hướng đến URL
    public IActionResult GoToGoogle()
    {
        return Redirect("https://google.com");
    }

    // 6. Content() - Trả về text thuần
    public IActionResult ShowText()
    {
        return Content("Hello World!");
    }

    // 7. Json() - Trả về JSON
    public IActionResult GetJson()
    {
        var data = new { Name = "iPhone", Price = 1000 };
        return Json(data);
    }

    // 8. NotFound() - Trả về 404
    public IActionResult ShowNotFound()
    {
        return NotFound();
    }

    // 9. BadRequest() - Trả về 400
    public IActionResult ShowBadRequest()
    {
        return BadRequest("Invalid data");
    }
}
```

### 3.2 HTTP Methods Attributes

```csharp
public class ProductsController : Controller
{
    // Mặc định là GET
    public IActionResult Index()
    {
        return View();
    }

    // Chỉ cho phép GET
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Chỉ cho phép POST
    [HttpPost]
    public IActionResult Create(Product product)
    {
        // Xử lý tạo mới
        return RedirectToAction("Index");
    }

    // PUT - Cập nhật
    [HttpPut]
    public IActionResult Update(int id, Product product)
    {
        return Ok();
    }

    // DELETE - Xóa
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}
```

---

## 4️⃣ ROUTING

### 4.1 Conventional Routing (Mặc định)

```csharp
// Program.cs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

| URL | Controller | Action | id |
|-----|------------|--------|-----|
| `/` | Home | Index | null |
| `/Products` | Products | Index | null |
| `/Products/Details` | Products | Details | null |
| `/Products/Details/5` | Products | Details | 5 |
| `/Products/Edit/10` | Products | Edit | 10 |

### 4.2 Attribute Routing

```csharp
[Route("san-pham")]  // Base route cho controller
public class ProductsController : Controller
{
    // GET: /san-pham
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }

    // GET: /san-pham/chi-tiet/5
    [HttpGet("chi-tiet/{id}")]
    public IActionResult Details(int id)
    {
        return View();
    }

    // GET: /san-pham/danh-muc/dien-thoai
    [HttpGet("danh-muc/{categoryName}")]
    public IActionResult ByCategory(string categoryName)
    {
        return View();
    }
}
```

### 4.3 Route Constraints

```csharp
// id phải là số nguyên
[HttpGet("details/{id:int}")]
public IActionResult Details(int id) { }

// id phải >= 1
[HttpGet("details/{id:int:min(1)}")]
public IActionResult Details(int id) { }

// name phải là string với độ dài tối thiểu
[HttpGet("search/{name:minlength(3)}")]
public IActionResult Search(string name) { }

// Các constraint phổ biến:
// {id:int}         - Số nguyên
// {id:long}        - Số nguyên dài
// {price:decimal}  - Số thập phân
// {active:bool}    - Boolean
// {date:datetime}  - DateTime
// {id:guid}        - GUID
// {name:alpha}     - Chỉ chữ cái
// {id:min(1)}      - Giá trị tối thiểu
// {id:max(100)}    - Giá trị tối đa
// {id:range(1,100)} - Trong khoảng
// {name:length(5)} - Độ dài chính xác
// {name:minlength(3)} - Độ dài tối thiểu
// {name:maxlength(50)} - Độ dài tối đa
```

---

## 5️⃣ TRUYỀN THAM SỐ

### 5.1 Route Parameters

```csharp
// URL: /Products/Details/5
public IActionResult Details(int id)
{
    // id = 5
    return View();
}

// URL: /Products/Edit/5/iphone
public IActionResult Edit(int id, string name)
{
    // id = 5, name = "iphone"
    return View();
}
```

### 5.2 Query String Parameters

```csharp
// URL: /Products/Search?keyword=iphone&page=2
public IActionResult Search(string keyword, int page = 1)
{
    // keyword = "iphone", page = 2
    return View();
}

// URL: /Products/Filter?categories=1&categories=2&categories=3
public IActionResult Filter(int[] categories)
{
    // categories = [1, 2, 3]
    return View();
}
```

### 5.3 Form Data (POST)

```csharp
// Method 1: Từng parameter
[HttpPost]
public IActionResult Create(string name, decimal price, string description)
{
    // Xử lý
    return RedirectToAction("Index");
}

// Method 2: Model binding (khuyên dùng)
[HttpPost]
public IActionResult Create(Product product)
{
    // product.Name, product.Price, product.Description
    return RedirectToAction("Index");
}
```

---

## 6️⃣ THỰC HÀNH: TẠO CONTROLLER QUẢN LÝ SẢN PHẨM

### Bước 1: Tạo Model

```csharp
// Models/Product.cs

namespace MyFirstMvc.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
```

### Bước 2: Tạo Controller với dữ liệu giả

```csharp
// Controllers/ProductsController.cs

using Microsoft.AspNetCore.Mvc;
using MyFirstMvc.Models;

namespace MyFirstMvc.Controllers
{
    public class ProductsController : Controller
    {
        // Dữ liệu giả (tạm thời, sau này sẽ dùng database)
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 15", Price = 25000000, Quantity = 50 },
            new Product { Id = 2, Name = "Samsung S24", Price = 22000000, Quantity = 30 },
            new Product { Id = 3, Name = "Xiaomi 14", Price = 15000000, Quantity = 100 },
        };

        // GET: /Products
        public IActionResult Index()
        {
            return View(_products);
        }

        // GET: /Products/Details/5
        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: /Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Products/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // Tạo ID mới
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
            
            return RedirectToAction("Index");
        }

        // GET: /Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: /Products/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            
            // Cập nhật thông tin
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Quantity = product.Quantity;
            existing.Description = product.Description;
            
            return RedirectToAction("Index");
        }

        // GET: /Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            
            return RedirectToAction("Index");
        }
    }
}
```

---

## ✅ BÀI TẬP

### Bài 1: Tạo CategoriesController

Tạo controller quản lý danh mục với các action:
- `Index()` - Hiển thị danh sách
- `Details(int id)` - Xem chi tiết
- `Create()` - Form tạo mới

### Bài 2: Custom Route

Thêm attribute routing để:
- `/danh-muc` → `CategoriesController.Index()`
- `/danh-muc/chi-tiet/5` → `CategoriesController.Details(5)`

---

## 📝 GHI NHỚ

| Khái niệm | Mô tả |
|-----------|-------|
| Controller | Class xử lý request, kế thừa từ `Controller` |
| Action | Public method trong Controller |
| `View()` | Trả về View tương ứng |
| `RedirectToAction()` | Chuyển hướng đến action khác |
| `[HttpPost]` | Chỉ chấp nhận POST request |
| Route parameter | Lấy từ URL: `/Products/{id}` |
| Query string | Lấy từ URL: `?page=1&size=10` |

---

**Bài tiếp theo**: [Bài 4 - Views và Razor Syntax](./Bai04_Views_Razor.md)
