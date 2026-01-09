# Bài 5.1: Styles

## 🎯 Mục Tiêu
- Tạo và áp dụng Styles
- Named/Implicit Styles, BasedOn
- Triggers

---

## 1. Inline Style vs Named Style

```xml
<!-- Inline (không reuse được) -->
<Button Background="Blue" Foreground="White" Padding="20,10"/>

<!-- Named Style -->
<Window.Resources>
    <Style x:Key="PrimaryButton" TargetType="Button">
        <Setter Property="Background" Value="#2196F3"/>
        <Setter Property="Foreground" Value="White"/>
        <Setter Property="Padding" Value="20,10"/>
        <Setter Property="BorderThickness" Value="0"/>
        <Setter Property="Cursor" Value="Hand"/>
    </Style>
</Window.Resources>

<Button Content="Click" Style="{StaticResource PrimaryButton}"/>
```

---

## 2. Implicit Style (TargetType)

```xml
<!-- Áp dụng cho TẤT CẢ Button trong scope -->
<Style TargetType="Button">
    <Setter Property="Margin" Value="5"/>
    <Setter Property="Padding" Value="10,5"/>
</Style>
```

---

## 3. BasedOn (Kế thừa)

```xml
<Style x:Key="BaseButton" TargetType="Button">
    <Setter Property="Padding" Value="15,8"/>
    <Setter Property="FontSize" Value="14"/>
</Style>

<Style x:Key="PrimaryButton" TargetType="Button" BasedOn="{StaticResource BaseButton}">
    <Setter Property="Background" Value="#4CAF50"/>
    <Setter Property="Foreground" Value="White"/>
</Style>

<Style x:Key="DangerButton" TargetType="Button" BasedOn="{StaticResource BaseButton}">
    <Setter Property="Background" Value="#F44336"/>
    <Setter Property="Foreground" Value="White"/>
</Style>
```

---

## 4. Triggers

### Property Trigger
```xml
<Style TargetType="Button">
    <Setter Property="Background" Value="#2196F3"/>
    <Style.Triggers>
        <Trigger Property="IsMouseOver" Value="True">
            <Setter Property="Background" Value="#1976D2"/>
        </Trigger>
        <Trigger Property="IsPressed" Value="True">
            <Setter Property="Background" Value="#0D47A1"/>
        </Trigger>
        <Trigger Property="IsEnabled" Value="False">
            <Setter Property="Opacity" Value="0.5"/>
        </Trigger>
    </Style.Triggers>
</Style>
```

### Data Trigger
```xml
<Style TargetType="TextBlock">
    <Style.Triggers>
        <DataTrigger Binding="{Binding Status}" Value="Error">
            <Setter Property="Foreground" Value="Red"/>
        </DataTrigger>
        <DataTrigger Binding="{Binding Status}" Value="Success">
            <Setter Property="Foreground" Value="Green"/>
        </DataTrigger>
    </Style.Triggers>
</Style>
```

### MultiTrigger
```xml
<Style.Triggers>
    <MultiTrigger>
        <MultiTrigger.Conditions>
            <Condition Property="IsMouseOver" Value="True"/>
            <Condition Property="IsEnabled" Value="True"/>
        </MultiTrigger.Conditions>
        <Setter Property="Background" Value="LightBlue"/>
    </MultiTrigger>
</Style.Triggers>
```

---

## 📝 Bài Tập
1. Tạo style hệ thống: PrimaryButton, SecondaryButton, DangerButton
2. Tạo styles cho form elements với hover/focus states

---

⬅️ [Bài 4.4](../04_Data_Binding/Bai_4.4_Data_Validation.md) | ➡️ [Bài 5.2](./Bai_5.2_Control_Templates.md)
