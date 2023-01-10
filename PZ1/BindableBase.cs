using PZ1.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PZ1
{
    public class BindableBase : INotifyPropertyChanged
    {

        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel MWVM = Application.Current.MainWindow.DataContext as MainWindowViewModel;

            Point p = Mouse.GetPosition((Application.Current.MainWindow as MainWindow).canvasName);
            MWVM.Child = sender as UIElement;
            MWVM.MousePositionX = (float)p.X;
            MWVM.MousePositionY = (float)p.Y;
        }
    }
}
