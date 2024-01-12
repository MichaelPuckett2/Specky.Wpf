using Specky.Wpf.Exceptions;
using System.Windows;

namespace Specky.Wpf.Attachables;

public sealed class Specky
{
    public static IServiceProvider ServiceProvider { get; set; } = new EmptyServiceProvider();
    private const string DataContext = "DataContext";
    public static object GetDataContext(DependencyObject obj)
        => obj.GetValue(DataContextProperty);
    public static void SetDataContext(DependencyObject obj, object value)
        => obj.SetValue(DataContextProperty, value);

    /// <summary>
    /// Example usage:
    /// xmlns:Specky="clr-namespace:Specky.Wpf.Attachables;assembly=Specky.Wpf"
    /// Specky:Specky.DataContext="{x:Type ViewModels:ExampleViewModel}"
    /// </summary>
    public static readonly DependencyProperty DataContextProperty =
        DependencyProperty.RegisterAttached(DataContext, typeof(object), typeof(Specky), new PropertyMetadata(null,
        new PropertyChangedCallback((s, e) =>
        {
            if (s is FrameworkElement frameworkElement && e.NewValue is Type type)
            {
                if (frameworkElement.IsLoaded)
                {
                    LoadSpeck(frameworkElement, type);
                }
                else
                {
                    frameworkElement.Loaded += FrameworkElementLoaded;
                }

                void FrameworkElementLoaded(object sender, RoutedEventArgs e)
                {
                    frameworkElement.Loaded -= FrameworkElementLoaded;
                    LoadSpeck(frameworkElement, type);
                }

                static void LoadSpeck(FrameworkElement frameworkElement, Type type)
                {
                    if (ServiceProvider is EmptyServiceProvider)
                    {
                        throw new Exception("The service provider has not been set. You must set the service provider during startup.  ex: Specky.Wpf.Attachables.Specky.ServiceProvider = builder.Services");
                    }
                    var speck = ServiceProvider.GetService(type) ?? throw new SpeckNotFoundException($"The type {type.Name} was not found in the service provider.");
                    frameworkElement.DataContext = speck;
                }
            }
        })));
}

internal class EmptyServiceProvider : IServiceProvider
{
    public object? GetService(Type serviceType) => null;
}
