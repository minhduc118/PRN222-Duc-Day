# Bài 9.2: Theming

## 🎯 Mục Tiêu
- Light/Dark Theme
- Theme switching
- UI Libraries

---

## 1. Theme ResourceDictionaries

```xml
<!-- Themes/LightTheme.xaml -->
<ResourceDictionary>
    <SolidColorBrush x:Key="BackgroundBrush" Color="#FFFFFF"/>
    <SolidColorBrush x:Key="ForegroundBrush" Color="#212121"/>
    <SolidColorBrush x:Key="PrimaryBrush" Color="#2196F3"/>
</ResourceDictionary>

<!-- Themes/DarkTheme.xaml -->
<ResourceDictionary>
    <SolidColorBrush x:Key="BackgroundBrush" Color="#121212"/>
    <SolidColorBrush x:Key="ForegroundBrush" Color="#E0E0E0"/>
    <SolidColorBrush x:Key="PrimaryBrush" Color="#BB86FC"/>
</ResourceDictionary>
```

---

## 2. Theme Switching

```csharp
public void ChangeTheme(string themeName)
{
    var dict = new ResourceDictionary();
    
    switch (themeName)
    {
        case "Dark":
            dict.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
            break;
        default:
            dict.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
            break;
    }
    
    // Remove old theme
    var oldDict = Application.Current.Resources.MergedDictionaries
        .FirstOrDefault(d => d.Source?.ToString().Contains("Theme") == true);
    if (oldDict != null)
        Application.Current.Resources.MergedDictionaries.Remove(oldDict);
    
    // Add new theme
    Application.Current.Resources.MergedDictionaries.Add(dict);
}
```

---

## 3. Sử Dụng DynamicResource

```xml
<Window Background="{DynamicResource BackgroundBrush}">
    <TextBlock Foreground="{DynamicResource ForegroundBrush}"/>
    <Button Background="{DynamicResource PrimaryBrush}"/>
</Window>
```

---

## 4. UI Libraries

### MahApps.Metro
```
dotnet add package MahApps.Metro
```

```xml
<mah:MetroWindow>
    <mah:MetroWindow.LeftWindowCommands>
        <mah:WindowCommands>
            <Button Content="Settings"/>
        </mah:WindowCommands>
    </mah:MetroWindow.LeftWindowCommands>
</mah:MetroWindow>
```

### Material Design
```
dotnet add package MaterialDesignThemes
```

```xml
<materialDesign:Card Padding="16" Margin="8">
    <TextBlock Text="Material Design Card"/>
</materialDesign:Card>
```

---

## 📝 Bài Tập
1. Implement Light/Dark theme toggle
2. Thử nghiệm MahApps.Metro hoặc Material Design

---

⬅️ [Bài 9.1](./Bai_9.1_Resources.md) | ➡️ [Bài 10.1](../10_EFCore_WPF/Bai_10.1_Tich_Hop_EFCore.md)
