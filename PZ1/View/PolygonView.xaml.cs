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
using PZ1.ViewModel;

namespace PZ1.View
{
    /// <summary>
    /// Interaction logic for PolygonView.xaml
    /// </summary>
    public partial class PolygonView : Window
    {
        private Queue<Point> polygonDots;
        public PolygonView()
        {
            InitializeComponent();
        }

        public PolygonView(Queue<Point> PolygonDots, Canvas c, Stack<Canvas> undoStack, Stack<Canvas> redoStack, UIElement MainWindowChild, float positionX, float positionY)
        {

            InitializeComponent();
            DataContext = new PolygonViewModel(c,undoStack, redoStack, MainWindowChild, positionX, positionY);
            polygonDots = PolygonDots;
        }

        private void Bclose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Polygon polygon = new Polygon();
            foreach(Point p in polygonDots)
            {
                polygon.Points.Add(p);
            }
           (DataContext as PolygonViewModel).DrawPolygon(polygon, FillColor.SelectedColor, BorderColor.SelectedColor);
            this.Close();         
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
