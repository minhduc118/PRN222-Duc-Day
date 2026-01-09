# Bài 6.1: Routed Events

## 🎯 Mục Tiêu
- Hiểu Routed Events trong WPF
- Bubbling, Tunneling, Direct events

---

## 1. Routed Events Là Gì?

Events trong WPF có thể "travel" qua Visual Tree.

```
        Window (3)              
           ↑ Bubble
        Grid (2)
           ↑ Bubble  
        Button (1) ← Click here
```

---

## 2. Event Routing Strategies

| Strategy | Direction | Prefix | Example |
|----------|-----------|--------|---------|
| **Bubbling** | Child → Parent | (none) | Click, MouseDown |
| **Tunneling** | Parent → Child | Preview | PreviewMouseDown |
| **Direct** | No routing | - | MouseEnter |

---

## 3. Bubbling Events

```xml
<Grid MouseDown="Grid_MouseDown">
    <Button Content="Click" MouseDown="Button_MouseDown"/>
</Grid>
```

```csharp
private void Button_MouseDown(object sender, MouseButtonEventArgs e)
{
    Debug.WriteLine("Button MouseDown");
    // e.Handled = true; // Ngăn bubble lên parent
}

private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
{
    Debug.WriteLine("Grid MouseDown"); // Được gọi sau Button
}
```

---

## 4. Tunneling Events (Preview)

```xml
<Grid PreviewMouseDown="Grid_PreviewMouseDown">
    <Button PreviewMouseDown="Button_PreviewMouseDown"/>
</Grid>
```

```csharp
// Thứ tự: Grid Preview → Button Preview → Button → Grid
private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
{
    Debug.WriteLine("1. Grid Preview");
}

private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
{
    Debug.WriteLine("2. Button Preview");
}
```

---

## 5. Handled Property

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    e.Handled = true; // Ngừng routing
}
```

---

## 📝 Bài Tập
1. Tạo demo hiển thị thứ tự event routing
2. Sử dụng Preview events để validate input

---

⬅️ [Bài 5.3](../05_Styles_Templates/Bai_5.3_Data_Templates.md) | ➡️ [Bài 6.2](./Bai_6.2_Commands.md)
