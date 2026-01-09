# 📚 LỘ TRÌNH HỌC WPF (Windows Presentation Foundation)

> **Mục tiêu**: Nắm vững WPF từ cơ bản đến nâng cao, xây dựng được ứng dụng Desktop hoàn chỉnh với .NET

---

## 🎯 Tổng Quan Khóa Học

| Phần | Nội Dung | Thời Gian Ước Tính |
|------|----------|-------------------|
| 1 | Giới Thiệu WPF & Setup | 1-2 ngày |
| 2 | XAML Cơ Bản | 2-3 ngày |
| 3 | Controls & Layouts | 3-4 ngày |
| 4 | Data Binding | 3-4 ngày |
| 5 | Styles & Templates | 2-3 ngày |
| 6 | Commands & Events | 2-3 ngày |
| 7 | MVVM Pattern | 4-5 ngày |
| 8 | Navigation & Windows | 2-3 ngày |
| 9 | Resources & Themes | 2-3 ngày |
| 10 | Entity Framework + WPF | 3-4 ngày |
| 11 | Dự Án Thực Hành | 5-7 ngày |

**Tổng thời gian**: ~4-5 tuần

---

## 📖 CHI TIẾT CÁC BÀI HỌC

---

### 📘 PHẦN 1: GIỚI THIỆU WPF & THIẾT LẬP MÔI TRƯỜNG

#### Bài 1.1: WPF Là Gì?
- [ ] WPF là gì và tại sao sử dụng WPF?
- [ ] So sánh WPF vs WinForms
- [ ] Kiến trúc của WPF (PresentationFramework, PresentationCore, milcore)
- [ ] Các ưu điểm của WPF: Graphics, Data Binding, Styling

#### Bài 1.2: Thiết Lập Môi Trường
- [ ] Cài đặt Visual Studio 2022
- [ ] Cài đặt .NET 6/7/8 SDK
- [ ] Tạo project WPF đầu tiên (WPF App .NET)
- [ ] Cấu trúc project WPF (App.xaml, MainWindow.xaml)
- [ ] Chạy và debug ứng dụng WPF

---

### 📘 PHẦN 2: XAML CƠ BẢN

#### Bài 2.1: Giới Thiệu XAML
- [ ] XAML là gì? (eXtensible Application Markup Language)
- [ ] Cú pháp XAML cơ bản
- [ ] Elements và Attributes
- [ ] Namespace trong XAML (xmlns)
- [ ] Property Elements vs Attribute Syntax

#### Bài 2.2: XAML Nâng Cao
- [ ] Attached Properties
- [ ] Markup Extensions ({Binding}, {StaticResource}, {x:Type})
- [ ] Content Property
- [ ] Type Converters
- [ ] Code-behind và x:Name

---

### 📘 PHẦN 3: CONTROLS & LAYOUTS

#### Bài 3.1: Layout Containers
- [ ] **Grid**: Chia lưới, RowDefinitions, ColumnDefinitions
- [ ] **StackPanel**: Xếp chồng theo chiều dọc/ngang
- [ ] **WrapPanel**: Tự động xuống dòng
- [ ] **DockPanel**: Dock các cạnh (Top, Left, Right, Bottom)
- [ ] **Canvas**: Định vị tuyệt đối
- [ ] **UniformGrid**: Chia đều các ô

#### Bài 3.2: Basic Controls
- [ ] **TextBlock** & **Label**: Hiển thị text
- [ ] **TextBox** & **PasswordBox**: Nhập liệu
- [ ] **Button** & **ToggleButton**: Nút bấm
- [ ] **CheckBox** & **RadioButton**: Chọn lựa
- [ ] **ComboBox** & **ListBox**: Danh sách chọn
- [ ] **Image**: Hiển thị hình ảnh

#### Bài 3.3: Advanced Controls
- [ ] **DataGrid**: Hiển thị dữ liệu dạng bảng
- [ ] **ListView**: Danh sách với multiple columns
- [ ] **TreeView**: Cấu trúc cây
- [ ] **TabControl**: Tab pages
- [ ] **Expander** & **GroupBox**: Nhóm controls
- [ ] **Menu** & **ContextMenu**: Menu ứng dụng
- [ ] **ToolBar** & **StatusBar**: Thanh công cụ

#### Bài 3.4: Thực Hành
- [ ] Tạo form đăng nhập
- [ ] Tạo form nhập liệu thông tin sinh viên
- [ ] Tạo layout dashboard cơ bản

---

### 📘 PHẦN 4: DATA BINDING

#### Bài 4.1: Data Binding Cơ Bản
- [ ] Binding là gì và tại sao cần binding?
- [ ] Binding Modes: OneWay, TwoWay, OneTime, OneWayToSource
- [ ] DataContext là gì?
- [ ] Binding to Properties
- [ ] INotifyPropertyChanged interface

#### Bài 4.2: Data Binding Nâng Cao
- [ ] ElementName Binding
- [ ] RelativeSource Binding
- [ ] UpdateSourceTrigger
- [ ] Binding to Collections (ObservableCollection)
- [ ] ICollectionView cho Sorting/Filtering

#### Bài 4.3: Value Converters
- [ ] IValueConverter interface
- [ ] Tạo và sử dụng Converter
- [ ] Built-in Converters
- [ ] MultiBinding & IMultiValueConverter
- [ ] Các Converter phổ biến: BoolToVisibility, DateTimeFormatter

#### Bài 4.4: Data Validation
- [ ] IDataErrorInfo
- [ ] INotifyDataErrorInfo
- [ ] Validation Rules
- [ ] Error Templates
- [ ] Hiển thị thông báo lỗi

---

### 📘 PHẦN 5: STYLES & TEMPLATES

#### Bài 5.1: Styles
- [ ] Inline Styles
- [ ] Named Styles
- [ ] BasedOn Styles (Kế thừa Style)
- [ ] Implicit Styles (TargetType)
- [ ] Triggers: Property Triggers, Data Triggers, Event Triggers

#### Bài 5.2: Control Templates
- [ ] ControlTemplate là gì?
- [ ] Tùy chỉnh giao diện control
- [ ] TemplateBinding
- [ ] ContentPresenter
- [ ] Tạo Custom Button, TextBox

#### Bài 5.3: Data Templates
- [ ] DataTemplate là gì?
- [ ] Sử dụng với ItemsControl, ListBox, ComboBox
- [ ] DataTemplateSelector
- [ ] Hierarchical Data Templates (TreeView)

---

### 📘 PHẦN 6: COMMANDS & EVENTS

#### Bài 6.1: Routed Events
- [ ] Bubble Events
- [ ] Tunnel Events (Preview)
- [ ] Direct Events
- [ ] Event Handlers trong XAML và Code-behind
- [ ] Handled Property

#### Bài 6.2: Commands
- [ ] ICommand interface
- [ ] Built-in Commands (ApplicationCommands, NavigationCommands)
- [ ] Custom Commands với RelayCommand/DelegateCommand
- [ ] CommandParameter
- [ ] CanExecute và Enable/Disable logic

---

### 📘 PHẦN 7: MVVM PATTERN

#### Bài 7.1: Giới Thiệu MVVM
- [ ] MVVM là gì? (Model-View-ViewModel)
- [ ] Tại sao sử dụng MVVM?
- [ ] Cấu trúc thư mục MVVM
- [ ] Separation of Concerns

#### Bài 7.2: Xây Dựng Base Classes
- [ ] ViewModelBase với INotifyPropertyChanged
- [ ] RelayCommand Implementation
- [ ] ObservableObject Pattern

#### Bài 7.3: Kết Nối View và ViewModel
- [ ] DataContext trong XAML
- [ ] ViewModelLocator Pattern
- [ ] Dependency Injection với MVVM
- [ ] Navigation trong MVVM

#### Bài 7.4: MVVM Frameworks
- [ ] Giới thiệu CommunityToolkit.Mvvm
- [ ] [ObservableProperty] Attribute
- [ ] [RelayCommand] Attribute
- [ ] Messenger Pattern

---

### 📘 PHẦN 8: NAVIGATION & WINDOWS

#### Bài 8.1: Windows & Dialogs
- [ ] Mở Window mới
- [ ] Modal vs Non-Modal Windows
- [ ] MessageBox
- [ ] Custom Dialogs
- [ ] WindowStyle & WindowState

#### Bài 8.2: Navigation
- [ ] Frame và Page Navigation
- [ ] NavigationService
- [ ] Passing Parameters giữa các pages
- [ ] Navigation với MVVM
- [ ] UserControl-based Navigation

---

### 📘 PHẦN 9: RESOURCES & THEMES

#### Bài 9.1: Resources
- [ ] Static Resources vs Dynamic Resources
- [ ] Resource Dictionaries
- [ ] Merged Resource Dictionaries
- [ ] Application-level Resources
- [ ] Finding Resources at Runtime

#### Bài 9.2: Theming
- [ ] Light/Dark Theme
- [ ] Theme Switching
- [ ] Sử dụng Material Design, MahApps.Metro
- [ ] Custom Theme Creation

---

### 📘 PHẦN 10: ENTITY FRAMEWORK CORE + WPF

#### Bài 10.1: Tích Hợp EF Core
- [ ] Cài đặt EF Core packages
- [ ] Tạo DbContext
- [ ] Migrations
- [ ] Code-First vs Database-First

#### Bài 10.2: CRUD với WPF
- [ ] Repository Pattern
- [ ] Service Layer
- [ ] Hiển thị dữ liệu trong DataGrid
- [ ] Thêm, Sửa, Xóa với Bindings
- [ ] Async/Await trong WPF

---

### 📘 PHẦN 11: DỰ ÁN THỰC HÀNH

#### Dự Án 1: Quản Lý Sinh Viên (Beginner)
- [ ] CRUD sinh viên
- [ ] Tìm kiếm, lọc, sắp xếp
- [ ] Export to Excel/PDF
- [ ] Validation

#### Dự Án 2: Quản Lý Bán Hàng (Intermediate)
- [ ] Quản lý sản phẩm, khách hàng
- [ ] Tạo đơn hàng
- [ ] Thống kê, báo cáo
- [ ] Dashboard với Charts

#### Dự Án 3: Ứng Dụng Hoàn Chỉnh (Advanced)
- [ ] MVVM + EF Core + SQL Server
- [ ] Authentication & Authorization
- [ ] Multi-window application
- [ ] Printing & Reporting
- [ ] Deployment với ClickOnce/MSIX

---

## 📂 CẤU TRÚC THƯ MỤC HỌC TẬP

```
WPF_Learning/
├── 01_Gioi_Thieu_WPF/
│   ├── Bai_1.1_WPF_La_Gi.md
│   └── Bai_1.2_Thiet_Lap_Moi_Truong.md
├── 02_XAML_Co_Ban/
│   ├── Bai_2.1_Gioi_Thieu_XAML.md
│   └── Bai_2.2_XAML_Nang_Cao.md
├── 03_Controls_Layouts/
│   ├── Bai_3.1_Layout_Containers.md
│   ├── Bai_3.2_Basic_Controls.md
│   ├── Bai_3.3_Advanced_Controls.md
│   └── Bai_3.4_Thuc_Hanh.md
├── 04_Data_Binding/
│   ├── Bai_4.1_Binding_Co_Ban.md
│   ├── Bai_4.2_Binding_Nang_Cao.md
│   ├── Bai_4.3_Value_Converters.md
│   └── Bai_4.4_Data_Validation.md
├── 05_Styles_Templates/
│   ├── Bai_5.1_Styles.md
│   ├── Bai_5.2_Control_Templates.md
│   └── Bai_5.3_Data_Templates.md
├── 06_Commands_Events/
│   ├── Bai_6.1_Routed_Events.md
│   └── Bai_6.2_Commands.md
├── 07_MVVM/
│   ├── Bai_7.1_Gioi_Thieu_MVVM.md
│   ├── Bai_7.2_Base_Classes.md
│   ├── Bai_7.3_Ket_Noi_View_ViewModel.md
│   └── Bai_7.4_MVVM_Frameworks.md
├── 08_Navigation_Windows/
│   ├── Bai_8.1_Windows_Dialogs.md
│   └── Bai_8.2_Navigation.md
├── 09_Resources_Themes/
│   ├── Bai_9.1_Resources.md
│   └── Bai_9.2_Theming.md
├── 10_EFCore_WPF/
│   ├── Bai_10.1_Tich_Hop_EFCore.md
│   └── Bai_10.2_CRUD_WPF.md
├── 11_Du_An_Thuc_Hanh/
│   ├── Project_1_Quan_Ly_Sinh_Vien/
│   ├── Project_2_Quan_Ly_Ban_Hang/
│   └── Project_3_Ung_Dung_Hoan_Chinh/
└── LO_TRINH_HOC_WPF.md (File này)
```

---

## 📌 TÀI LIỆU THAM KHẢO

### Tài Liệu Chính Thức
- [Microsoft WPF Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
- [XAML Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/xaml/)
- [WPF Tutorial](https://www.wpftutorial.net/)

### Video Courses
- Pluralsight: WPF Fundamentals
- Udemy: Complete WPF Course
- YouTube: AngelSix WPF Tutorial Series

### GitHub Repositories
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)
- [MahApps.Metro](https://github.com/MahApps/MahApps.Metro)
- [MaterialDesignInXAML](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)

---

## ✅ CHECKLIST TIẾN ĐỘ

| Phần | Trạng Thái | Ngày Hoàn Thành |
|------|-----------|-----------------|
| Phần 1: Giới Thiệu | ⬜ Chưa bắt đầu | |
| Phần 2: XAML | ⬜ Chưa bắt đầu | |
| Phần 3: Controls | ⬜ Chưa bắt đầu | |
| Phần 4: Data Binding | ⬜ Chưa bắt đầu | |
| Phần 5: Styles | ⬜ Chưa bắt đầu | |
| Phần 6: Commands | ⬜ Chưa bắt đầu | |
| Phần 7: MVVM | ⬜ Chưa bắt đầu | |
| Phần 8: Navigation | ⬜ Chưa bắt đầu | |
| Phần 9: Resources | ⬜ Chưa bắt đầu | |
| Phần 10: EF Core | ⬜ Chưa bắt đầu | |
| Phần 11: Dự Án | ⬜ Chưa bắt đầu | |

---

> 💡 **Ghi chú**: Đây là lộ trình học WPF toàn diện. Bạn có thể điều chỉnh thứ tự và thời gian theo nhu cầu cá nhân. Khuyến khích thực hành code song song với việc học lý thuyết!
