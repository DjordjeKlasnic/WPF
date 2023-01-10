using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace PZ1.ViewModel
{
    public class ImageViewModel:BindableBase
    {
        public ImageViewModel(Canvas c, Stack<Canvas> stack, Stack<Canvas> stack_redo, UIElement mainWindowChild, float positionX, float positionY)
        {
            Canvas = c;
            UndoStack = stack;
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

        private ImageSource imageSourceValue;
        public ImageSource ImageSourceValue
        {
            get { return imageSourceValue; }
            set
            {
                imageSourceValue = value;
                OnPropertyChanged();
            }
        }

        private Stack<Canvas> undoStack;
        public Stack<Canvas> UndoStack
        {
            get { return undoStack; }
            set { undoStack = value; }
        }

        public void DrawImage(float x, float y)
        {
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Source = ImageSourceValue;
            image.Width = double.Parse(Width);
            image.Height = double.Parse(Height);
            image.ClipToBounds = false;
            image.MouseDown += Click;
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

            canvas.Children.Add(image);
            if (x + image.Width > 739)
                x = (float)(739 - image.Width);
            Canvas.SetLeft(image, x);
            if (y + image.Height > 335)
                y = (float)(335 - image.Height);
            Canvas.SetTop(image, y);
        }
    }
}
