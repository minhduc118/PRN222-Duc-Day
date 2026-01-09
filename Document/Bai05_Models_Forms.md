# 📚 BÀI 5: MODELS VÀ FORM HANDLING

> **Thời gian**: 2-3 giờ  
> **Mục tiêu**: Hiểu Model Binding, Data Annotations và Validation

---

## 1️⃣ MODEL TRONG MVC

Model đại diện cho dữ liệu và business logic.

### 1.1 Tạo Model cơ bản

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
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Foreign Key
        public int CategoryId { get; set; }
        
        // Navigation Property
        public Category? Category { get; set; }
    }
}
```

```csharp
// Models/Category.cs

namespace MyFirstMvc.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        // Navigation Property (1-N relationship)
        public List<Product> Products { get; set; } = new();
    }
}
```

---

## 2️⃣ MODEL BINDING

Model Binding tự động map dữ liệu từ HTTP request vào parameters/model.

### 2.1 Từ Route Parameters

```csharp
// URL: /Products/Details/5
public IActionResult Details(int id)  // id = 5
{
    return View();
}

// URL: /Products/Edit/5
public IActionResult Edit(int id)  // id = 5
{
    return View();
}
```

### 2.2 Từ Query String

```csharp
// URL: /Products?page=2&size=10&keyword=iphone
public IActionResult Index(int page = 1, int size = 10, string? keyword = null)
{
    // page = 2, size = 10, keyword = "iphone"
    return View();
}
```

### 2.3 Từ Form Data (POST)

```csharp
// Cách 1: Từng parameter
[HttpPost]
public IActionResult Create(string name, decimal price, int quantity)
{
    var product = new Product
    {
        Name = name,
        Price = price,
        Quantity = quantity
    };
    return RedirectToAction("Index");
}

// Cách 2: Model binding (KHUYÊN DÙNG)
[HttpPost]
public IActionResult Create(Product product)
{
    // product.Name, product.Price, product.Quantity được tự động map
    return RedirectToAction("Index");
}
```

### 2.4 Binding Attributes

```csharp
// [FromRoute] - Lấy từ URL route
public IActionResult Details([FromRoute] int id) { }

// [FromQuery] - Lấy từ query string
public IActionResult Search([FromQuery] string keyword) { }

// [FromForm] - Lấy từ form data
[HttpPost]
public IActionResult Create([FromForm] Product product) { }

// [FromBody] - Lấy từ JSON body (API)
[HttpPost]
public IActionResult CreateApi([FromBody] Product product) { }

// [FromHeader] - Lấy từ HTTP header
public IActionResult Check([FromHeader(Name = "X-Api-Key")] string apiKey) { }
```

---

## 3️⃣ DATA ANNOTATIONS (VALIDATION)

### 3.1 Validation Attributes

```csharp
using System.ComponentModel.DataAnnotations;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(100, MinimumLength = 3, 
        ErrorMessage = "Tên phải từ 3-100 ký tự")]
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Giá không được để trống")]
    [Range(1000, 1000000000, ErrorMessage = "Giá từ 1,000 đến 1,000,000,000")]
    [Display(Name = "Giá bán")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Range(0, 10000, ErrorMessage = "Số lượng từ 0-10000")]
    [Display(Name = "Số lượng")]
    public int Quantity { get; set; }

    [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Đang kinh doanh")]
    public bool IsActive { get; set; } = true;

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }
}
```

### 3.2 Các Validation Attributes phổ biến

| Attribute | Mô tả | Ví dụ |
|-----------|-------|-------|
| `[Required]` | Bắt buộc nhập | `[Required(ErrorMessage = "...")]` |
| `[StringLength]` | Độ dài chuỗi | `[StringLength(100, MinimumLength = 3)]` |
| `[Range]` | Khoảng giá trị | `[Range(1, 100)]` |
| `[EmailAddress]` | Email hợp lệ | `[EmailAddress]` |
| `[Phone]` | Số điện thoại | `[Phone]` |
| `[Url]` | URL hợp lệ | `[Url]` |
| `[Compare]` | So sánh với field khác | `[Compare("Password")]` |
| `[RegularExpression]` | Regex pattern | `[RegularExpression(@"^\d{10}$")]` |
| `[CreditCard]` | Số thẻ tín dụng | `[CreditCard]` |

### 3.3 Display Attributes

```csharp
[Display(Name = "Tên sản phẩm")]          // Label hiển thị
[DisplayFormat(DataFormatString = "{0:N0}")] // Format số
[DataType(DataType.Currency)]              // Hiển thị tiền tệ
[DataType(DataType.Date)]                  // Hiển thị ngày
[DataType(DataType.Password)]              // Input password
[DataType(DataType.MultilineText)]         // Textarea
[DataType(DataType.EmailAddress)]          // Input email
[ScaffoldColumn(false)]                    // Ẩn trong scaffold
```

---

## 4️⃣ VALIDATION TRONG CONTROLLER

### 4.1 Kiểm tra ModelState

```csharp
[HttpPost]
public IActionResult Create(Product product)
{
    // Kiểm tra validation
    if (!ModelState.IsValid)
    {
        // Có lỗi validation → trả về form với lỗi
        return View(product);
    }

    // Validation OK → xử lý lưu
    _products.Add(product);
    TempData["Success"] = "Thêm sản phẩm thành công!";
    return RedirectToAction("Index");
}

[HttpPost]
public IActionResult Edit(int id, Product product)
{
    if (id != product.Id)
    {
        return BadRequest();
    }

    if (!ModelState.IsValid)
    {
        return View(product);
    }

    // Cập nhật...
    return RedirectToAction("Index");
}
```

### 4.2 Thêm lỗi thủ công

```csharp
[HttpPost]
public IActionResult Create(Product product)
{
    // Validation tùy chỉnh
    if (_products.Any(p => p.Name == product.Name))
    {
        ModelState.AddModelError("Name", "Tên sản phẩm đã tồn tại!");
    }

    if (product.Price < product.Quantity * 1000)
    {
        ModelState.AddModelError("", "Giá không hợp lệ so với số lượng!");
    }

    if (!ModelState.IsValid)
    {
        return View(product);
    }

    // Lưu...
    return RedirectToAction("Index");
}
```

---

## 5️⃣ VALIDATION TRONG VIEW

### 5.1 Hiển thị lỗi từng field

```html
<form asp-action="Create" method="post">
    <div class="mb-3">
        <label asp-for="Name" class="form-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <div class="mb-3">
        <label asp-for="Price" class="form-label"></label>
        <input asp-for="Price" class="form-control" />
        <span asp-validation-for="Price" class="text-danger"></span>
    </div>

    <button type="submit" class="btn btn-primary">Lưu</button>
</form>
```

### 5.2 Hiển thị tất cả lỗi (Summary)

```html
<!-- Hiển thị ở đầu form -->
<div asp-validation-summary="All" class="alert alert-danger"></div>

<!-- Hoặc chỉ hiển thị lỗi model (không có field cụ thể) -->
<div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>
```

### 5.3 CSS cho input invalid

```html
<!-- Bootstrap tự động thêm class is-invalid khi có lỗi -->
<input asp-for="Name" class="form-control" />

<!-- CSS tùy chỉnh -->
<style>
    .input-validation-error {
        border-color: red;
    }
    .field-validation-error {
        color: red;
        font-size: 0.875rem;
    }
</style>
```

### 5.4 Client-side Validation

```html
<!-- Thêm vào cuối View hoặc trong @section Scripts -->
@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

File `_ValidationScriptsPartial.cshtml`:
```html
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script>
<script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
```

---

## 6️⃣ VIEWMODEL

ViewModel là model tùy chỉnh cho View, có thể khác với Entity model.

### 6.1 Khi nào dùng ViewModel?

- Khi View cần dữ liệu từ nhiều Model
- Khi không muốn expose toàn bộ Entity
- Khi cần validation khác với Entity

### 6.2 Ví dụ ViewModel

```csharp
// Models/ViewModels/ProductCreateViewModel.cs

public class ProductCreateViewModel
{
    [Required(ErrorMessage = "Tên không được để trống")]
    [StringLength(100)]
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1000, 1000000000)]
    [Display(Name = "Giá bán")]
    public decimal Price { get; set; }

    [Display(Name = "Số lượng")]
    public int Quantity { get; set; }

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Chọn danh mục")]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    // Dùng để populate dropdown
    public List<SelectListItem> Categories { get; set; } = new();
}
```

```csharp
// Models/ViewModels/ProductIndexViewModel.cs

public class ProductIndexViewModel
{
    public List<Product> Products { get; set; } = new();
    public string? Keyword { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public int? CategoryId { get; set; }
    public List<SelectListItem> Categories { get; set; } = new();
}
```

### 6.3 Sử dụng trong Controller

```csharp
// GET: Create
public IActionResult Create()
{
    var viewModel = new ProductCreateViewModel
    {
        Categories = _categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList()
    };
    return View(viewModel);
}

// POST: Create
[HttpPost]
public IActionResult Create(ProductCreateViewModel model)
{
    if (!ModelState.IsValid)
    {
        // Load lại categories nếu có lỗi
        model.Categories = _categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
        return View(model);
    }

    // Map ViewModel → Entity
    var product = new Product
    {
        Name = model.Name,
        Price = model.Price,
        Quantity = model.Quantity,
        Description = model.Description,
        CategoryId = model.CategoryId
    };

    _products.Add(product);
    return RedirectToAction("Index");
}
```

---

## 7️⃣ THỰC HÀNH: FORM HOÀN CHỈNH

### Controller

```csharp
public class ProductsController : Controller
{
    private static List<Category> _categories = new()
    {
        new Category { Id = 1, Name = "Điện thoại" },
        new Category { Id = 2, Name = "Laptop" },
        new Category { Id = 3, Name = "Tablet" }
    };

    private static List<Product> _products = new();
    private static int _nextId = 1;

    public IActionResult Index()
    {
        return View(_products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_categories, "Id", "Name");
            return View(product);
        }

        product.Id = _nextId++;
        product.CreatedAt = DateTime.Now;
        _products.Add(product);
        
        TempData["Success"] = "Thêm sản phẩm thành công!";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        ViewBag.Categories = new SelectList(_categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(int id, Product product)
    {
        if (id != product.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        var existing = _products.FirstOrDefault(p => p.Id == id);
        if (existing == null) return NotFound();

        existing.Name = product.Name;
        existing.Price = product.Price;
        existing.Quantity = product.Quantity;
        existing.Description = product.Description;
        existing.CategoryId = product.CategoryId;
        existing.IsActive = product.IsActive;

        TempData["Success"] = "Cập nhật thành công!";
        return RedirectToAction("Index");
    }
}
```

### View Create

```html
@model Product

@{
    ViewData["Title"] = "Thêm sản phẩm";
}

<h1>@ViewData["Title"]</h1>

<div class="row">
    <div class="col-md-6">
        <form asp-action="Create" method="post">
            <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>

            <div class="mb-3">
                <label asp-for="Name" class="form-label"></label>
                <input asp-for="Name" class="form-control" />
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>

            <div class="mb-3">
                <label asp-for="Price" class="form-label"></label>
                <input asp-for="Price" class="form-control" type="number" step="1000" />
                <span asp-validation-for="Price" class="text-danger"></span>
            </div>

            <div class="mb-3">
                <label asp-for="Quantity" class="form-label"></label>
                <input asp-for="Quantity" class="form-control" type="number" />
                <span asp-validation-for="Quantity" class="text-danger"></span>
            </div>

            <div class="mb-3">
                <label asp-for="CategoryId" class="form-label"></label>
                <select asp-for="CategoryId" asp-items="ViewBag.Categories" class="form-select">
                    <option value="">-- Chọn danh mục --</option>
                </select>
                <span asp-validation-for="CategoryId" class="text-danger"></span>
            </div>

            <div class="mb-3">
                <label asp-for="Description" class="form-label"></label>
                <textarea asp-for="Description" class="form-control" rows="3"></textarea>
                <span asp-validation-for="Description" class="text-danger"></span>
            </div>

            <div class="form-check mb-3">
                <input asp-for="IsActive" class="form-check-input" />
                <label asp-for="IsActive" class="form-check-label"></label>
            </div>

            <button type="submit" class="btn btn-primary">Lưu</button>
            <a asp-action="Index" class="btn btn-secondary">Hủy</a>
        </form>
    </div>
</div>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

---

## ✅ BÀI TẬP

### Bài 1: Tạo Model User

Tạo `User.cs` với validation:
- Username: 3-50 ký tự, bắt buộc
- Email: email hợp lệ, bắt buộc
- Password: tối thiểu 6 ký tự
- ConfirmPassword: so sánh với Password
- Phone: regex số điện thoại VN

### Bài 2: Form đăng ký

Tạo form đăng ký người dùng với validation đầy đủ (client + server)

---

## 📝 GHI NHỚ

| Khái niệm | Mô tả |
|-----------|-------|
| Model Binding | Tự động map data từ request → parameters |
| `[Required]` | Bắt buộc nhập |
| `[StringLength]` | Giới hạn độ dài |
| `[Range]` | Giới hạn giá trị |
| `ModelState.IsValid` | Kiểm tra validation |
| `asp-validation-for` | Hiển thị lỗi từng field |
| `asp-validation-summary` | Hiển thị tất cả lỗi |
| ViewModel | Model tùy chỉnh cho View |

---

**Bài tiếp theo**: [Bài 6 - Entity Framework Core](./Bai06_Entity_Framework.md)
