# Bài 6.2: Commands

## 🎯 Mục Tiêu
- Hiểu ICommand interface
- Tạo RelayCommand/DelegateCommand
- Sử dụng Commands trong MVVM

---

## 1. ICommand Interface

```csharp
public interface ICommand
{
    event EventHandler CanExecuteChanged;
    bool CanExecute(object parameter);
    void Execute(object parameter);
}
```

---

## 2. RelayCommand Implementation

```csharp
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
    public void Execute(object parameter) => _execute(parameter);
}
```

---

## 3. Sử Dụng Command trong ViewModel

```csharp
public class MainViewModel : BaseViewModel
{
    private string _name;
    public string Name { get => _name; set => SetProperty(ref _name, value); }
    
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    
    public MainViewModel()
    {
        SaveCommand = new RelayCommand(
            execute: _ => Save(),
            canExecute: _ => !string.IsNullOrEmpty(Name)
        );
        
        DeleteCommand = new RelayCommand(
            execute: param => Delete(param as Product),
            canExecute: param => param != null
        );
    }
    
    private void Save() { /* Save logic */ }
    private void Delete(Product product) { /* Delete logic */ }
}
```

---

## 4. Binding Commands trong XAML

```xml
<Button Content="Save" 
        Command="{Binding SaveCommand}"/>

<Button Content="Delete" 
        Command="{Binding DeleteCommand}"
        CommandParameter="{Binding SelectedProduct}"/>

<!-- Trong DataGrid -->
<DataGridTemplateColumn>
    <DataGridTemplateColumn.CellTemplate>
        <DataTemplate>
            <Button Content="Delete"
                    Command="{Binding DataContext.DeleteCommand, 
                             RelativeSource={RelativeSource AncestorType=DataGrid}}"
                    CommandParameter="{Binding}"/>
        </DataTemplate>
    </DataGridTemplateColumn.CellTemplate>
</DataGridTemplateColumn>
```

---

## 5. Built-in Commands

```xml
<Menu>
    <MenuItem Header="New" Command="ApplicationCommands.New"/>
    <MenuItem Header="Open" Command="ApplicationCommands.Open"/>
    <MenuItem Header="Save" Command="ApplicationCommands.Save"/>
</Menu>

<!-- Command Bindings -->
<Window.CommandBindings>
    <CommandBinding Command="ApplicationCommands.Save" 
                    Executed="Save_Executed"
                    CanExecute="Save_CanExecute"/>
</Window.CommandBindings>
```

---

## 📝 Bài Tập
1. Tạo ViewModel với commands: Add, Edit, Delete, Search
2. Implement CanExecute cho mỗi command

---

⬅️ [Bài 6.1](./Bai_6.1_Routed_Events.md) | ➡️ [Bài 7.1](../07_MVVM/Bai_7.1_Gioi_Thieu_MVVM.md)
