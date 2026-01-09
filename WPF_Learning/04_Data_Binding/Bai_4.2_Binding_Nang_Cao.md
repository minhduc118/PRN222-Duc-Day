# Bài 4.2: Data Binding Nâng Cao

## 🎯 Mục Tiêu Bài Học
- ElementName Binding
- RelativeSource Binding
- Binding to Collections
- ICollectionView cho Sorting/Filtering

---

## 1. ElementName Binding

Binding giữa các elements trong cùng XAML.

```xml
<!-- Slider và TextBlock sync -->
<StackPanel>
    <Slider x:Name="fontSizeSlider" 
            Minimum="10" Maximum="40" Value="16"/>
    
    <TextBlock Text="Preview Text"
               FontSize="{Binding ElementName=fontSizeSlider, Path=Value}"/>
    
    <TextBlock Text="{Binding ElementName=fontSizeSlider, 
                              Path=Value, 
                              StringFormat='Font Size: {0:F0}'}"/>
</StackPanel>

<!-- ColorPicker binding -->
<StackPanel>
    <Slider x:Name="redSlider" Maximum="255" Value="128"/>
    <Slider x:Name="greenSlider" Maximum="255" Value="128"/>
    <Slider x:Name="blueSlider" Maximum="255" Value="128"/>
    
    <Rectangle Width="100" Height="100">
        <Rectangle.Fill>
            <SolidColorBrush>
                <SolidColorBrush.Color>
                    <Color A="255"
                           R="{Binding ElementName=redSlider, Path=Value}"
                           G="{Binding ElementName=greenSlider, Path=Value}"
                           B="{Binding ElementName=blueSlider, Path=Value}"/>
                </SolidColorBrush.Color>
            </SolidColorBrush>
        </Rectangle.Fill>
    </Rectangle>
</StackPanel>
```

---

## 2. RelativeSource Binding

Binding dựa trên vị trí trong Visual Tree.

### 2.1 RelativeSource Self

```xml
<!-- Bind đến chính element đó -->
<Border Width="100"
        Height="{Binding RelativeSource={RelativeSource Self}, Path=Width}"/>

<!-- Tooltip hiện tên control -->
<Button Content="Click Me"
        ToolTip="{Binding RelativeSource={RelativeSource Self}, Path=Name}"/>
```

### 2.2 RelativeSource TemplatedParent

Trong ControlTemplate, bind đến control đang được template:

```xml
<ControlTemplate TargetType="Button">
    <Border Background="{Binding RelativeSource={RelativeSource TemplatedParent}, 
                                 Path=Background}"
            Padding="{Binding RelativeSource={RelativeSource TemplatedParent}, 
                              Path=Padding}">
        <ContentPresenter/>
    </Border>
</ControlTemplate>

<!-- Shorthand: TemplateBinding -->
<ControlTemplate TargetType="Button">
    <Border Background="{TemplateBinding Background}"
            Padding="{TemplateBinding Padding}">
        <ContentPresenter/>
    </Border>
</ControlTemplate>
```

### 2.3 RelativeSource FindAncestor

Tìm parent element theo type:

```xml
<Grid x:Name="rootGrid" Tag="Root Data">
    <StackPanel>
        <Border>
            <!-- Tìm Grid ancestor, lấy Tag property -->
            <TextBlock Text="{Binding RelativeSource={RelativeSource 
                                      AncestorType=Grid}, Path=Tag}"/>
        </Border>
    </StackPanel>
</Grid>

<!-- Tìm ancestor cụ thể (level) -->
<TextBlock Text="{Binding RelativeSource={RelativeSource 
                          AncestorType=StackPanel, 
                          AncestorLevel=2}, Path=Name}"/>
```

### 2.4 Use Case: DataGrid Column Binding

```xml
<DataGrid ItemsSource="{Binding Products}">
    <DataGrid.Columns>
        <DataGridTemplateColumn Header="Actions">
            <DataGridTemplateColumn.CellTemplate>
                <DataTemplate>
                    <!-- Bind đến ViewModel của Window, không phải row item -->
                    <Button Content="Delete"
                            Command="{Binding DataContext.DeleteCommand, 
                                     RelativeSource={RelativeSource 
                                     AncestorType=DataGrid}}"
                            CommandParameter="{Binding}"/>
                </DataTemplate>
            </DataGridTemplateColumn.CellTemplate>
        </DataGridTemplateColumn>
    </DataGrid.Columns>
</DataGrid>
```

---

## 3. Binding to Collections

### 3.1 ObservableCollection

```csharp
using System.Collections.ObjectModel;

public class MainViewModel : BaseViewModel
{
    public ObservableCollection<Product> Products { get; set; }
    
    public MainViewModel()
    {
        Products = new ObservableCollection<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 999 },
            new Product { Id = 2, Name = "Mouse", Price = 29 },
            new Product { Id = 3, Name = "Keyboard", Price = 79 }
        };
    }
    
    public void AddProduct(Product product)
    {
        Products.Add(product);  // UI tự động update!
    }
    
    public void RemoveProduct(Product product)
    {
        Products.Remove(product);  // UI tự động update!
    }
}
```

```xml
<ListBox ItemsSource="{Binding Products}"
         DisplayMemberPath="Name"/>

<DataGrid ItemsSource="{Binding Products}"
          AutoGenerateColumns="True"/>
```

### 3.2 List vs ObservableCollection

| | List<T> | ObservableCollection<T> |
|--|---------|------------------------|
| Add/Remove Update UI | ❌ Không | ✅ Có |
| Performance | Nhanh hơn | Chậm hơn một chút |
| Use when | Static data | Dynamic data |

### 3.3 ItemsSource Properties

```xml
<ListBox ItemsSource="{Binding Products}">
    <!-- DisplayMemberPath: property hiển thị text -->
    <!-- SelectedItem: item đang chọn -->
    <!-- SelectedValue: giá trị của SelectedValuePath -->
    <!-- SelectedValuePath: property dùng làm value -->
</ListBox>

<ComboBox ItemsSource="{Binding Categories}"
          DisplayMemberPath="Name"
          SelectedValuePath="Id"
          SelectedValue="{Binding SelectedCategoryId}"/>
```

---

## 4. ICollectionView

**ICollectionView** cung cấp sorting, filtering, grouping cho collections.

### 4.1 Tạo CollectionView

```csharp
public class MainViewModel : BaseViewModel
{
    private ObservableCollection<Product> _products;
    private ICollectionView _productsView;
    
    public ICollectionView ProductsView => _productsView;
    
    public MainViewModel()
    {
        _products = new ObservableCollection<Product>
        {
            new Product { Name = "Laptop", Category = "Electronics", Price = 999 },
            new Product { Name = "Mouse", Category = "Electronics", Price = 29 },
            new Product { Name = "Desk", Category = "Furniture", Price = 199 },
            new Product { Name = "Chair", Category = "Furniture", Price = 149 }
        };
        
        _productsView = CollectionViewSource.GetDefaultView(_products);
    }
}
```

### 4.2 Sorting

```csharp
// Sort by Name ascending
_productsView.SortDescriptions.Add(
    new SortDescription("Name", ListSortDirection.Ascending));

// Sort by Price descending
_productsView.SortDescriptions.Add(
    new SortDescription("Price", ListSortDirection.Descending));

// Clear sorting
_productsView.SortDescriptions.Clear();
```

```xml
<!-- Bind to CollectionView -->
<DataGrid ItemsSource="{Binding ProductsView}"/>
```

### 4.3 Filtering

```csharp
private string _searchText;
public string SearchText
{
    get => _searchText;
    set
    {
        SetProperty(ref _searchText, value);
        _productsView.Refresh();  // Re-apply filter
    }
}

public MainViewModel()
{
    // ...
    _productsView.Filter = FilterProducts;
}

private bool FilterProducts(object obj)
{
    if (string.IsNullOrEmpty(SearchText))
        return true;
        
    Product product = obj as Product;
    return product.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
}
```

```xml
<StackPanel>
    <TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}"
             Margin="0,0,0,10"/>
    <DataGrid ItemsSource="{Binding ProductsView}"/>
</StackPanel>
```

### 4.4 Grouping

```csharp
public MainViewModel()
{
    // ...
    _productsView.GroupDescriptions.Add(
        new PropertyGroupDescription("Category"));
}
```

```xml
<ListBox ItemsSource="{Binding ProductsView}">
    <ListBox.GroupStyle>
        <GroupStyle>
            <GroupStyle.HeaderTemplate>
                <DataTemplate>
                    <Border Background="#2196F3" Padding="5">
                        <TextBlock Text="{Binding Name}" 
                                   Foreground="White" 
                                   FontWeight="Bold"/>
                    </Border>
                </DataTemplate>
            </GroupStyle.HeaderTemplate>
        </GroupStyle>
    </ListBox.GroupStyle>
    <ListBox.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Name}" Margin="20,2"/>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

---

## 5. Current Item

```csharp
// Get current selected item
Product current = _productsView.CurrentItem as Product;

// Move to next/previous
_productsView.MoveCurrentToNext();
_productsView.MoveCurrentToPrevious();
_productsView.MoveCurrentToFirst();
_productsView.MoveCurrentToLast();

// Move to specific item
_productsView.MoveCurrentTo(someProduct);

// Current changed event
_productsView.CurrentChanged += (s, e) =>
{
    Product selected = _productsView.CurrentItem as Product;
    // Handle selection
};
```

```xml
<!-- Sync với IsSynchronizedWithCurrentItem -->
<ListBox ItemsSource="{Binding ProductsView}"
         IsSynchronizedWithCurrentItem="True"/>

<TextBlock Text="{Binding ProductsView/Name}"/>  <!-- Current item's Name -->
```

---

## 6. CollectionViewSource trong XAML

```xml
<Window.Resources>
    <CollectionViewSource x:Key="productsViewSource"
                          Source="{Binding Products}">
        <CollectionViewSource.SortDescriptions>
            <scm:SortDescription PropertyName="Name"/>
        </CollectionViewSource.SortDescriptions>
        <CollectionViewSource.GroupDescriptions>
            <PropertyGroupDescription PropertyName="Category"/>
        </CollectionViewSource.GroupDescriptions>
    </CollectionViewSource>
</Window.Resources>

<DataGrid ItemsSource="{Binding Source={StaticResource productsViewSource}}"/>
```

---

## 7. Thực Hành Tổng Hợp

```csharp
public class ProductsViewModel : BaseViewModel
{
    private ObservableCollection<Product> _products;
    private ICollectionView _productsView;
    private string _searchText;
    private string _selectedCategory;
    
    public ICollectionView ProductsView => _productsView;
    
    public string SearchText
    {
        get => _searchText;
        set { SetProperty(ref _searchText, value); ApplyFilter(); }
    }
    
    public string SelectedCategory
    {
        get => _selectedCategory;
        set { SetProperty(ref _selectedCategory, value); ApplyFilter(); }
    }
    
    public List<string> Categories { get; } = new()
    {
        "All", "Electronics", "Furniture", "Clothing"
    };
    
    public ProductsViewModel()
    {
        LoadProducts();
        _productsView = CollectionViewSource.GetDefaultView(_products);
        _productsView.Filter = FilterProducts;
        _selectedCategory = "All";
    }
    
    private void LoadProducts()
    {
        _products = new ObservableCollection<Product>
        {
            // ... products
        };
    }
    
    private void ApplyFilter()
    {
        _productsView.Refresh();
    }
    
    private bool FilterProducts(object obj)
    {
        Product p = obj as Product;
        if (p == null) return false;
        
        bool matchSearch = string.IsNullOrEmpty(SearchText) ||
                          p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
        
        bool matchCategory = SelectedCategory == "All" ||
                            p.Category == SelectedCategory;
        
        return matchSearch && matchCategory;
    }
}
```

```xml
<StackPanel>
    <Grid Margin="0,0,0,10">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*"/>
            <ColumnDefinition Width="150"/>
        </Grid.ColumnDefinitions>
        
        <TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}"
                 Padding="5"/>
        
        <ComboBox Grid.Column="1" 
                  ItemsSource="{Binding Categories}"
                  SelectedValue="{Binding SelectedCategory}"
                  Margin="10,0,0,0"/>
    </Grid>
    
    <DataGrid ItemsSource="{Binding ProductsView}"
              AutoGenerateColumns="True"
              Height="300"/>
</StackPanel>
```

---

## 📝 Bài Tập

1. Tạo ứng dụng với:
   - ListBox hiển thị danh sách employees
   - TextBox search filter theo tên
   - ComboBox filter theo department
   - Sorting theo Name và Salary

2. Tạo Master-Detail view:
   - ListBox danh sách Orders
   - DataGrid chi tiết OrderItems của order được chọn

---

## 📚 Tài Liệu Tham Khảo
- [ICollectionView](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.icollectionview)
- [CollectionViewSource](https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.collectionviewsource)

---

⬅️ **Bài trước**: [Bài 4.1: Data Binding Cơ Bản](./Bai_4.1_Binding_Co_Ban.md)  
➡️ **Bài tiếp theo**: [Bài 4.3: Value Converters](./Bai_4.3_Value_Converters.md)
