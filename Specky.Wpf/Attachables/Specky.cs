using System;
using System.Windows;

namespace Specky.Wpf.Attachables
{
    public sealed class Ioc
    {
        private const string DataContext = "DataContext";
        public static object GetDataContext(DependencyObject obj)
            => obj.GetValue(DataContextProperty);
        public static void SetDataContext(DependencyObject obj, object value)
            => obj.SetValue(DataContextProperty, value);

        public static readonly DependencyProperty DataContextProperty =
            DependencyProperty.RegisterAttached(DataContext, typeof(object), typeof(Ioc), new PropertyMetadata(null,
            new PropertyChangedCallback((s, e) =>
            {
                if (s is FrameworkElement frameworkElement && e.NewValue is Type type)
                {
                    if (frameworkElement.IsLoaded)
                    {
                        var speck = SpeckyContainer.Instance.GetSpeck(type);
                        frameworkElement.DataContext = speck;
                    }
                    else
                    {
                        frameworkElement.Loaded += FrameworkElementLoaded;
                    }

                    void FrameworkElementLoaded(object sender, RoutedEventArgs e)
                    {
                        frameworkElement.Loaded -= FrameworkElementLoaded;
                        var speck = SpeckyContainer.Instance.GetSpeck(type);
                        frameworkElement.DataContext = speck;
                    }
                }
            })));
    }
}
