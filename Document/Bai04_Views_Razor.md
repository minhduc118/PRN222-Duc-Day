# 📚 BÀI 4: VIEWS VÀ RAZOR SYNTAX

> **Thời gian**: 2-3 giờ  
> **Mục tiêu**: Hiểu Razor syntax, tạo Views và sử dụng Tag Helpers

---

## 1️⃣ RAZOR SYNTAX CƠ BẢN

Razor là template engine của ASP.NET, cho phép kết hợp C# với HTML.

### 1.1 Cú pháp cơ bản

```html
<!-- 1. Hiển thị giá trị với @ -->
<h1>Xin chào @name</h1>
<p>Giá: @product.Price VNĐ</p>
<p>Ngày: @DateTime.Now.ToString("dd/MM/yyyy")</p>

<!-- 2. Block code với @{ } -->
@{
    var greeting = "Hello World";
    var total = 100 * 2;
}
<p>@greeting</p>
<p>Total: @total</p>

<!-- 3. If-else -->
@if (product.IsActive)
{
    <span class="badge bg-success">Đang bán</span>
}
else
{
    <span class="badge bg-danger">Ngừng bán</span>
}

<!-- 4. Switch -->
@switch (product.Status)
{
    case "new":
        <span>Mới</span>
        break;
    case "sale":
        <span>Giảm giá</span>
        break;
    default:
        <span>Bình thường</span>
        break;
}

<!-- 5. For loop -->
@for (int i = 0; i < 5; i++)
{
    <p>Item @i</p>
}

<!-- 6. Foreach -->
@foreach (var item in products)
{
    <tr>
        <td>@item.Id</td>
        <td>@item.Name</td>
        <td>@item.Price</td>
    </tr>
}

<!-- 7. Text thuần trong block code -->
@{
    <text>Đây là text thuần</text>
    @:Hoặc dùng @: cho một dòng
}

<!-- 8. Escape @ -->
<p>Email: user@@gmail.com</p>

<!-- 9. Comment -->
@* Đây là Razor comment, không hiển thị trong HTML *@
```

---

## 2️⃣ TRUYỀN DỮ LIỆU TỪ CONTROLLER → VIEW

### 2.1 ViewData (Dictionary)

```csharp
// Controller
public IActionResult Index()
{
    ViewData["Title"] = "Danh sách sản phẩm";
    ViewData["Message"] = "Chào mừng!";
    ViewData["Count"] = 100;
    return View();
}
```

```html
<!-- View -->
<h1>@ViewData["Title"]</h1>
<p>@ViewData["Message"]</p>
<p>Số lượng: @ViewData["Count"]</p>
```

### 2.2 ViewBag (Dynamic)

```csharp
// Controller
public IActionResult Index()
{
    ViewBag.Title = "Danh sách sản phẩm";
    ViewBag.Categories = new List<string> { "Điện thoại", "Laptop" };
    return View();
}
```

```html
<!-- View -->
<h1>@ViewBag.Title</h1>
@foreach (var cat in ViewBag.Categories)
{
    <span>@cat</span>
}
```

### 2.3 Model (Strongly Typed) ⭐ KHUYÊN DÙNG

```csharp
// Controller
public IActionResult Index()
{
    var products = GetProducts(); // List<Product>
    return View(products);
}

public IActionResult Details(int id)
{
    var product = GetProduct(id); // Product
    return View(product);
}
```

```html
<!-- View: Views/Products/Index.cshtml -->
@model List<Product>

<h1>Danh sách sản phẩm (@Model.Count sản phẩm)</h1>
@foreach (var item in Model)
{
    <p>@item.Name - @item.Price</p>
}

<!-- View: Views/Products/Details.cshtml -->
@model Product

<h1>@Model.Name</h1>
<p>Giá: @Model.Price</p>
<p>Mô tả: @Model.Description</p>
```

### 2.4 TempData (Giữ qua redirect)

```csharp
// Controller
[HttpPost]
public IActionResult Create(Product product)
{
    // Lưu sản phẩm...
    TempData["Success"] = "Tạo sản phẩm thành công!";
    return RedirectToAction("Index");
}

public IActionResult Index()
{
    // TempData["Success"] vẫn còn sau khi redirect
    return View();
}
```

```html
<!-- View -->
@if (TempData["Success"] != null)
{
    <div class="alert alert-success">
        @TempData["Success"]
    </div>
}
```

---

## 3️⃣ LAYOUT VÀ SECTIONS

### 3.1 Layout (_Layout.cshtml)

```html
<!-- Views/Shared/_Layout.cshtml -->

<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"] - MyApp</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" />
    
    @* Section CSS tùy chọn *@
    @await RenderSectionAsync("Styles", required: false)
</head>
<body>
    <nav class="navbar navbar-expand-lg bg-dark navbar-dark">
        <div class="container">
            <a class="navbar-brand" href="/">MyApp</a>
        </div>
    </nav>

    <div class="container mt-4">
        @* Nội dung các View con sẽ render ở đây *@
        @RenderBody()
    </div>

    <footer class="footer mt-5 py-3 bg-light">
        <div class="container text-center">
            &copy; 2026 - MyApp
        </div>
    </footer>

    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    
    @* Section Scripts tùy chọn *@
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

### 3.2 View sử dụng Layout

```html
<!-- Views/Products/Index.cshtml -->

@{
    ViewData["Title"] = "Danh sách sản phẩm";
}

<h1>Sản phẩm</h1>
<p>Nội dung trang...</p>

@* Section Scripts cho trang này *@
@section Scripts {
    <script>
        console.log("Script chỉ load ở trang này");
    </script>
}

@section Styles {
    <style>
        .custom-style { color: red; }
    </style>
}
```

### 3.3 _ViewStart.cshtml

```html
<!-- Views/_ViewStart.cshtml -->
@{
    Layout = "_Layout";  // Layout mặc định cho tất cả Views
}
```

### 3.4 _ViewImports.cshtml

```html
<!-- Views/_ViewImports.cshtml -->

@* Import namespace để không phải gõ đầy đủ *@
@using MyFirstMvc
@using MyFirstMvc.Models

@* Bật Tag Helpers *@
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

---

## 4️⃣ PARTIAL VIEWS

### 4.1 Tạo Partial View

```html
<!-- Views/Shared/_ProductCard.cshtml -->

@model Product

<div class="card">
    <div class="card-body">
        <h5 class="card-title">@Model.Name</h5>
        <p class="card-text">@Model.Price.ToString("N0") VNĐ</p>
        <a asp-action="Details" asp-route-id="@Model.Id" class="btn btn-primary">
            Chi tiết
        </a>
    </div>
</div>
```

### 4.2 Sử dụng Partial View

```html
<!-- Cách 1: Html.PartialAsync -->
@await Html.PartialAsync("_ProductCard", product)

<!-- Cách 2: Tag Helper (khuyên dùng) -->
<partial name="_ProductCard" model="product" />

<!-- Cách 3: Trong vòng lặp -->
@foreach (var product in Model)
{
    <div class="col-md-4">
        <partial name="_ProductCard" model="product" />
    </div>
}
```

---

## 5️⃣ TAG HELPERS ⭐

Tag Helpers giúp viết code HTML sạch hơn, an toàn hơn.

### 5.1 Link Tag Helpers

```html
<!-- Thay vì viết URL thủ công -->
<a href="/Products/Details/5">Chi tiết</a>

<!-- Dùng Tag Helper -->
<a asp-controller="Products" asp-action="Details" asp-route-id="5">
    Chi tiết
</a>

<!-- Với nhiều route params -->
<a asp-controller="Products" 
   asp-action="Search" 
   asp-route-category="phone"
   asp-route-page="2">
    Tìm kiếm
</a>
<!-- Output: /Products/Search?category=phone&page=2 -->
```

### 5.2 Form Tag Helpers

```html
<form asp-controller="Products" asp-action="Create" method="post">
    <!-- Input -->
    <div class="mb-3">
        <label asp-for="Name" class="form-label"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <!-- Textarea -->
    <div class="mb-3">
        <label asp-for="Description" class="form-label"></label>
        <textarea asp-for="Description" class="form-control" rows="4"></textarea>
    </div>

    <!-- Select -->
    <div class="mb-3">
        <label asp-for="CategoryId" class="form-label"></label>
        <select asp-for="CategoryId" asp-items="ViewBag.Categories" class="form-select">
            <option value="">-- Chọn danh mục --</option>
        </select>
    </div>

    <!-- Checkbox -->
    <div class="form-check mb-3">
        <input asp-for="IsActive" class="form-check-input" />
        <label asp-for="IsActive" class="form-check-label"></label>
    </div>

    <button type="submit" class="btn btn-primary">Lưu</button>
</form>
```

### 5.3 Image và Script Tag Helpers

```html
<!-- Image với cache busting -->
<img src="~/images/logo.png" asp-append-version="true" />
<!-- Output: /images/logo.png?v=abc123 -->

<!-- Script và CSS với version -->
<link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
<script src="~/js/site.js" asp-append-version="true"></script>
```

### 5.4 Environment Tag Helper

```html
<!-- Chỉ hiển thị trong Development -->
<environment include="Development">
    <link rel="stylesheet" href="~/css/debug.css" />
</environment>

<!-- Chỉ hiển thị trong Production -->
<environment exclude="Development">
    <link rel="stylesheet" href="~/css/site.min.css" />
</environment>
```

---

## 6️⃣ THỰC HÀNH: TẠO VIEWS CHO CRUD

### Views/Products/Index.cshtml

```html
@model List<Product>

@{
    ViewData["Title"] = "Danh sách sản phẩm";
}

<div class="d-flex justify-content-between align-items-center mb-4">
    <h1>Sản phẩm</h1>
    <a asp-action="Create" class="btn btn-primary">
        <i class="bi bi-plus"></i> Thêm mới
    </a>
</div>

@if (TempData["Success"] != null)
{
    <div class="alert alert-success alert-dismissible fade show">
        @TempData["Success"]
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>
}

<table class="table table-striped table-hover">
    <thead class="table-dark">
        <tr>
            <th>ID</th>
            <th>Tên sản phẩm</th>
            <th>Giá</th>
            <th>Số lượng</th>
            <th>Trạng thái</th>
            <th width="200">Hành động</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@item.Id</td>
                <td>@item.Name</td>
                <td>@item.Price.ToString("N0") VNĐ</td>
                <td>@item.Quantity</td>
                <td>
                    @if (item.IsActive)
                    {
                        <span class="badge bg-success">Đang bán</span>
                    }
                    else
                    {
                        <span class="badge bg-secondary">Ngừng bán</span>
                    }
                </td>
                <td>
                    <a asp-action="Details" asp-route-id="@item.Id" 
                       class="btn btn-sm btn-info">Chi tiết</a>
                    <a asp-action="Edit" asp-route-id="@item.Id" 
                       class="btn btn-sm btn-warning">Sửa</a>
                    <a asp-action="Delete" asp-route-id="@item.Id" 
                       class="btn btn-sm btn-danger">Xóa</a>
                </td>
            </tr>
        }
    </tbody>
</table>

@if (!Model.Any())
{
    <div class="alert alert-info">
        Chưa có sản phẩm nào. <a asp-action="Create">Thêm sản phẩm đầu tiên</a>
    </div>
}
```

### Views/Products/Create.cshtml

```html
@model Product

@{
    ViewData["Title"] = "Thêm sản phẩm mới";
}

<h1>@ViewData["Title"]</h1>

<div class="row">
    <div class="col-md-6">
        <form asp-action="Create" method="post">
            <div class="mb-3">
                <label asp-for="Name" class="form-label">Tên sản phẩm</label>
                <input asp-for="Name" class="form-control" />
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>

            <div class="mb-3">
                <label asp-for="Price" class="form-label">Giá</label>
                <input asp-for="Price" class="form-control" type="number" />
                <span asp-validation-for="Price" class="text-danger"></span>
            </div>

            <div class="mb-3">
                <label asp-for="Quantity" class="form-label">Số lượng</label>
                <input asp-for="Quantity" class="form-control" type="number" />
            </div>

            <div class="mb-3">
                <label asp-for="Description" class="form-label">Mô tả</label>
                <textarea asp-for="Description" class="form-control" rows="4"></textarea>
            </div>

            <div class="form-check mb-3">
                <input asp-for="IsActive" class="form-check-input" />
                <label asp-for="IsActive" class="form-check-label">Đang bán</label>
            </div>

            <div class="d-flex gap-2">
                <button type="submit" class="btn btn-primary">Lưu</button>
                <a asp-action="Index" class="btn btn-secondary">Hủy</a>
            </div>
        </form>
    </div>
</div>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

### Views/Products/Details.cshtml

```html
@model Product

@{
    ViewData["Title"] = "Chi tiết sản phẩm";
}

<h1>@Model.Name</h1>

<div class="card" style="max-width: 500px;">
    <div class="card-body">
        <table class="table table-borderless">
            <tr>
                <th width="150">ID:</th>
                <td>@Model.Id</td>
            </tr>
            <tr>
                <th>Giá:</th>
                <td>@Model.Price.ToString("N0") VNĐ</td>
            </tr>
            <tr>
                <th>Số lượng:</th>
                <td>@Model.Quantity</td>
            </tr>
            <tr>
                <th>Trạng thái:</th>
                <td>
                    @if (Model.IsActive)
                    {
                        <span class="badge bg-success">Đang bán</span>
                    }
                    else
                    {
                        <span class="badge bg-secondary">Ngừng bán</span>
                    }
                </td>
            </tr>
            <tr>
                <th>Mô tả:</th>
                <td>@(Model.Description ?? "Không có mô tả")</td>
            </tr>
        </table>
    </div>
    <div class="card-footer">
        <a asp-action="Edit" asp-route-id="@Model.Id" class="btn btn-warning">Sửa</a>
        <a asp-action="Index" class="btn btn-secondary">Quay lại</a>
    </div>
</div>
```

---

## ✅ BÀI TẬP

### Bài 1: Hoàn thiện Views

Tạo thêm:
- `Edit.cshtml` - Form chỉnh sửa sản phẩm
- `Delete.cshtml` - Xác nhận xóa sản phẩm

### Bài 2: Tạo Partial View

Tạo `_ProductRow.cshtml` để render mỗi dòng trong bảng sản phẩm

---

## 📝 GHI NHỚ

| Khái niệm | Cú pháp |
|-----------|---------|
| Hiển thị giá trị | `@variable` |
| Block code | `@{ code }` |
| If-else | `@if (condition) { }` |
| Foreach | `@foreach (var item in list) { }` |
| Model strongly typed | `@model ClassName` |
| Tag Helper link | `asp-controller`, `asp-action`, `asp-route-*` |
| Tag Helper form | `asp-for`, `asp-validation-for` |
| Partial View | `<partial name="_Name" model="data" />` |
| Section | `@section Scripts { }` |

---

**Bài tiếp theo**: [Bài 5 - Models và Form Handling](./Bai05_Models_Forms.md)
