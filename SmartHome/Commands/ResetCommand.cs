﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SmartHome.Http;

namespace SmartHome.Commands
{
    public class ResetCommand : ICommand
    {

        public ResetCommand() { ; }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            DataProvider.Instance.UpdateDesiredValuesToDefault();
        }




    }
}
