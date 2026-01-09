# Bài 9.1: Resources

## 🎯 Mục Tiêu
- StaticResource vs DynamicResource
- Resource Dictionaries
- Application-level Resources

---

## 1. Định Nghĩa Resources

```xml
<Window.Resources>
    <!-- Colors -->
    <SolidColorBrush x:Key="PrimaryBrush" Color="#2196F3"/>
    <SolidColorBrush x:Key="AccentBrush" Color="#FF5722"/>
    
    <!-- Values -->
    <sys:Double x:Key="HeaderFontSize">24</sys:Double>
    <Thickness x:Key="DefaultMargin">10,5,10,5</Thickness>
    
    <!-- Styles -->
    <Style x:Key="HeaderStyle" TargetType="TextBlock">
        <Setter Property="FontSize" Value="{StaticResource HeaderFontSize}"/>
        <Setter Property="Foreground" Value="{StaticResource PrimaryBrush}"/>
    </Style>
</Window.Resources>
```

---

## 2. StaticResource vs DynamicResource

```xml
<!-- StaticResource: resolved at load time -->
<Button Background="{StaticResource PrimaryBrush}"/>

<!-- DynamicResource: resolved at runtime, can change -->
<Button Background="{DynamicResource PrimaryBrush}"/>
```

| | StaticResource | DynamicResource |
|--|---------------|-----------------|
| Resolve | Load time | Runtime |
| Performance | Faster | Slower |
| Theme switching | No | Yes |

---

## 3. Resource Dictionaries

```xml
<!-- Styles/Colors.xaml -->
<ResourceDictionary>
    <SolidColorBrush x:Key="Primary" Color="#2196F3"/>
    <SolidColorBrush x:Key="Danger" Color="#F44336"/>
</ResourceDictionary>
```

```xml
<!-- App.xaml -->
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="Styles/Colors.xaml"/>
            <ResourceDictionary Source="Styles/Buttons.xaml"/>
            <ResourceDictionary Source="Styles/TextBoxes.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

---

## 4. FindResource in Code

```csharp
// Get resource
var brush = (SolidColorBrush)FindResource("PrimaryBrush");
var style = (Style)TryFindResource("ButtonStyle");

// Dynamic change
Resources["PrimaryBrush"] = new SolidColorBrush(Colors.Red);
```

---

## 📝 Bài Tập
1. Tạo ResourceDictionary cho Colors, Fonts, Styles
2. Organize resources theo modules

---

⬅️ [Bài 8.2](../08_Navigation_Windows/Bai_8.2_Navigation.md) | ➡️ [Bài 9.2](./Bai_9.2_Theming.md)
