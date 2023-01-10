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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PZ1.ViewModel;

namespace PZ1.View
{
    /// <summary>
    /// Interaction logic for EllipseView.xaml
    /// </summary>
    public partial class EllipseView : Window
    {
        public float x, y;

        public EllipseView()
        {
            InitializeComponent();
        }

        public EllipseView(float X, float Y, Canvas c, Stack<Canvas> undoStack, Stack<Canvas> redoStack, UIElement MainWindowChild, float positionX,float positionY)
        {
            InitializeComponent();
            DataContext = new EllipseViewModel(c, undoStack, redoStack, MainWindowChild, positionX, positionY);
            x = X;
            y = Y;
        }
        public EllipseView(Ellipse e, Canvas c)
        {
            InitializeComponent();
            DataContext = new EllipseViewModel(e,c);
            Radiusx.IsReadOnly = true;
            FillColor.SelectedColor = e.Fill;
            BorderColor.SelectedColor = e.Stroke;
        }

        private void BClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DrawEllipseButton(object sender, RoutedEventArgs e)
        {
            (DataContext as EllipseViewModel).DrawEllipse(FillColor.SelectedColor, BorderColor.SelectedColor, x, y);
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
}
}
