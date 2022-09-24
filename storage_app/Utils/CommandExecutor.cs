using System;
using System.Windows.Input;

namespace storage_app.Utils
{
    internal class CommandExecutor : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {

            Action(parameter);
        }

        private readonly Action<object?> Action;
        public CommandExecutor(Action<object?> action)
        {
            Action = action;
        }
    }
}
