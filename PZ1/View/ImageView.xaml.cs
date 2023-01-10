using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PZ1.ViewModel;

namespace PZ1.View
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : Window
    {
        public float x, y;


        public ImageView()
        {
            InitializeComponent();
        }

        public ImageView(float X, float Y, Canvas c, Stack<Canvas> undoStack, Stack<Canvas> redoStack, UIElement MainWindowChild, float positionX, float positionY)
        {
            InitializeComponent();
            DataContext = new ImageViewModel(c,undoStack, redoStack, MainWindowChild, positionX, positionY);
            x = X;
            y = Y;
        }
        private void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
               (DataContext as ImageViewModel).ImageSourceValue = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void DrawPictureButton(object sender, RoutedEventArgs e)
        {
            (DataContext as ImageViewModel).DrawImage(x, y);
            this.Close();
        }

       
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
