using Specky.Wpf.Exceptions;
using Specky.Wpf.Interfaces;
using System.Windows.Input;

namespace Specky.Wpf.Commands;

public sealed class RelayCommand(Action<object?> execute, Predicate<object?> canExecute) : ICommand, IUpdateCommandCanExecute
{
    private readonly Action<object?> execute = execute ?? throw new RelayCommandParameterNullException($"{nameof(execute)} cannot be null in {nameof(RelayCommand)}");
    private readonly Predicate<object?> canExecute = canExecute ?? throw new RelayCommandParameterNullException($"{nameof(canExecute)} cannot be null in {nameof(RelayCommand)}");

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(Action<object?> execute) : this(execute, x => true) { }

    public bool CanExecute(object? parameter) => canExecute.Invoke(parameter);
    public void Execute(object? parameter) => execute.Invoke(parameter);
    public void UpdateCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
