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
    public class RectangleViewModel: BindableBase
    {
        public RectangleViewModel(Canvas c, Stack<Canvas> stack, Stack<Canvas> stack_redo, UIElement MainWindowChild, float positionX, float positionY)
        {
            Canvas = c;
            UndoStack = stack;
            RedoStack = stack_redo;
            redoStack = stack_redo;
            PositionX = positionX;
            PositionY = positionY;
            MainWindowChild = mainWindowChild;
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

        private string width;
        public string Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged();
            }

        }

        private Stack<Canvas> undoStack;
        public Stack<Canvas> UndoStack
        {
            get { return undoStack; }
            set { undoStack = value; }
        }

        private Stack<Canvas> redoStack;
        public Stack<Canvas> RedoStack
        {
            get { return redoStack; }
            set { redoStack = value; }
        }

        private Canvas canvas;
        public Canvas Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        private string height;
        public string Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged();
            }

        }

        private string borderThicknesValidation;
        public string BorderThicknesValidation
        {
            get { return borderThicknesValidation; }
            set
            {
                borderThicknesValidation = value;
                OnPropertyChanged();
            }
        }


        public void DrawRectangle(Brush fill, Brush border, float x, float y)
        {
            Rectangle rec = new Rectangle();
            rec.Width = float.Parse(Width);
            rec.Height = float.Parse(Height);
            rec.Fill = fill;
            rec.StrokeThickness = float.Parse(BorderThicknesValidation);
            rec.Stroke = border;
            rec.ClipToBounds = false;
            rec.MouseDown += Click;

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
            canvas.Children.Add(rec);
            if (x + rec.Width > 739)
                x = (float)(739 - rec.Width);
            Canvas.SetLeft(rec, x);
            if (y + rec.Height > 335)
                y = (float)(335 - rec.Height);
            Canvas.SetTop(rec, y);

        }
    }
}
