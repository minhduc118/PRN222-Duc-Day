# Bài 10.2: CRUD với WPF

## 🎯 Mục Tiêu
- Repository Pattern
- CRUD operations với EF Core
- Async/Await trong WPF

---

## 1. Repository Interface

```csharp
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
}
```

---

## 2. Repository Implementation

```csharp
public class ProductRepository : IRepository<Product>
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context) => _context = context;
    
    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.Include(p => p.Category).ToListAsync();
    
    public async Task<Product> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);
    
    public async Task AddAsync(Product entity)
    {
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Product entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
```

---

## 3. ViewModel với Async Commands

```csharp
public partial class ProductViewModel : ObservableObject
{
    private readonly IRepository<Product> _repository;
    
    [ObservableProperty]
    private ObservableCollection<Product> _products;
    
    [ObservableProperty]
    private Product _selectedProduct;
    
    [ObservableProperty]
    private bool _isLoading;
    
    public ProductViewModel(IRepository<Product> repository)
    {
        _repository = repository;
    }
    
    [RelayCommand]
    private async Task LoadAsync()
    {
        IsLoading = true;
        var data = await _repository.GetAllAsync();
        Products = new ObservableCollection<Product>(data);
        IsLoading = false;
    }
    
    [RelayCommand]
    private async Task SaveAsync(Product product)
    {
        if (product.Id == 0)
            await _repository.AddAsync(product);
        else
            await _repository.UpdateAsync(product);
        
        await LoadAsync();
    }
    
    [RelayCommand]
    private async Task DeleteAsync(Product product)
    {
        if (product == null) return;
        
        var result = MessageBox.Show("Delete?", "Confirm", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            await _repository.DeleteAsync(product.Id);
            await LoadAsync();
        }
    }
}
```

---

## 4. View

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="*"/>
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>
    
    <!-- Toolbar -->
    <StackPanel Orientation="Horizontal" Margin="10">
        <Button Content="Load" Command="{Binding LoadCommand}"/>
        <Button Content="Add" Command="{Binding AddCommand}"/>
        <Button Content="Delete" Command="{Binding DeleteCommand}" 
                CommandParameter="{Binding SelectedProduct}"/>
    </StackPanel>
    
    <!-- Loading indicator -->
    <ProgressBar Grid.Row="1" IsIndeterminate="True"
                 Visibility="{Binding IsLoading, Converter={StaticResource BoolToVis}}"/>
    
    <!-- DataGrid -->
    <DataGrid Grid.Row="1" ItemsSource="{Binding Products}"
              SelectedItem="{Binding SelectedProduct}"
              AutoGenerateColumns="False">
        <DataGrid.Columns>
            <DataGridTextColumn Header="Name" Binding="{Binding Name}"/>
            <DataGridTextColumn Header="Price" Binding="{Binding Price, StringFormat='{}{0:C}'}"/>
        </DataGrid.Columns>
    </DataGrid>
</Grid>
```

---

## 📝 Bài Tập
1. Implement complete CRUD cho Products
2. Add search/filter functionality
3. Implement validation với INotifyDataErrorInfo

---

⬅️ [Bài 10.1](./Bai_10.1_Tich_Hop_EFCore.md) | 🏁 **Hoàn Thành Lộ Trình!**
