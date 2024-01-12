# Specky.WPF
Assist's with injecting types in WPF applications via XAML. Also provides basic ViewModelBase and RelayCommand.

## Example of injecting DataContext in XAML

When building services at start up.
```    
    builder.Services.AddSpeckyWpf();
```
On any XAML view - example:
```
    xmlns:Specky="clr-namespace:Specky.Wpf.Attachables;assembly=Specky.Wpf"
    Specky:Specky.DataContext="{x:Type ViewModels:ExampleViewModel}"
```