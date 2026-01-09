# 📚 BÀI 1: WEB CƠ BẢN CHO .NET DEVELOPER

> **Thời gian**: 2-3 giờ  
> **Mục tiêu**: Hiểu cách web hoạt động và nắm vững HTML/CSS/Bootstrap cơ bản

---

## 1️⃣ HTTP PROTOCOL - CÁCH WEB HOẠT ĐỘNG

### 1.1 Request - Response Cycle

```
┌─────────┐     HTTP Request      ┌─────────┐
│         │ ─────────────────────►│         │
│ Browser │                       │ Server  │
│         │ ◄─────────────────────│         │
└─────────┘     HTTP Response     └─────────┘
```

### 1.2 HTTP Methods quan trọng

| Method | Mục đích | Ví dụ |
|--------|----------|-------|
| **GET** | Lấy dữ liệu | Xem danh sách sản phẩm |
| **POST** | Gửi dữ liệu mới | Tạo sản phẩm mới |
| **PUT** | Cập nhật toàn bộ | Sửa thông tin sản phẩm |
| **DELETE** | Xóa dữ liệu | Xóa sản phẩm |

### 1.3 HTTP Status Codes

| Code | Ý nghĩa | Ví dụ |
|------|---------|-------|
| **200** | OK - Thành công | Lấy dữ liệu thành công |
| **201** | Created - Đã tạo | Tạo mới thành công |
| **400** | Bad Request | Dữ liệu gửi lên sai |
| **404** | Not Found | Không tìm thấy trang |
| **500** | Server Error | Lỗi server |

---

## 2️⃣ HTML5 CƠ BẢN

### 2.1 Cấu trúc cơ bản

```html
<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Tiêu đề trang</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
    <header>
        <nav>Menu điều hướng</nav>
    </header>
    
    <main>
        <h1>Tiêu đề chính</h1>
        <p>Nội dung trang</p>
    </main>
    
    <footer>
        <p>Chân trang</p>
    </footer>
    
    <script src="script.js"></script>
</body>
</html>
```

### 2.2 Các thẻ HTML quan trọng

```html
<!-- Headings -->
<h1>Heading 1 - Quan trọng nhất</h1>
<h2>Heading 2</h2>
<h3>Heading 3</h3>

<!-- Text -->
<p>Đoạn văn bản</p>
<span>Inline text</span>
<strong>In đậm</strong>
<em>In nghiêng</em>

<!-- Links & Images -->
<a href="https://google.com">Link đến Google</a>
<img src="hinh.jpg" alt="Mô tả hình">

<!-- Lists -->
<ul>
    <li>Danh sách không thứ tự</li>
</ul>
<ol>
    <li>Danh sách có thứ tự</li>
</ol>

<!-- Containers -->
<div>Block container</div>
<section>Phần nội dung</section>
<article>Bài viết</article>
```

### 2.3 Form - RẤT QUAN TRỌNG CHO MVC

```html
<form action="/Products/Create" method="post">
    <!-- Text input -->
    <label for="name">Tên sản phẩm:</label>
    <input type="text" id="name" name="Name" required>
    
    <!-- Number input -->
    <label for="price">Giá:</label>
    <input type="number" id="price" name="Price" min="0">
    
    <!-- Textarea -->
    <label for="desc">Mô tả:</label>
    <textarea id="desc" name="Description" rows="4"></textarea>
    
    <!-- Select dropdown -->
    <label for="category">Danh mục:</label>
    <select id="category" name="CategoryId">
        <option value="">-- Chọn danh mục --</option>
        <option value="1">Điện thoại</option>
        <option value="2">Laptop</option>
    </select>
    
    <!-- Checkbox -->
    <label>
        <input type="checkbox" name="IsActive" value="true">
        Đang bán
    </label>
    
    <!-- Radio buttons -->
    <label>
        <input type="radio" name="Status" value="new"> Mới
    </label>
    <label>
        <input type="radio" name="Status" value="used"> Cũ
    </label>
    
    <!-- Submit button -->
    <button type="submit">Lưu sản phẩm</button>
</form>
```

### 2.4 Table

```html
<table>
    <thead>
        <tr>
            <th>ID</th>
            <th>Tên</th>
            <th>Giá</th>
            <th>Hành động</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>1</td>
            <td>iPhone 15</td>
            <td>25,000,000đ</td>
            <td>
                <a href="/edit/1">Sửa</a>
                <a href="/delete/1">Xóa</a>
            </td>
        </tr>
    </tbody>
</table>
```

---

## 3️⃣ CSS CƠ BẢN

### 3.1 Cách thêm CSS

```html
<!-- 1. Inline (tránh dùng) -->
<p style="color: red;">Text màu đỏ</p>

<!-- 2. Internal -->
<style>
    p { color: red; }
</style>

<!-- 3. External (khuyên dùng) -->
<link rel="stylesheet" href="style.css">
```

### 3.2 CSS Selectors

```css
/* Element selector */
p { color: blue; }

/* Class selector */
.btn { padding: 10px; }

/* ID selector */
#header { background: #333; }

/* Descendant selector */
.container p { margin: 10px; }

/* Multiple classes */
.btn.btn-primary { background: blue; }
```

### 3.3 Box Model

```css
.box {
    /* Content */
    width: 300px;
    height: 200px;
    
    /* Padding - khoảng cách bên trong */
    padding: 20px;           /* Tất cả các hướng */
    padding: 10px 20px;      /* top-bottom, left-right */
    padding: 10px 20px 15px 25px; /* top, right, bottom, left */
    
    /* Border */
    border: 1px solid #ccc;
    border-radius: 8px;      /* Bo góc */
    
    /* Margin - khoảng cách bên ngoài */
    margin: 20px;
    margin: 0 auto;          /* Căn giữa */
    
    /* Background */
    background-color: #f5f5f5;
}
```

### 3.4 Flexbox - Layout phổ biến nhất

```css
.container {
    display: flex;
    
    /* Hướng sắp xếp */
    flex-direction: row;        /* Hàng ngang (mặc định) */
    flex-direction: column;     /* Hàng dọc */
    
    /* Căn chỉnh theo trục chính */
    justify-content: flex-start;   /* Đầu */
    justify-content: center;       /* Giữa */
    justify-content: flex-end;     /* Cuối */
    justify-content: space-between;/* Đều nhau */
    
    /* Căn chỉnh theo trục phụ */
    align-items: flex-start;
    align-items: center;
    align-items: flex-end;
    
    /* Cho phép xuống dòng */
    flex-wrap: wrap;
    
    /* Khoảng cách giữa items */
    gap: 20px;
}

.item {
    flex: 1;        /* Chiếm đều không gian */
    flex: 0 0 25%;  /* Chiếm 25% */
}
```

---

## 4️⃣ BOOTSTRAP 5

### 4.1 Cài đặt Bootstrap

```html
<!DOCTYPE html>
<html>
<head>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <!-- Nội dung -->
    
    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
```

### 4.2 Grid System (12 cột)

```html
<div class="container">
    <div class="row">
        <div class="col-12">        <!-- Full width -->
        <div class="col-6">         <!-- 50% width -->
        <div class="col-4">         <!-- 33% width -->
        <div class="col-3">         <!-- 25% width -->
    </div>
</div>

<!-- Responsive -->
<div class="container">
    <div class="row">
        <!-- Mobile: full, Tablet: 50%, Desktop: 33% -->
        <div class="col-12 col-md-6 col-lg-4">
            Card 1
        </div>
        <div class="col-12 col-md-6 col-lg-4">
            Card 2
        </div>
        <div class="col-12 col-md-6 col-lg-4">
            Card 3
        </div>
    </div>
</div>
```

### 4.3 Components thường dùng

```html
<!-- Buttons -->
<button class="btn btn-primary">Primary</button>
<button class="btn btn-success">Success</button>
<button class="btn btn-danger">Danger</button>
<button class="btn btn-outline-primary">Outline</button>

<!-- Form -->
<form>
    <div class="mb-3">
        <label class="form-label">Tên:</label>
        <input type="text" class="form-control" placeholder="Nhập tên">
    </div>
    <div class="mb-3">
        <label class="form-label">Email:</label>
        <input type="email" class="form-control">
    </div>
    <div class="mb-3">
        <label class="form-label">Danh mục:</label>
        <select class="form-select">
            <option>Chọn...</option>
        </select>
    </div>
    <button class="btn btn-primary">Submit</button>
</form>

<!-- Table -->
<table class="table table-striped table-hover">
    <thead class="table-dark">
        <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>1</td>
            <td>Product A</td>
            <td>
                <a class="btn btn-sm btn-warning">Sửa</a>
                <a class="btn btn-sm btn-danger">Xóa</a>
            </td>
        </tr>
    </tbody>
</table>

<!-- Card -->
<div class="card" style="width: 18rem;">
    <img src="product.jpg" class="card-img-top" alt="...">
    <div class="card-body">
        <h5 class="card-title">Tên sản phẩm</h5>
        <p class="card-text">Mô tả ngắn</p>
        <a href="#" class="btn btn-primary">Chi tiết</a>
    </div>
</div>

<!-- Alert -->
<div class="alert alert-success">Thành công!</div>
<div class="alert alert-danger">Có lỗi xảy ra!</div>

<!-- Navbar -->
<nav class="navbar navbar-expand-lg bg-dark navbar-dark">
    <div class="container">
        <a class="navbar-brand" href="#">MyApp</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a class="nav-link active" href="#">Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Products</a>
                </li>
            </ul>
        </div>
    </div>
</nav>
```

### 4.4 Spacing Utilities

```html
<!-- Margin -->
<div class="m-3">   <!-- margin all sides -->
<div class="mt-3">  <!-- margin-top -->
<div class="mb-3">  <!-- margin-bottom -->
<div class="ms-3">  <!-- margin-start (left) -->
<div class="me-3">  <!-- margin-end (right) -->
<div class="mx-3">  <!-- margin left & right -->
<div class="my-3">  <!-- margin top & bottom -->

<!-- Padding - tương tự, thay m thành p -->
<div class="p-3">   <!-- padding all sides -->
<div class="py-3">  <!-- padding top & bottom -->

<!-- Size: 0, 1, 2, 3, 4, 5, auto -->
```

---

## ✅ BÀI TẬP THỰC HÀNH

### Bài 1: Tạo trang danh sách sản phẩm

Tạo file `index.html` với:
- Navbar với logo và menu
- Bảng hiển thị danh sách sản phẩm (5-10 sản phẩm giả)
- Mỗi dòng có nút Sửa, Xóa
- Nút "Thêm sản phẩm" ở trên bảng

### Bài 2: Tạo form thêm sản phẩm

Tạo file `create.html` với form gồm:
- Input: Tên sản phẩm (text)
- Input: Giá (number)
- Textarea: Mô tả
- Select: Danh mục
- Checkbox: Đang bán
- Nút Submit

---

## 📝 KIẾN THỨC CẦN NHỚ

> ⭐ **Quan trọng cho MVC:**
> - Form với `action` và `method`
> - Input với thuộc tính `name` (mapping với Model)
> - Table để hiển thị danh sách
> - Bootstrap classes cho UI đẹp

---

**Bài tiếp theo**: [Bài 2 - Tạo project ASP.NET MVC đầu tiên](./Bai02_Tao_Project_MVC.md)
