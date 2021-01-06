using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace juego
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Ellipse newEllipse;
        Random rnd = new Random();
        DispatcherTimer myTimer;
        DateTime startTime;
        int contR = 0, contA = 0;
        public MainPage()
        {
            this.InitializeComponent();
            myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromSeconds(2);
            myTimer.Tick += dispatcherTimer_Tick;
            myTimer.Start();
            startTime = DateTime.Now;
            this.Azul.Text = "0";
            this.Rojo.Text = "0";
        }

        private void dispatcherTimer_Tick(object sender, object e) {
            
            // si han pasado x segundos llamar al metodo que pinta el circulo con el codigo de abajo
            myTimer.Interval = TimeSpan.FromSeconds(rnd.Next(1,3));

            // si el evento ha durado mas de 1 min llamar a myTimer.Stop() ESTO NO FUNCIONA
            int elapsed = (DateTime.Now - startTime).Seconds;
            if (elapsed == 59)
                myTimer.Stop();

            // generar de formar random entre colores rojo y azul
            if (rnd.Next() % 2 == 0)
            {
                agregarRojo(1);
            }
            else
            {
                agregarAzul(1);
            }
        
        }

        private void agregarRojo(int nivel)
        {
            newEllipse = pintarCirculo(Windows.UI.Colors.Red, 100/nivel, 100/nivel);
            newEllipse.Tag = "R";
            this.canvas.Children.Add(newEllipse);
            Canvas.SetTop(newEllipse, rnd.Next(10, 500));
            Canvas.SetLeft(newEllipse, rnd.Next(500, 900));

        }

        private void agregarAzul(int nivel)
        {
            newEllipse = pintarCirculo(Windows.UI.Colors.BlueViolet, 100 / nivel, 100 / nivel);
            newEllipse.Tag = "A";
            this.canvas.Children.Add(newEllipse);
            Canvas.SetTop(newEllipse, rnd.Next(10, 500));
            Canvas.SetLeft(newEllipse, rnd.Next(10, 450));
        }


        private void canvas_LayoutUpdated(object sender, object e)
        {
            
        }

        private Ellipse pintarCirculo(Windows.UI.Color color, int hei, int wid)
        {
            SolidColorBrush brush = new SolidColorBrush(color);
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = brush,
                Fill = brush,
                StrokeThickness = 5,
                Height = hei,
                Width = wid
            };
            newEllipse.Tapped += circle_Tapped;
            return newEllipse;
        }

        private void circle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Ellipse newEllipse = (Ellipse)sender;
            this.canvas.Children.Remove(newEllipse);
            if (newEllipse.Tag.Equals("R")){
                this.Rojo.Text = ++contR + "";
            }
            else
            {
                this.Azul.Text = ++contA + "";

            }
        }
    }
    
}
