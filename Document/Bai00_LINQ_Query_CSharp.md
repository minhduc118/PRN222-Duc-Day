# Bài 0: Ôn Tập LINQ và Query trong C#

> 📚 Tài liệu ôn tập kiến thức nền tảng về LINQ trước khi học ASP.NET MVC

---

## 1. LINQ là gì?

**LINQ (Language Integrated Query)** là tính năng cho phép truy vấn dữ liệu trực tiếp trong C# với cú pháp giống SQL.

### Lợi ích của LINQ:
- ✅ Viết query ngay trong C#, không cần học thêm ngôn ngữ
- ✅ IntelliSense hỗ trợ, dễ debug
- ✅ Hoạt động với nhiều nguồn dữ liệu: Collections, Database, XML, JSON

---

## 2. Hai cách viết LINQ

### 2.1. Query Syntax (Cú pháp truy vấn - giống SQL)

```csharp
var result = from item in collection
             where item.Price > 100
             orderby item.Name
             select item;
```

### 2.2. Method Syntax (Cú pháp phương thức - phổ biến hơn)

```csharp
var result = collection
    .Where(item => item.Price > 100)
    .OrderBy(item => item.Name);
```

> 💡 **Khuyến nghị**: Sử dụng **Method Syntax** vì linh hoạt và phổ biến hơn trong thực tế.

---

## 3. Dữ liệu mẫu

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }
}

// Dữ liệu mẫu
List<Product> products = new List<Product>
{
    new Product { Id = 1, Name = "iPhone 15", Price = 999, Category = "Phone", Stock = 50 },
    new Product { Id = 2, Name = "Samsung S24", Price = 899, Category = "Phone", Stock = 30 },
    new Product { Id = 3, Name = "MacBook Pro", Price = 1999, Category = "Laptop", Stock = 20 },
    new Product { Id = 4, Name = "Dell XPS", Price = 1499, Category = "Laptop", Stock = 15 },
    new Product { Id = 5, Name = "AirPods Pro", Price = 249, Category = "Accessory", Stock = 100 },
    new Product { Id = 6, Name = "iPad Pro", Price = 1099, Category = "Tablet", Stock = 25 }
};
```

---

## 4. Các phương thức LINQ phổ biến

### 4.1. Where - Lọc dữ liệu

```csharp
// Lọc sản phẩm có giá > 500
var expensive = products.Where(p => p.Price > 500);

// Lọc sản phẩm category = "Phone"
var phones = products.Where(p => p.Category == "Phone");

// Lọc nhiều điều kiện
var result = products.Where(p => p.Price > 500 && p.Stock > 20);
```

**Query Syntax:**
```csharp
var expensive = from p in products
                where p.Price > 500
                select p;
```

---

### 4.2. Select - Chọn/Chuyển đổi dữ liệu

```csharp
// Lấy danh sách tên sản phẩm
var names = products.Select(p => p.Name);
// Kết quả: ["iPhone 15", "Samsung S24", ...]

// Tạo object mới (Anonymous Type)
var productInfo = products.Select(p => new 
{
    p.Name,
    p.Price,
    PriceVND = p.Price * 25000
});

// Tạo DTO object
var dtos = products.Select(p => new ProductDTO 
{
    ProductName = p.Name,
    DisplayPrice = $"{p.Price:C}"
});
```

---

### 4.3. OrderBy / OrderByDescending - Sắp xếp

```csharp
// Sắp xếp theo giá tăng dần
var sortedAsc = products.OrderBy(p => p.Price);

// Sắp xếp theo giá giảm dần
var sortedDesc = products.OrderByDescending(p => p.Price);

// Sắp xếp theo nhiều tiêu chí
var sorted = products
    .OrderBy(p => p.Category)
    .ThenByDescending(p => p.Price);
```

**Query Syntax:**
```csharp
var sorted = from p in products
             orderby p.Category, p.Price descending
             select p;
```

---

### 4.4. First / FirstOrDefault - Lấy phần tử đầu tiên

```csharp
// Lấy sản phẩm đầu tiên (throw exception nếu rỗng)
var first = products.First();

// Lấy sản phẩm đầu tiên thỏa điều kiện
var firstPhone = products.First(p => p.Category == "Phone");

// An toàn hơn: trả về null nếu không tìm thấy
var phone = products.FirstOrDefault(p => p.Category == "Phone");

// Kiểm tra null
if (phone != null)
{
    Console.WriteLine(phone.Name);
}
```

> ⚠️ **Lưu ý**: Dùng `FirstOrDefault` thay vì `First` để tránh exception khi không có dữ liệu.

---

### 4.5. Single / SingleOrDefault - Lấy duy nhất 1 phần tử

```csharp
// Lấy sản phẩm có Id = 3 (throw exception nếu không có hoặc có nhiều hơn 1)
var product = products.Single(p => p.Id == 3);

// An toàn hơn
var product = products.SingleOrDefault(p => p.Id == 3);
```

| Phương thức | Không tìm thấy | Nhiều hơn 1 |
|-------------|----------------|-------------|
| `Single` | Exception | Exception |
| `SingleOrDefault` | null | Exception |
| `First` | Exception | Trả về đầu tiên |
| `FirstOrDefault` | null | Trả về đầu tiên |

---

### 4.6. Count / Sum / Average / Min / Max - Tổng hợp

```csharp
// Đếm số sản phẩm
int count = products.Count();
int phoneCount = products.Count(p => p.Category == "Phone");

// Tổng giá trị
decimal totalPrice = products.Sum(p => p.Price);

// Trung bình
decimal avgPrice = products.Average(p => p.Price);

// Min / Max
decimal minPrice = products.Min(p => p.Price);
decimal maxPrice = products.Max(p => p.Price);

// Tổng giá trị tồn kho
decimal totalValue = products.Sum(p => p.Price * p.Stock);
```

---

### 4.7. Any / All - Kiểm tra điều kiện

```csharp
// Kiểm tra có sản phẩm nào giá > 1000 không?
bool hasExpensive = products.Any(p => p.Price > 1000);
// true

// Kiểm tra tất cả sản phẩm có giá > 100 không?
bool allOver100 = products.All(p => p.Price > 100);
// true

// Kiểm tra danh sách có rỗng không?
bool isEmpty = !products.Any();
```

---

### 4.8. Take / Skip - Phân trang

```csharp
// Lấy 3 sản phẩm đầu tiên
var top3 = products.Take(3);

// Bỏ qua 2 sản phẩm đầu, lấy phần còn lại
var skipFirst2 = products.Skip(2);

// PHÂN TRANG: Trang 2, mỗi trang 3 sản phẩm
int pageNumber = 2;
int pageSize = 3;

var page2 = products
    .Skip((pageNumber - 1) * pageSize)  // Bỏ qua 3 sản phẩm đầu
    .Take(pageSize);                     // Lấy 3 sản phẩm tiếp theo
```

---

### 4.9. GroupBy - Nhóm dữ liệu

```csharp
// Nhóm sản phẩm theo Category
var groups = products.GroupBy(p => p.Category);

foreach (var group in groups)
{
    Console.WriteLine($"Category: {group.Key}");
    foreach (var product in group)
    {
        Console.WriteLine($"  - {product.Name}: {product.Price}");
    }
}

// Kết quả:
// Category: Phone
//   - iPhone 15: 999
//   - Samsung S24: 899
// Category: Laptop
//   - MacBook Pro: 1999
//   - Dell XPS: 1499
// ...

// Thống kê theo nhóm
var stats = products
    .GroupBy(p => p.Category)
    .Select(g => new 
    {
        Category = g.Key,
        Count = g.Count(),
        TotalValue = g.Sum(p => p.Price),
        AvgPrice = g.Average(p => p.Price)
    });
```

**Query Syntax:**
```csharp
var groups = from p in products
             group p by p.Category into g
             select new 
             {
                 Category = g.Key,
                 Products = g.ToList()
             };
```

---

### 4.10. Join - Nối 2 tập dữ liệu

```csharp
// Dữ liệu Category
List<Category> categories = new List<Category>
{
    new Category { Id = 1, Name = "Phone", Description = "Điện thoại" },
    new Category { Id = 2, Name = "Laptop", Description = "Máy tính xách tay" }
};

// Join Product với Category
var result = products.Join(
    categories,
    p => p.Category,          // Key từ Product
    c => c.Name,              // Key từ Category
    (p, c) => new             // Kết quả
    {
        p.Name,
        p.Price,
        CategoryDescription = c.Description
    }
);
```

**Query Syntax:**
```csharp
var result = from p in products
             join c in categories on p.Category equals c.Name
             select new 
             {
                 p.Name,
                 p.Price,
                 CategoryDescription = c.Description
             };
```

---

### 4.11. Distinct - Loại bỏ trùng lặp

```csharp
// Lấy danh sách Category không trùng
var categories = products.Select(p => p.Category).Distinct();
// ["Phone", "Laptop", "Accessory", "Tablet"]
```

---

### 4.12. ToList / ToArray / ToDictionary - Chuyển đổi

```csharp
// Chuyển thành List
List<Product> list = products.Where(p => p.Price > 500).ToList();

// Chuyển thành Array
Product[] array = products.Where(p => p.Price > 500).ToArray();

// Chuyển thành Dictionary (key = Id)
Dictionary<int, Product> dict = products.ToDictionary(p => p.Id);

// Truy cập theo key
Product product = dict[3]; // Lấy product có Id = 3
```

---

## 5. Kết hợp nhiều phương thức (Method Chaining)

```csharp
// Lấy top 5 sản phẩm Phone đắt nhất
var top5Phones = products
    .Where(p => p.Category == "Phone")     // 1. Lọc Phone
    .OrderByDescending(p => p.Price)       // 2. Sắp xếp giá giảm dần
    .Take(5)                               // 3. Lấy 5 cái đầu
    .Select(p => new                       // 4. Chọn thông tin cần thiết
    {
        p.Name,
        p.Price,
        PriceVND = p.Price * 25000
    })
    .ToList();                             // 5. Chuyển thành List
```

---

## 6. Bài tập thực hành

### Bài 1: Lọc và sắp xếp
Lấy danh sách sản phẩm có Stock > 20, sắp xếp theo tên.

<details>
<summary>💡 Đáp án</summary>

```csharp
var result = products
    .Where(p => p.Stock > 20)
    .OrderBy(p => p.Name)
    .ToList();
```
</details>

---

### Bài 2: Thống kê
Tính tổng giá trị tồn kho của tất cả sản phẩm (Price × Stock).

<details>
<summary>💡 Đáp án</summary>

```csharp
decimal totalInventory = products.Sum(p => p.Price * p.Stock);
```
</details>

---

### Bài 3: Nhóm và thống kê
Đếm số sản phẩm trong mỗi Category.

<details>
<summary>💡 Đáp án</summary>

```csharp
var stats = products
    .GroupBy(p => p.Category)
    .Select(g => new 
    {
        Category = g.Key,
        Count = g.Count()
    })
    .ToList();
```
</details>

---

### Bài 4: Tìm kiếm
Tìm sản phẩm có tên chứa "Pro" (không phân biệt hoa thường).

<details>
<summary>💡 Đáp án</summary>

```csharp
var result = products
    .Where(p => p.Name.ToLower().Contains("pro"))
    .ToList();
```
</details>

---

### Bài 5: Phân trang
Thực hiện phân trang: Lấy sản phẩm trang 3, mỗi trang 2 sản phẩm.

<details>
<summary>💡 Đáp án</summary>

```csharp
int page = 3;
int pageSize = 2;

var result = products
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToList();
```
</details>

---

## 7. LINQ trong ASP.NET MVC

### Ứng dụng trong Controller:

```csharp
public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    // Lấy tất cả sản phẩm
    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }

    // Tìm kiếm
    public IActionResult Search(string keyword)
    {
        var products = _context.Products
            .Where(p => p.Name.Contains(keyword))
            .ToList();
        return View("Index", products);
    }

    // Chi tiết sản phẩm
    public IActionResult Details(int id)
    {
        var product = _context.Products
            .FirstOrDefault(p => p.Id == id);
        
        if (product == null)
            return NotFound();
        
        return View(product);
    }

    // Phân trang
    public IActionResult Index(int page = 1)
    {
        int pageSize = 10;
        
        var products = _context.Products
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        return View(products);
    }
}
```

---

## 8. Tổng kết

| Phương thức | Chức năng | Ví dụ |
|-------------|-----------|-------|
| `Where` | Lọc | `.Where(x => x.Price > 100)` |
| `Select` | Chọn/Chuyển đổi | `.Select(x => x.Name)` |
| `OrderBy` | Sắp xếp tăng | `.OrderBy(x => x.Price)` |
| `OrderByDescending` | Sắp xếp giảm | `.OrderByDescending(x => x.Price)` |
| `First/FirstOrDefault` | Lấy đầu tiên | `.FirstOrDefault(x => x.Id == 1)` |
| `Single/SingleOrDefault` | Lấy duy nhất | `.SingleOrDefault(x => x.Id == 1)` |
| `Count` | Đếm | `.Count()` |
| `Sum/Average/Min/Max` | Tổng hợp | `.Sum(x => x.Price)` |
| `Any/All` | Kiểm tra | `.Any(x => x.Stock > 0)` |
| `Take/Skip` | Phân trang | `.Skip(10).Take(5)` |
| `GroupBy` | Nhóm | `.GroupBy(x => x.Category)` |
| `Join` | Nối | `.Join(...)` |
| `ToList/ToArray` | Chuyển đổi | `.ToList()` |

---

📌 **Tiếp theo**: [Bài 01 - Web Cơ Bản](./Bai01_Web_Co_Ban.md)
