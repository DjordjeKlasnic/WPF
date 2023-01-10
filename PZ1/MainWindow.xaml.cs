using PZ1.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PZ1
{
    public enum Shape { ellipse=0, rectangle, polygone, image, none }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(canvasName);
        }

        private void Ellipse_Click(object sender, RoutedEventArgs e)
        {
            this.ellipse.Background = Brushes.DeepSkyBlue;
            this.rectangle.Background = Brushes.WhiteSmoke;
            this.polygone.Background = Brushes.WhiteSmoke;
            this.image.Background = Brushes.WhiteSmoke;
            (DataContext as MainWindowViewModel).Shape = Shape.ellipse;
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            this.ellipse.Background = Brushes.WhiteSmoke;
            this.rectangle.Background = Brushes.DeepSkyBlue;
            this.polygone.Background = Brushes.WhiteSmoke;
            this.image.Background = Brushes.WhiteSmoke;
            (DataContext as MainWindowViewModel).Shape = Shape.rectangle;
        }

        private void Polygone_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).PolygonDots.Clear();
            this.ellipse.Background = Brushes.WhiteSmoke;
            this.rectangle.Background = Brushes.WhiteSmoke;
            this.polygone.Background = Brushes.DeepSkyBlue;
            this.image.Background = Brushes.WhiteSmoke;
            (DataContext as MainWindowViewModel).Shape = Shape.polygone;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            this.ellipse.Background = Brushes.WhiteSmoke;
            this.rectangle.Background = Brushes.WhiteSmoke;
            this.polygone.Background = Brushes.WhiteSmoke;
            this.image.Background = Brushes.DeepSkyBlue;
            (DataContext as MainWindowViewModel).Shape = Shape.image;
        }

        private void RightClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).RightClickFunc(Mouse.GetPosition(sender as Canvas));
        }


        private void LeftUP(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).LeftClickFunc(Mouse.GetPosition(sender as Canvas));
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).ClearCanvas();
        }


        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).UndoCanvas();
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).RedoCanvas();
        }
    }
}
