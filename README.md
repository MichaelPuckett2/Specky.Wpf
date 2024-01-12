# Specky.WPF
Assist's with injecting types in WPF applications via XAML. Also provides basic ViewModelBase and RelayCommand.

## Example of injecting DataContext in XAML

When building services at start up.
```    
using Microsoft.Extensions.DependencyInjection;
using Specky.Wpf;
using Specky.Wpf.Exceptions;
using Specky7;
using System.Windows;

namespace iSam.WPF;

public partial class App : Application
{
    private readonly IServiceProvider serviceProvider;
    public App()
    {
        var services = new ServiceCollection();
        services.AddSpecks(options => options.AddAssemblies([typeof(App).Assembly, typeof(Core.Logging.LogEngine).Assembly, typeof(User32.Hooking.HookBase).Assembly]));
        serviceProvider = services.BuildServiceProvider();
        serviceProvider.AddSpeckyWpf();
    }

    private void ApplicationStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = serviceProvider.GetService<MainWindow>()
              ?? throw new SpeckNotFoundException($"The type {nameof(WPF.MainWindow)} was not found in the service provider.");
        mainWindow.Show();
    }
}
```
On any XAML view - example:
```
xmlns:Specky="clr-namespace:Specky.Wpf.Attachables;assembly=Specky.Wpf"
Specky:Specky.DataContext="{x:Type ViewModels:ExampleViewModel}"
```
Example full view

```
<Window x:Class="iSam.WPF.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:Specky="clr-namespace:Specky.Wpf.Attachables;assembly=Specky.Wpf"
        xmlns:ViewModels="clr-namespace:iSam.WPF.ViewModels"
        Specky:Specky.DataContext="{x:Type ViewModels:TestViewModel}"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <Label Content="{Binding TextName}" />
    </Grid>
</Window>
```