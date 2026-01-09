# Bài 4.1: Data Binding Cơ Bản

## 🎯 Mục Tiêu Bài Học
- Hiểu Data Binding là gì và tại sao cần
- Các Binding Mode: OneWay, TwoWay, OneTime
- DataContext và INotifyPropertyChanged

---

## 1. Data Binding Là Gì?

**Data Binding** là cơ chế liên kết dữ liệu giữa UI (View) và dữ liệu (Model/ViewModel).

```
┌─────────────────┐     Binding     ┌─────────────────┐
│      View       │◄───────────────►│   Data Source   │
│  (TextBox.Text) │                 │  (Property)     │
└─────────────────┘                 └─────────────────┘
```

### Tại Sao Cần Data Binding?

**Không có Binding:**
```csharp
// Phải update UI thủ công
txtName.Text = user.Name;
user.Name = txtName.Text;
```

**Có Binding:**
```xml
<!-- UI tự động sync với data -->
<TextBox Text="{Binding Name}"/>
```

---

## 2. Cú Pháp Binding Cơ Bản

### 2.1 Simple Binding

```xml
<TextBlock Text="{Binding UserName}"/>
<TextBlock Text="{Binding Path=UserName}"/>  <!-- Tương đương -->
```

### 2.2 Binding với Path phức tạp

```xml
<!-- Binding nested property -->
<TextBlock Text="{Binding User.Address.City}"/>

<!-- Binding indexer -->
<TextBlock Text="{Binding Items[0].Name}"/>
```

---

## 3. DataContext

**DataContext** là nguồn dữ liệu mặc định cho Binding.

### 3.1 Set DataContext trong Code-behind

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}

public MainWindow()
{
    InitializeComponent();
    
    // Set DataContext cho cả Window
    this.DataContext = new Person
    {
        Name = "John Doe",
        Age = 25,
        Email = "john@example.com"
    };
}
```

```xml
<Window>
    <StackPanel>
        <TextBlock Text="{Binding Name}"/>
        <TextBlock Text="{Binding Age}"/>
        <TextBlock Text="{Binding Email}"/>
    </StackPanel>
</Window>
```

### 3.2 DataContext Inheritance

DataContext được kế thừa từ parent xuống children.

```xml
<Window>
    <!-- DataContext cho cả Window -->
    <Grid>
        <Grid.DataContext>
            <local:Person Name="Jane" Age="30"/>
        </Grid.DataContext>
        
        <!-- Kế thừa DataContext từ Grid -->
        <TextBlock Text="{Binding Name}"/>
        
        <!-- Override DataContext -->
        <StackPanel>
            <StackPanel.DataContext>
                <local:Person Name="Bob" Age="25"/>
            </StackPanel.DataContext>
            <TextBlock Text="{Binding Name}"/>  <!-- Bob -->
        </StackPanel>
    </Grid>
</Window>
```

---

## 4. Binding Modes

### 4.1 Các Mode

| Mode | Hướng | Mô Tả |
|------|-------|-------|
| **OneWay** | Source → Target | UI hiển thị data, không gửi ngược |
| **TwoWay** | Source ↔ Target | UI và data sync 2 chiều |
| **OneTime** | Source → Target (1 lần) | Chỉ lấy data khi load |
| **OneWayToSource** | Target → Source | UI gửi data về source |

### 4.2 Ví Dụ

```xml
<!-- OneWay (mặc định cho hầu hết properties) -->
<TextBlock Text="{Binding Name, Mode=OneWay}"/>

<!-- TwoWay (mặc định cho TextBox.Text) -->
<TextBox Text="{Binding Name, Mode=TwoWay}"/>

<!-- OneTime -->
<TextBlock Text="{Binding CreatedDate, Mode=OneTime}"/>

<!-- OneWayToSource -->
<TextBox Text="{Binding SearchQuery, Mode=OneWayToSource}"/>
```

### 4.3 Default Binding Mode

| Control Property | Default Mode |
|-----------------|--------------|
| TextBox.Text | TwoWay |
| CheckBox.IsChecked | TwoWay |
| ComboBox.SelectedItem | TwoWay |
| TextBlock.Text | OneWay |
| Label.Content | OneWay |

---

## 5. INotifyPropertyChanged

Để UI tự động cập nhật khi data thay đổi, object cần implement **INotifyPropertyChanged**.

### 5.1 Không có INPC

```csharp
public class Person
{
    public string Name { get; set; }
}

// UI sẽ KHÔNG tự động cập nhật khi Name thay đổi
person.Name = "New Name";  // UI vẫn hiển thị tên cũ!
```

### 5.2 Có INPC

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Person : INotifyPropertyChanged
{
    private string _name;
    
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();  // Notify UI
            }
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

```csharp
// UI sẽ TỰ ĐỘNG cập nhật
person.Name = "New Name";  // UI hiển thị "New Name"!
```

### 5.3 BaseViewModel

Tạo base class để reuse:

```csharp
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

// Usage
public class PersonViewModel : BaseViewModel
{
    private string _name;
    private int _age;
    
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    
    public int Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }
}
```

---

## 6. UpdateSourceTrigger

Xác định KHI NÀO data được gửi từ Target về Source.

```xml
<!-- PropertyChanged: Cập nhật ngay khi gõ -->
<TextBox Text="{Binding SearchText, UpdateSourceTrigger=PropertyChanged}"/>

<!-- LostFocus (mặc định): Cập nhật khi mất focus -->
<TextBox Text="{Binding Name, UpdateSourceTrigger=LostFocus}"/>

<!-- Explicit: Cập nhật thủ công bằng code -->
<TextBox x:Name="txtName" 
         Text="{Binding Name, UpdateSourceTrigger=Explicit}"/>
```

```csharp
// Với Explicit, phải gọi thủ công
private void Save_Click(object sender, RoutedEventArgs e)
{
    BindingExpression binding = txtName.GetBindingExpression(TextBox.TextProperty);
    binding.UpdateSource();
}
```

---

## 7. Thực Hành: Form Binding

### PersonViewModel.cs

```csharp
public class PersonViewModel : BaseViewModel
{
    private string _firstName;
    private string _lastName;
    private int _age;
    
    public string FirstName
    {
        get => _firstName;
        set
        {
            SetProperty(ref _firstName, value);
            OnPropertyChanged(nameof(FullName));  // Notify FullName changed too
        }
    }
    
    public string LastName
    {
        get => _lastName;
        set
        {
            SetProperty(ref _lastName, value);
            OnPropertyChanged(nameof(FullName));
        }
    }
    
    public int Age
    {
        get => _age;
        set => SetProperty(ref _age, value);
    }
    
    // Computed property
    public string FullName => $"{FirstName} {LastName}";
}
```

### MainWindow.xaml

```xml
<Window x:Class="DataBindingDemo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:DataBindingDemo"
        Title="Data Binding Demo" Height="300" Width="400">
    
    <Window.DataContext>
        <local:PersonViewModel FirstName="John" LastName="Doe" Age="25"/>
    </Window.DataContext>
    
    <Grid Margin="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="100"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>

        <!-- First Name -->
        <Label Content="First Name:" Grid.Row="0"/>
        <TextBox Grid.Row="0" Grid.Column="1"
                 Text="{Binding FirstName, UpdateSourceTrigger=PropertyChanged}"
                 Margin="0,5"/>

        <!-- Last Name -->
        <Label Content="Last Name:" Grid.Row="1"/>
        <TextBox Grid.Row="1" Grid.Column="1"
                 Text="{Binding LastName, UpdateSourceTrigger=PropertyChanged}"
                 Margin="0,5"/>

        <!-- Age -->
        <Label Content="Age:" Grid.Row="2"/>
        <TextBox Grid.Row="2" Grid.Column="1"
                 Text="{Binding Age, UpdateSourceTrigger=PropertyChanged}"
                 Margin="0,5"/>

        <!-- Display (Read-only) -->
        <Border Grid.Row="4" Grid.ColumnSpan="2" 
                Background="#E3F2FD" Padding="15" Margin="0,20,0,0">
            <StackPanel>
                <TextBlock Text="Preview:" FontWeight="Bold"/>
                <TextBlock>
                    <Run Text="Name: "/>
                    <Run Text="{Binding FullName}" FontWeight="Bold"/>
                </TextBlock>
                <TextBlock>
                    <Run Text="Age: "/>
                    <Run Text="{Binding Age}"/>
                    <Run Text=" years old"/>
                </TextBlock>
            </StackPanel>
        </Border>
    </Grid>
</Window>
```

---

## 📝 Bài Tập

1. Tạo form với binding 2 chiều:
   - TextBox nhập tên
   - Slider chọn tuổi (0-100)
   - TextBlock hiển thị: "Hello, [Name]! You are [Age] years old."

2. Implement INotifyPropertyChanged cho class Product:
   - Properties: Name, Price, Quantity
   - Computed: TotalValue = Price * Quantity

3. Giải thích sự khác nhau giữa:
   - OneWay và TwoWay
   - UpdateSourceTrigger=PropertyChanged và LostFocus

---

## 📚 Tài Liệu Tham Khảo
- [Data Binding Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/data-binding-overview)
- [INotifyPropertyChanged](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged)

---

⬅️ **Bài trước**: [Bài 3.3: Advanced Controls](../03_Controls_Layouts/Bai_3.3_Advanced_Controls.md)  
➡️ **Bài tiếp theo**: [Bài 4.2: Data Binding Nâng Cao](./Bai_4.2_Binding_Nang_Cao.md)
