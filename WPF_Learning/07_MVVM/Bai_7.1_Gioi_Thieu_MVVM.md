# Bài 7.1: Giới Thiệu MVVM

## 🎯 Mục Tiêu
- Hiểu MVVM pattern
- Cấu trúc project MVVM
- Tại sao sử dụng MVVM

---

## 1. MVVM Là Gì?

**MVVM (Model-View-ViewModel)** là pattern tách biệt UI và Business Logic.

```
┌─────────────────────────────────────────────────────┐
│                    View (XAML)                      │
│  - UI elements, layouts                             │
│  - Data Binding to ViewModel                        │
└─────────────────────────────────────────────────────┘
                         ▲
                         │ Binding
                         ▼
┌─────────────────────────────────────────────────────┐
│                   ViewModel                         │
│  - Properties (INotifyPropertyChanged)              │
│  - Commands (ICommand)                              │
│  - Business Logic                                   │
└─────────────────────────────────────────────────────┘
                         ▲
                         │ References
                         ▼
┌─────────────────────────────────────────────────────┐
│                     Model                           │
│  - Data classes (POCO)                              │
│  - Database entities                                │
└─────────────────────────────────────────────────────┘
```

---

## 2. Lợi Ích MVVM

| Lợi ích | Mô tả |
|---------|-------|
| **Testability** | ViewModel có thể unit test |
| **Separation** | UI tách biệt với logic |
| **Maintainability** | Dễ bảo trì, sửa đổi |
| **Reusability** | ViewModel có thể reuse |
| **Designer/Developer** | Làm việc song song |

---

## 3. Cấu Trúc Project

```
MyApp/
├── Models/
│   ├── Product.cs
│   └── Category.cs
├── ViewModels/
│   ├── BaseViewModel.cs
│   ├── MainViewModel.cs
│   └── ProductViewModel.cs
├── Views/
│   ├── MainWindow.xaml
│   └── ProductView.xaml
├── Commands/
│   └── RelayCommand.cs
├── Services/
│   └── ProductService.cs
└── App.xaml
```

---

## 4. Ví Dụ Đơn Giản

### Model
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

### ViewModel
```csharp
public class ProductViewModel : BaseViewModel
{
    private Product _product;
    
    public string Name
    {
        get => _product.Name;
        set { _product.Name = value; OnPropertyChanged(); }
    }
    
    public decimal Price
    {
        get => _product.Price;
        set { _product.Price = value; OnPropertyChanged(); }
    }
    
    public ICommand SaveCommand { get; }
    
    public ProductViewModel(Product product)
    {
        _product = product;
        SaveCommand = new RelayCommand(_ => Save());
    }
    
    private void Save() { /* Save to database */ }
}
```

### View
```xml
<Window DataContext="{Binding ProductVM, Source={StaticResource Locator}}">
    <StackPanel>
        <TextBox Text="{Binding Name}"/>
        <TextBox Text="{Binding Price}"/>
        <Button Content="Save" Command="{Binding SaveCommand}"/>
    </StackPanel>
</Window>
```

---

## 📝 Bài Tập
1. Tạo cấu trúc project MVVM cho ứng dụng quản lý sinh viên
2. Liệt kê ưu nhược điểm của MVVM so với code-behind

---

⬅️ [Bài 6.2](../06_Commands_Events/Bai_6.2_Commands.md) | ➡️ [Bài 7.2](./Bai_7.2_Base_Classes.md)
