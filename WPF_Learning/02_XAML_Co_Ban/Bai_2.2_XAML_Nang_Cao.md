# Bài 2.2: XAML Nâng Cao

## 🎯 Mục Tiêu Bài Học
- Hiểu Attached Properties
- Sử dụng Markup Extensions
- Làm việc với x:Name và Code-behind
- Nắm vững các kỹ thuật XAML nâng cao

---

## 1. Attached Properties

**Attached Properties** là properties được định nghĩa bởi một class nhưng có thể được sử dụng bởi các class khác.

### Ví dụ phổ biến

```xml
<Grid>
    <!-- Grid.Row và Grid.Column là Attached Properties -->
    <Button Content="Button 1" 
            Grid.Row="0" 
            Grid.Column="0"/>
    
    <Button Content="Button 2" 
            Grid.Row="1" 
            Grid.Column="2"/>
</Grid>

<DockPanel>
    <!-- DockPanel.Dock là Attached Property -->
    <Menu DockPanel.Dock="Top"/>
    <StatusBar DockPanel.Dock="Bottom"/>
    <TextBlock/>
</DockPanel>

<Canvas>
    <!-- Canvas.Left, Canvas.Top là Attached Properties -->
    <Button Content="Click" 
            Canvas.Left="50" 
            Canvas.Top="100"/>
</Canvas>
```

### Tại sao cần Attached Properties?
- Button không biết về Grid
- Nhưng Button cần nói cho Grid biết nó nằm ở row/column nào
- Grid định nghĩa `Grid.Row` và "gắn" (attach) vào Button

### Tạo Custom Attached Property

```csharp
public static class AttachedProps
{
    // Declare Attached Property
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.RegisterAttached(
            "CornerRadius",
            typeof(double),
            typeof(AttachedProps),
            new PropertyMetadata(0.0));

    // Getter
    public static double GetCornerRadius(DependencyObject obj)
    {
        return (double)obj.GetValue(CornerRadiusProperty);
    }

    // Setter
    public static void SetCornerRadius(DependencyObject obj, double value)
    {
        obj.SetValue(CornerRadiusProperty, value);
    }
}
```

```xml
<!-- Sử dụng -->
<Button Content="Rounded" 
        local:AttachedProps.CornerRadius="10"/>
```

---

## 2. Markup Extensions

**Markup Extensions** là cú pháp đặc biệt trong XAML, bắt đầu bằng `{` và kết thúc bằng `}`.

### 2.1 {Binding}
Liên kết dữ liệu:

```xml
<!-- Binding cơ bản -->
<TextBlock Text="{Binding UserName}"/>

<!-- Binding với Path -->
<TextBlock Text="{Binding Path=User.Email}"/>

<!-- TwoWay Binding -->
<TextBox Text="{Binding Email, Mode=TwoWay}"/>

<!-- Binding với ElementName -->
<Slider x:Name="slider" Minimum="0" Maximum="100"/>
<TextBlock Text="{Binding ElementName=slider, Path=Value}"/>
```

### 2.2 {StaticResource} và {DynamicResource}

```xml
<Window.Resources>
    <SolidColorBrush x:Key="PrimaryColor" Color="#2196F3"/>
    <Style x:Key="ButtonStyle" TargetType="Button">
        <Setter Property="Background" Value="{StaticResource PrimaryColor}"/>
    </Style>
</Window.Resources>

<!-- StaticResource: resolve lúc load XAML -->
<Button Background="{StaticResource PrimaryColor}"/>

<!-- DynamicResource: resolve lúc runtime (có thể thay đổi) -->
<Button Background="{DynamicResource PrimaryColor}"/>
```

| | StaticResource | DynamicResource |
|--|---------------|-----------------|
| Resolve | Load time | Runtime |
| Performance | Nhanh hơn | Chậm hơn |
| Can change | Không | Có |
| Use when | Resource cố định | Theme switching |

### 2.3 {x:Type}

```xml
<!-- Tham chiếu đến một Type -->
<Style TargetType="{x:Type Button}">
    <Setter Property="FontSize" Value="14"/>
</Style>

<!-- Shorthand (tương đương) -->
<Style TargetType="Button">
    <Setter Property="FontSize" Value="14"/>
</Style>
```

### 2.4 {x:Null}

```xml
<!-- Set property thành null -->
<TextBox Background="{x:Null}"/>
<Button Style="{x:Null}"/>  <!-- Remove style -->
```

### 2.5 {x:Static}

```xml
<!-- Tham chiếu static member -->
<TextBlock Text="{x:Static sys:Environment.MachineName}"/>
<Rectangle Fill="{x:Static SystemColors.HighlightBrush}"/>

<!-- Với custom class -->
<TextBlock Text="{x:Static local:Constants.AppName}"/>
```

```csharp
// Constants.cs
public static class Constants
{
    public static string AppName => "My WPF App";
    public static double DefaultFontSize => 14.0;
}
```

### 2.6 {RelativeSource}

```xml
<!-- Self - bind đến chính element đó -->
<Border Width="{Binding RelativeSource={RelativeSource Self}, 
                        Path=Height}"/>

<!-- TemplatedParent - trong ControlTemplate -->
<Border Background="{Binding RelativeSource={RelativeSource TemplatedParent}, 
                             Path=Background}"/>

<!-- FindAncestor - tìm parent container -->
<TextBlock Text="{Binding RelativeSource={RelativeSource 
                          AncestorType=Window}, Path=Title}"/>
```

### 2.7 {TemplateBinding}
Shorthand cho RelativeSource TemplatedParent trong ControlTemplate:

```xml
<ControlTemplate TargetType="Button">
    <Border Background="{TemplateBinding Background}"
            BorderBrush="{TemplateBinding BorderBrush}"
            Padding="{TemplateBinding Padding}">
        <ContentPresenter/>
    </Border>
</ControlTemplate>
```

---

## 3. x:Name vs Name

```xml
<!-- Cả hai đều hoạt động cho hầu hết WPF controls -->
<Button x:Name="btn1"/>
<Button Name="btn2"/>
```

| | x:Name | Name |
|--|--------|------|
| Định nghĩa | XAML language | FrameworkElement property |
| Scope | Mọi XAML element | Chỉ FrameworkElement |
| Sử dụng | Mọi nơi | Hầu hết controls |

**Best Practice**: Luôn dùng `x:Name` để nhất quán.

---

## 4. Code-behind Integration

### 4.1 x:Class

```xml
<!-- MainWindow.xaml -->
<Window x:Class="MyApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
</Window>
```

```csharp
// MainWindow.xaml.cs
namespace MyApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  // Parse XAML và tạo UI
        }
    }
}
```

### 4.2 Event Handlers

```xml
<Button Content="Click" Click="Button_Click"/>
<TextBox TextChanged="TextBox_TextChanged"/>
<Window Loaded="Window_Loaded" Closing="Window_Closing"/>
```

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    Button btn = sender as Button;
    MessageBox.Show($"Clicked: {btn.Content}");
}

private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
{
    TextBox txt = sender as TextBox;
    // Handle text change
}

private void Window_Loaded(object sender, RoutedEventArgs e)
{
    // Initialization after window is loaded
}
```

### 4.3 Access Named Elements

```xml
<TextBox x:Name="txtInput"/>
<Button x:Name="btnSubmit" Content="Submit"/>
```

```csharp
public MainWindow()
{
    InitializeComponent();
    
    // Truy cập elements bằng x:Name
    txtInput.Text = "Default Value";
    btnSubmit.IsEnabled = false;
    
    // Lấy nhiều elements
    txtInput.Focus();
}
```

---

## 5. CDATA Sections

Khi cần text chứa ký tự đặc biệt:

```xml
<TextBlock>
    <TextBlock.Text>
        <![CDATA[
            if (x < 10 && y > 20) {
                // some code
            }
        ]]>
    </TextBlock.Text>
</TextBlock>
```

---

## 6. Escaped Characters

| Character | XAML Entity |
|-----------|-------------|
| `<` | `&lt;` |
| `>` | `&gt;` |
| `&` | `&amp;` |
| `"` | `&quot;` |
| `'` | `&apos;` |

```xml
<TextBlock Text="Price: $100 &amp; Tax: &lt;10%"/>
```

---

## 7. Thực Hành Tổng Hợp

```xml
<Window x:Class="XAMLAdvanced.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        xmlns:local="clr-namespace:XAMLAdvanced"
        Title="{x:Static local:Constants.AppTitle}" 
        Height="450" Width="600">
    
    <Window.Resources>
        <!-- Resources -->
        <SolidColorBrush x:Key="AccentColor" Color="#6200EE"/>
        <sys:Double x:Key="HeaderSize">24</sys:Double>
        
        <Style x:Key="HeaderStyle" TargetType="TextBlock">
            <Setter Property="FontSize" Value="{StaticResource HeaderSize}"/>
            <Setter Property="Foreground" Value="{StaticResource AccentColor}"/>
            <Setter Property="FontWeight" Value="Bold"/>
        </Style>
    </Window.Resources>

    <Grid Margin="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>

        <!-- Header với StaticResource -->
        <TextBlock Grid.Row="0"
                   Text="XAML Advanced Demo"
                   Style="{StaticResource HeaderStyle}"/>

        <!-- Content với Binding -->
        <StackPanel Grid.Row="1" Margin="0,20">
            <Slider x:Name="fontSlider" 
                    Minimum="10" Maximum="40" Value="14"/>
            
            <!-- ElementName Binding -->
            <TextBlock Text="Dynamic Font Size"
                       FontSize="{Binding ElementName=fontSlider, Path=Value}"
                       Margin="0,10"/>
            
            <!-- RelativeSource Self -->
            <Border BorderBrush="{StaticResource AccentColor}"
                    BorderThickness="2"
                    CornerRadius="10"
                    Padding="20"
                    Margin="0,10"
                    Width="{Binding RelativeSource={RelativeSource Self}, 
                                    Path=Height}">
                <TextBlock Text="Square Box" 
                           HorizontalAlignment="Center"/>
            </Border>
        </StackPanel>

        <!-- Footer -->
        <TextBlock Grid.Row="2"
                   Text="{x:Static sys:DateTime.Now}"
                   HorizontalAlignment="Right"
                   Foreground="Gray"/>
    </Grid>
</Window>
```

```csharp
// Constants.cs
namespace XAMLAdvanced
{
    public static class Constants
    {
        public static string AppTitle => "XAML Advanced Demo";
    }
}
```

---

## 📝 Bài Tập

1. Tạo một form với:
   - StaticResource cho colors và fonts
   - Binding giữa Slider và opacity của Button
   - x:Static để hiển thị tên máy tính

2. Giải thích sự khác nhau giữa:
   - `{Binding Path=Value}` và `{Binding Value}`
   - `{StaticResource X}` và `{DynamicResource X}`

3. Viết XAML sử dụng RelativeSource AncestorType

---

## 📚 Tài Liệu Tham Khảo
- [Markup Extensions](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/markup-extensions-and-wpf-xaml)
- [Attached Properties Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/attached-properties-overview)

---

⬅️ **Bài trước**: [Bài 2.1: Giới Thiệu XAML](./Bai_2.1_Gioi_Thieu_XAML.md)  
➡️ **Bài tiếp theo**: [Bài 3.1: Layout Containers](../03_Controls_Layouts/Bai_3.1_Layout_Containers.md)
