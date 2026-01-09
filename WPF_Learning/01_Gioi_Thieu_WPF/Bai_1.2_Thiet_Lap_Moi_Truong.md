# Bài 1.2: Thiết Lập Môi Trường Phát Triển WPF

## 🎯 Mục Tiêu Bài Học
- Cài đặt Visual Studio 2022 với WPF workload
- Tạo project WPF đầu tiên
- Hiểu cấu trúc project WPF
- Chạy và debug ứng dụng

---

## 1. Cài Đặt Visual Studio 2022

### Bước 1: Tải Visual Studio
1. Truy cập [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/)
2. Tải **Visual Studio 2022 Community** (miễn phí)

### Bước 2: Chọn Workloads
Trong Visual Studio Installer, chọn:
- ✅ **.NET desktop development**

![Workload Selection](https://docs.microsoft.com/en-us/visualstudio/install/media/vs-2022/vs-installer-workloads.png)

### Bước 3: Kiểm tra .NET SDK
Mở Command Prompt/PowerShell:
```powershell
dotnet --list-sdks
```

Kết quả mong đợi:
```
6.0.xxx [C:\Program Files\dotnet\sdk]
7.0.xxx [C:\Program Files\dotnet\sdk]
8.0.xxx [C:\Program Files\dotnet\sdk]
```

---

## 2. Tạo Project WPF Đầu Tiên

### Cách 1: Sử dụng Visual Studio

1. **File → New → Project**
2. Tìm kiếm "**WPF Application**"
3. Chọn **WPF Application** (C#, .NET)
4. Đặt tên project: `MyFirstWPFApp`
5. Chọn **.NET 8.0** (hoặc version mới nhất)
6. Click **Create**

### Cách 2: Sử dụng .NET CLI

```powershell
# Tạo project WPF
dotnet new wpf -n MyFirstWPFApp

# Di chuyển vào thư mục project
cd MyFirstWPFApp

# Mở bằng Visual Studio Code (tùy chọn)
code .

# Hoặc chạy ngay
dotnet run
```

---

## 3. Cấu Trúc Project WPF

```
MyFirstWPFApp/
├── 📁 bin/                    # Output compiled files
├── 📁 obj/                    # Intermediate files
├── 📄 App.xaml               # Application configuration
├── 📄 App.xaml.cs            # Application code-behind
├── 📄 MainWindow.xaml        # Main window UI
├── 📄 MainWindow.xaml.cs     # Main window code-behind
├── 📄 MyFirstWPFApp.csproj   # Project file
└── 📄 AssemblyInfo.cs        # Assembly metadata
```

### 3.1 File App.xaml
```xml
<Application x:Class="MyFirstWPFApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <!-- Global resources, styles go here -->
    </Application.Resources>
</Application>
```

**Giải thích:**
- `StartupUri`: Window đầu tiên được mở khi app chạy
- `Application.Resources`: Nơi định nghĩa resources dùng chung

### 3.2 File App.xaml.cs
```csharp
using System.Windows;

namespace MyFirstWPFApp
{
    public partial class App : Application
    {
        // Override OnStartup để custom logic khởi động
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Initialization code here
        }
    }
}
```

### 3.3 File MainWindow.xaml
```xml
<Window x:Class="MyFirstWPFApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <!-- UI elements go here -->
    </Grid>
</Window>
```

**Giải thích các namespace:**
| Prefix | URI | Ý Nghĩa |
|--------|-----|---------|
| (default) | `http://schemas.microsoft.com/.../presentation` | WPF controls chuẩn |
| `x:` | `http://schemas.microsoft.com/.../xaml` | XAML language features |

### 3.4 File .csproj
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <UseWPF>true</UseWPF>
  </PropertyGroup>
</Project>
```

**Giải thích:**
- `OutputType`: WinExe (Windows Executable, không hiện console)
- `TargetFramework`: .NET 8.0 cho Windows
- `UseWPF`: Enable WPF framework

---

## 4. Chạy và Debug Ứng Dụng

### 4.1 Chạy Ứng Dụng
- **F5**: Start with Debugging
- **Ctrl + F5**: Start without Debugging
- **CLI**: `dotnet run`

### 4.2 Debug Basics

#### Breakpoints
```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    // Đặt breakpoint ở đây (F9)
    string message = "Hello WPF!";  // ← Click vào margin trái
    MessageBox.Show(message);
}
```

#### Debug Windows
- **Locals**: Xem biến local
- **Watch**: Theo dõi expression cụ thể
- **Call Stack**: Xem stack trace
- **Immediate**: Chạy code real-time

### 4.3 Hot Reload (VS 2022)
- Sửa XAML → UI cập nhật ngay khi đang chạy
- Tiết kiệm thời gian development

---

## 5. Thực Hành: Tạo Ứng Dụng Đầu Tiên

### Yêu Cầu
Tạo một ứng dụng WPF với:
- Một TextBox để nhập tên
- Một Button "Greet"
- Một TextBlock hiển thị lời chào

### MainWindow.xaml
```xml
<Window x:Class="MyFirstWPFApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Greeting App" Height="200" Width="400"
        WindowStartupLocation="CenterScreen">
    <Grid Margin="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>

        <!-- Row 0: Input -->
        <TextBlock Text="Tên của bạn:" 
                   Grid.Row="0" Grid.Column="0"
                   VerticalAlignment="Center"
                   Margin="0,0,10,0"/>
        <TextBox x:Name="txtName" 
                 Grid.Row="0" Grid.Column="1"
                 Padding="5"/>

        <!-- Row 1: Button -->
        <Button Content="Chào!" 
                Grid.Row="1" Grid.Column="1"
                Margin="0,15,0,15"
                Padding="20,8"
                HorizontalAlignment="Left"
                Click="BtnGreet_Click"/>

        <!-- Row 2: Output -->
        <TextBlock x:Name="txtGreeting" 
                   Grid.Row="2" Grid.Column="0" Grid.ColumnSpan="2"
                   FontSize="18"
                   FontWeight="Bold"
                   Foreground="#4CAF50"/>
    </Grid>
</Window>
```

### MainWindow.xaml.cs
```csharp
using System.Windows;

namespace MyFirstWPFApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnGreet_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            
            if (string.IsNullOrEmpty(name))
            {
                txtGreeting.Text = "Vui lòng nhập tên!";
                txtGreeting.Foreground = System.Windows.Media.Brushes.Red;
            }
            else
            {
                txtGreeting.Text = $"Xin chào, {name}! 👋";
                txtGreeting.Foreground = System.Windows.Media.Brushes.Green;
            }
        }
    }
}
```

---

## 📝 Bài Tập

1. Tạo project WPF mới tên `Calculator`
2. Thêm 2 TextBox cho 2 số
3. Thêm 4 Button: +, -, *, /
4. Hiển thị kết quả khi click button

---

## ⚠️ Lỗi Thường Gặp

| Lỗi | Nguyên Nhân | Cách Sửa |
|-----|-------------|----------|
| "Could not find type" | Thiếu namespace | Thêm `xmlns:local="clr-namespace:..."` |
| "x:Name not found" | Chưa build | Build project (Ctrl+Shift+B) |
| Window không hiện | StartupUri sai | Kiểm tra App.xaml |

---

## 📚 Tài Liệu Tham Khảo
- [Getting Started with WPF](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/get-started/)
- [Visual Studio Installation](https://docs.microsoft.com/en-us/visualstudio/install/)

---

⬅️ **Bài trước**: [Bài 1.1: WPF Là Gì?](./Bai_1.1_WPF_La_Gi.md)  
➡️ **Bài tiếp theo**: [Bài 2.1: Giới Thiệu XAML](../02_XAML_Co_Ban/Bai_2.1_Gioi_Thieu_XAML.md)
