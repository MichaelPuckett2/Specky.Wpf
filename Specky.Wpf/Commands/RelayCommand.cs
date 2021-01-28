using Specky.Wpf.Exceptions;
using Specky.Wpf.Interfaces;
using System;
using System.Windows.Input;

namespace Specky.Wpf.Commands
{
    public sealed class RelayCommand : ICommand, IUpdateCommandCanExecute
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute) : this(execute, x => true) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new RelayCommandParameterNullException($"{nameof(execute)} cannot be null in {nameof(RelayCommand)}");
            this.canExecute = canExecute ?? throw new RelayCommandParameterNullException($"{nameof(canExecute)} cannot be null in {nameof(RelayCommand)}");
        }

        public bool CanExecute(object parameter) => canExecute.Invoke(parameter);
        public void Execute(object parameter) => execute.Invoke(parameter);
        public void UpdateCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
