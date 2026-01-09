# Bài 3.1: Layout Containers

## 🎯 Mục Tiêu Bài Học
- Hiểu và sử dụng các Layout Containers trong WPF
- Biết khi nào nên dùng layout nào
- Kết hợp nhiều layouts để tạo UI phức tạp

---

## 1. Tổng Quan Về Layout

WPF sử dụng **Layout Containers (Panels)** để sắp xếp và định vị các controls con.

```
┌────────────────────────────────────────┐
│ Panel (Layout Container)               │
│  ┌──────┐ ┌──────┐ ┌──────┐           │
│  │Child │ │Child │ │Child │ ...       │
│  └──────┘ └──────┘ └──────┘           │
└────────────────────────────────────────┘
```

### Các Layout Chính

| Layout | Mô Tả | Use Case |
|--------|-------|----------|
| **Grid** | Chia thành rows/columns | Form layouts, complex UIs |
| **StackPanel** | Xếp chồng theo 1 hướng | Lists, toolbars |
| **WrapPanel** | Wrap xuống dòng khi hết chỗ | Tags, thumbnails |
| **DockPanel** | Dock vào các cạnh | Main window layout |
| **Canvas** | Định vị tuyệt đối (x, y) | Drawing, games |
| **UniformGrid** | Grid đều các ô | Calculators, keyboards |

---

## 2. Grid

**Grid** là layout phổ biến và mạnh mẽ nhất trong WPF.

### 2.1 Cơ Bản

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>    <!-- Chiều cao tự động -->
        <RowDefinition Height="100"/>     <!-- Chiều cao cố định -->
        <RowDefinition Height="*"/>       <!-- Chiếm phần còn lại -->
        <RowDefinition Height="2*"/>      <!-- Chiếm 2 phần -->
    </Grid.RowDefinitions>
    
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto"/>
        <ColumnDefinition Width="150"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>
    
    <!-- Đặt controls -->
    <TextBlock Grid.Row="0" Grid.Column="0" Text="Row 0, Col 0"/>
    <TextBox Grid.Row="0" Grid.Column="1" Text="Row 0, Col 1"/>
    <Button Grid.Row="1" Grid.Column="0" Content="Row 1, Col 0"/>
</Grid>
```

### 2.2 Column/Row Spanning

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="50"/>
        <RowDefinition Height="*"/>
        <RowDefinition Height="30"/>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>

    <!-- Span 3 columns -->
    <TextBlock Grid.Row="0" Grid.Column="0" Grid.ColumnSpan="3"
               Text="Header spanning all columns"
               Background="#2196F3" Foreground="White"
               TextAlignment="Center" Padding="10"/>

    <!-- Span 2 rows -->
    <Border Grid.Row="1" Grid.Column="0" Grid.RowSpan="2"
            Background="#E3F2FD" Margin="5">
        <TextBlock Text="Sidebar" VerticalAlignment="Center" 
                   HorizontalAlignment="Center"/>
    </Border>
</Grid>
```

### 2.3 Star Sizing

```xml
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>    <!-- 1 phần -->
        <ColumnDefinition Width="2*"/>   <!-- 2 phần -->
        <ColumnDefinition Width="3*"/>   <!-- 3 phần -->
    </Grid.ColumnDefinitions>
    
    <!-- Tổng 6 phần: 1/6, 2/6, 3/6 = 16.7%, 33.3%, 50% -->
</Grid>
```

### 2.4 SharedSizeGroup

```xml
<Grid IsSharedSizeScope="True">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>
    
    <Grid Grid.Row="0">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" SharedSizeGroup="Labels"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>
        <TextBlock Text="Username:" Grid.Column="0"/>
        <TextBox Grid.Column="1"/>
    </Grid>
    
    <Grid Grid.Row="1">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" SharedSizeGroup="Labels"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>
        <TextBlock Text="Email Address:" Grid.Column="0"/>
        <TextBox Grid.Column="1"/>
    </Grid>
</Grid>
```

---

## 3. StackPanel

Xếp chồng các elements theo một hướng (dọc hoặc ngang).

```xml
<!-- Vertical (mặc định) -->
<StackPanel>
    <Button Content="Button 1"/>
    <Button Content="Button 2"/>
    <Button Content="Button 3"/>
</StackPanel>

<!-- Horizontal -->
<StackPanel Orientation="Horizontal">
    <Button Content="Button 1" Margin="5"/>
    <Button Content="Button 2" Margin="5"/>
    <Button Content="Button 3" Margin="5"/>
</StackPanel>
```

### Khi Nào Dùng StackPanel?
✅ Menu items  
✅ Toolbar buttons  
✅ Form fields  
✅ List items  

---

## 4. WrapPanel

Giống StackPanel nhưng tự động wrap xuống dòng khi hết chỗ.

```xml
<WrapPanel>
    <Button Content="Button 1" Width="100" Height="40" Margin="5"/>
    <Button Content="Button 2" Width="100" Height="40" Margin="5"/>
    <Button Content="Button 3" Width="100" Height="40" Margin="5"/>
    <Button Content="Button 4" Width="100" Height="40" Margin="5"/>
    <Button Content="Button 5" Width="100" Height="40" Margin="5"/>
    <!-- Tự động wrap khi window resize -->
</WrapPanel>

<!-- Vertical orientation -->
<WrapPanel Orientation="Vertical" Height="200">
    <!-- Wrap theo cột -->
</WrapPanel>
```

### Khi Nào Dùng WrapPanel?
✅ Photo gallery thumbnails  
✅ Tag clouds  
✅ Responsive button groups  

---

## 5. DockPanel

Dock các elements vào các cạnh (Top, Bottom, Left, Right).

```xml
<DockPanel LastChildFill="True">
    <!-- Dock theo thứ tự -->
    <Menu DockPanel.Dock="Top" Height="25">
        <MenuItem Header="File"/>
        <MenuItem Header="Edit"/>
    </Menu>
    
    <StatusBar DockPanel.Dock="Bottom" Height="25">
        <TextBlock Text="Ready"/>
    </StatusBar>
    
    <Border DockPanel.Dock="Left" Width="200" Background="#F5F5F5">
        <TextBlock Text="Sidebar"/>
    </Border>
    
    <!-- LastChildFill="True": Element cuối chiếm phần còn lại -->
    <Border Background="White">
        <TextBlock Text="Main Content Area"/>
    </Border>
</DockPanel>
```

### Thứ Tự Dock Quan Trọng!

```
┌─────────────────────────────────────┐
│ Menu (Dock="Top")                   │
├─────────┬───────────────────────────┤
│         │                           │
│ Sidebar │     Main Content          │
│ (Left)  │     (Fill)                │
│         │                           │
├─────────┴───────────────────────────┤
│ StatusBar (Dock="Bottom")           │
└─────────────────────────────────────┘
```

---

## 6. Canvas

Định vị tuyệt đối bằng tọa độ (x, y).

```xml
<Canvas Background="#ECEFF1">
    <!-- Positioned from Top-Left corner -->
    <Rectangle Canvas.Left="50" Canvas.Top="50"
               Width="100" Height="60"
               Fill="#F44336"/>
    
    <Ellipse Canvas.Left="200" Canvas.Top="80"
             Width="80" Height="80"
             Fill="#2196F3"/>
    
    <!-- Z-Index for layering -->
    <Rectangle Canvas.Left="100" Canvas.Top="100"
               Width="150" Height="100"
               Fill="#4CAF50"
               Panel.ZIndex="1"/>
    
    <!-- Positioned from Bottom-Right -->
    <Button Canvas.Right="20" Canvas.Bottom="20"
            Content="Bottom Right"
            Padding="10"/>
</Canvas>
```

### Khi Nào Dùng Canvas?
✅ Drawing applications  
✅ Game development  
✅ Diagram/flowchart editors  
✅ Custom positioning  

---

## 7. UniformGrid

Grid với tất cả cells có kích thước bằng nhau.

```xml
<!-- Tự động chia -->
<UniformGrid Rows="3" Columns="3">
    <Button Content="1"/>
    <Button Content="2"/>
    <Button Content="3"/>
    <Button Content="4"/>
    <Button Content="5"/>
    <Button Content="6"/>
    <Button Content="7"/>
    <Button Content="8"/>
    <Button Content="9"/>
</UniformGrid>

<!-- Calculator layout -->
<UniformGrid Rows="4" Columns="4" Margin="5">
    <Button Content="7" Margin="2"/>
    <Button Content="8" Margin="2"/>
    <Button Content="9" Margin="2"/>
    <Button Content="÷" Margin="2"/>
    <Button Content="4" Margin="2"/>
    <Button Content="5" Margin="2"/>
    <Button Content="6" Margin="2"/>
    <Button Content="×" Margin="2"/>
    <!-- ... -->
</UniformGrid>
```

---

## 8. Alignment và Margin

### 8.1 Alignment

```xml
<!-- HorizontalAlignment: Left, Center, Right, Stretch -->
<!-- VerticalAlignment: Top, Center, Bottom, Stretch -->

<Grid>
    <Button Content="Top-Left"
            HorizontalAlignment="Left"
            VerticalAlignment="Top"/>
    
    <Button Content="Center"
            HorizontalAlignment="Center"
            VerticalAlignment="Center"/>
    
    <Button Content="Stretch Horizontal"
            HorizontalAlignment="Stretch"
            VerticalAlignment="Bottom"
            Height="40"/>
</Grid>
```

### 8.2 Margin và Padding

```xml
<!-- Margin: Khoảng cách BÊN NGOÀI element -->
<Button Margin="10"/>               <!-- All sides = 10 -->
<Button Margin="10,20"/>            <!-- Left/Right=10, Top/Bottom=20 -->
<Button Margin="10,20,30,40"/>      <!-- Left, Top, Right, Bottom -->

<!-- Padding: Khoảng cách BÊN TRONG element -->
<Button Padding="20,10" Content="Padded"/>
<Border Padding="15">
    <TextBlock Text="Content with padding"/>
</Border>
```

---

## 9. Kết Hợp Layouts

```xml
<DockPanel>
    <!-- Header -->
    <Border DockPanel.Dock="Top" Background="#1565C0" Padding="15">
        <TextBlock Text="My Application" Foreground="White" FontSize="20"/>
    </Border>
    
    <!-- Sidebar -->
    <StackPanel DockPanel.Dock="Left" Width="200" Background="#E3F2FD">
        <Button Content="Dashboard" Margin="10"/>
        <Button Content="Users" Margin="10"/>
        <Button Content="Settings" Margin="10"/>
    </StackPanel>
    
    <!-- Main Content -->
    <Grid Margin="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        
        <TextBlock Grid.Row="0" Text="Welcome!" FontSize="24"/>
        
        <WrapPanel Grid.Row="1" Margin="0,20,0,0">
            <Border Width="150" Height="100" Background="#FFCDD2" Margin="10"/>
            <Border Width="150" Height="100" Background="#C8E6C9" Margin="10"/>
            <Border Width="150" Height="100" Background="#BBDEFB" Margin="10"/>
        </WrapPanel>
    </Grid>
</DockPanel>
```

---

## 📝 Bài Tập

1. Tạo layout cho form đăng ký với Grid:
   - Labels ở column 0
   - TextBoxes ở column 1
   - Buttons ở row cuối, căn phải

2. Tạo calculator UI với UniformGrid

3. Tạo main window layout với DockPanel:
   - Menu bar (Top)
   - Sidebar (Left)
   - Status bar (Bottom)
   - Content area (Fill)

---

## 📚 Tài Liệu Tham Khảo
- [Panels Overview](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/controls/panels-overview)
- [Layout](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/advanced/layout)

---

⬅️ **Bài trước**: [Bài 2.2: XAML Nâng Cao](../02_XAML_Co_Ban/Bai_2.2_XAML_Nang_Cao.md)  
➡️ **Bài tiếp theo**: [Bài 3.2: Basic Controls](./Bai_3.2_Basic_Controls.md)
