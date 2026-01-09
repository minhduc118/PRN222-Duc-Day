# Bài 10.1: Tích Hợp Entity Framework Core

## 🎯 Mục Tiêu
- Cài đặt EF Core
- DbContext và Models
- Migrations

---

## 1. Cài Đặt Packages

```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## 2. Tạo Models

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; }
}
```

---

## 3. DbContext

```csharp
public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(@"Server=.;Database=WPFShop;Trusted_Connection=True;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}
```

---

## 4. Migrations

```powershell
# Tạo migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Rollback
dotnet ef database update PreviousMigration
```

---

## 5. Sử Dụng trong ViewModel

```csharp
public class ProductViewModel : BaseViewModel
{
    private readonly AppDbContext _context;
    
    public ProductViewModel()
    {
        _context = new AppDbContext();
        LoadProducts();
    }
    
    private void LoadProducts()
    {
        Products = new ObservableCollection<Product>(
            _context.Products.Include(p => p.Category).ToList()
        );
    }
}
```

---

## 📝 Bài Tập
1. Tạo DbContext với 2-3 entities
2. Tạo migrations và seed data

---

⬅️ [Bài 9.2](../09_Resources_Themes/Bai_9.2_Theming.md) | ➡️ [Bài 10.2](./Bai_10.2_CRUD_WPF.md)
