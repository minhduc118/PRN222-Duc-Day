# Bài 4.4: Data Validation

## 🎯 Mục Tiêu
- Validation với IDataErrorInfo, INotifyDataErrorInfo
- ValidationRule
- Error Templates

---

## 1. IDataErrorInfo

```csharp
public class Person : BaseViewModel, IDataErrorInfo
{
    private string _name;
    public string Name { get => _name; set => SetProperty(ref _name, value); }
    
    public string Error => null;
    
    public string this[string columnName]
    {
        get
        {
            if (columnName == nameof(Name))
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return "Name is required";
                if (Name.Length < 2)
                    return "Name must be at least 2 characters";
            }
            return null;
        }
    }
}
```

```xml
<TextBox Text="{Binding Name, ValidatesOnDataErrors=True, UpdateSourceTrigger=PropertyChanged}"/>
```

---

## 2. INotifyDataErrorInfo (Recommended)

```csharp
public class PersonViewModel : BaseViewModel, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new();
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    public bool HasErrors => _errors.Any();
    
    public IEnumerable GetErrors(string propertyName)
    {
        return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
    }
    
    private void ValidateName()
    {
        ClearErrors(nameof(Name));
        if (string.IsNullOrWhiteSpace(Name))
            AddError(nameof(Name), "Name is required");
    }
    
    private void AddError(string prop, string error)
    {
        if (!_errors.ContainsKey(prop)) _errors[prop] = new();
        _errors[prop].Add(error);
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(prop));
    }
}
```

---

## 3. ValidationRule

```csharp
public class AgeValidationRule : ValidationRule
{
    public int Min { get; set; } = 0;
    public int Max { get; set; } = 120;
    
    public override ValidationResult Validate(object value, CultureInfo culture)
    {
        if (!int.TryParse(value?.ToString(), out int age))
            return new ValidationResult(false, "Must be a number");
        if (age < Min || age > Max)
            return new ValidationResult(false, $"Age must be {Min}-{Max}");
        return ValidationResult.ValidResult;
    }
}
```

```xml
<TextBox>
    <TextBox.Text>
        <Binding Path="Age" UpdateSourceTrigger="PropertyChanged">
            <Binding.ValidationRules>
                <local:AgeValidationRule Min="18" Max="65"/>
            </Binding.ValidationRules>
        </Binding>
    </TextBox.Text>
</TextBox>
```

---

## 4. Error Template

```xml
<Style TargetType="TextBox">
    <Setter Property="Validation.ErrorTemplate">
        <Setter.Value>
            <ControlTemplate>
                <StackPanel>
                    <AdornedElementPlaceholder x:Name="placeholder"/>
                    <TextBlock Text="{Binding ElementName=placeholder, 
                               Path=AdornedElement.(Validation.Errors)[0].ErrorContent}"
                               Foreground="Red" FontSize="10" Margin="2,0"/>
                </StackPanel>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
    <Style.Triggers>
        <Trigger Property="Validation.HasError" Value="True">
            <Setter Property="BorderBrush" Value="Red"/>
        </Trigger>
    </Style.Triggers>
</Style>
```

---

## 📝 Bài Tập
1. Tạo form với validation: Name (required), Email (format), Age (18-65)
2. Hiển thị error messages với custom ErrorTemplate

---

⬅️ [Bài 4.3](./Bai_4.3_Value_Converters.md) | ➡️ [Bài 5.1](../05_Styles_Templates/Bai_5.1_Styles.md)
