# Bài 4.3: Value Converters

## 🎯 Mục Tiêu Bài Học
- Hiểu và tạo IValueConverter
- Các converter phổ biến  
- MultiBinding và IMultiValueConverter

---

## 1. IValueConverter Interface

```csharp
public interface IValueConverter
{
    object Convert(object value, Type targetType, object parameter, CultureInfo culture);
    object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
}
```

---

## 2. BoolToVisibilityConverter

```csharp
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is bool b && b ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is Visibility v && v == Visibility.Visible;
    }
}
```

```xml
<Window.Resources>
    <local:BoolToVisibilityConverter x:Key="BoolToVis"/>
</Window.Resources>

<Border Visibility="{Binding IsVisible, Converter={StaticResource BoolToVis}}"/>
```

---

## 3. Các Converter Phổ Biến

### InverseBoolConverter
```csharp
public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is bool b ? !b : value;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value is bool b ? !b : value;
}
```

### NullToVisibilityConverter
```csharp
public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value == null ? Visibility.Collapsed : Visibility.Visible;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
```

---

## 4. ConverterParameter

```csharp
public class MathConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double num && parameter is string p && double.TryParse(p, out double mult))
            return num * mult;
        return value;
    }
    public object ConvertBack(object value, Type t, object p, CultureInfo c) => throw new NotImplementedException();
}
```

```xml
<TextBlock Text="{Binding Price, Converter={StaticResource Math}, ConverterParameter='1.1'}"/>
```

---

## 5. MultiBinding

```csharp
public class FullNameConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] is string first && values[1] is string last)
            return $"{first} {last}";
        return string.Empty;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
```

```xml
<TextBlock>
    <TextBlock.Text>
        <MultiBinding Converter="{StaticResource FullNameConverter}">
            <Binding Path="FirstName"/>
            <Binding Path="LastName"/>
        </MultiBinding>
    </TextBlock.Text>
</TextBlock>
```

---

## 6. StringFormat Alternative

```xml
<TextBlock Text="{Binding Price, StringFormat='{}{0:C}'}"/>
<TextBlock Text="{Binding Date, StringFormat='{}{0:dd/MM/yyyy}'}"/>
<TextBlock Text="{Binding Count, StringFormat='Items: {0}'}"/>
```

---

## 📝 Bài Tập

1. Tạo `FileSizeConverter`: Bytes → KB/MB/GB
2. Tạo `StatusToColorConverter` cho các trạng thái khác nhau
3. Tạo MultiBinding converter tính Area = Width × Height

---

⬅️ **Bài trước**: [Bài 4.2: Binding Nâng Cao](./Bai_4.2_Binding_Nang_Cao.md)  
➡️ **Bài tiếp theo**: [Bài 4.4: Data Validation](./Bai_4.4_Data_Validation.md)
