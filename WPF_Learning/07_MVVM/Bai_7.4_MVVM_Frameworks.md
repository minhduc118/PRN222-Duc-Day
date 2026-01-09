# Bài 7.4: MVVM Frameworks

## 🎯 Mục Tiêu
- CommunityToolkit.Mvvm
- Source Generators

---

## 1. CommunityToolkit.Mvvm

Install: `dotnet add package CommunityToolkit.Mvvm`

---

## 2. [ObservableProperty] Attribute

```csharp
using CommunityToolkit.Mvvm.ComponentModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private int _age;
    
    // Auto-generates:
    // public string Name { get => _name; set => SetProperty(ref _name, value); }
}
```

---

## 3. [RelayCommand] Attribute

```csharp
using CommunityToolkit.Mvvm.Input;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;
    
    [RelayCommand]
    private void Save()
    {
        // Save logic
    }
    
    [RelayCommand(CanExecute = nameof(CanDelete))]
    private void Delete(Product product)
    {
        Products.Remove(product);
    }
    
    private bool CanDelete(Product product) => product != null;
    
    // Auto-generates: SaveCommand, DeleteCommand
}
```

---

## 4. Complete Example

```csharp
public partial class ProductViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Product> _products = new();
    
    [ObservableProperty]
    private Product _selectedProduct;
    
    [ObservableProperty]
    private string _searchText;
    
    [RelayCommand]
    private async Task LoadAsync()
    {
        var data = await _service.GetAllAsync();
        Products = new ObservableCollection<Product>(data);
    }
    
    [RelayCommand]
    private void Add()
    {
        Products.Add(new Product { Name = "New" });
    }
    
    [RelayCommand(CanExecute = nameof(CanModify))]
    private void Delete(Product p) => Products.Remove(p);
    
    private bool CanModify(Product p) => p != null;
}
```

```xml
<StackPanel>
    <Button Content="Load" Command="{Binding LoadCommand}"/>
    <Button Content="Add" Command="{Binding AddCommand}"/>
    <DataGrid ItemsSource="{Binding Products}" SelectedItem="{Binding SelectedProduct}"/>
    <Button Content="Delete" Command="{Binding DeleteCommand}" 
            CommandParameter="{Binding SelectedProduct}"/>
</StackPanel>
```

---

## 📝 Bài Tập
1. Refactor ViewModel sang CommunityToolkit.Mvvm
2. Sử dụng async command với LoadCommand

---

⬅️ [Bài 7.3](./Bai_7.3_Ket_Noi_View_ViewModel.md) | ➡️ [Bài 8.1](../08_Navigation_Windows/Bai_8.1_Windows_Dialogs.md)
