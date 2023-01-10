using PZ1.View;
using PZ1.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PZ1.UserControl1
{
    /// <summary>
    /// Interaction logic for Colorpicker.xaml
    /// </summary>
    public partial class Colorpicker : UserControl
        
    {
        public Colorpicker()
        {
            InitializeComponent();
        }

        public Brush SelectedColor
        {
            get { return (Brush)GetValue(SelectedColorProperty); }
            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }


        // Using a DependencyProperty as the backing store for SelectedColor.  
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor",
        typeof(Brush), typeof(Colorpicker), new UIPropertyMetadata(null));

        //private void Changed(object sender, SelectionChangedEventArgs e)
        //{
        //    UploadTrue = true;
        //}

        //protected void OnPropertyChanged([CallerMemberName] string name = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}
        //public event PropertyChangedEventHandler PropertyChanged;


        //private bool uploadTrue = false;
        //public bool UploadTrue
        //{
        //    get { return uploadTrue; }
        //    set
        //    {
        //        uploadTrue = value;
        //        OnPropertyChanged();
        //    }
        //}
    }
}
