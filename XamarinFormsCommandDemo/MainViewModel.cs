using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinFormsCommandDemo
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region PropertyChange

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
        private double number;

        public double Number
        {
            get => number;
            set
            {
                SetProperty(ref number,value);
                (CalculateCommand as Command).ChangeCanExecute();
            }
        }
        private double result;

        public double Result
        {
            get => result;
            set
            {
                SetProperty(ref result, value);
            }
        }
        public ICommand CalculateCommand { get; set; }
        public MainViewModel()
        {
            CalculateCommand = new Command(Calculate,CanCalculate);
        }

        private bool CanCalculate(object arg)
        {
            return Number > 0;
        }

        private void Calculate(object obj)
        {
            Result = number * 5;
            (CalculateCommand as Command).ChangeCanExecute();
        }
      
    }
}

