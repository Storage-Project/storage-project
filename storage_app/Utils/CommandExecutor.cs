using System;
using System.Windows.Input;

namespace storage_app.Utils
{
    internal class CommandExecutor : ICommand
    {
        public bool IsFunc;

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

        public dynamic ExecuteFunc(object? parameter)
        {
            return Func(parameter);
        }

        private readonly Action<object?> Action;
        private readonly Func<object?, dynamic> Func;
        public CommandExecutor(Action<object?> action)
        {
            Action = action;
            Func = (_) => { return false; };
            IsFunc = false;
        }
        public CommandExecutor(Func<object?, dynamic> func)
        {
            Action = (_) => { };
            Func = func;
            IsFunc = true;
        }
    }
}
