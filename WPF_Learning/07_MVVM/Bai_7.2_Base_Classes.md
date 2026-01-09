# Bài 7.2: Base Classes cho MVVM

## 🎯 Mục Tiêu
- Tạo BaseViewModel
- Implement RelayCommand
- ObservableObject pattern

---

## 1. BaseViewModel

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
```

---

## 2. RelayCommand

```csharp
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
    public void Execute(object parameter) => _execute(parameter);
}

// Generic version
public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Predicate<T> _canExecute;

    public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? true;
    public void Execute(object parameter) => _execute((T)parameter);
}
```

---

## 3. Sử Dụng trong ViewModel

```csharp
public class MainViewModel : BaseViewModel
{
    private string _title;
    private ObservableCollection<Product> _products;
    private Product _selectedProduct;

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public ObservableCollection<Product> Products
    {
        get => _products;
        set => SetProperty(ref _products, value);
    }

    public Product SelectedProduct
    {
        get => _selectedProduct;
        set => SetProperty(ref _selectedProduct, value);
    }

    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }

    public MainViewModel()
    {
        Title = "Product Manager";
        Products = new ObservableCollection<Product>();
        
        AddCommand = new RelayCommand(_ => AddProduct());
        DeleteCommand = new RelayCommand<Product>(
            p => DeleteProduct(p),
            p => p != null
        );
    }

    private void AddProduct()
    {
        Products.Add(new Product { Name = "New Product" });
    }

    private void DeleteProduct(Product product)
    {
        Products.Remove(product);
    }
}
```

---

## 📝 Bài Tập
1. Tạo BaseViewModel và RelayCommand cho project của bạn
2. Tạo MainViewModel với CRUD commands

---

⬅️ [Bài 7.1](./Bai_7.1_Gioi_Thieu_MVVM.md) | ➡️ [Bài 7.3](./Bai_7.3_Ket_Noi_View_ViewModel.md)
