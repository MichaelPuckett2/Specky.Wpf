using System;

namespace Specky.Wpf.Exceptions
{
    [Serializable]
    public sealed class RelayCommandParameterNullException : Exception
    {
        public RelayCommandParameterNullException(string message) : base(message) { }
    }
}
