# Bài 3.3: Advanced Controls

## 🎯 Mục Tiêu Bài Học
- Sử dụng DataGrid để hiển thị dữ liệu dạng bảng
- Làm việc với ListView và TreeView
- Tạo Menu, ToolBar và TabControl

---

## 1. DataGrid

**DataGrid** là control mạnh mẽ để hiển thị và edit dữ liệu dạng bảng.

### 1.1 Basic DataGrid

```xml
<DataGrid x:Name="dgProducts"
          AutoGenerateColumns="True"
          Height="300"/>
```

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public MainWindow()
{
    InitializeComponent();
    
    dgProducts.ItemsSource = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Price = 999.99m, Stock = 10 },
        new Product { Id = 2, Name = "Mouse", Price = 29.99m, Stock = 50 },
        new Product { Id = 3, Name = "Keyboard", Price = 79.99m, Stock = 30 }
    };
}
```

### 1.2 Custom Columns

```xml
<DataGrid ItemsSource="{Binding Products}"
          AutoGenerateColumns="False"
          CanUserAddRows="False"
          CanUserDeleteRows="False"
          IsReadOnly="True"
          AlternatingRowBackground="#F5F5F5"
          GridLinesVisibility="Horizontal">
    
    <DataGrid.Columns>
        <!-- Text Column -->
        <DataGridTextColumn Header="ID" 
                            Binding="{Binding Id}"
                            Width="50"/>
        
        <DataGridTextColumn Header="Product Name" 
                            Binding="{Binding Name}"
                            Width="*"/>
        
        <!-- Format Price -->
        <DataGridTextColumn Header="Price" 
                            Binding="{Binding Price, StringFormat='{}{0:C}'}"
                            Width="100"/>
        
        <!-- ComboBox Column -->
        <DataGridComboBoxColumn Header="Category"
                                SelectedItemBinding="{Binding Category}"
                                ItemsSource="{Binding Source={StaticResource Categories}}"/>
        
        <!-- CheckBox Column -->
        <DataGridCheckBoxColumn Header="In Stock"
                                Binding="{Binding IsInStock}"
                                Width="80"/>
        
        <!-- Template Column (custom) -->
        <DataGridTemplateColumn Header="Actions" Width="150">
            <DataGridTemplateColumn.CellTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal">
                        <Button Content="Edit" 
                                Margin="2"
                                Command="{Binding DataContext.EditCommand, 
                                         RelativeSource={RelativeSource AncestorType=DataGrid}}"
                                CommandParameter="{Binding}"/>
                        <Button Content="Delete" 
                                Margin="2" 
                                Background="#F44336" 
                                Foreground="White"/>
                    </StackPanel>
                </DataTemplate>
            </DataGridTemplateColumn.CellTemplate>
        </DataGridTemplateColumn>
    </DataGrid.Columns>
</DataGrid>
```

### 1.3 DataGrid Events

```csharp
// Selection Changed
private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    Product selected = dgProducts.SelectedItem as Product;
    if (selected != null)
    {
        txtDetails.Text = $"Selected: {selected.Name}";
    }
}

// Cell Editing
private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
{
    if (e.EditAction == DataGridEditAction.Commit)
    {
        // Save changes
    }
}

// Double-click row
private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
{
    Product selected = dgProducts.SelectedItem as Product;
    if (selected != null)
    {
        // Open detail window
    }
}
```

---

## 2. ListView

### 2.1 Basic ListView

```xml
<ListView x:Name="lvItems">
    <ListViewItem Content="Item 1"/>
    <ListViewItem Content="Item 2"/>
    <ListViewItem Content="Item 3"/>
</ListView>
```

### 2.2 ListView với GridView

```xml
<ListView ItemsSource="{Binding Employees}">
    <ListView.View>
        <GridView>
            <GridViewColumn Header="ID" 
                            Width="50"
                            DisplayMemberBinding="{Binding Id}"/>
            <GridViewColumn Header="Full Name" 
                            Width="150"
                            DisplayMemberBinding="{Binding FullName}"/>
            <GridViewColumn Header="Department" 
                            Width="120"
                            DisplayMemberBinding="{Binding Department}"/>
            <GridViewColumn Header="Salary" 
                            Width="100">
                <GridViewColumn.CellTemplate>
                    <DataTemplate>
                        <TextBlock Text="{Binding Salary, StringFormat='{}{0:N0} VND'}"
                                   TextAlignment="Right"/>
                    </DataTemplate>
                </GridViewColumn.CellTemplate>
            </GridViewColumn>
        </GridView>
    </ListView.View>
</ListView>
```

### 2.3 ListView với Custom ItemTemplate

```xml
<ListView ItemsSource="{Binding Contacts}">
    <ListView.ItemTemplate>
        <DataTemplate>
            <Border BorderBrush="#E0E0E0" 
                    BorderThickness="0,0,0,1"
                    Padding="10">
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="50"/>
                        <ColumnDefinition Width="*"/>
                    </Grid.ColumnDefinitions>
                    
                    <Ellipse Grid.Column="0" 
                             Width="40" Height="40"
                             Fill="#2196F3"/>
                    
                    <StackPanel Grid.Column="1" Margin="10,0">
                        <TextBlock Text="{Binding Name}" 
                                   FontWeight="Bold" 
                                   FontSize="14"/>
                        <TextBlock Text="{Binding Email}" 
                                   Foreground="Gray"/>
                    </StackPanel>
                </Grid>
            </Border>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

---

## 3. TreeView

### 3.1 Static TreeView

```xml
<TreeView>
    <TreeViewItem Header="Documents" IsExpanded="True">
        <TreeViewItem Header="Work">
            <TreeViewItem Header="Report.docx"/>
            <TreeViewItem Header="Presentation.pptx"/>
        </TreeViewItem>
        <TreeViewItem Header="Personal">
            <TreeViewItem Header="Photos"/>
            <TreeViewItem Header="Videos"/>
        </TreeViewItem>
    </TreeViewItem>
    <TreeViewItem Header="Downloads">
        <TreeViewItem Header="Software"/>
        <TreeViewItem Header="Music"/>
    </TreeViewItem>
</TreeView>
```

### 3.2 Hierarchical Data Binding

```csharp
public class Folder
{
    public string Name { get; set; }
    public ObservableCollection<Folder> SubFolders { get; set; }
}
```

```xml
<TreeView ItemsSource="{Binding RootFolders}">
    <TreeView.ItemTemplate>
        <HierarchicalDataTemplate ItemsSource="{Binding SubFolders}">
            <StackPanel Orientation="Horizontal">
                <TextBlock Text="📁 " FontSize="14"/>
                <TextBlock Text="{Binding Name}"/>
            </StackPanel>
        </HierarchicalDataTemplate>
    </TreeView.ItemTemplate>
</TreeView>
```

### 3.3 TreeView Events

```csharp
private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
{
    Folder selectedFolder = e.NewValue as Folder;
    if (selectedFolder != null)
    {
        StatusText.Text = $"Selected: {selectedFolder.Name}";
    }
}
```

---

## 4. TabControl

### 4.1 Basic TabControl

```xml
<TabControl>
    <TabItem Header="General">
        <StackPanel Margin="10">
            <TextBlock Text="General Settings"/>
            <!-- Content -->
        </StackPanel>
    </TabItem>
    
    <TabItem Header="Advanced">
        <StackPanel Margin="10">
            <TextBlock Text="Advanced Settings"/>
            <!-- Content -->
        </StackPanel>
    </TabItem>
    
    <TabItem Header="About">
        <StackPanel Margin="10">
            <TextBlock Text="About this application"/>
        </StackPanel>
    </TabItem>
</TabControl>
```

### 4.2 Styled Tab Headers

```xml
<TabControl>
    <TabItem>
        <TabItem.Header>
            <StackPanel Orientation="Horizontal">
                <TextBlock Text="🏠" Margin="0,0,5,0"/>
                <TextBlock Text="Home"/>
            </StackPanel>
        </TabItem.Header>
        <TextBlock Text="Home content"/>
    </TabItem>
    
    <TabItem>
        <TabItem.Header>
            <StackPanel Orientation="Horizontal">
                <TextBlock Text="⚙️" Margin="0,0,5,0"/>
                <TextBlock Text="Settings"/>
            </StackPanel>
        </TabItem.Header>
        <TextBlock Text="Settings content"/>
    </TabItem>
</TabControl>
```

### 4.3 TabControl TabStripPlacement

```xml
<!-- Tabs on Left -->
<TabControl TabStripPlacement="Left">
    <TabItem Header="Tab 1"/>
    <TabItem Header="Tab 2"/>
</TabControl>

<!-- Tabs on Bottom -->
<TabControl TabStripPlacement="Bottom">
    <TabItem Header="Tab 1"/>
    <TabItem Header="Tab 2"/>
</TabControl>
```

---

## 5. Menu & ContextMenu

### 5.1 Menu

```xml
<DockPanel>
    <Menu DockPanel.Dock="Top">
        <MenuItem Header="_File">
            <MenuItem Header="_New" 
                      InputGestureText="Ctrl+N"
                      Click="New_Click">
                <MenuItem.Icon>
                    <TextBlock Text="📄"/>
                </MenuItem.Icon>
            </MenuItem>
            <MenuItem Header="_Open" InputGestureText="Ctrl+O"/>
            <MenuItem Header="_Save" InputGestureText="Ctrl+S"/>
            <Separator/>
            <MenuItem Header="E_xit" Click="Exit_Click"/>
        </MenuItem>
        
        <MenuItem Header="_Edit">
            <MenuItem Header="_Undo" InputGestureText="Ctrl+Z"/>
            <MenuItem Header="_Redo" InputGestureText="Ctrl+Y"/>
            <Separator/>
            <MenuItem Header="Cu_t" InputGestureText="Ctrl+X"/>
            <MenuItem Header="_Copy" InputGestureText="Ctrl+C"/>
            <MenuItem Header="_Paste" InputGestureText="Ctrl+V"/>
        </MenuItem>
        
        <MenuItem Header="_View">
            <MenuItem Header="Show _Toolbar" IsCheckable="True" IsChecked="True"/>
            <MenuItem Header="Show _Statusbar" IsCheckable="True" IsChecked="True"/>
            <Separator/>
            <MenuItem Header="_Theme">
                <MenuItem Header="Light" IsCheckable="True" IsChecked="True"/>
                <MenuItem Header="Dark" IsCheckable="True"/>
            </MenuItem>
        </MenuItem>
        
        <MenuItem Header="_Help">
            <MenuItem Header="_About" Click="About_Click"/>
        </MenuItem>
    </Menu>
    
    <!-- Main Content -->
    <TextBox AcceptsReturn="True"/>
</DockPanel>
```

### 5.2 ContextMenu

```xml
<TextBox Text="Right-click me">
    <TextBox.ContextMenu>
        <ContextMenu>
            <MenuItem Header="Cut" Command="ApplicationCommands.Cut"/>
            <MenuItem Header="Copy" Command="ApplicationCommands.Copy"/>
            <MenuItem Header="Paste" Command="ApplicationCommands.Paste"/>
            <Separator/>
            <MenuItem Header="Select All" Command="ApplicationCommands.SelectAll"/>
        </ContextMenu>
    </TextBox.ContextMenu>
</TextBox>

<!-- ContextMenu cho DataGrid Row -->
<DataGrid>
    <DataGrid.ContextMenu>
        <ContextMenu>
            <MenuItem Header="View Details" Click="ViewDetails_Click"/>
            <MenuItem Header="Edit" Click="Edit_Click"/>
            <Separator/>
            <MenuItem Header="Delete" Click="Delete_Click"/>
        </ContextMenu>
    </DataGrid.ContextMenu>
</DataGrid>
```

---

## 6. ToolBar

```xml
<DockPanel>
    <ToolBarTray DockPanel.Dock="Top">
        <ToolBar>
            <Button ToolTip="New">
                <TextBlock Text="📄" FontSize="16"/>
            </Button>
            <Button ToolTip="Open">
                <TextBlock Text="📂" FontSize="16"/>
            </Button>
            <Button ToolTip="Save">
                <TextBlock Text="💾" FontSize="16"/>
            </Button>
            <Separator/>
            <Button ToolTip="Cut">
                <TextBlock Text="✂️" FontSize="16"/>
            </Button>
            <Button ToolTip="Copy">
                <TextBlock Text="📋" FontSize="16"/>
            </Button>
            <Button ToolTip="Paste">
                <TextBlock Text="📎" FontSize="16"/>
            </Button>
            <Separator/>
            <ComboBox Width="100" SelectedIndex="0">
                <ComboBoxItem Content="Normal"/>
                <ComboBoxItem Content="Bold"/>
                <ComboBoxItem Content="Italic"/>
            </ComboBox>
        </ToolBar>
    </ToolBarTray>
    
    <TextBox AcceptsReturn="True"/>
</DockPanel>
```

---

## 7. Expander & GroupBox

### 7.1 Expander

```xml
<StackPanel>
    <Expander Header="Basic Information" IsExpanded="True">
        <StackPanel Margin="20,10">
            <TextBlock Text="Name: John Doe"/>
            <TextBlock Text="Email: john@example.com"/>
        </StackPanel>
    </Expander>
    
    <Expander Header="Advanced Settings">
        <StackPanel Margin="20,10">
            <CheckBox Content="Enable logging"/>
            <CheckBox Content="Auto-save"/>
        </StackPanel>
    </Expander>
</StackPanel>
```

### 7.2 GroupBox

```xml
<GroupBox Header="Personal Information" Margin="10" Padding="10">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>
        
        <Label Content="Name:" Grid.Row="0" Grid.Column="0"/>
        <TextBox Grid.Row="0" Grid.Column="1"/>
        
        <Label Content="Age:" Grid.Row="1" Grid.Column="0"/>
        <TextBox Grid.Row="1" Grid.Column="1"/>
    </Grid>
</GroupBox>
```

---

## 8. StatusBar

```xml
<DockPanel>
    <StatusBar DockPanel.Dock="Bottom">
        <StatusBarItem>
            <TextBlock x:Name="txtStatus" Text="Ready"/>
        </StatusBarItem>
        <Separator/>
        <StatusBarItem>
            <ProgressBar Width="100" Height="15" Value="75"/>
        </StatusBarItem>
        <Separator/>
        <StatusBarItem HorizontalAlignment="Right">
            <TextBlock Text="{Binding CurrentTime, StringFormat='HH:mm:ss'}"/>
        </StatusBarItem>
    </StatusBar>
    
    <!-- Main Content -->
    <TextBox/>
</DockPanel>
```

---

## 📝 Bài Tập

1. Tạo ứng dụng quản lý sản phẩm với:
   - DataGrid hiển thị danh sách sản phẩm
   - Menu với File (New, Open, Save, Exit)
   - ToolBar với các buttons tương ứng
   - ContextMenu cho DataGrid (Edit, Delete)

2. Tạo File Explorer đơn giản với:
   - TreeView hiển thị cấu trúc thư mục
   - ListView hiển thị files trong thư mục được chọn

---

## 📚 Tài Liệu Tham Khảo
- [DataGrid Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datagrid)
- [ListView Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.listview)

---

⬅️ **Bài trước**: [Bài 3.2: Basic Controls](./Bai_3.2_Basic_Controls.md)  
➡️ **Bài tiếp theo**: [Bài 4.1: Data Binding Cơ Bản](../04_Data_Binding/Bai_4.1_Binding_Co_Ban.md)
