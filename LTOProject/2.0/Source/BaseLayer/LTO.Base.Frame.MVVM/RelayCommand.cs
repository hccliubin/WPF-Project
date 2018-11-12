﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LTO.Base.Frame.MVVM
{
    /// <summary> 带参数的命令 </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> _action;
        public RelayCommand(Action<object> action)
        {
            _action = action;
        }
        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _action(parameter);
            }
            else
            {
                _action("Hello");
            }
        }
        #endregion



        /// <summary> 隐式转换 </summary>
        static public implicit operator RelayCommand(Action<object> action)
        {
            RelayCommand s = new RelayCommand(action);
            return s;
        }
    }
    
}
