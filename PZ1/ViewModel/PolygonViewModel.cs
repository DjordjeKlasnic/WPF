using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PZ1.ViewModel
{
    class PolygonViewModel: BindableBase
    {
        public PolygonViewModel(Canvas c, Stack<Canvas> stack, Stack<Canvas> stack_redo, UIElement mainWindowChild, float positionX, float positionY)
        {
            Canvas = c;
            UndoStack = stack;
            redoStack = stack_redo;
            PositionX = positionX;
            PositionY = positionY;
            MainWindowChild = mainWindowChild;
        }

        private Stack<Canvas> redoStack;
        public Stack<Canvas> RedoStack
        {
            get { return redoStack; }
            set { redoStack = value; }
        }

        private string borderThicknes;
        public string BorderThicknes
        {
            get { return borderThicknes; }
            set
            {
                borderThicknes = value;
                OnPropertyChanged();
            }
        }

        private Canvas canvas;
        public Canvas Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        private Stack<Canvas> undoStack;
        public Stack<Canvas> UndoStack
        {
            get { return undoStack; }
            set { undoStack = value; }
        }

        private float positionX;
        public float PositionX
        {
            get { return positionX; }
            set { positionX = value; }
        }

        private float positionY;
        public float PositionY
        {
            get { return positionY; }
            set { positionY = value; }
        }

        private UIElement mainWindowChild;
        public UIElement MainWindowChild
        {
            get { return mainWindowChild; }
            set { mainWindowChild = value; }
        }


        public void DrawPolygon (Polygon polygon, Brush FillColor, Brush BorderColor)
        {
            Polygon p = polygon;
            p.Stroke = BorderColor;
            p.Fill = FillColor;
            p.StrokeThickness = float.Parse(BorderThicknes);
            p.ClipToBounds = false;
            p.MouseDown += Click;

            Canvas copy = canvas;
            Canvas copyState = new Canvas();
            foreach (UIElement child in copy.Children)
            {
                var xaml = System.Windows.Markup.XamlWriter.Save(child);
                var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                copyState.Children.Add(deepCopy);
            }
            undoStack.Push(copyState);
            RedoStack.Clear();
            canvas.Children.Add(p);
        }
    }
}
