# Bài 8.2: Navigation

## 🎯 Mục Tiêu
- Frame và Page navigation
- UserControl-based navigation
- Navigation với MVVM

---

## 1. Frame & Page

```xml
<!-- MainWindow.xaml -->
<Window>
    <DockPanel>
        <StackPanel DockPanel.Dock="Left" Width="150">
            <Button Content="Home" Click="Home_Click"/>
            <Button Content="Products" Click="Products_Click"/>
            <Button Content="Settings" Click="Settings_Click"/>
        </StackPanel>
        
        <Frame x:Name="MainFrame" NavigationUIVisibility="Hidden"/>
    </DockPanel>
</Window>
```

```csharp
private void Home_Click(object sender, RoutedEventArgs e)
{
    MainFrame.Navigate(new HomePage());
}

private void Products_Click(object sender, RoutedEventArgs e)
{
    MainFrame.Navigate(new ProductsPage());
}
```

---

## 2. UserControl Navigation (MVVM)

```csharp
public class MainViewModel : BaseViewModel
{
    private BaseViewModel _currentView;
    
    public BaseViewModel CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }
    
    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateProductsCommand { get; }
    
    public MainViewModel()
    {
        NavigateHomeCommand = new RelayCommand(_ => CurrentView = new HomeViewModel());
        NavigateProductsCommand = new RelayCommand(_ => CurrentView = new ProductsViewModel());
        
        CurrentView = new HomeViewModel();
    }
}
```

```xml
<Window>
    <Window.Resources>
        <DataTemplate DataType="{x:Type vm:HomeViewModel}">
            <views:HomeView/>
        </DataTemplate>
        <DataTemplate DataType="{x:Type vm:ProductsViewModel}">
            <views:ProductsView/>
        </DataTemplate>
    </Window.Resources>
    
    <DockPanel>
        <StackPanel DockPanel.Dock="Left">
            <Button Content="Home" Command="{Binding NavigateHomeCommand}"/>
            <Button Content="Products" Command="{Binding NavigateProductsCommand}"/>
        </StackPanel>
        
        <ContentControl Content="{Binding CurrentView}"/>
    </DockPanel>
</Window>
```

---

## 3. Navigation Service

```csharp
public interface INavigationService
{
    void NavigateTo<TViewModel>() where TViewModel : BaseViewModel;
}

public class NavigationService : INavigationService
{
    private readonly Func<Type, BaseViewModel> _viewModelFactory;
    private readonly MainViewModel _mainViewModel;
    
    public NavigationService(MainViewModel mainVM, Func<Type, BaseViewModel> factory)
    {
        _mainViewModel = mainVM;
        _viewModelFactory = factory;
    }
    
    public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
    {
        _mainViewModel.CurrentView = _viewModelFactory(typeof(TViewModel));
    }
}
```

---

## 📝 Bài Tập
1. Implement navigation với 3 pages: Home, Products, Settings
2. Tạo NavigationService inject vào ViewModels

---

⬅️ [Bài 8.1](./Bai_8.1_Windows_Dialogs.md) | ➡️ [Bài 9.1](../09_Resources_Themes/Bai_9.1_Resources.md)
