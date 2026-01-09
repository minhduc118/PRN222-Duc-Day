# 📚 BÀI 7: DỰ ÁN CRUD HOÀN CHỈNH

> **Thời gian**: 4-6 giờ  
> **Mục tiêu**: Xây dựng ứng dụng quản lý sản phẩm hoàn chỉnh với tất cả kiến thức đã học

---

## 🎯 MỤC TIÊU DỰ ÁN

Xây dựng ứng dụng **Quản lý sản phẩm** với các chức năng:
- ✅ CRUD Categories (Danh mục)
- ✅ CRUD Products (Sản phẩm)
- ✅ Tìm kiếm, lọc sản phẩm
- ✅ Phân trang
- ✅ Validation đầy đủ
- ✅ Giao diện đẹp với Bootstrap

---

## 📁 CẤU TRÚC PROJECT

```
ProductManagement/
├── Controllers/
│   ├── HomeController.cs
│   ├── CategoriesController.cs
│   └── ProductsController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
│   ├── Category.cs
│   ├── Product.cs
│   └── ViewModels/
│       ├── ProductIndexViewModel.cs
│       └── ProductCreateViewModel.cs
├── Views/
│   ├── Home/
│   │   └── Index.cshtml
│   ├── Categories/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│   ├── Products/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Details.cshtml
│   │   ├── Edit.cshtml
│   │   └── Delete.cshtml
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── _Pagination.cshtml
│       └── _ProductCard.cshtml
├── wwwroot/
│   ├── css/
│   │   └── site.css
│   └── js/
│       └── site.js
├── Program.cs
└── appsettings.json
```

---

## 1️⃣ BƯỚC 1: TẠO PROJECT

```bash
# Tạo project mới
dotnet new mvc -n ProductManagement
cd ProductManagement

# Cài đặt EF Core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## 2️⃣ BƯỚC 2: TẠO MODELS

### Models/Category.cs

```csharp
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên từ 2-100 ký tự")]
        [Display(Name = "Tên danh mục")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
```

### Models/Product.cs

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Tên từ 3-200 ký tự")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(1000, 100000000000, ErrorMessage = "Giá từ 1,000 đến 100 tỷ")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá bán")]
        public decimal Price { get; set; }

        [StringLength(1000)]
        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Range(0, 100000, ErrorMessage = "Số lượng từ 0-100,000")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Display(Name = "Đang bán")]
        public bool IsActive { get; set; } = true;

        [StringLength(500)]
        [Display(Name = "Hình ảnh")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        // Navigation property
        public virtual Category? Category { get; set; }
    }
}
```

### Models/ViewModels/ProductIndexViewModel.cs

```csharp
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductManagement.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; } = new();
        
        // Search & Filter
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsActive { get; set; }
        
        // Pagination
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        
        // Dropdown
        public List<SelectListItem> Categories { get; set; } = new();
    }
}
```

---

## 3️⃣ BƯỚC 3: TẠO DBCONTEXT

### Data/ApplicationDbContext.cs

```csharp
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại", Description = "Smartphone các loại" },
                new Category { Id = 2, Name = "Laptop", Description = "Máy tính xách tay" },
                new Category { Id = 3, Name = "Máy tính bảng", Description = "Tablet các loại" },
                new Category { Id = 4, Name = "Phụ kiện", Description = "Phụ kiện điện tử" }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product 
                { 
                    Id = 1, Name = "iPhone 15 Pro Max", Price = 34990000, 
                    Quantity = 50, CategoryId = 1, IsActive = true,
                    Description = "iPhone 15 Pro Max 256GB"
                },
                new Product 
                { 
                    Id = 2, Name = "Samsung Galaxy S24 Ultra", Price = 31990000, 
                    Quantity = 30, CategoryId = 1, IsActive = true,
                    Description = "Samsung Galaxy S24 Ultra 256GB"
                },
                new Product 
                { 
                    Id = 3, Name = "MacBook Pro 14 M3", Price = 49990000, 
                    Quantity = 20, CategoryId = 2, IsActive = true,
                    Description = "MacBook Pro 14 inch M3 chip"
                },
                new Product 
                { 
                    Id = 4, Name = "iPad Pro 12.9", Price = 28990000, 
                    Quantity = 25, CategoryId = 3, IsActive = true
                },
                new Product 
                { 
                    Id = 5, Name = "AirPods Pro 2", Price = 6490000, 
                    Quantity = 100, CategoryId = 4, IsActive = true
                }
            );
        }
    }
}
```

---

## 4️⃣ BƯỚC 4: CẤU HÌNH

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Program.cs

```csharp
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

---

## 5️⃣ BƯỚC 5: TẠO MIGRATION

```bash
# Tạo migration
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update
```

---

## 6️⃣ BƯỚC 6: CONTROLLERS

### Controllers/ProductsController.cs (Đầy đủ)

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;
using ProductManagement.Models.ViewModels;

namespace ProductManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly int _pageSize = 10;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(
            string? keyword, 
            int? categoryId, 
            decimal? minPrice, 
            decimal? maxPrice,
            bool? isActive,
            int page = 1)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            // Filter by keyword
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.Name.Contains(keyword) || 
                                        (p.Description != null && p.Description.Contains(keyword)));
            }

            // Filter by category
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            // Filter by price range
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            // Filter by status
            if (isActive.HasValue)
            {
                query = query.Where(p => p.IsActive == isActive);
            }

            // Count total
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)_pageSize);

            // Pagination
            var products = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .ToListAsync();

            var viewModel = new ProductIndexViewModel
            {
                Products = products,
                Keyword = keyword,
                CategoryId = categoryId,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                IsActive = isActive,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = _pageSize,
                TotalItems = totalItems,
                Categories = await _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync()
            };

            return View(viewModel);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedAt = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    product.UpdatedAt = DateTime.Now;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật sản phẩm thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa sản phẩm thành công!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
```

---

## 7️⃣ BƯỚC 7: VIEWS

### Views/Products/Index.cshtml

```html
@model ProductManagement.Models.ViewModels.ProductIndexViewModel

@{
    ViewData["Title"] = "Quản lý sản phẩm";
}

<div class="d-flex justify-content-between align-items-center mb-4">
    <h1>
        <i class="bi bi-box-seam"></i> Sản phẩm
        <small class="text-muted fs-6">(@Model.TotalItems sản phẩm)</small>
    </h1>
    <a asp-action="Create" class="btn btn-primary">
        <i class="bi bi-plus-lg"></i> Thêm mới
    </a>
</div>

@if (TempData["Success"] != null)
{
    <div class="alert alert-success alert-dismissible fade show">
        <i class="bi bi-check-circle"></i> @TempData["Success"]
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>
}

<!-- Search & Filter -->
<div class="card mb-4">
    <div class="card-body">
        <form asp-action="Index" method="get">
            <div class="row g-3">
                <div class="col-md-3">
                    <label class="form-label">Từ khóa</label>
                    <input type="text" name="keyword" value="@Model.Keyword" 
                           class="form-control" placeholder="Tên sản phẩm...">
                </div>
                <div class="col-md-2">
                    <label class="form-label">Danh mục</label>
                    <select name="categoryId" class="form-select">
                        <option value="">-- Tất cả --</option>
                        @foreach (var cat in Model.Categories)
                        {
                            <option value="@cat.Value" 
                                selected="@(Model.CategoryId?.ToString() == cat.Value)">
                                @cat.Text
                            </option>
                        }
                    </select>
                </div>
                <div class="col-md-2">
                    <label class="form-label">Giá từ</label>
                    <input type="number" name="minPrice" value="@Model.MinPrice" 
                           class="form-control" placeholder="0">
                </div>
                <div class="col-md-2">
                    <label class="form-label">Đến</label>
                    <input type="number" name="maxPrice" value="@Model.MaxPrice" 
                           class="form-control" placeholder="999,999,999">
                </div>
                <div class="col-md-2">
                    <label class="form-label">Trạng thái</label>
                    <select name="isActive" class="form-select">
                        <option value="">-- Tất cả --</option>
                        <option value="true" selected="@(Model.IsActive == true)">Đang bán</option>
                        <option value="false" selected="@(Model.IsActive == false)">Ngừng bán</option>
                    </select>
                </div>
                <div class="col-md-1 d-flex align-items-end">
                    <button type="submit" class="btn btn-primary w-100">
                        <i class="bi bi-search"></i>
                    </button>
                </div>
            </div>
        </form>
    </div>
</div>

<!-- Products Table -->
<div class="card">
    <div class="table-responsive">
        <table class="table table-hover mb-0">
            <thead class="table-dark">
                <tr>
                    <th width="60">ID</th>
                    <th>Sản phẩm</th>
                    <th>Danh mục</th>
                    <th class="text-end">Giá</th>
                    <th class="text-center">SL</th>
                    <th class="text-center">Trạng thái</th>
                    <th width="180" class="text-center">Thao tác</th>
                </tr>
            </thead>
            <tbody>
                @if (Model.Products.Any())
                {
                    @foreach (var item in Model.Products)
                    {
                        <tr>
                            <td>@item.Id</td>
                            <td>
                                <strong>@item.Name</strong>
                                @if (!string.IsNullOrEmpty(item.Description))
                                {
                                    <br><small class="text-muted">@item.Description</small>
                                }
                            </td>
                            <td>
                                <span class="badge bg-secondary">@item.Category?.Name</span>
                            </td>
                            <td class="text-end">
                                <strong class="text-primary">@item.Price.ToString("N0") đ</strong>
                            </td>
                            <td class="text-center">
                                @if (item.Quantity > 0)
                                {
                                    <span class="badge bg-info">@item.Quantity</span>
                                }
                                else
                                {
                                    <span class="badge bg-danger">Hết hàng</span>
                                }
                            </td>
                            <td class="text-center">
                                @if (item.IsActive)
                                {
                                    <span class="badge bg-success">Đang bán</span>
                                }
                                else
                                {
                                    <span class="badge bg-secondary">Ngừng bán</span>
                                }
                            </td>
                            <td class="text-center">
                                <div class="btn-group btn-group-sm">
                                    <a asp-action="Details" asp-route-id="@item.Id" 
                                       class="btn btn-info" title="Chi tiết">
                                        <i class="bi bi-eye"></i>
                                    </a>
                                    <a asp-action="Edit" asp-route-id="@item.Id" 
                                       class="btn btn-warning" title="Sửa">
                                        <i class="bi bi-pencil"></i>
                                    </a>
                                    <a asp-action="Delete" asp-route-id="@item.Id" 
                                       class="btn btn-danger" title="Xóa">
                                        <i class="bi bi-trash"></i>
                                    </a>
                                </div>
                            </td>
                        </tr>
                    }
                }
                else
                {
                    <tr>
                        <td colspan="7" class="text-center py-5">
                            <i class="bi bi-inbox fs-1 text-muted"></i>
                            <p class="text-muted mt-2">Không tìm thấy sản phẩm nào</p>
                            <a asp-action="Create" class="btn btn-primary">
                                <i class="bi bi-plus"></i> Thêm sản phẩm đầu tiên
                            </a>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    </div>
</div>

<!-- Pagination -->
@if (Model.TotalPages > 1)
{
    <nav class="mt-4">
        <ul class="pagination justify-content-center">
            <li class="page-item @(Model.CurrentPage == 1 ? "disabled" : "")">
                <a class="page-link" 
                   asp-action="Index" 
                   asp-route-page="@(Model.CurrentPage - 1)"
                   asp-route-keyword="@Model.Keyword"
                   asp-route-categoryId="@Model.CategoryId"
                   asp-route-minPrice="@Model.MinPrice"
                   asp-route-maxPrice="@Model.MaxPrice"
                   asp-route-isActive="@Model.IsActive">
                    <i class="bi bi-chevron-left"></i>
                </a>
            </li>
            
            @for (int i = 1; i <= Model.TotalPages; i++)
            {
                <li class="page-item @(i == Model.CurrentPage ? "active" : "")">
                    <a class="page-link" 
                       asp-action="Index" 
                       asp-route-page="@i"
                       asp-route-keyword="@Model.Keyword"
                       asp-route-categoryId="@Model.CategoryId"
                       asp-route-minPrice="@Model.MinPrice"
                       asp-route-maxPrice="@Model.MaxPrice"
                       asp-route-isActive="@Model.IsActive">
                        @i
                    </a>
                </li>
            }
            
            <li class="page-item @(Model.CurrentPage == Model.TotalPages ? "disabled" : "")">
                <a class="page-link" 
                   asp-action="Index" 
                   asp-route-page="@(Model.CurrentPage + 1)"
                   asp-route-keyword="@Model.Keyword"
                   asp-route-categoryId="@Model.CategoryId"
                   asp-route-minPrice="@Model.MinPrice"
                   asp-route-maxPrice="@Model.MaxPrice"
                   asp-route-isActive="@Model.IsActive">
                    <i class="bi bi-chevron-right"></i>
                </a>
            </li>
        </ul>
    </nav>
}
```

### Views/Products/Create.cshtml

```html
@model ProductManagement.Models.Product

@{
    ViewData["Title"] = "Thêm sản phẩm";
}

<nav aria-label="breadcrumb" class="mb-3">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a asp-action="Index">Sản phẩm</a></li>
        <li class="breadcrumb-item active">Thêm mới</li>
    </ol>
</nav>

<div class="card">
    <div class="card-header">
        <h5 class="mb-0"><i class="bi bi-plus-circle"></i> Thêm sản phẩm mới</h5>
    </div>
    <div class="card-body">
        <form asp-action="Create" method="post">
            <div asp-validation-summary="ModelOnly" class="alert alert-danger"></div>
            
            <div class="row">
                <div class="col-md-8">
                    <div class="mb-3">
                        <label asp-for="Name" class="form-label"></label>
                        <input asp-for="Name" class="form-control" placeholder="Nhập tên sản phẩm" />
                        <span asp-validation-for="Name" class="text-danger"></span>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label asp-for="Price" class="form-label"></label>
                            <div class="input-group">
                                <input asp-for="Price" class="form-control" type="number" step="1000" />
                                <span class="input-group-text">VNĐ</span>
                            </div>
                            <span asp-validation-for="Price" class="text-danger"></span>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label asp-for="Quantity" class="form-label"></label>
                            <input asp-for="Quantity" class="form-control" type="number" />
                            <span asp-validation-for="Quantity" class="text-danger"></span>
                        </div>
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
                        <textarea asp-for="Description" class="form-control" rows="4" 
                                  placeholder="Mô tả sản phẩm..."></textarea>
                        <span asp-validation-for="Description" class="text-danger"></span>
                    </div>

                    <div class="mb-3">
                        <label asp-for="ImageUrl" class="form-label"></label>
                        <input asp-for="ImageUrl" class="form-control" placeholder="https://..." />
                    </div>

                    <div class="form-check mb-3">
                        <input asp-for="IsActive" class="form-check-input" checked />
                        <label asp-for="IsActive" class="form-check-label"></label>
                    </div>
                </div>
            </div>

            <hr />
            <div class="d-flex gap-2">
                <button type="submit" class="btn btn-primary">
                    <i class="bi bi-check-lg"></i> Lưu sản phẩm
                </button>
                <a asp-action="Index" class="btn btn-secondary">
                    <i class="bi bi-x-lg"></i> Hủy
                </a>
            </div>
        </form>
    </div>
</div>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

### Views/Shared/_Layout.cshtml (với Bootstrap Icons)

```html
<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Product Management</title>
    
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <div class="container">
            <a class="navbar-brand" asp-controller="Home" asp-action="Index">
                <i class="bi bi-shop"></i> ProductManager
            </a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav">
                    <li class="nav-item">
                        <a class="nav-link" asp-controller="Home" asp-action="Index">
                            <i class="bi bi-house"></i> Trang chủ
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" asp-controller="Categories" asp-action="Index">
                            <i class="bi bi-folder"></i> Danh mục
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" asp-controller="Products" asp-action="Index">
                            <i class="bi bi-box-seam"></i> Sản phẩm
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <main class="container my-4">
        @RenderBody()
    </main>

    <footer class="footer mt-auto py-3 bg-light">
        <div class="container text-center">
            <span class="text-muted">&copy; 2026 - Product Management System</span>
        </div>
    </footer>

    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

---

## 8️⃣ CHẠY VÀ TEST

```bash
# Chạy ứng dụng
dotnet watch run

# Mở browser tại:
# https://localhost:xxxx/Products
```

---

## ✅ CHECKLIST HOÀN THÀNH

- [ ] Tạo project MVC
- [ ] Cài đặt EF Core packages
- [ ] Tạo Models (Category, Product)
- [ ] Tạo DbContext
- [ ] Cấu hình connection string
- [ ] Migration và seed data
- [ ] Controller CRUD Products
- [ ] Controller CRUD Categories
- [ ] Views với Bootstrap
- [ ] Tìm kiếm và lọc
- [ ] Phân trang
- [ ] Validation đầy đủ
- [ ] Test tất cả chức năng

---

## 🎉 CHÚC MỪNG!

Bạn đã hoàn thành khóa học ASP.NET MVC cơ bản!

### Tiếp theo có thể học:
- Authentication & Authorization (đăng nhập, phân quyền)
- File Upload
- Web API
- Identity Framework
- Dependency Injection nâng cao
- Unit Testing

---

**Quay lại**: [Lộ trình học](./LO_TRINH_HOC_ASPNET_MVC.md)
