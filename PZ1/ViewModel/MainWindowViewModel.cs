using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PZ1.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private List<Point> pointList = new List<Point>();
        public List<Point> PointList
        {
            get { return pointList; }
            set { pointList = value; }
        }

        private UIElement child;
        public UIElement Child
        {
            get { return child; }
            set { child = value; }
        }

        private Shape shape = Shape.none;
        public Shape Shape
        {
            get { return shape; }
            set { shape = value; }
        }


        private float mousePositionX;
        public float MousePositionX
        {
            get { return mousePositionX; }
            set { mousePositionX = value; }
        }

        private float mousePositionY;
        public float MousePositionY
        {
            get { return mousePositionY; }
            set { mousePositionY = value; }
        }


        private Queue<Point> polygonDots = new Queue<Point>();
        public Queue<Point> PolygonDots
        {
            get { return polygonDots; }
            set { polygonDots = value; }
        }

        private Stack<Canvas> undoStack = new Stack<Canvas>();
        public Stack<Canvas> UndoStack
        {
            get { return undoStack; }
            set { undoStack = value; }
        }
        private Stack<Canvas> redoStack = new Stack<Canvas>();
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
        public MainWindowViewModel(Canvas c)
        {
            canvas = c;
        }

        public void RightClickFunc(Point MousePosition)
        {
            if (child != null)
            {
                mousePositionX = (float)MousePosition.X;
                mousePositionY = (float)MousePosition.Y;

                UIElement moveUI;
                int i = 0;
                moveUI = (child as Rectangle);
                if (moveUI != null)
                {
                    Rectangle rectangle = child as Rectangle;   
                    return;
                }

                moveUI = (child as Ellipse);
                if (moveUI != null)
                {
                    Ellipse ellipse = child as Ellipse;
                    View.EllipseView ellipseView = new View.EllipseView(ellipse,canvas);
                    ellipseView.ShowDialog();
                    return;
                }

                moveUI = (child as Polygon);
                if (moveUI != null)
                {
                    Polygon polygon = child as Polygon;
                    return;
                }

                moveUI = (child as Image);
                if (moveUI != null)
                {
                    Image image = child as Image;
                    return;
                }



            }
            else
            {
                MousePositionX = (float)MousePosition.X;
                MousePositionY = (float)MousePosition.Y;

                switch (Shape)
                {
                    case Shape.ellipse:
                        View.EllipseView ellipseView = new View.EllipseView(MousePositionX, MousePositionY, Canvas, UndoStack, RedoStack, Child, MousePositionX, MousePositionY);
                        (ellipseView.DataContext as EllipseViewModel).ElementAddedEvent += AddElementToCanvas;
                        ellipseView.ShowDialog();
                        return;
                    case Shape.rectangle:
                        View.RectangleView rectangleView = new View.RectangleView(MousePositionX, MousePositionY, canvas, undoStack, redoStack, Child, MousePositionX, MousePositionY);
                        rectangleView.ShowDialog();
                        return;
                    case Shape.polygone:
                        PolygonDots.Enqueue(MousePosition);
                        return;
                    case Shape.image:
                        View.ImageView imageView = new View.ImageView(MousePositionX, MousePositionY, canvas, undoStack, redoStack, Child, MousePositionX, MousePositionY);
                        imageView.ShowDialog();
                        return;
                    default:
                        return;
                }
            }
        }

        private void AddElementToCanvas(Ellipse ellipse)
        {
            Console.WriteLine();
        }

        public void LeftClickFunc(Point MousePoint)
        {
            if (child != null)
            {
                Canvas copyState = new Canvas();
                foreach (UIElement child in canvas.Children)
                {
                    var xaml = System.Windows.Markup.XamlWriter.Save(child);
                    var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                    copyState.Children.Add(deepCopy);
                }
                undoStack.Push(copyState);

                float X = (float)MousePoint.X;
                float Y = (float)MousePoint.Y;

                UIElement moveUI;
                int i = 0;
                moveUI = (child as Rectangle);
                if (moveUI == null)
                {
                    moveUI = (child as Ellipse);
                    i++;
                    if (moveUI == null)
                    {
                        moveUI = (child as Image);
                        i++;
                        if (moveUI == null)
                        {
                            i++;
                        }
                    }
                }
                if (i < 3)
                {
                    double left = Canvas.GetLeft(child);
                    double top = Canvas.GetTop(child);
                    double setLeft = X - (MousePositionX - left);
                    double setTop = Y - (MousePositionY - top);

                    double Width;
                    double Height;
                    switch (i)
                    {
                        case 0:
                            Width = (child as Rectangle).Width;
                            Height = (child as Rectangle).Height;
                            break;
                        case 1:
                            Width = (child as Ellipse).Width;
                            Height = (child as Ellipse).Height;
                            break;
                        default:
                            Width = (child as Image).Width;
                            Height = (child as Image).Height;
                            break;
                    }

                    if (setLeft + Width > 738)
                        setLeft = (float)(738 - Width);
                    else if (setLeft < 0)
                        setLeft = 0;
                    Canvas.SetLeft(child, setLeft);
                    if (setTop + Height > 335)
                        setTop = (float)(335 - Height);
                    else if (setTop < 0)
                        setTop = 0;
                    Canvas.SetTop(child, setTop);
                }
                else
                {
                    pointList.Clear();
                    Polygon polygon = child as Polygon;
                    foreach (Point point in polygon.Points)
                    {
                        Point newPoint = new Point(point.X + (X - MousePositionX), point.Y + (Y - MousePositionY));
                        pointList.Add(newPoint);
                    }
                    double x = 0, y = 0;
                    bool correct;
                    do
                    {
                        x = 0;
                        y = 0;
                        correct = true;
                        foreach (Point point in pointList)
                        {
                            if (point.X < 0)
                                x = -point.X;
                            else if (point.X > 738)
                                x = -point.X + 738;
                            if (point.Y < 0)
                                y = -point.Y;
                            else if (point.Y > 334)
                                y = -point.Y + 334;
                        }
                        if (x != 0 || y != 0)
                        {
                            CorrectPolygon(x, y);
                            correct = false;
                        }
                    } while (correct != true);

                    polygon.Points.Clear();
                    foreach (Point pointAdd in pointList)
                    {
                        polygon.Points.Add(pointAdd);
                    }
                }
                child = null;

            }
            else
            {
                if (Shape == Shape.polygone)
                {
                    View.PolygonView polygonView = new View.PolygonView(PolygonDots, canvas, undoStack, redoStack, Child, MousePositionX, MousePositionY);
                    polygonView.ShowDialog();
                    PolygonDots.Clear();
                }
            }
        }

        private void CorrectPolygon(double correctX, double correctY)
        {
            List<Point> pointListcopy = new List<Point>();

            foreach (Point point in pointList)
            {
                Point p = point;
                p.X += correctX;
                p.Y += correctY;
                pointListcopy.Add(p);
            }
            pointList.Clear();
            pointList = pointListcopy;
        }

        public void ClearCanvas()
        {
            if (canvas.Children.Count != 0)
            {
                Canvas copyState = new Canvas();
                foreach (UIElement child in canvas.Children)
                {
                    var xaml = System.Windows.Markup.XamlWriter.Save(child);
                    var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                    copyState.Children.Add(deepCopy);
                }
                undoStack.Push(copyState);
                RedoStack.Clear();
                canvas.Children.Clear();
            }
        }

        public void UndoCanvas()
        {
            if (undoStack.Count != 0)
            {
                //this.redo.IsEnabled = true;
                Canvas redo = undoStack.Pop();
                Canvas copyState = new Canvas();
                foreach (UIElement child in canvas.Children)
                {
                    var xaml = System.Windows.Markup.XamlWriter.Save(child);
                    var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                    copyState.Children.Add(deepCopy);
                }
                redoStack.Push(copyState);

                canvas.Children.Clear();
                foreach (UIElement child in redo.Children)
                {
                    var xaml = System.Windows.Markup.XamlWriter.Save(child);
                    var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                    deepCopy.ClipToBounds = false;
                    deepCopy.MouseDown += Click;
                    canvas.Children.Add(deepCopy);
                }
            }
        }

        public void RedoCanvas()
        {
            if (redoStack.Count != 0)
            {
                Canvas undo = redoStack.Pop();
                //if (redoStack.Count == 0)
                //    this.redo.IsEnabled = false;

                Canvas copyState = new Canvas();
                foreach (UIElement child in canvas.Children)
                {
                    var xaml = System.Windows.Markup.XamlWriter.Save(child);
                    var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                    copyState.Children.Add(deepCopy);
                }
                undoStack.Push(copyState);
                canvas.Children.Clear();
                foreach (UIElement child in undo.Children)
                {
                    var xaml = System.Windows.Markup.XamlWriter.Save(child);
                    var deepCopy = System.Windows.Markup.XamlReader.Parse(xaml) as UIElement;
                    deepCopy.ClipToBounds = false;
                    deepCopy.MouseDown += Click;
                    canvas.Children.Add(deepCopy);
                }
            }
        }
    }
}