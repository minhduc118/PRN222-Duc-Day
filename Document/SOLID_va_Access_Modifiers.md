# SOLID Principles và Access Modifiers trong C#

---

## Phần 1: Access Modifiers (Từ khóa truy cập)

Access Modifiers kiểm soát **phạm vi truy cập** của class, method, property, field.

### 📊 Bảng tổng hợp Access Modifiers

| Modifier | Same Class | Same Assembly | Derived Class | Everywhere |
|----------|:----------:|:-------------:|:-------------:|:----------:|
| `private` | ✅ | ❌ | ❌ | ❌ |
| `protected` | ✅ | ❌ | ✅ | ❌ |
| `internal` | ✅ | ✅ | ❌ | ❌ |
| `protected internal` | ✅ | ✅ | ✅ | ❌ |
| `public` | ✅ | ✅ | ✅ | ✅ |
| `private protected` | ✅ | ❌ | ✅ (same assembly) | ❌ |

---

### 1. `public` - Công khai hoàn toàn

```csharp
public class Author
{
    public int Id { get; set; }        // Truy cập được từ mọi nơi
    public string Name { get; set; }
}
```

**Khi nào dùng:** 
- Class, method, property cần expose ra bên ngoài
- API, Service interfaces

---

### 2. `private` - Riêng tư trong class

```csharp
public class Author
{
    private int _internalCounter;      // Chỉ dùng trong class này
    
    private void CalculateSomething()  // Method nội bộ
    {
        _internalCounter++;
    }
}
```

**Khi nào dùng:**
- Fields nội bộ
- Helper methods không cần expose

---

### 3. `protected` - Cho phép class con kế thừa

```csharp
public class BaseService
{
    protected AppDbContext _context;   // Class con có thể truy cập
    
    protected void LogAction()
    {
        Console.WriteLine("Action logged");
    }
}

public class AuthorService : BaseService
{
    public void DoSomething()
    {
        _context.Authors.ToList();     // ✅ Được phép
        LogAction();                    // ✅ Được phép
    }
}
```

---

### 4. `internal` - Chỉ trong cùng Assembly (Project)

```csharp
internal class InternalHelper      // Chỉ dùng trong project này
{
    internal void DoInternalWork()
    {
        // ...
    }
}
```

**Khi nào dùng:**
- Class/method chỉ dùng nội bộ trong project
- Không muốn expose ra cho project khác reference

---

### 5. `protected internal` - Kết hợp protected + internal

```csharp
public class BaseClass
{
    protected internal string Data;    // Truy cập từ:
                                       // - Cùng assembly, HOẶC
                                       // - Class con (kể cả khác assembly)
}
```

---

### ⚠️ Lưu ý quan trọng với WPF & EF Core

```csharp
// ❌ SAI - EF Core không thể map được
internal class Author
{
    private int Id { get; set; }    // EF Core cần public
}

// ✅ ĐÚNG - EF Core yêu cầu public
public class Author
{
    public int Id { get; set; }     // Public để EF Core mapping
    public string Name { get; set; }
}
```

---

## Phần 2: SOLID Principles

SOLID là 5 nguyên tắc thiết kế OOP giúp code **dễ maintain, mở rộng, test**.

### 📌 Tổng quan SOLID

| Chữ | Tên đầy đủ | Ý nghĩa ngắn gọn |
|:---:|------------|------------------|
| **S** | Single Responsibility | Một class chỉ làm một việc |
| **O** | Open/Closed | Mở để mở rộng, đóng để sửa đổi |
| **L** | Liskov Substitution | Class con thay thế được class cha |
| **I** | Interface Segregation | Interface nhỏ, chuyên biệt |
| **D** | Dependency Inversion | Phụ thuộc vào abstraction |

---

### S - Single Responsibility Principle (SRP)

> **Một class chỉ nên có MỘT lý do để thay đổi**

```csharp
// ❌ VI PHẠM SRP - Class làm quá nhiều việc
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public void SaveToDatabase() { }      // Trách nhiệm 1: Lưu DB
    public void SendEmail() { }           // Trách nhiệm 2: Gửi email
    public void GenerateReport() { }      // Trách nhiệm 3: Tạo báo cáo
}

// ✅ ĐÚNG SRP - Mỗi class một trách nhiệm
public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class AuthorRepository
{
    public void Save(Author author) { }   // Chỉ lo việc lưu DB
}

public class EmailService
{
    public void Send(string to) { }       // Chỉ lo việc gửi email
}
```

---

### O - Open/Closed Principle (OCP)

> **Mở để MỞ RỘNG, đóng để SỬA ĐỔI**

```csharp
// ❌ VI PHẠM OCP - Phải sửa code khi thêm loại mới
public class DiscountCalculator
{
    public decimal Calculate(string type, decimal price)
    {
        if (type == "percent")
            return price * 0.1m;
        else if (type == "fixed")
            return 10;
        // Thêm loại mới → phải sửa code ở đây
    }
}

// ✅ ĐÚNG OCP - Mở rộng bằng cách thêm class mới
public interface IDiscount
{
    decimal Calculate(decimal price);
}

public class PercentDiscount : IDiscount
{
    public decimal Calculate(decimal price) => price * 0.1m;
}

public class FixedDiscount : IDiscount
{
    public decimal Calculate(decimal price) => 10;
}

// Thêm loại mới → tạo class mới, không sửa code cũ
public class VIPDiscount : IDiscount
{
    public decimal Calculate(decimal price) => price * 0.2m;
}
```

---

### L - Liskov Substitution Principle (LSP)

> **Class con phải thay thế được class cha mà không làm hỏng chương trình**

```csharp
// ❌ VI PHẠM LSP
public class Bird
{
    public virtual void Fly() { Console.WriteLine("Flying"); }
}

public class Penguin : Bird
{
    public override void Fly() 
    { 
        throw new Exception("I can't fly!");  // Vi phạm!
    }
}

// ✅ ĐÚNG LSP - Thiết kế lại
public abstract class Bird
{
    public abstract void Move();
}

public class Sparrow : Bird
{
    public override void Move() { Console.WriteLine("Flying"); }
}

public class Penguin : Bird
{
    public override void Move() { Console.WriteLine("Swimming"); }
}
```

---

### I - Interface Segregation Principle (ISP)

> **Nhiều interface nhỏ tốt hơn một interface lớn**

```csharp
// ❌ VI PHẠM ISP - Interface quá lớn
public interface IWorker
{
    void Work();
    void Eat();
    void Sleep();
}

public class Robot : IWorker
{
    public void Work() { }
    public void Eat() { }     // Robot không ăn!
    public void Sleep() { }   // Robot không ngủ!
}

// ✅ ĐÚNG ISP - Chia nhỏ interface
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public class Human : IWorkable, IEatable
{
    public void Work() { }
    public void Eat() { }
}

public class Robot : IWorkable
{
    public void Work() { }    // Chỉ implement những gì cần
}
```

---

### D - Dependency Inversion Principle (DIP)

> **Phụ thuộc vào ABSTRACTION, không phụ thuộc vào IMPLEMENTATION**

```csharp
// ❌ VI PHẠM DIP - Phụ thuộc trực tiếp vào class cụ thể
public class MainWindow
{
    private readonly AppDbContext _context;  // Phụ thuộc cụ thể
    
    public MainWindow()
    {
        _context = new AppDbContext();       // Tạo trực tiếp
    }
}

// ✅ ĐÚNG DIP - Phụ thuộc vào Interface
public class MainWindow
{
    private readonly IAuthorService _authorService;  // Phụ thuộc abstraction
    
    public MainWindow(IAuthorService authorService)  // Inject từ bên ngoài
    {
        _authorService = authorService;
    }
}

// Sử dụng
var window = new MainWindow(new AuthorService());
```

---

## Phần 3: Áp dụng vào Project WPF

### Cấu trúc theo SOLID

```
project_2/
├── Model/                    # Entities (SRP)
│   ├── Author.cs
│   └── Book.cs
├── Service/                  # Business Logic (SRP, DIP)
│   ├── IAuthorService.cs     # Interface (ISP, DIP)
│   ├── AuthorService.cs      # Implementation
│   ├── IBookService.cs
│   └── BookService.cs
└── MainWindow.xaml.cs        # UI chỉ lo hiển thị (SRP)
```

### Ví dụ áp dụng

```csharp
// Interface - nhỏ gọn, chuyên biệt (ISP)
public interface IAuthorService
{
    List<Author> GetAllAuthors();
    Author? GetAuthorById(int id);
}

// Implementation (SRP - chỉ lo logic Author)
public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;
    
    public AuthorService(AppDbContext context)
    {
        _context = context;  // DIP - inject từ bên ngoài
    }
    
    public List<Author> GetAllAuthors()
    {
        return _context.Authors.ToList();
    }
}

// UI Layer (SRP - chỉ lo hiển thị)
public partial class MainWindow : Window
{
    private readonly IAuthorService _authorService;  // DIP
    
    public MainWindow()
    {
        _authorService = new AuthorService(new AppDbContext());
        LoadAuthors();
    }
    
    private void LoadAuthors()
    {
        cboAuthors.ItemsSource = _authorService.GetAllAuthors();
    }
}
```

---

## Tóm tắt

| Nguyên tắc | Nhớ nhanh |
|------------|-----------|
| **SRP** | Một class = Một việc |
| **OCP** | Thêm class mới, không sửa class cũ |
| **LSP** | Class con thay thế được class cha |
| **ISP** | Interface nhỏ, không ép implement thừa |
| **DIP** | Dùng Interface, inject dependency |

| Modifier | Nhớ nhanh |
|----------|-----------|
| `public` | Ai cũng thấy |
| `private` | Chỉ mình thấy |
| `protected` | Con cháu thấy |
| `internal` | Cùng project thấy |
