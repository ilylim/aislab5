using System;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Действие, совершаемое командой
        /// </summary>
        private Action<object> execute;

        /// <summary>
        /// Ограничение возможности вызова команды 
        /// </summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Событие изменения возможности вызова команды
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Конструктор создания команды с ограничениями 
        /// </summary>
        /// <param name="execute">вызываемый метод при вызове команды</param>
        /// <param name="canExecute">ограничения</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
