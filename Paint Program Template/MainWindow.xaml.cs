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

namespace Paint_Program_Template
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastX = -1;
        double lastY = -1;
        double currentX = -1;
        double currentY = -1;
        Brush currentColor = Brushes.Red;
        double currentBrushSize = 5;
        public MainWindow()
        {
            InitializeComponent();
            drawingCanvas.Height = bgCanvas.Height * .7;
            drawingCanvas.Width = bgCanvas.Width * .7;
            Canvas.SetTop(drawingCanvas,(bgCanvas.Height - drawingCanvas.Height) / 2);
            Canvas.SetLeft(drawingCanvas,(bgCanvas.Width - drawingCanvas.Width) / 2);
        }

        public void renderPoint(Canvas parent,double x,double y) {
            Rectangle r = new Rectangle();
            r.Stroke = currentColor;
            r.Fill = currentColor;
            r.Height = currentBrushSize;
            r.Width = currentBrushSize;
            Canvas.SetTop(r, y);
            Canvas.SetLeft(r, x);
            parent.Children.Add(r);
        }

        public void draw(Object sender, RoutedEventArgs e)
        {
            Point p = Mouse.GetPosition((sender as Canvas));
            currentX = p.X;
            currentY = p.Y;
            if (System.Windows.Input.Mouse.LeftButton != MouseButtonState.Pressed) {
                lastX = currentX;
                lastY = currentY;
                return;
            }
            int steps = Math.Abs(currentX-lastX) > Math.Abs(currentY - lastY) ? (int)Math.Abs(currentX - lastX) : (int)Math.Abs(currentY - lastY);
            double xIncrement = (currentX - lastX) / (double)steps;
            double yIncrement = (currentY - lastY) / (double)steps;
            double tempX = lastX;
            double tempY = lastY;
            for (int i = 0;i<steps;i++) {
                renderPoint((sender as Canvas),tempX,tempY);
                tempX += xIncrement;
                tempY += yIncrement;
            }
            lastX = currentX;
            lastY = currentY;
        }
        public void selectColor(Object Sender, RoutedEventArgs e) {
            if (!(Sender as ComboBox).IsLoaded)
            {
                return;
            }
            ComboBox c = (Sender as ComboBox);
            ComboBoxItem ci = c.SelectedItem as ComboBoxItem;
            Rectangle r = ci.Content as Rectangle;
            currentColor = r.Fill;
        }
        public void selectSize(Object Sender, RoutedEventArgs e) {
            if (!(Sender as ComboBox).IsLoaded)
            {
                return;
            }
            ComboBox c = (Sender as ComboBox);
            ComboBoxItem ci = c.SelectedItem as ComboBoxItem;
            Rectangle r = ci.Content as Rectangle;
            currentBrushSize = r.Height;
        }
    }
}
