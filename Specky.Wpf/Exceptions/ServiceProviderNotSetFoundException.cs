namespace Specky.Wpf.Exceptions;

[Serializable]
public sealed class ServiceProviderNotSetFoundException : Exception
{
    public ServiceProviderNotSetFoundException(string message) : base(message) { }
}
