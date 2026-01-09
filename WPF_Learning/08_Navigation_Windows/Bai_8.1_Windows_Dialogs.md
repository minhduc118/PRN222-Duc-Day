# Bài 8.1: Windows & Dialogs

## 🎯 Mục Tiêu
- Mở Window mới
- Modal vs Non-Modal
- Custom Dialogs

---

## 1. Mở Window Mới

```csharp
// Non-Modal (không block)
var detailWindow = new DetailWindow();
detailWindow.Show();

// Modal (block parent)
var detailWindow = new DetailWindow();
detailWindow.Owner = this;
bool? result = detailWindow.ShowDialog();

if (result == true)
{
    // User clicked OK
}
```

---

## 2. Passing Data

```csharp
// Qua Constructor
var editWindow = new EditProductWindow(selectedProduct);
editWindow.ShowDialog();

// Qua Property
var editWindow = new EditProductWindow();
editWindow.Product = selectedProduct;
editWindow.ShowDialog();

// Lấy result
if (editWindow.DialogResult == true)
{
    Product updatedProduct = editWindow.Product;
}
```

---

## 3. DialogResult

```csharp
public partial class EditProductWindow : Window
{
    public Product Product { get; set; }
    
    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        // Validate
        DialogResult = true;  // Đóng window, return true
    }
    
    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }
}
```

```xml
<Button Content="Save" Click="BtnSave_Click" IsDefault="True"/>
<Button Content="Cancel" Click="BtnCancel_Click" IsCancel="True"/>
```

---

## 4. MessageBox

```csharp
// Simple
MessageBox.Show("Hello World!");

// With title
MessageBox.Show("File saved!", "Success");

// With buttons
var result = MessageBox.Show(
    "Delete this item?",
    "Confirm",
    MessageBoxButton.YesNo,
    MessageBoxImage.Question);

if (result == MessageBoxResult.Yes)
{
    // Delete
}
```

---

## 5. OpenFileDialog / SaveFileDialog

```csharp
var openDialog = new OpenFileDialog
{
    Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
};

if (openDialog.ShowDialog() == true)
{
    string filePath = openDialog.FileName;
}

var saveDialog = new SaveFileDialog
{
    Filter = "Text files (*.txt)|*.txt",
    DefaultExt = ".txt"
};

if (saveDialog.ShowDialog() == true)
{
    File.WriteAllText(saveDialog.FileName, content);
}
```

---

## 📝 Bài Tập
1. Tạo Add/Edit Product window với DialogResult
2. Implement confirm dialog khi delete

---

⬅️ [Bài 7.4](../07_MVVM/Bai_7.4_MVVM_Frameworks.md) | ➡️ [Bài 8.2](./Bai_8.2_Navigation.md)
