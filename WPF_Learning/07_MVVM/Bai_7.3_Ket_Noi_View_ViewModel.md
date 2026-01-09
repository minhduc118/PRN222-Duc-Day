# Bài 7.3: Kết Nối View và ViewModel

## 🎯 Mục Tiêu
- Các cách set DataContext
- ViewModelLocator pattern
- Dependency Injection

---

## 1. DataContext trong XAML

```xml
<Window xmlns:local="clr-namespace:MyApp.ViewModels">
    <Window.DataContext>
        <local:MainViewModel/>
    </Window.DataContext>
    
    <TextBlock Text="{Binding Title}"/>
</Window>
```

---

## 2. DataContext trong Code-behind

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
```

---

## 3. ViewModelLocator Pattern

```csharp
// ViewModelLocator.cs
public class ViewModelLocator
{
    public MainViewModel MainVM => new MainViewModel();
    public ProductViewModel ProductVM => new ProductViewModel();
}
```

```xml
<!-- App.xaml -->
<Application.Resources>
    <local:ViewModelLocator x:Key="Locator"/>
</Application.Resources>

<!-- MainWindow.xaml -->
<Window DataContext="{Binding MainVM, Source={StaticResource Locator}}">
```

---

## 4. Với Dependency Injection

```csharp
// App.xaml.cs
public partial class App : Application
{
    public static IServiceProvider Services { get; private set; }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        
        // Register services
        services.AddSingleton<IProductService, ProductService>();
        
        // Register ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<ProductViewModel>();
        
        Services = services.BuildServiceProvider();
        
        var mainWindow = new MainWindow
        {
            DataContext = Services.GetRequiredService<MainViewModel>()
        };
        mainWindow.Show();
    }
}
```

---

## 📝 Bài Tập
1. Implement ViewModelLocator cho 3 ViewModels
2. Setup Dependency Injection với Microsoft.Extensions.DependencyInjection

---

⬅️ [Bài 7.2](./Bai_7.2_Base_Classes.md) | ➡️ [Bài 7.4](./Bai_7.4_MVVM_Frameworks.md)
