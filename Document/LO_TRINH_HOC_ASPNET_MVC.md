# 🎯 LỘ TRÌNH HỌC ASP.NET MVC CHI TIẾT

> **Mục tiêu**: Từ người mới bắt đầu đến thành thạo ASP.NET Core MVC  
> **Thời gian dự kiến**: 8-12 tuần (3-4 giờ/ngày)  
> **Phiên bản**: .NET 8 (LTS)

---

## 📋 MỤC LỤC

1. [Giai đoạn 1: Nền tảng C# và .NET](#giai-đoạn-1-nền-tảng-c-và-net-2-tuần)
2. [Giai đoạn 2: Web cơ bản](#giai-đoạn-2-web-cơ-bản-1-tuần)
3. [Giai đoạn 3: ASP.NET MVC Cơ bản](#giai-đoạn-3-aspnet-mvc-cơ-bản-2-tuần)
4. [Giai đoạn 4: Database và Entity Framework](#giai-đoạn-4-database-và-entity-framework-2-tuần)
5. [Giai đoạn 5: ASP.NET MVC Nâng cao](#giai-đoạn-5-aspnet-mvc-nâng-cao-2-tuần)
6. [Giai đoạn 6: Dự án thực hành](#giai-đoạn-6-dự-án-thực-hành-2-tuần)
7. [Tài nguyên học tập](#tài-nguyên-học-tập)

---

## 📚 GIAI ĐOẠN 1: NỀN TẢNG C# VÀ .NET (2 tuần)

> ⚠️ **Bắt buộc** - Nếu bạn đã biết C#, có thể bỏ qua giai đoạn này

### Tuần 1: C# Cơ bản

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | Cài đặt môi trường | Visual Studio 2022, .NET 8 SDK | 2 giờ |
| 2 | Cú pháp cơ bản | Variables, Data types, Operators | 4 giờ |
| 3 | Điều khiển luồng | if/else, switch, loops (for, while, foreach) | 4 giờ |
| 4 | Mảng và Collections | Array, List, Dictionary, LINQ cơ bản | 6 giờ |
| 5 | Methods và Functions | Parameters, return types, overloading | 4 giờ |

### Tuần 2: C# OOP (Lập trình hướng đối tượng)

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | Classes & Objects | Properties, Fields, Constructors | 4 giờ |
| 2 | Inheritance | Kế thừa, base class, override | 4 giờ |
| 3 | Interfaces | Interface, Abstract class | 4 giờ |
| 4 | Encapsulation | Access modifiers (public, private, protected) | 3 giờ |
| 5 | Exception Handling | try-catch-finally, custom exceptions | 3 giờ |
| 6 | Async/Await | async, await, Task | 4 giờ |

### ✅ Bài tập kiểm tra:
- [ ] Tạo ứng dụng Console quản lý sinh viên (CRUD)
- [ ] Sử dụng OOP: Class Student, Class StudentManager
- [ ] Implement exception handling

---

## 🌐 GIAI ĐOẠN 2: WEB CƠ BẢN (1 tuần)

> Hiểu cách web hoạt động trước khi học framework

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | HTML5 | Cấu trúc trang web, form, table | 4 giờ |
| 2 | CSS3 | Selectors, Box model, Flexbox, Grid | 6 giờ |
| 3 | Bootstrap 5 | Grid system, Components, Utilities | 4 giờ |
| 4 | JavaScript cơ bản | DOM manipulation, Events, Fetch API | 6 giờ |
| 5 | HTTP Protocol | Request/Response, Methods (GET, POST), Status codes | 2 giờ |

### ✅ Bài tập kiểm tra:
- [ ] Tạo trang web tĩnh giới thiệu bản thân với HTML/CSS/Bootstrap
- [ ] Thêm JavaScript để validate form

---

## 🏗️ GIAI ĐOẠN 3: ASP.NET MVC CƠ BẢN (2 tuần)

### Tuần 1: Cấu trúc MVC

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | **Tạo project MVC** | `dotnet new mvc`, cấu trúc thư mục | 2 giờ |
| 2 | **Controllers** | Tạo controller, action methods, routing | 4 giờ |
| 3 | **Views** | Razor syntax, Layout, Partial views | 6 giờ |
| 4 | **Models** | Model binding, ViewModel | 4 giờ |
| 5 | **Routing** | Attribute routing, Conventional routing | 4 giờ |

### Cấu trúc chuẩn MVC cần nhớ:

```
MyMvcProject/
├── Controllers/          ← Xử lý logic, nhận request
│   ├── HomeController.cs
│   └── ProductController.cs
├── Models/               ← Định nghĩa dữ liệu
│   ├── Product.cs
│   └── ViewModels/
├── Views/                ← Giao diện hiển thị
│   ├── Home/
│   │   ├── Index.cshtml
│   │   └── About.cshtml
│   ├── Product/
│   └── Shared/
│       ├── _Layout.cshtml
│       └── _ValidationScriptsPartial.cshtml
├── wwwroot/              ← Static files (CSS, JS, images)
├── Program.cs            ← Entry point
└── appsettings.json      ← Cấu hình
```

### Tuần 2: Luồng dữ liệu và Forms

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | **Razor Syntax** | @, @{}, @Html helpers, @Url helpers | 4 giờ |
| 2 | **Tag Helpers** | asp-for, asp-action, asp-controller | 4 giờ |
| 3 | **Form Handling** | GET/POST, Model binding, [HttpPost] | 6 giờ |
| 4 | **Validation** | Data Annotations, Client-side, Server-side | 4 giờ |
| 5 | **ViewBag/ViewData/TempData** | Truyền dữ liệu Controller → View | 4 giờ |

### ✅ Bài tập kiểm tra:
- [ ] Tạo trang CRUD sản phẩm (dữ liệu giả - không database)
- [ ] Validate form nhập sản phẩm
- [ ] Sử dụng Layout và Partial View

---

## 🗄️ GIAI ĐOẠN 4: DATABASE VÀ ENTITY FRAMEWORK (2 tuần)

### Tuần 1: Entity Framework Core cơ bản

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | **EF Core Overview** | ORM là gì, DbContext, DbSet | 2 giờ |
| 2 | **Code First** | Tạo Models → Migration → Database | 6 giờ |
| 3 | **Database First** | Scaffold từ database có sẵn | 4 giờ |
| 4 | **CRUD Operations** | Add, Find, Update, Remove, SaveChanges | 6 giờ |
| 5 | **LINQ Queries** | Where, Select, OrderBy, Include | 4 giờ |

### Các lệnh Migration quan trọng:

```bash
# Tạo migration mới
dotnet ef migrations add <TenMigration>

# Cập nhật database
dotnet ef database update

# Xóa migration cuối
dotnet ef migrations remove

# Scaffold từ database có sẵn
dotnet ef dbcontext scaffold "ConnectionString" Microsoft.EntityFrameworkCore.SqlServer
```

### Tuần 2: Entity Framework Core nâng cao

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | **Relationships** | One-to-One, One-to-Many, Many-to-Many | 6 giờ |
| 2 | **Fluent API** | Cấu hình relationship, constraints | 4 giờ |
| 3 | **Eager/Lazy Loading** | Include(), ThenInclude() | 4 giờ |
| 4 | **Repository Pattern** | Tách biệt data access logic | 4 giờ |
| 5 | **Unit of Work** | Quản lý transaction | 4 giờ |

### ✅ Bài tập kiểm tra:
- [ ] Tạo database với EF Core (Code First)
- [ ] CRUD hoàn chỉnh với database thật
- [ ] Implement Repository Pattern

---

## 🚀 GIAI ĐOẠN 5: ASP.NET MVC NÂNG CAO (2 tuần)

### Tuần 1: Authentication & Authorization

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | **Identity Framework** | Cài đặt, cấu hình ASP.NET Identity | 4 giờ |
| 2 | **Authentication** | Login, Logout, Register | 6 giờ |
| 3 | **Authorization** | [Authorize], Roles, Policies | 4 giờ |
| 4 | **Claims & Cookie** | Claim-based authentication | 4 giờ |
| 5 | **External Login** | Google, Facebook OAuth | 4 giờ |

### Tuần 2: Kiến thức nâng cao

| STT | Chủ đề | Nội dung chi tiết | Thời gian |
|-----|--------|-------------------|-----------|
| 1 | **Dependency Injection** | Service lifetime (Transient, Scoped, Singleton) | 4 giờ |
| 2 | **Middleware** | Custom middleware, Request pipeline | 4 giờ |
| 3 | **Filters** | Action filters, Exception filters | 4 giờ |
| 4 | **Session & Caching** | Session state, Memory cache | 4 giờ |
| 5 | **File Upload** | Upload, validate, save files | 3 giờ |
| 6 | **Logging** | ILogger, Serilog | 3 giờ |

### ✅ Bài tập kiểm tra:
- [ ] Thêm hệ thống đăng nhập/đăng ký
- [ ] Phân quyền: Admin, User
- [ ] Implement custom middleware

---

## 🎓 GIAI ĐOẠN 6: DỰ ÁN THỰC HÀNH (2 tuần)

### Dự án đề xuất: **Hệ thống quản lý bán hàng**

#### Yêu cầu chức năng:

| Module | Chức năng |
|--------|-----------|
| **Quản lý User** | Đăng ký, đăng nhập, phân quyền |
| **Quản lý Sản phẩm** | CRUD sản phẩm, danh mục, hình ảnh |
| **Quản lý Đơn hàng** | Tạo đơn, xem lịch sử, thay đổi trạng thái |
| **Giỏ hàng** | Thêm/xóa sản phẩm, tính tổng tiền |
| **Báo cáo** | Thống kê doanh thu, sản phẩm bán chạy |

#### Yêu cầu kỹ thuật:

- [ ] Kiến trúc 3-Layer (Presentation, Business Logic, Data Access)
- [ ] Entity Framework Core với SQL Server
- [ ] ASP.NET Identity cho Authentication
- [ ] Repository + Unit of Work Pattern
- [ ] Validation cả client-side và server-side
- [ ] Responsive design với Bootstrap

---

## 📖 TÀI NGUYÊN HỌC TẬP

### Video Courses (Miễn phí)

| Nguồn | Link | Ghi chú |
|-------|------|---------|
| **Microsoft Learn** | [learn.microsoft.com](https://learn.microsoft.com/en-us/aspnet/core/mvc/) | Tài liệu chính thức |
| **YouTube - Les Jackson** | [.NET Core MVC Tutorial](https://youtube.com) | Tiếng Anh, rất chi tiết |
| **YouTube - F8** | [Học lập trình web](https://youtube.com) | Tiếng Việt, cơ bản |

### Documentation

| Tài liệu | Link |
|----------|------|
| ASP.NET Core Docs | https://docs.microsoft.com/aspnet/core |
| Entity Framework Core | https://docs.microsoft.com/ef/core |
| C# Language Reference | https://docs.microsoft.com/dotnet/csharp |

### Tools cần thiết

| Tool | Mục đích |
|------|----------|
| **Visual Studio 2022** | IDE chính |
| **SQL Server Express** | Database |
| **SQL Server Management Studio** | Quản lý database |
| **Postman** | Test API |
| **Git** | Version control |

---

## 📊 CHECKLIST TỔNG HỢP

### Kiến thức cần đạt được:

- [ ] Hiểu pattern MVC (Model-View-Controller)
- [ ] Tạo Controllers và định nghĩa Action methods
- [ ] Sử dụng Razor Views với Tag Helpers
- [ ] Model Binding và Data Validation
- [ ] Entity Framework Core CRUD operations
- [ ] Relationships trong database
- [ ] Authentication với ASP.NET Identity
- [ ] Authorization với Roles/Policies
- [ ] Dependency Injection
- [ ] Repository Pattern
- [ ] Hoàn thành 1 dự án thực tế

---

## 💡 MẸO HỌC HIỆU QUẢ

1. **Code Along** - Không chỉ xem, phải gõ code theo
2. **Debug nhiều** - Đặt breakpoint, xem luồng dữ liệu
3. **Đọc lỗi** - Học cách đọc và hiểu error messages
4. **Làm project** - Áp dụng ngay những gì học được
5. **Google là bạn** - Học cách search hiệu quả
6. **Tham gia cộng đồng** - Stack Overflow, Reddit, Discord

---

> 📝 **Ghi chú**: Lộ trình này có thể điều chỉnh tùy theo tốc độ học của bạn. Quan trọng nhất là **THỰC HÀNH** nhiều!

---

*Tạo bởi: AI Assistant*  
*Cập nhật: Tháng 1/2026*
