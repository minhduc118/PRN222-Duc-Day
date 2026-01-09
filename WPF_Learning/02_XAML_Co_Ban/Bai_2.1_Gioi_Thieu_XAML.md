# Bài 2.1: Giới Thiệu XAML

## 🎯 Mục Tiêu Bài Học
- Hiểu XAML là gì và cách hoạt động
- Nắm vững cú pháp XAML cơ bản
- Sử dụng Elements và Attributes
- Hiểu về Namespaces trong XAML

---

## 1. XAML Là Gì?

**XAML (eXtensible Application Markup Language)** là ngôn ngữ đánh dấu dựa trên XML, được sử dụng để định nghĩa giao diện người dùng trong WPF.

### Tại sao dùng XAML?
- ✅ **Tách biệt UI và Logic**: Designer làm XAML, Developer làm C#
- ✅ **Declarative**: Khai báo "cái gì" thay vì "làm thế nào"
- ✅ **Tools Support**: Visual Studio Designer, Blend
- ✅ **Data Binding**: Dễ dàng bind dữ liệu

### So sánh XAML vs C# Code

```xml
<!-- XAML -->
<Button Content="Click Me" 
        Width="100" 
        Height="30"
        Background="Blue"/>
```

```csharp
// C# Code tương đương
Button button = new Button();
button.Content = "Click Me";
button.Width = 100;
button.Height = 30;
button.Background = Brushes.Blue;
```

---

## 2. Cú Pháp XAML Cơ Bản

### 2.1 Object Elements
Mỗi element trong XAML đại diện cho một object .NET:

```xml
<!-- Tạo instance của Button -->
<Button/>

<!-- Tạo instance của TextBox -->
<TextBox/>

<!-- Tạo instance của Grid -->
<Grid>
    <!-- Child elements -->
</Grid>
```

### 2.2 Attributes (Property Syntax)
Đặt giá trị property bằng attributes:

```xml
<Button 
    Content="Click Me"       <!-- Content property -->
    Width="100"              <!-- Width property -->
    Height="50"              <!-- Height property -->
    Background="Red"         <!-- Background property -->
    FontSize="14"            <!-- FontSize property -->
    IsEnabled="True"/>       <!-- IsEnabled property -->
```

### 2.3 Property Element Syntax
Khi giá trị phức tạp, dùng property element:

```xml
<!-- Attribute Syntax (đơn giản) -->
<Button Background="Red"/>

<!-- Property Element Syntax (phức tạp) -->
<Button>
    <Button.Background>
        <LinearGradientBrush>
            <GradientStop Color="Yellow" Offset="0"/>
            <GradientStop Color="Red" Offset="1"/>
        </LinearGradientBrush>
    </Button.Background>
    <Button.Content>
        <StackPanel Orientation="Horizontal">
            <Image Source="icon.png" Width="16"/>
            <TextBlock Text="Click Me" Margin="5,0,0,0"/>
        </StackPanel>
    </Button.Content>
</Button>
```

### 2.4 Content Property
Mỗi control có một Content Property mặc định:

```xml
<!-- Đầy đủ -->
<Button>
    <Button.Content>
        Click Me
    </Button.Content>
</Button>

<!-- Rút gọn (Content là default property) -->
<Button>
    Click Me
</Button>

<!-- Hoặc dùng attribute -->
<Button Content="Click Me"/>
```

---

## 3. Namespaces Trong XAML

### 3.1 Default Namespaces

```xml
<Window 
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
</Window>
```

| Namespace | Prefix | Chứa |
|-----------|--------|------|
| `.../presentation` | (none) | WPF controls: Button, Grid, TextBox... |
| `.../xaml` | `x:` | XAML language: x:Name, x:Class, x:Key... |

### 3.2 Custom Namespaces

```xml
<Window 
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="clr-namespace:MyApp"
    xmlns:models="clr-namespace:MyApp.Models"
    xmlns:sys="clr-namespace:System;assembly=mscorlib">
    
    <!-- Sử dụng -->
    <local:MyCustomControl/>
    <sys:String x:Key="greeting">Hello</sys:String>
</Window>
```

### 3.3 Common x: Attributes

| Attribute | Mô Tả | Ví Dụ |
|-----------|-------|-------|
| `x:Class` | Liên kết với code-behind class | `x:Class="MyApp.MainWindow"` |
| `x:Name` | Đặt tên cho element (dùng trong code) | `x:Name="txtName"` |
| `x:Key` | Key cho resource | `x:Key="MyStyle"` |
| `x:Type` | Tham chiếu đến Type | `{x:Type Button}` |
| `x:Null` | Giá trị null | `Background="{x:Null}"` |
| `x:Static` | Tham chiếu static member | `{x:Static Colors.Red}` |

---

## 4. Comments Trong XAML

```xml
<!-- Đây là comment một dòng -->

<!--
    Đây là comment
    nhiều dòng
-->

<StackPanel>
    <!-- Button này đã bị disable tạm thời
    <Button Content="Hidden Button"/>
    -->
    <Button Content="Visible Button"/>
</StackPanel>
```

---

## 5. Type Converters

XAML tự động convert string thành các type phức tạp:

```xml
<!-- String → Brush -->
<Button Background="Red"/>           <!-- SolidColorBrush -->
<Button Background="#FF5722"/>       <!-- Hex color -->
<Button Background="#80FF5722"/>     <!-- Hex với alpha -->

<!-- String → Thickness -->
<Button Margin="10"/>                <!-- All sides = 10 -->
<Button Margin="10,20"/>             <!-- Left/Right=10, Top/Bottom=20 -->
<Button Margin="10,20,30,40"/>       <!-- Left, Top, Right, Bottom -->

<!-- String → FontWeight -->
<TextBlock FontWeight="Bold"/>

<!-- String → Visibility -->
<Button Visibility="Collapsed"/>

<!-- String → GridLength -->
<ColumnDefinition Width="100"/>      <!-- Fixed -->
<ColumnDefinition Width="Auto"/>      <!-- Auto-size -->
<ColumnDefinition Width="*"/>        <!-- Star (proportional) -->
<ColumnDefinition Width="2*"/>       <!-- 2 phần -->
```

---

## 6. Thực Hành

### Ví dụ Tổng Hợp

```xml
<Window x:Class="XAMLBasics.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="XAML Basics Demo" 
        Height="400" Width="500"
        WindowStartupLocation="CenterScreen">
    
    <!-- Grid với 3 rows -->
    <Grid Margin="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>

        <!-- Header -->
        <TextBlock Grid.Row="0"
                   Text="Welcome to XAML!"
                   FontSize="24"
                   FontWeight="Bold"
                   Foreground="#2196F3"
                   HorizontalAlignment="Center"/>

        <!-- Content Area -->
        <Border Grid.Row="1" 
                Margin="0,20"
                BorderBrush="#E0E0E0"
                BorderThickness="1"
                CornerRadius="10">
            <StackPanel VerticalAlignment="Center"
                        HorizontalAlignment="Center">
                
                <TextBox x:Name="txtInput"
                         Width="200"
                         Padding="10"
                         FontSize="14"/>
                
                <Button x:Name="btnSubmit"
                        Content="Submit"
                        Margin="0,15,0,0"
                        Padding="30,10"
                        Click="BtnSubmit_Click">
                    <Button.Background>
                        <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
                            <GradientStop Color="#667eea" Offset="0"/>
                            <GradientStop Color="#764ba2" Offset="1"/>
                        </LinearGradientBrush>
                    </Button.Background>
                </Button>
            </StackPanel>
        </Border>

        <!-- Footer -->
        <TextBlock Grid.Row="2"
                   x:Name="txtResult"
                   HorizontalAlignment="Center"
                   FontSize="16"/>
    </Grid>
</Window>
```

---

## 📝 Bài Tập

1. Tạo một Window với:
   - Header: TextBlock với gradient background
   - Body: 3 TextBox với labels
   - Footer: 2 Buttons (Save, Cancel)

2. Cho biết sự khác nhau giữa:
   - `x:Name` và `Name`
   - Attribute Syntax và Property Element Syntax

3. Convert XAML sau sang Property Element Syntax:
   ```xml
   <Button Content="Click" Background="Blue" Margin="10,20"/>
   ```

---

## 📚 Tài Liệu Tham Khảo
- [XAML Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/xaml/)
- [XAML Syntax In Detail](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/xaml-syntax-in-detail)

---

⬅️ **Bài trước**: [Bài 1.2: Thiết Lập Môi Trường](../01_Gioi_Thieu_WPF/Bai_1.2_Thiet_Lap_Moi_Truong.md)  
➡️ **Bài tiếp theo**: [Bài 2.2: XAML Nâng Cao](./Bai_2.2_XAML_Nang_Cao.md)
