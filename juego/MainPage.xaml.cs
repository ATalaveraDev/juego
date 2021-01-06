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
        List<Ellipse> objetos = new List<Ellipse>();
        public MainPage()
        {
            this.InitializeComponent();
            myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromSeconds(2);
            myTimer.Tick += dispatcherTimer_Tick;
            myTimer.Start();
            startTime = DateTime.UtcNow;
        }

        private void dispatcherTimer_Tick(object sender, object e) {
            
            // si han pasado x segundos llamar al metodo que pinta el circulo con el codigo de abajo
            myTimer.Interval = TimeSpan.FromSeconds(rnd.Next(1,5));
            
            // si el evento ha durado mas de 1 min llamar a myTimer.Stop() ESTO NO FUNCIONA
            if (DateTime.UtcNow.Subtract(startTime).TotalSeconds == 60)
                myTimer.Stop();

            // generar de formar random entre colores rojo y azul
            if (rnd.Next()%2==0)
                newEllipse = pintarCirculo(Windows.UI.Colors.Red, 100,100);
            else
                newEllipse = pintarCirculo(Windows.UI.Colors.BlueViolet, 100, 100);

            this.canvas.Children.Add(newEllipse);
            
            //esto hace que la posición sea aleatoria.
            Canvas.SetTop(newEllipse, rnd.Next(10, 800));
            Canvas.SetLeft(newEllipse, rnd.Next(10, 800));

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
            this.canvas.Children.Remove((Ellipse)sender);
           
        }
    }
}
