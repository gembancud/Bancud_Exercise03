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

namespace Exercise03_B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LinkedList<Outline> _outline;
        private Outline _tempOutline;
        private Node<Outline> _generationPointer;
        private int _numberOfEvolutionPoints;
        private int GenerationCounter;

        public MainWindow()
        {
            InitializeComponent();
            _outline = new LinkedList<Outline>(); ;
        }
        //Creatye Outline Event
        private void CreateOutlineEvent(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("DEBUG: Create Outline");
            _tempOutline = new Outline();
            OutlineCanvas.MouseDown += new MouseButtonEventHandler(CreateCoordinateInCanvas);

            FinishCreationButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
            CreateOutlineButton.IsEnabled = false;

        }
        //Takes point from Pointer in canvas
        private void CreateCoordinateInCanvas(object sender, RoutedEventArgs e)
        {
            Point P = Mouse.GetPosition(OutlineCanvas);
            PaintCoordinateInCanvas(P, 0);
            Coordinate newCoordinate = new Coordinate(Convert.ToDouble(P.X), Convert.ToDouble(P.Y));
            _tempOutline.CoordinateList.AddToTail(newCoordinate);
        }
        // Paints coordinate with coordinate values beside it
        private void PaintCoordinateInCanvas(Point p, double importance)
        {
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = Brushes.Blue;
            newEllipse.Width = 12;
            newEllipse.Height = 12;

            Canvas.SetTop(newEllipse, p.Y - 6);
            Canvas.SetLeft(newEllipse, p.X - 6);

            if (NodesCheckBox.IsChecked == true) OutlineCanvas.Children.Add(newEllipse);

            TextBlock newTextBlock = new TextBlock();
            if (CoordinatesCheckBox.IsChecked == true) newTextBlock.Text = ($"({(int)p.X},{(int)p.Y})\n");
            if (ImportanceCheckBox.IsChecked == true) newTextBlock.Text += ($"Importance:{(int)importance}");
            newTextBlock.Foreground = Brushes.White;
            newTextBlock.Background = Brushes.Black;
            newTextBlock.Width = 80;
            newTextBlock.Height = 32;

            Canvas.SetTop(newTextBlock, p.Y);
            Canvas.SetLeft(newTextBlock, p.X);

            if (CoordinatesCheckBox.IsChecked == true || ImportanceCheckBox.IsChecked == true)
                OutlineCanvas.Children.Add(newTextBlock);

        }
        // Paints Lines in between Coordinates
        private void PaintLineInCanvas(Point o, Point p)
        {
            Line newLine = new Line();
            newLine.Stroke = Brushes.Crimson;

            newLine.X1 = o.X;
            newLine.X2 = p.X;
            newLine.Y1 = o.Y;
            newLine.Y2 = p.Y;

            newLine.StrokeThickness = 2;
            if (LinesCheckBox.IsChecked == true)
                OutlineCanvas.Children.Add(newLine);

            double sumOfSquares = Math.Pow(o.X - p.X, 2) + Math.Pow(o.Y - p.Y, 2);
            double distance = Math.Sqrt(sumOfSquares);

            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Text = "d=" + (int)distance;
            newTextBlock.Foreground = Brushes.Cyan;
            newTextBlock.Background = Brushes.DarkOliveGreen;
            newTextBlock.Width = 45;
            newTextBlock.Height = 18;

            Canvas.SetTop(newTextBlock, (o.Y + p.Y) / 2);
            Canvas.SetLeft(newTextBlock, (o.X + p.X) / 2);

            if (DistanceCheckBox.IsChecked == true)
                OutlineCanvas.Children.Add(newTextBlock);
        }
        //Finish Outline Buttom Command
        private void FinishOutlineEvent(object sender, RoutedEventArgs e)
        {
            CreateOutlineButton.IsEnabled = false;
            FinishCreationButton.IsEnabled = false;
            PEvolutionButton.IsEnabled = true;
            GenerationTextBox.IsEnabled = true;


            OutlineCanvas.MouseDown -= new MouseButtonEventHandler(CreateCoordinateInCanvas);
            EvolveButton.IsEnabled = true;
            // MessageBox.Show("Debug: creation done");
            _outline.AddToTail(_tempOutline);
            GenerationCounter = 0;

            _tempOutline = _outline.Tail.Data;
            PrintOutline();
        }
        //Generic Evolve function
        private void DoEvolve(int generations)
        {
            for (int i = 0; i < generations; i++)
            {
                _outline.AddToTail(_outline.Tail.Data.Evolve());
                GenerationCounter++;
            }

            if (_outline.Tail.Data.CoordinateList.Size == 0)
            {
                EvolveButton.IsEnabled = false;
                PEvolutionButton.IsEnabled = false;
            }

        }
        //Prints _tempOutline in canvas
        private void PrintOutline()
        {
            //  MessageBox.Show("Debug: PrintOutline Reached");
            OutlineCanvas.Children.Clear();
            foreach (Node<Coordinate> coor in _tempOutline.CoordinateList)
            {
                Point p = new Point(coor.Data.X, coor.Data.Y);
                Point o = new Point(coor.Prev.Data.X, coor.Prev.Data.Y);
                PaintCoordinateInCanvas(p, coor.Data.Importance);
                PaintLineInCanvas(o, p);
            }
            GenerationCount.Text = GenerationCounter.ToString();
        }
        //Singular Evolution Command Event
        private void EvolveButtonEvent(object sender, RoutedEventArgs e)
        {
            PEvolutionForwardButton.IsEnabled = true;
            PEvolutionBackButton.IsEnabled = true;
            PEvolutionOriginButton.IsEnabled = true;
            PEvolutionFinaleButton.IsEnabled = true;


            DoEvolve(1);
            _generationPointer = _outline.Tail;
            _tempOutline = _outline.Tail.Data;
            PrintOutline();
        }
        //Reset Button Event
        private void ResetButtonCommand(object sender, RoutedEventArgs e)
        {
            CreateOutlineButton.IsEnabled = true;
            EvolveButton.IsEnabled = false;
            PEvolutionButton.IsEnabled = false;
            PEvolutionForwardButton.IsEnabled = false;
            PEvolutionBackButton.IsEnabled = false;
            PEvolutionOriginButton.IsEnabled = false;
            PEvolutionFinaleButton.IsEnabled = false;
            GenerationTextBox.IsEnabled = false;

            _outline = new LinkedList<Outline>();
            OutlineCanvas.Children.Clear();
            GenerationCounter = 0;
        }

        #region Progressive Evolution Cluster

        private void PEvolutionButtonEvent(object sender, RoutedEventArgs e)
        {
            PEvolutionForwardButton.IsEnabled = true;
            PEvolutionBackButton.IsEnabled = true;
            PEvolutionOriginButton.IsEnabled = true;
            PEvolutionFinaleButton.IsEnabled = true;

            _numberOfEvolutionPoints = Convert.ToInt32(GenerationTextBox.Text);
            _tempOutline = _outline.Tail.Data;
            DoEvolve(_tempOutline.CoordinateList.Size - _numberOfEvolutionPoints);
            _tempOutline = _outline.Tail.Data;
            _generationPointer = _outline.Tail;
            PrintOutline();
        }

        private void PEvolutionForwardButtonCommand(object sender, RoutedEventArgs e)
        {
            _generationPointer = _generationPointer.Next;
            _tempOutline = _generationPointer.Data;
            GenerationCounter++;
            if (GenerationCounter > _outline.Size - 1) GenerationCounter = 0;
            PrintOutline();
        }

        private void PEvolutionBackButtonCommand(object sender, RoutedEventArgs e)
        {
            _generationPointer = _generationPointer.Prev;
            _tempOutline = _generationPointer.Data;
            GenerationCounter--;
            if (GenerationCounter < 0) GenerationCounter = _outline.Size - 1;
            PrintOutline();
        }

        private void PEvolutionOriginButtonCommand(object sender, RoutedEventArgs e)
        {
            _generationPointer = _outline.Head;
            _tempOutline = _generationPointer.Data;
            GenerationCounter = 0;
            PrintOutline();
        }

        private void PEvolutionFinaleButtonCommand(object sender, RoutedEventArgs e)
        {
            _generationPointer = _outline.Tail;
            _tempOutline = _generationPointer.Data;
            GenerationCounter = _outline.Size - 1;
            PrintOutline();
        }

        #endregion
    }
}
