using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace juego
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Ellipse newEllipse;
        DispatcherTimer myTimer;
        DateTime startTime;
        int contR = 0, contA = 0, totR=0, totA=0;
        Random rnd = new Random();
        public MainPage()
        {
            this.InitializeComponent();
            myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromSeconds(1);
            myTimer.Tick += dispatcherTimer_Tick;
            
            this.Azul.Text = "0 / 0";
            this.Rojo.Text = "0 / 0";
        }

        private void dispatcherTimer_Tick(object sender, object e) {
            /* Generador de dificultad. La dificultad se establece segun una probabilidad
             que se define segun la relacion entre velocidad del usuario versus velocidad
             del tick. Por tanto a mayor aumento del indice de probabilidad de crear una bola
            mayor dificultad */
            Random gen = new Random();
            int probability = gen.Next(100);

            if (probability < 50) {
                if (rnd.Next() % 2 == 0)
                {
                    agregarRojo(1);
                }
                else
                {
                    agregarAzul(1);
                }
            }

            int elapsed = (DateTime.Now - startTime).Seconds;  
            if (elapsed == 59)
            {
                myTimer.Stop();
                this.canvas.Children.Clear();

                int azul = totA - contA;
                int rojo = totR - contR;

                if (totA < totR) //gana el que tiene menos diferencia
                {
                    this.Rojo.Background = (SolidColorBrush)Resources["Green"];
                    this.Rojo.Text = "WIN";
                }
                else
                {
                    this.Azul.Background = (SolidColorBrush)Resources["Green"];
                    this.Azul.Text = "WIN";
                }

                this.starter.Visibility = Visibility.Visible;

                return;
            }
        }

        private void agregarRojo(int nivel)
        {
            newEllipse = pintarCirculo(Windows.UI.Colors.Red, 100/nivel, 100/nivel);
            if (nivel == 2)
            {
                newEllipse.Tag = "RR";
            } else
            {
                newEllipse.Tag = "R";
            }
            
            this.canvas.Children.Add(newEllipse);
            Canvas.SetTop(newEllipse, rnd.Next(110, 550));
            Canvas.SetLeft(newEllipse, rnd.Next(400, 1300));
            this.Rojo.Text = contR + " / " + ++totR;
        }

        
        private void agregarAzul(int nivel)
        {
            newEllipse = pintarCirculo(Windows.UI.Colors.Blue, 100 / nivel, 100 / nivel);
            if (nivel == 2)
            {
                newEllipse.Tag = "AA";
            }
            else
            {
                newEllipse.Tag = "A";
            }
            this.canvas.Children.Add(newEllipse);
            Canvas.SetTop(newEllipse, rnd.Next(10, 450));
            Canvas.SetLeft(newEllipse, rnd.Next(10, 900));
            this.Azul.Text = contA + " / " + ++totA;
        }

        private Ellipse pintarCirculo(Windows.UI.Color color, int hei, int wid)
        {
            SolidColorBrush brush2 = new SolidColorBrush(Windows.UI.Colors.Black);
            SolidColorBrush brush = new SolidColorBrush(color);
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = brush2,
                Fill = brush,
                StrokeThickness = 5,
                Height = hei,
                Width = wid
            };
            newEllipse.Tapped += circle_Tapped;
            newEllipse.Holding += circle_Holding;

            return newEllipse;
        }

        private void circle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Ellipse tempE = (Ellipse)sender;
            this.canvas.Children.Remove(tempE);
            String temp = (String)tempE.Tag;

            switch (temp)
            {
                case "A":
                    agregarAzul(2);
                    agregarAzul(2);
                    break;
                case "AA":
                    this.Azul.Text = ++contA + " / " + totA;
                    break;
                case "R":
                    agregarRojo(2);
                    agregarRojo(2);
                    break;
                case "RR":
                    this.Rojo.Text = ++contR + " / " + totR;
                    break;
            }

        }
        private void circle_Holding(object sender, HoldingRoutedEventArgs e)
        {

            Ellipse tempE = (Ellipse)sender;
            this.canvas.Children.Remove(tempE);
            
            String temp = (String)tempE.Tag;

            switch (temp)
            {
                case "A":
                    agregarRojo(1);
                    agregarRojo(1);
                    this.Azul.Text = contA + " / " + --totA;
                    break;
                case "R":
                    agregarAzul(1);
                    agregarAzul(1);
                    this.Rojo.Text = contR + " / " + --totR;
                    break;
                case "AA":
                    agregarRojo(2);
                    agregarRojo(2);
                    this.Azul.Text = contA + " / " + --totA;
                    break;
                case "RR":
                    agregarAzul(2);
                    agregarAzul(2);
                    this.Rojo.Text = contR + " / " + --totR;
                    break;
            }
        }

        private void starter_Click(object sender, RoutedEventArgs e)
        {
            this.Azul.Text = "0 / 0";
            this.Azul.Background = (SolidColorBrush)Resources["AzulBackground"];
            this.Rojo.Text = "0 / 0";
            this.Rojo.Background = (SolidColorBrush)Resources["RojoBackground"];
            myTimer.Start();
            startTime = DateTime.Now;
            this.starter.Visibility = Visibility.Collapsed;
        }
    }

}
