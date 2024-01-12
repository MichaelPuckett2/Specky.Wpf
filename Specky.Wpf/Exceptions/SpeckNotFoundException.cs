namespace Specky.Wpf.Exceptions;

[Serializable]
public sealed class SpeckNotFoundException : Exception
{
    public SpeckNotFoundException(string message) : base(message) { }
}
