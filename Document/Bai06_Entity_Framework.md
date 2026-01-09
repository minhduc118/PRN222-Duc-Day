# 📚 BÀI 6: ENTITY FRAMEWORK CORE

> **Thời gian**: 3-4 giờ  
> **Mục tiêu**: Kết nối database, tạo Migration và thực hiện CRUD với EF Core

---

## 1️⃣ ENTITY FRAMEWORK CORE LÀ GÌ?

EF Core là ORM (Object-Relational Mapping) của Microsoft, giúp:
- Làm việc với database bằng C# objects
- Không cần viết SQL thủ công
- Hỗ trợ nhiều database: SQL Server, SQLite, PostgreSQL, MySQL...

```
┌─────────────┐        ┌─────────────┐        ┌─────────────┐
│   C# Code   │ ←────► │   EF Core   │ ←────► │  Database   │
│   Objects   │        │    ORM      │        │   Tables    │
└─────────────┘        └─────────────┘        └─────────────┘
```

---

## 2️⃣ CÀI ĐẶT EF CORE

### 2.1 Cài đặt NuGet Packages

```bash
# EF Core cho SQL Server
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

# Tools để tạo migration
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Design package
dotnet add package Microsoft.EntityFrameworkCore.Design
```

Hoặc trong Visual Studio:
- Tools → NuGet Package Manager → Manage NuGet Packages
- Tìm và cài: `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools`

### 2.2 Cấu hình Connection String

```json
// appsettings.json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyMvcDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

**Các loại connection string phổ biến:**

```json
// SQL Server LocalDB (development)
"Server=(localdb)\\mssqllocaldb;Database=MyDb;Trusted_Connection=True;"

// SQL Server Express
"Server=.\\SQLEXPRESS;Database=MyDb;Trusted_Connection=True;"

// SQL Server với username/password
"Server=localhost;Database=MyDb;User Id=sa;Password=YourPassword;"

// SQLite
"Data Source=mydatabase.db"
```

---

## 3️⃣ TẠO MODELS (ENTITIES)

### 3.1 Entity Models

```csharp
// Models/Category.cs

using System.ComponentModel.DataAnnotations;

namespace MyFirstMvc.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        // Navigation property (1-N)
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
```

```csharp
// Models/Product.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstMvc.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        public int Quantity { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation property
        public virtual Category? Category { get; set; }
    }
}
```

---

## 4️⃣ TẠO DBCONTEXT

DbContext là cầu nối giữa code và database.

```csharp
// Data/ApplicationDbContext.cs

using Microsoft.EntityFrameworkCore;
using MyFirstMvc.Models;

namespace MyFirstMvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet tương ứng với các bảng trong database
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // Cấu hình thêm (optional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data (dữ liệu mẫu)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại", Description = "Các loại điện thoại" },
                new Category { Id = 2, Name = "Laptop", Description = "Máy tính xách tay" },
                new Category { Id = 3, Name = "Tablet", Description = "Máy tính bảng" }
            );

            // Cấu hình relationship (optional - EF Core tự detect)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình index
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name);
        }
    }
}
```

---

## 5️⃣ ĐĂNG KÝ DBCONTEXT

```csharp
// Program.cs

using Microsoft.EntityFrameworkCore;
using MyFirstMvc.Data;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// ... middleware pipeline
```

---

## 6️⃣ MIGRATION

Migration quản lý việc tạo/cập nhật schema database.

### 6.1 Các lệnh Migration cơ bản

```bash
# Tạo migration đầu tiên
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update

# Xem danh sách migrations
dotnet ef migrations list

# Xóa migration cuối (chưa apply)
dotnet ef migrations remove

# Rollback về migration cụ thể
dotnet ef database update <MigrationName>

# Tạo SQL script
dotnet ef migrations script
```

### 6.2 Trong Package Manager Console (Visual Studio)

```powershell
# Tạo migration
Add-Migration InitialCreate

# Cập nhật database
Update-Database

# Xóa migration cuối
Remove-Migration

# Script SQL
Script-Migration
```

### 6.3 Workflow thông thường

```bash
# 1. Tạo/sửa Models
# 2. Tạo migration
dotnet ef migrations add AddProductQuantity

# 3. Review migration file trong Migrations/
# 4. Apply migration
dotnet ef database update
```

---

## 7️⃣ CRUD VỚI EF CORE

### 7.1 Inject DbContext vào Controller

```csharp
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }
}
```

### 7.2 READ (Đọc dữ liệu)

```csharp
// Lấy tất cả
public async Task<IActionResult> Index()
{
    var products = await _context.Products.ToListAsync();
    return View(products);
}

// Lấy với điều kiện
public async Task<IActionResult> Index(string? keyword)
{
    var query = _context.Products.AsQueryable();
    
    if (!string.IsNullOrEmpty(keyword))
    {
        query = query.Where(p => p.Name.Contains(keyword));
    }
    
    var products = await query.ToListAsync();
    return View(products);
}

// Lấy kèm relationship (Include)
public async Task<IActionResult> Index()
{
    var products = await _context.Products
        .Include(p => p.Category)  // Load Category
        .ToListAsync();
    return View(products);
}

// Lấy 1 record theo ID
public async Task<IActionResult> Details(int id)
{
    var product = await _context.Products
        .Include(p => p.Category)
        .FirstOrDefaultAsync(p => p.Id == id);
        
    if (product == null)
    {
        return NotFound();
    }
    return View(product);
}

// Các query phổ biến
var count = await _context.Products.CountAsync();
var exists = await _context.Products.AnyAsync(p => p.Id == id);
var first = await _context.Products.FirstAsync();
var firstOrNull = await _context.Products.FirstOrDefaultAsync();
var single = await _context.Products.SingleAsync(p => p.Id == id);

// Sắp xếp và phân trang
var products = await _context.Products
    .OrderByDescending(p => p.CreatedAt)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();
```

### 7.3 CREATE (Thêm mới)

```csharp
// GET: Products/Create
public IActionResult Create()
{
    ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
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
    
    ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
    return View(product);
}
```

### 7.4 UPDATE (Cập nhật)

```csharp
// GET: Products/Edit/5
public async Task<IActionResult> Edit(int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
        return NotFound();
    }
    
    ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
    return View(product);
}

// POST: Products/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Product product)
{
    if (id != product.Id)
    {
        return BadRequest();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật thành công!";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(product.Id))
            {
                return NotFound();
            }
            throw;
        }
        return RedirectToAction(nameof(Index));
    }
    
    ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
    return View(product);
}

private bool ProductExists(int id)
{
    return _context.Products.Any(e => e.Id == id);
}
```

### 7.5 DELETE (Xóa)

```csharp
// GET: Products/Delete/5
public async Task<IActionResult> Delete(int id)
{
    var product = await _context.Products
        .Include(p => p.Category)
        .FirstOrDefaultAsync(m => m.Id == id);
        
    if (product == null)
    {
        return NotFound();
    }

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
        TempData["Success"] = "Xóa thành công!";
    }
    
    return RedirectToAction(nameof(Index));
}
```

---

## 8️⃣ ANTI-FORGERY TOKEN

Bảo vệ form khỏi tấn công CSRF.

```html
<!-- Trong form -->
<form asp-action="Create" method="post">
    @* Tag Helper tự động thêm anti-forgery token *@
    ...
</form>

<!-- Hoặc thủ công -->
<form method="post">
    @Html.AntiForgeryToken()
    ...
</form>
```

```csharp
// Trong Controller
[HttpPost]
[ValidateAntiForgeryToken]  // Bắt buộc có token
public async Task<IActionResult> Create(Product product)
{
    // ...
}
```

---

## 9️⃣ THỰC HÀNH: CONTROLLER HOÀN CHỈNH

```csharp
// Controllers/ProductsController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFirstMvc.Data;
using MyFirstMvc.Models;

namespace MyFirstMvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? keyword, int? categoryId)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.Name.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", categoryId);
            ViewBag.Keyword = keyword;

            var products = await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
            return View(products);
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
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
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
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
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
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
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
    }
}
```

---

## ✅ BÀI TẬP

### Bài 1: Tạo CategoriesController

Tạo CRUD đầy đủ cho Categories với EF Core

### Bài 2: Thêm phân trang

Thêm phân trang cho danh sách Products (10 sản phẩm/trang)

### Bài 3: Tìm kiếm nâng cao

Thêm tìm kiếm theo:
- Keyword (tên sản phẩm)
- Category
- Khoảng giá (min, max)
- Trạng thái (IsActive)

---

## 📝 GHI NHỚ

| Khái niệm | Mô tả |
|-----------|-------|
| DbContext | Cầu nối giữa code và database |
| DbSet | Đại diện cho table |
| Migration | Quản lý schema changes |
| `ToListAsync()` | Lấy danh sách |
| `FindAsync(id)` | Tìm theo primary key |
| `FirstOrDefaultAsync()` | Lấy 1 record |
| `Add()` | Thêm mới |
| `Update()` | Cập nhật |
| `Remove()` | Xóa |
| `SaveChangesAsync()` | Lưu thay đổi vào DB |
| `Include()` | Load relationship |

---

**Bài tiếp theo**: [Bài 7 - CRUD Hoàn Chỉnh với Database](./Bai07_CRUD_Database.md)
