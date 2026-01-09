# Bài 1.1: WPF Là Gì?

## 🎯 Mục Tiêu Bài Học
- Hiểu WPF là gì và tại sao nên sử dụng
- Phân biệt được WPF với WinForms
- Nắm vững kiến trúc của WPF

---

## 1. WPF Là Gì?

**WPF (Windows Presentation Foundation)** là một framework để xây dựng ứng dụng desktop trên Windows, được Microsoft giới thiệu từ .NET Framework 3.0.

### Đặc điểm chính:
- **XAML-based UI**: Sử dụng XAML (XML) để định nghĩa giao diện
- **Vector Graphics**: Đồ họa vector, scale tốt trên mọi độ phân giải
- **Hardware Acceleration**: Sử dụng DirectX để render, tận dụng GPU
- **Rich Data Binding**: Hệ thống binding mạnh mẽ
- **Styling & Templating**: Tùy chỉnh giao diện linh hoạt

```
┌─────────────────────────────────────────┐
│           WPF Application               │
├─────────────────────────────────────────┤
│  XAML (UI Definition)                   │
│  + Code-behind (C#/VB.NET)              │
├─────────────────────────────────────────┤
│  PresentationFramework                  │
│  (Controls, Layouts, Data Binding)      │
├─────────────────────────────────────────┤
│  PresentationCore                       │
│  (Visual, Drawing, Events)              │
├─────────────────────────────────────────┤
│  milcore (Media Integration Layer)      │
│  (DirectX, Hardware Acceleration)       │
└─────────────────────────────────────────┘
```

---

## 2. So Sánh WPF vs WinForms

| Tiêu Chí | WinForms | WPF |
|----------|----------|-----|
| **Ra đời** | 2002 (.NET 1.0) | 2006 (.NET 3.0) |
| **Render Engine** | GDI+ (CPU) | DirectX (GPU) |
| **UI Definition** | Code hoặc Designer | XAML + Code-behind |
| **Resolution** | Fixed pixels | Resolution Independent |
| **Data Binding** | Cơ bản | Mạnh mẽ, TwoWay binding |
| **Styling** | Limited | Styles, Templates, Triggers |
| **Animation** | Manual | Built-in animation system |
| **Learning Curve** | Dễ | Khó hơn |
| **Performance (UI phức tạp)** | Chậm | Nhanh hơn |

### Khi nào dùng WPF?
✅ Ứng dụng cần UI đẹp, phức tạp  
✅ Cần Data Binding mạnh  
✅ Ứng dụng cần scale tốt trên nhiều màn hình  
✅ Áp dụng pattern MVVM  

### Khi nào dùng WinForms?
✅ Ứng dụng đơn giản, utility tools  
✅ Team quen thuộc WinForms  
✅ Maintain legacy code  

---

## 3. Kiến Trúc WPF

### 3.1 Các Layer Chính

```csharp
// Layer 1: PresentationFramework.dll
// - Controls: Button, TextBox, DataGrid...
// - Layouts: Grid, StackPanel, DockPanel...
// - Data Binding, Resources, Styles

// Layer 2: PresentationCore.dll  
// - Visual class (base cho rendering)
// - Drawing primitives
// - Input events

// Layer 3: milcore.dll (unmanaged)
// - DirectX integration
// - Hardware composition
// - Rendering engine
```

### 3.2 Visual Tree vs Logical Tree

```xml
<!-- XAML (Logical Tree) -->
<Button Content="Click Me"/>

<!-- Visual Tree (thực tế render) -->
<!--
Button
├── ButtonChrome (Border, Background)
│   └── ContentPresenter
│       └── TextBlock "Click Me"
-->
```

**Logical Tree**: Cấu trúc UI bạn định nghĩa trong XAML
**Visual Tree**: Cấu trúc thực tế được render (bao gồm templates)

---

## 4. Ưu Điểm Của WPF

### 4.1 Separation of Concerns
```
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│    XAML      │    │  ViewModel   │    │    Model     │
│  (View/UI)   │◄───│   (Logic)    │◄───│   (Data)     │
└──────────────┘    └──────────────┘    └──────────────┘
     Designer          Developer           Developer
```

### 4.2 Rich Styling
```xml
<!-- Một Style có thể áp dụng cho nhiều controls -->
<Style TargetType="Button">
    <Setter Property="Background" Value="#2196F3"/>
    <Setter Property="Foreground" Value="White"/>
    <Setter Property="FontSize" Value="14"/>
    <Setter Property="Padding" Value="20,10"/>
    <Style.Triggers>
        <Trigger Property="IsMouseOver" Value="True">
            <Setter Property="Background" Value="#1976D2"/>
        </Trigger>
    </Style.Triggers>
</Style>
```

### 4.3 Powerful Data Binding
```xml
<!-- Binding tự động cập nhật UI khi data thay đổi -->
<TextBlock Text="{Binding UserName}"/>
<TextBox Text="{Binding Email, UpdateSourceTrigger=PropertyChanged}"/>
<ListBox ItemsSource="{Binding Products}"/>
```

### 4.4 Resolution Independence
```xml
<!-- Sử dụng device-independent units (1/96 inch) -->
<!-- UI tự scale trên các màn hình khác nhau -->
<Button Width="100" Height="40"/>
```

---

## 5. Hello World - WPF

### MainWindow.xaml
```xml
<Window x:Class="HelloWPF.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Hello WPF" Height="200" Width="300">
    <Grid>
        <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
            <TextBlock Text="Chào mừng đến với WPF!" 
                       FontSize="20" 
                       FontWeight="Bold"
                       Foreground="#2196F3"/>
            <Button Content="Click Me!" 
                    Margin="0,20,0,0"
                    Padding="20,10"
                    Click="Button_Click"/>
        </StackPanel>
    </Grid>
</Window>
```

### MainWindow.xaml.cs (Code-behind)
```csharp
using System.Windows;

namespace HelloWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello từ WPF!", "Thông báo");
        }
    }
}
```

---

## 📝 Bài Tập

1. Liệt kê 5 ưu điểm chính của WPF so với WinForms
2. Giải thích sự khác biệt giữa Logical Tree và Visual Tree
3. WPF sử dụng công nghệ gì để render đồ họa?

---

## 📚 Tài Liệu Tham Khảo
- [Microsoft Docs - WPF Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/overview/)
- [WPF Architecture](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/wpf-architecture)

---

➡️ **Bài tiếp theo**: [Bài 1.2: Thiết Lập Môi Trường](./Bai_1.2_Thiet_Lap_Moi_Truong.md)
