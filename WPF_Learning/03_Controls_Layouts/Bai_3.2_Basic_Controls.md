# Bài 3.2: Basic Controls

## 🎯 Mục Tiêu Bài Học
- Sử dụng các controls cơ bản trong WPF
- Hiểu properties và events của từng control
- Tạo form nhập liệu hoàn chỉnh

---

## 1. Text Display Controls

### 1.1 TextBlock
Hiển thị text (read-only), hỗ trợ formatting.

```xml
<!-- Basic -->
<TextBlock Text="Hello World"/>

<!-- Formatted -->
<TextBlock FontSize="20" 
           FontWeight="Bold" 
           Foreground="#2196F3"
           TextWrapping="Wrap"
           TextAlignment="Center">
    <Run Text="Hello " FontStyle="Italic"/>
    <Run Text="World" Foreground="Red"/>
    <LineBreak/>
    <Run Text="New Line"/>
</TextBlock>

<!-- Text Trimming (khi text dài) -->
<TextBlock Text="Very long text that will be trimmed..."
           TextTrimming="CharacterEllipsis"
           Width="100"/>
```

**Properties quan trọng:**

| Property | Mô Tả | Giá Trị |
|----------|-------|---------|
| `Text` | Nội dung văn bản | String |
| `TextWrapping` | Xuống dòng | NoWrap, Wrap, WrapWithOverflow |
| `TextTrimming` | Cắt text | None, CharacterEllipsis, WordEllipsis |
| `TextAlignment` | Căn chỉnh | Left, Center, Right, Justify |
| `LineHeight` | Chiều cao dòng | Double |

### 1.2 Label
Tương tự TextBlock nhưng có thể chứa một control con và hỗ trợ Access Keys.

```xml
<!-- Basic -->
<Label Content="Username:"/>

<!-- Access Key (Alt+U focus vào TextBox) -->
<StackPanel>
    <Label Content="_Username:" Target="{Binding ElementName=txtUsername}"/>
    <TextBox x:Name="txtUsername"/>
</StackPanel>

<!-- Label với content phức tạp -->
<Label>
    <StackPanel Orientation="Horizontal">
        <Image Source="icon.png" Width="16"/>
        <TextBlock Text="Settings" Margin="5,0,0,0"/>
    </StackPanel>
</Label>
```

---

## 2. Text Input Controls

### 2.1 TextBox

```xml
<!-- Basic -->
<TextBox Text="Enter text here"/>

<!-- Multiline -->
<TextBox AcceptsReturn="True"
         TextWrapping="Wrap"
         VerticalScrollBarVisibility="Auto"
         Height="100"/>

<!-- Placeholder (với Watermark style) -->
<TextBox x:Name="txtSearch">
    <TextBox.Style>
        <Style TargetType="TextBox">
            <Style.Triggers>
                <Trigger Property="Text" Value="">
                    <Setter Property="Background">
                        <Setter.Value>
                            <VisualBrush Stretch="None" AlignmentX="Left">
                                <VisualBrush.Visual>
                                    <TextBlock Text="Search..." Foreground="Gray" Margin="5,0"/>
                                </VisualBrush.Visual>
                            </VisualBrush>
                        </Setter.Value>
                    </Setter>
                </Trigger>
            </Style.Triggers>
        </Style>
    </TextBox.Style>
</TextBox>
```

**Properties quan trọng:**

| Property | Mô Tả |
|----------|-------|
| `Text` | Nội dung |
| `MaxLength` | Số ký tự tối đa |
| `IsReadOnly` | Chỉ đọc |
| `AcceptsReturn` | Cho phép Enter (multiline) |
| `AcceptsTab` | Cho phép Tab |
| `TextWrapping` | Xuống dòng |

**Events:**

```csharp
// TextChanged - mỗi khi text thay đổi
private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
{
    TextBox txt = sender as TextBox;
    lblCount.Content = $"{txt.Text.Length} characters";
}

// PreviewTextInput - filter input
private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
{
    // Only allow numbers
    e.Handled = !IsNumeric(e.Text);
}

private bool IsNumeric(string text)
{
    return int.TryParse(text, out _);
}
```

### 2.2 PasswordBox

```xml
<PasswordBox x:Name="txtPassword"
             PasswordChar="●"
             MaxLength="20"
             PasswordChanged="PasswordBox_PasswordChanged"/>
```

```csharp
private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
{
    PasswordBox pb = sender as PasswordBox;
    string password = pb.Password; // Lấy password
}
```

> ⚠️ **Lưu ý**: PasswordBox không thể binding Password property vì lý do bảo mật.

### 2.3 RichTextBox

```xml
<RichTextBox Height="200">
    <FlowDocument>
        <Paragraph>
            <Bold>Bold text</Bold>
            <Italic>Italic text</Italic>
            <Underline>Underlined</Underline>
        </Paragraph>
        <Paragraph>
            <Run Foreground="Red">Colored text</Run>
        </Paragraph>
    </FlowDocument>
</RichTextBox>
```

---

## 3. Button Controls

### 3.1 Button

```xml
<!-- Basic -->
<Button Content="Click Me" Click="Button_Click"/>

<!-- Styled -->
<Button Padding="20,10"
        Background="#4CAF50"
        Foreground="White"
        BorderThickness="0"
        Cursor="Hand">
    <StackPanel Orientation="Horizontal">
        <TextBlock Text="🔍" Margin="0,0,10,0"/>
        <TextBlock Text="Search"/>
    </StackPanel>
</Button>

<!-- IsDefault and IsCancel -->
<StackPanel Orientation="Horizontal" HorizontalAlignment="Right">
    <Button Content="OK" IsDefault="True" Margin="5"/>     <!-- Enter key -->
    <Button Content="Cancel" IsCancel="True" Margin="5"/>  <!-- Escape key -->
</StackPanel>
```

### 3.2 ToggleButton

```xml
<ToggleButton x:Name="toggleBold"
              Content="B"
              FontWeight="Bold"
              Width="30" Height="30"
              Checked="ToggleBold_Checked"
              Unchecked="ToggleBold_Unchecked"/>
```

```csharp
private void ToggleBold_Checked(object sender, RoutedEventArgs e)
{
    txtContent.FontWeight = FontWeights.Bold;
}

private void ToggleBold_Unchecked(object sender, RoutedEventArgs e)
{
    txtContent.FontWeight = FontWeights.Normal;
}
```

### 3.3 RepeatButton
Liên tục phát event khi giữ button.

```xml
<RepeatButton Content="+" 
              Delay="500"       <!-- ms trước khi repeat -->
              Interval="100"    <!-- ms giữa các repeat -->
              Click="Increment_Click"/>
```

---

## 4. Selection Controls

### 4.1 CheckBox

```xml
<StackPanel>
    <CheckBox Content="Option 1" IsChecked="True"/>
    <CheckBox Content="Option 2"/>
    <CheckBox Content="Three State" IsThreeState="True"/>
    <!-- IsChecked: True, False, null (indeterminate) -->
</StackPanel>
```

```csharp
private void CheckBox_Changed(object sender, RoutedEventArgs e)
{
    CheckBox cb = sender as CheckBox;
    
    if (cb.IsChecked == true)
        MessageBox.Show("Checked");
    else if (cb.IsChecked == false)
        MessageBox.Show("Unchecked");
    else
        MessageBox.Show("Indeterminate");
}
```

### 4.2 RadioButton

```xml
<!-- Default: cùng parent = cùng group -->
<StackPanel>
    <RadioButton Content="Male" IsChecked="True"/>
    <RadioButton Content="Female"/>
    <RadioButton Content="Other"/>
</StackPanel>

<!-- GroupName để tách group -->
<StackPanel>
    <TextBlock Text="Size:"/>
    <RadioButton GroupName="Size" Content="Small"/>
    <RadioButton GroupName="Size" Content="Medium" IsChecked="True"/>
    <RadioButton GroupName="Size" Content="Large"/>
    
    <TextBlock Text="Color:" Margin="0,10,0,0"/>
    <RadioButton GroupName="Color" Content="Red"/>
    <RadioButton GroupName="Color" Content="Blue" IsChecked="True"/>
    <RadioButton GroupName="Color" Content="Green"/>
</StackPanel>
```

### 4.3 ComboBox

```xml
<!-- Static Items -->
<ComboBox x:Name="cboCountry" SelectedIndex="0">
    <ComboBoxItem Content="Vietnam"/>
    <ComboBoxItem Content="USA"/>
    <ComboBoxItem Content="Japan"/>
</ComboBox>

<!-- Binding Items -->
<ComboBox ItemsSource="{Binding Countries}"
          DisplayMemberPath="Name"
          SelectedValuePath="Id"
          SelectedValue="{Binding SelectedCountryId}"/>

<!-- Editable -->
<ComboBox IsEditable="True" IsTextSearchEnabled="True">
    <ComboBoxItem Content="Apple"/>
    <ComboBoxItem Content="Banana"/>
    <ComboBoxItem Content="Orange"/>
</ComboBox>
```

```csharp
private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    ComboBox cbo = sender as ComboBox;
    ComboBoxItem selectedItem = cbo.SelectedItem as ComboBoxItem;
    
    if (selectedItem != null)
    {
        MessageBox.Show($"Selected: {selectedItem.Content}");
    }
}
```

### 4.4 ListBox

```xml
<!-- Basic -->
<ListBox x:Name="lstItems" Height="150">
    <ListBoxItem Content="Item 1"/>
    <ListBoxItem Content="Item 2"/>
    <ListBoxItem Content="Item 3"/>
</ListBox>

<!-- Multi-select -->
<ListBox SelectionMode="Extended">  <!-- Ctrl/Shift + Click -->
    <!-- Items -->
</ListBox>

<ListBox SelectionMode="Multiple">  <!-- Click to toggle -->
    <!-- Items -->
</ListBox>

<!-- With DataTemplate -->
<ListBox ItemsSource="{Binding Products}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <Image Source="{Binding ImagePath}" Width="50"/>
                <StackPanel Margin="10,0">
                    <TextBlock Text="{Binding Name}" FontWeight="Bold"/>
                    <TextBlock Text="{Binding Price, StringFormat='{}{0:C}'}"/>
                </StackPanel>
            </StackPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

---

## 5. Other Basic Controls

### 5.1 Image

```xml
<!-- Local image -->
<Image Source="/Images/logo.png" 
       Width="100" Height="100"
       Stretch="Uniform"/>

<!-- From URL -->
<Image Source="https://example.com/image.png"/>

<!-- Stretch modes -->
<Image Source="photo.jpg" Stretch="None"/>      <!-- Original size -->
<Image Source="photo.jpg" Stretch="Fill"/>       <!-- Stretch to fill -->
<Image Source="photo.jpg" Stretch="Uniform"/>    <!-- Maintain ratio -->
<Image Source="photo.jpg" Stretch="UniformToFill"/> <!-- Fill, may crop -->
```

### 5.2 Slider

```xml
<Slider Minimum="0" 
        Maximum="100" 
        Value="50"
        TickFrequency="10"
        TickPlacement="BottomRight"
        IsSnapToTickEnabled="True"/>

<!-- Bind với TextBlock -->
<StackPanel>
    <Slider x:Name="sldVolume" Minimum="0" Maximum="100" Value="50"/>
    <TextBlock Text="{Binding ElementName=sldVolume, Path=Value, StringFormat='Volume: {0:F0}%'}"/>
</StackPanel>
```

### 5.3 ProgressBar

```xml
<!-- Determinate -->
<ProgressBar Value="75" Minimum="0" Maximum="100" Height="20"/>

<!-- Indeterminate (loading) -->
<ProgressBar IsIndeterminate="True" Height="20"/>
```

### 5.4 DatePicker

```xml
<DatePicker SelectedDate="{Binding BirthDate}"
            DisplayDateStart="1900-01-01"
            DisplayDateEnd="{x:Static sys:DateTime.Today}"
            FirstDayOfWeek="Monday"/>
```

---

## 6. Thực Hành: Form Đăng Ký

```xml
<Window x:Class="BasicControls.RegistrationForm"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Registration Form" Height="500" Width="400"
        WindowStartupLocation="CenterScreen">
    
    <Grid Margin="20">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="*"/>
        </Grid.ColumnDefinitions>

        <!-- Title -->
        <TextBlock Grid.Row="0" Grid.ColumnSpan="2"
                   Text="User Registration"
                   FontSize="24" FontWeight="Bold"
                   Foreground="#1976D2"
                   Margin="0,0,0,20"/>

        <!-- Full Name -->
        <Label Grid.Row="1" Grid.Column="0" Content="_Full Name:" 
               Target="{Binding ElementName=txtFullName}"/>
        <TextBox Grid.Row="1" Grid.Column="1" x:Name="txtFullName"
                 Padding="5" Margin="0,5"/>

        <!-- Email -->
        <Label Grid.Row="2" Grid.Column="0" Content="_Email:"/>
        <TextBox Grid.Row="2" Grid.Column="1" x:Name="txtEmail"
                 Padding="5" Margin="0,5"/>

        <!-- Password -->
        <Label Grid.Row="3" Grid.Column="0" Content="_Password:"/>
        <PasswordBox Grid.Row="3" Grid.Column="1" x:Name="txtPassword"
                     Padding="5" Margin="0,5"/>

        <!-- Gender -->
        <Label Grid.Row="4" Grid.Column="0" Content="Gender:"/>
        <StackPanel Grid.Row="4" Grid.Column="1" Orientation="Horizontal"
                    Margin="0,5">
            <RadioButton Content="Male" GroupName="Gender" Margin="0,0,15,0"/>
            <RadioButton Content="Female" GroupName="Gender" Margin="0,0,15,0"/>
            <RadioButton Content="Other" GroupName="Gender"/>
        </StackPanel>

        <!-- Country -->
        <Label Grid.Row="5" Grid.Column="0" Content="Country:"/>
        <ComboBox Grid.Row="5" Grid.Column="1" x:Name="cboCountry"
                  Padding="5" Margin="0,5">
            <ComboBoxItem Content="Vietnam" IsSelected="True"/>
            <ComboBoxItem Content="USA"/>
            <ComboBoxItem Content="Japan"/>
            <ComboBoxItem Content="Korea"/>
        </ComboBox>

        <!-- Terms -->
        <CheckBox Grid.Row="6" Grid.Column="1" 
                  x:Name="chkTerms" Margin="0,10"
                  Content="I agree to the Terms and Conditions"/>

        <!-- Buttons -->
        <StackPanel Grid.Row="8" Grid.ColumnSpan="2"
                    Orientation="Horizontal"
                    HorizontalAlignment="Right">
            <Button Content="Register" 
                    Padding="20,10" Margin="5"
                    Background="#4CAF50" Foreground="White"
                    Click="Register_Click"/>
            <Button Content="Cancel"
                    Padding="20,10" Margin="5"
                    IsCancel="True"/>
        </StackPanel>
    </Grid>
</Window>
```

---

## 📝 Bài Tập

1. Tạo form Login với:
   - TextBox cho Username
   - PasswordBox cho Password
   - CheckBox "Remember me"
   - Button Login và Cancel

2. Tạo Settings form với:
   - RadioButtons cho Theme (Light/Dark)
   - Slider cho Font Size
   - ComboBox cho Language

---

## 📚 Tài Liệu Tham Khảo
- [Controls by Category](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/controls/)
- [TextBox Class](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.textbox)

---

⬅️ **Bài trước**: [Bài 3.1: Layout Containers](./Bai_3.1_Layout_Containers.md)  
➡️ **Bài tiếp theo**: [Bài 3.3: Advanced Controls](./Bai_3.3_Advanced_Controls.md)
