using PZ1.UserControl1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PZ1.ViewModel
{
    public delegate void ElementAddedEvent(Ellipse ellipse);
    public class EllipseViewModel : BindableBase
    {
        public ElementAddedEvent ElementAddedEvent;
        public bool edit;
        public Ellipse e;
        public EllipseViewModel(Canvas c, Stack<Canvas> stack, Stack<Canvas> stack_redo, UIElement mainWindowChild, float positionX, float positionY)
        {
            edit = false;
            Canvas = c;
            UndoStack = stack;
            redoStack = stack_redo;
            PositionX = positionX;
            PositionY = positionY;
            MainWindowChild = mainWindowChild;
        }

        public EllipseViewModel(Ellipse ell, Canvas c)
        {
            Canvas = c;
            e = ell;
            edit = true;
            RadiusXValidation = (e.Width / 2).ToString();
            radiusYValidation = (e.Width / 2).ToString();
            borderThickValidation = e.StrokeThickness.ToString();
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

        private string radiusXValidation;
        public string RadiusXValidation
        {
            get { return radiusXValidation; }
            set
            {
                radiusXValidation = value;
                OnPropertyChanged();
            }

        }

        private string radiusYValidation;
        public string RadiusYValidation
        {
            get { return radiusYValidation; }
            set
            {
                radiusYValidation = value;
                OnPropertyChanged();
            }

        }

        private string borderThickValidation;
        public string BorderThickValidation
        {
            get { return borderThickValidation; }
            set
            {
                borderThickValidation = value;
                OnPropertyChanged();
            }

        }

        public void DrawEllipse(Brush fill, Brush border, float x, float y)
        {
            if (edit == false)
            {
                Ellipse el = new Ellipse();
                el.Width = 2 * float.Parse(RadiusXValidation);
                el.Height = 2 * float.Parse(RadiusYValidation);
                el.Fill = fill;
                el.StrokeThickness = float.Parse(BorderThickValidation);
                el.Stroke = border;
                el.ClipToBounds = false;

                ElementAddedEvent?.Invoke(el);

                el.MouseDown += Click;
                Canvas copy = canvas;
                Canvas copyState = new Canvas();
                foreach (UIElement child in copy.Children)
                {
                    var xaml = System.Windows.Markup.XamlWriter.Save(child);
                    var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                    copyState.Children.Add(deepCopy);
                }
                UndoStack.Push(copyState);
                RedoStack.Clear();
                canvas.Children.Add(el);
                if (x + el.Width > 739)
                    x = (float)(739 - el.Width);
                Canvas.SetLeft(el, x);
                if (y + el.Height > 335)
                    y = (float)(335 - el.Height);
                Canvas.SetTop(el, y);
            }
            else
            {
                Ellipse el = new Ellipse();
                el.Width = 2 * float.Parse(RadiusXValidation);
                el.Height = 2 * float.Parse(RadiusYValidation);
                el.Fill = fill;
                el.StrokeThickness = float.Parse(BorderThickValidation);
                el.Stroke = border;
                el.ClipToBounds = false;
                canvas.Children.Add(el);
                Canvas.SetTop(el, Canvas.GetTop(e));
                Canvas.SetLeft(el, Canvas.GetLeft(e));
                canvas.Children.Remove(e);
            }
        }

    }


}
