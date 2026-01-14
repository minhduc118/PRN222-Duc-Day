# 📚 BÀI 2: TẠO PROJECT ASP.NET MVC ĐẦU TIÊN

> **Thời gian**: 2 giờ  
> **Mục tiêu**: Tạo project MVC, hiểu cấu trúc thư mục và chạy ứng dụng đầu tiên

---

## 1️⃣ TẠO PROJECT MVC

### Cách 1: Dùng Command Line

```bash
# Tạo project MVC mới
dotnet new mvc -n MyFirstMvc

# Di chuyển vào thư mục project
cd MyFirstMvc

# Chạy ứng dụng
dotnet run
```

### Cách 2: Dùng Visual Studio 2022

1. Mở Visual Studio → **Create a new project**
2. Tìm kiếm "ASP.NET Core Web App (Model-View-Controller)"
3. Đặt tên project: `MyFirstMvc`
4. Chọn **.NET 8.0** (hoặc phiên bản mới nhất)
5. Nhấn **Create**

---

## 2️⃣ CẤU TRÚC THƯ MỤC PROJECT

```
MyFirstMvc/
│
├── Controllers/              ← 🎮 XỬ LÝ LOGIC
│   └── HomeController.cs     ← Controller mặc định
│
├── Models/                   ← 📦 ĐỊNH NGHĨA DỮ LIỆU
│   └── ErrorViewModel.cs
│
├── Views/                    ← 🖼️ GIAO DIỆN
│   ├── Home/                 ← Views cho HomeController
│   │   ├── Index.cshtml      ← Trang chủ
│   │   └── Privacy.cshtml    ← Trang Privacy
│   └── Shared/               ← Views dùng chung
│       ├── _Layout.cshtml    ← Layout chính
│       ├── _ValidationScriptsPartial.cshtml
│       └── Error.cshtml
│   ├── _ViewImports.cshtml   ← Import namespaces
│   └── _ViewStart.cshtml     ← Cấu hình mặc định
│
├── wwwroot/                  ← 📁 STATIC FILES
│   ├── css/
│   │   └── site.css
│   ├── js/
│   │   └── site.js
│   └── lib/                  ← Thư viện (Bootstrap, jQuery)
│
├── Properties/
│   └── launchSettings.json   ← Cấu hình chạy app
│
├── appsettings.json          ← ⚙️ CẤU HÌNH ỨNG DỤNG
├── appsettings.Development.json
├── Program.cs                ← 🚀 ENTRY POINT
└── MyFirstMvc.csproj         ← Project file
```

---

## 3️⃣ GIẢI THÍCH CÁC FILE QUAN TRỌNG

### 3.1 Program.cs - Entry Point

```csharp
var builder = WebApplication.CreateBuilder(args);

// ========== ĐĂNG KÝ SERVICES ==========
// Thêm MVC services vào DI container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ========== CẤU HÌNH MIDDLEWARE PIPELINE ==========

// Xử lý lỗi trong môi trường Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Redirect HTTP -> HTTPS
app.UseHttpsRedirection();

// Cho phép serve static files (css, js, images)
app.UseStaticFiles();

// Bật routing
app.UseRouting();

// Bật authorization
app.UseAuthorization();

// ========== CẤU HÌNH ROUTE MẶC ĐỊNH ==========
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //         ↑             ↑           ↑
    //    Controller     Action      Tham số (optional)

// URL mặc định: /Home/Index
// Các ví dụ:
// /           → HomeController.Index()
// /Home       → HomeController.Index()
// /Home/Index → HomeController.Index()
// /Products   → ProductsController.Index()
// /Products/Details/5 → ProductsController.Details(5)

app.Run();
```

### 3.2 Controller - Xử lý logic

```csharp
// Controllers/HomeController.cs

using Microsoft.AspNetCore.Mvc;

namespace MyFirstMvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Index hoặc /
        public IActionResult Index()
        {
            return View(); // Trả về Views/Home/Index.cshtml
        }

        // GET: /Home/Privacy
        public IActionResult Privacy()
        {
            return View(); // Trả về Views/Home/Privacy.cshtml
        }
    }
}
```

### 3.3 View - Giao diện (Razor)

```html
<!-- Views/Home/Index.cshtml -->

@{
    ViewData["Title"] = "Trang chủ";
}

<div class="text-center">
    <h1 class="display-4">Chào mừng đến với ASP.NET MVC!</h1>
    <p>Đây là ứng dụng MVC đầu tiên của tôi.</p>
</div>
```

#### 📌 Giải thích cú pháp Razor

Khi tạo View mới (Razor View - Empty), file sẽ có nội dung mặc định:

```cshtml
@*
    For more information on enabling MVC for empty projects...
*@
@{
}
```

**1. Comment (Chú thích Razor)**

```cshtml
@* Đây là comment trong Razor *@
```

- `@*` ... `*@` = Cú pháp comment trong Razor
- Nội dung **KHÔNG được gửi** về trình duyệt (an toàn hơn `<!-- -->` của HTML)

**2. Code Block (Khối mã C#)**

```cshtml
@{
    // Viết code C# ở đây
    var message = "Hello World";
    var currentDate = DateTime.Now;
}
```

- `@{ }` = Khối code C# trong View
- Dùng để: khai báo biến, xử lý logic, gán ViewData

**3. Bảng tổng hợp cú pháp Razor**

| Cú pháp | Mô tả | Ví dụ |
|---------|-------|-------|
| `@variable` | Hiển thị giá trị | `<p>@name</p>` |
| `@{ }` | Khối code C# | `@{ var x = 10; }` |
| `@* *@` | Comment | `@* Chú thích *@` |
| `@if(){}` | Điều kiện | `@if(x > 5){ <p>Lớn</p> }` |
| `@foreach(){}` | Vòng lặp | `@foreach(var item in list){ }` |
| `@Model` | Truy cập Model | `@Model.Name` |
| `@ViewData["key"]` | Truy cập ViewData | `@ViewData["Title"]` |

**4. Ví dụ thực tế**

```cshtml
@{
    ViewData["Title"] = "Danh sách";
    var students = new List<string> { "An", "Bình", "Chi" };
}

<h1>@ViewData["Title"]</h1>
<p>Hôm nay: @DateTime.Now.ToString("dd/MM/yyyy")</p>

<ul>
@foreach(var student in students)
{
    <li>@student</li>
}
</ul>
```

> 💡 **Lưu ý**: Ký tự `@` là dấu hiệu để Razor biết đây là code C#, không phải HTML thuần.

### 3.4 Layout - Template chung

```html
<!-- Views/Shared/_Layout.cshtml -->

<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - MyFirstMvc</title>
    
    <!-- CSS -->
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" />
</head>
<body>
    <!-- Navbar -->
    <header>
        <nav class="navbar navbar-expand-lg bg-dark navbar-dark">
            <div class="container">
                <a class="navbar-brand" href="/">MyApp</a>
                <div class="navbar-nav">
                    <a class="nav-link" asp-controller="Home" asp-action="Index">Home</a>
                    <a class="nav-link" asp-controller="Home" asp-action="Privacy">Privacy</a>
                </div>
            </div>
        </nav>
    </header>

    <!-- Content - Nội dung từng trang sẽ được render ở đây -->
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <!-- Footer -->
    <footer class="footer border-top text-muted">
        <div class="container">
            &copy; 2026 - MyFirstMvc
        </div>
    </footer>

    <!-- JavaScript -->
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/js/site.js"></script>
    
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

---

## 4️⃣ LUỒNG HOẠT ĐỘNG CỦA MVC

```
┌─────────────────────────────────────────────────────────────┐
│                        USER REQUEST                          │
│                    GET /Products/Details/5                   │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                         ROUTING                              │
│           Phân tích URL → Controller: Products               │
│                         → Action: Details                    │
│                         → id: 5                              │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                       CONTROLLER                             │
│                                                              │
│   public class ProductsController : Controller               │
│   {                                                          │
│       public IActionResult Details(int id)   ← id = 5        │
│       {                                                      │
│           var product = GetProduct(id);      ← Lấy dữ liệu   │
│           return View(product);              ← Truyền Model  │
│       }                                                      │
│   }                                                          │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                          VIEW                                │
│            Views/Products/Details.cshtml                     │
│                                                              │
│   @model Product                                             │
│   <h1>@Model.Name</h1>                                       │
│   <p>Price: @Model.Price</p>                                 │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                      HTML RESPONSE                           │
│              Trả về HTML cho trình duyệt                     │
└─────────────────────────────────────────────────────────────┘
```

---

## 5️⃣ CHẠY ỨNG DỤNG

### Cách 1: Visual Studio

- Nhấn **F5** (Debug mode) hoặc **Ctrl+F5** (Without debugging)
- Trình duyệt tự động mở tại `https://localhost:xxxx`

### Cách 2: Command Line

```bash
# Chạy với hot reload
dotnet watch run

# Output:
# Now listening on: https://localhost:7xxx
# Now listening on: http://localhost:5xxx
```

### Cách 3: Xem debug output

- Mở **Debug Console** trong Visual Studio
- Xem các request và log

---

## 6️⃣ THỰC HÀNH: TẠO TRANG ABOUT

### Bước 1: Thêm Action trong Controller

```csharp
// Controllers/HomeController.cs

public IActionResult About()
{
    ViewData["Message"] = "Đây là trang giới thiệu.";
    return View();
}
```

### Bước 2: Tạo View

Tạo file `Views/Home/About.cshtml`:

```html
@{
    ViewData["Title"] = "Giới thiệu";
}

<h1>@ViewData["Title"]</h1>
<p>@ViewData["Message"]</p>

<div class="card">
    <div class="card-body">
        <h5>Thông tin sinh viên</h5>
        <p><strong>Họ tên:</strong> Nguyễn Văn A</p>
        <p><strong>MSSV:</strong> SE123456</p>
        <p><strong>Email:</strong> nguyenvana@fpt.edu.vn</p>
    </div>
</div>
```

### Bước 3: Thêm link vào Navbar

```html
<!-- Views/Shared/_Layout.cshtml -->
<div class="navbar-nav">
    <a class="nav-link" asp-controller="Home" asp-action="Index">Home</a>
    <a class="nav-link" asp-controller="Home" asp-action="About">About</a>
    <a class="nav-link" asp-controller="Home" asp-action="Privacy">Privacy</a>
</div>
```

### Bước 4: Chạy và test

Truy cập: `https://localhost:xxxx/Home/About`

---

## ✅ BÀI TẬP

### Bài 1: Thêm trang Contact

1. Thêm Action `Contact()` trong `HomeController`
2. Tạo View `Contact.cshtml` với form liên hệ (chưa cần xử lý submit)
3. Thêm link Contact vào Navbar

### Bài 2: Tạo Controller mới

1. Tạo `ProductsController.cs` với action `Index()`
2. Tạo View `Views/Products/Index.cshtml`
3. Hiển thị bảng sản phẩm (dữ liệu cứng)

---

## 📝 GHI NHỚ

| Thành phần | Vị trí | Chức năng |
|------------|--------|-----------|
| Controller | `Controllers/` | Xử lý request, gọi business logic |
| View | `Views/{Controller}/` | Hiển thị giao diện |
| Model | `Models/` | Định nghĩa cấu trúc dữ liệu |
| Static files | `wwwroot/` | CSS, JS, images |

---

**Bài tiếp theo**: [Bài 3 - Controllers và Routing](./Bai03_Controllers_Routing.md)
