﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

class RelayCommand : ICommand
{
    private Action<object> execute;
    private Func<object, bool> canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        if (execute == null)
        {
            throw new ArgumentNullException(nameof(execute));
        }
        else
        {
            this.execute = execute;
        }
        this.canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
        }
        remove
        {
            CommandManager.RequerySuggested -= value;
        }
    }

    public bool CanExecute(object parameter)
    {
        if (canExecute == null)
        {
            return true;
        }
        else
        {
            return canExecute(parameter);
        }
    }

    public void Execute(object parameter)
    {
        execute(parameter);
    }

}

