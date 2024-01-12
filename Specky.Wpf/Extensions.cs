namespace Specky.Wpf;

public static class Extensions
{
    public static IServiceProvider AddSpeckyWpf(this IServiceProvider serviceProvider)
    {
        Attachables.Specky.ServiceProvider = serviceProvider;
        return serviceProvider;
    }
}
