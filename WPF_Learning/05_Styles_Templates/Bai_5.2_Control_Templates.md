# Bài 5.2: Control Templates

## 🎯 Mục Tiêu
- Hiểu ControlTemplate
- Tùy chỉnh hoàn toàn giao diện control
- TemplateBinding và ContentPresenter

---

## 1. ControlTemplate Là Gì?

ControlTemplate định nghĩa **cấu trúc visual** của control.

```xml
<Button Content="Custom Button">
    <Button.Template>
        <ControlTemplate TargetType="Button">
            <Border Background="{TemplateBinding Background}"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding BorderThickness}"
                    CornerRadius="5"
                    Padding="{TemplateBinding Padding}">
                <ContentPresenter HorizontalAlignment="Center"
                                  VerticalAlignment="Center"/>
            </Border>
        </ControlTemplate>
    </Button.Template>
</Button>
```

---

## 2. TemplateBinding

Bind properties từ templated control:

```xml
<ControlTemplate TargetType="Button">
    <Border Background="{TemplateBinding Background}"
            Padding="{TemplateBinding Padding}">
        <ContentPresenter/>
    </Border>
</ControlTemplate>
```

---

## 3. ContentPresenter

Hiển thị Content của control:

```xml
<ControlTemplate TargetType="Button">
    <Border>
        <!-- Content của Button sẽ hiển thị ở đây -->
        <ContentPresenter HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                          VerticalAlignment="{TemplateBinding VerticalContentAlignment}"/>
    </Border>
</ControlTemplate>
```

---

## 4. Template với Triggers

```xml
<ControlTemplate TargetType="Button">
    <Border x:Name="border" Background="#2196F3" CornerRadius="5" Padding="15,10">
        <ContentPresenter HorizontalAlignment="Center"/>
    </Border>
    <ControlTemplate.Triggers>
        <Trigger Property="IsMouseOver" Value="True">
            <Setter TargetName="border" Property="Background" Value="#1976D2"/>
        </Trigger>
        <Trigger Property="IsPressed" Value="True">
            <Setter TargetName="border" Property="Background" Value="#0D47A1"/>
        </Trigger>
    </ControlTemplate.Triggers>
</ControlTemplate>
```

---

## 5. Rounded TextBox Example

```xml
<Style TargetType="TextBox">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="TextBox">
                <Border Background="{TemplateBinding Background}"
                        BorderBrush="{TemplateBinding BorderBrush}"
                        BorderThickness="{TemplateBinding BorderThickness}"
                        CornerRadius="5">
                    <ScrollViewer x:Name="PART_ContentHost" Margin="5,0"/>
                </Border>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

---

## 📝 Bài Tập
1. Tạo Custom Button với gradient background và rounded corners
2. Tạo Custom CheckBox với custom check mark

---

⬅️ [Bài 5.1](./Bai_5.1_Styles.md) | ➡️ [Bài 5.3](./Bai_5.3_Data_Templates.md)
