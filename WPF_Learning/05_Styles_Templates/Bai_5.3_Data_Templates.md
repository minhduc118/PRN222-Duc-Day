# Bài 5.3: Data Templates

## 🎯 Mục Tiêu
- DataTemplate cho data presentation
- ItemTemplate trong ListBox, ComboBox
- DataTemplateSelector

---

## 1. DataTemplate Là Gì?

DataTemplate định nghĩa cách **hiển thị data object**.

```xml
<ListBox ItemsSource="{Binding Products}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal" Margin="5">
                <Image Source="{Binding ImageUrl}" Width="50" Height="50"/>
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

## 2. DataTemplate trong Resources

```xml
<Window.Resources>
    <DataTemplate x:Key="ProductTemplate">
        <Border BorderBrush="#E0E0E0" BorderThickness="1" Padding="10" Margin="5">
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="60"/>
                    <ColumnDefinition Width="*"/>
                    <ColumnDefinition Width="Auto"/>
                </Grid.ColumnDefinitions>
                
                <Image Source="{Binding ImageUrl}" Width="50"/>
                <TextBlock Grid.Column="1" Text="{Binding Name}" VerticalAlignment="Center"/>
                <TextBlock Grid.Column="2" Text="{Binding Price, StringFormat='{}{0:C}'}"
                           FontWeight="Bold" Foreground="#4CAF50"/>
            </Grid>
        </Border>
    </DataTemplate>
</Window.Resources>

<ListBox ItemTemplate="{StaticResource ProductTemplate}"/>
```

---

## 3. DataType (Implicit DataTemplate)

```xml
<!-- Auto-apply cho tất cả Product objects -->
<DataTemplate DataType="{x:Type local:Product}">
    <TextBlock Text="{Binding Name}"/>
</DataTemplate>

<DataTemplate DataType="{x:Type local:Category}">
    <TextBlock Text="{Binding CategoryName}" FontWeight="Bold"/>
</DataTemplate>
```

---

## 4. DataTemplateSelector

```csharp
public class ProductTemplateSelector : DataTemplateSelector
{
    public DataTemplate NormalTemplate { get; set; }
    public DataTemplate FeaturedTemplate { get; set; }
    
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is Product product)
            return product.IsFeatured ? FeaturedTemplate : NormalTemplate;
        return base.SelectTemplate(item, container);
    }
}
```

```xml
<Window.Resources>
    <DataTemplate x:Key="NormalProduct">...</DataTemplate>
    <DataTemplate x:Key="FeaturedProduct">...</DataTemplate>
    
    <local:ProductTemplateSelector x:Key="ProductSelector"
        NormalTemplate="{StaticResource NormalProduct}"
        FeaturedTemplate="{StaticResource FeaturedProduct}"/>
</Window.Resources>

<ListBox ItemTemplateSelector="{StaticResource ProductSelector}"/>
```

---

## 5. HierarchicalDataTemplate (TreeView)

```xml
<TreeView ItemsSource="{Binding Categories}">
    <TreeView.ItemTemplate>
        <HierarchicalDataTemplate ItemsSource="{Binding Products}">
            <TextBlock Text="{Binding Name}" FontWeight="Bold"/>
            <HierarchicalDataTemplate.ItemTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding Name}"/>
                </DataTemplate>
            </HierarchicalDataTemplate.ItemTemplate>
        </HierarchicalDataTemplate>
    </TreeView.ItemTemplate>
</TreeView>
```

---

## 📝 Bài Tập
1. Tạo DataTemplate cho Contact list với avatar, name, email
2. Tạo DataTemplateSelector cho các status khác nhau (pending, completed, failed)

---

⬅️ [Bài 5.2](./Bai_5.2_Control_Templates.md) | ➡️ [Bài 6.1](../06_Commands_Events/Bai_6.1_Routed_Events.md)
