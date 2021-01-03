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
        DispatcherTimer myTimer;
        DateTime startTime;
        public MainPage()
        {
            this.InitializeComponent();
            myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromSeconds(1);
            myTimer.Tick += dispatcherTimer_Tick;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            myTimer.Start();
            startTime = DateTime.UtcNow;
        }

        private void dispatcherTimer_Tick(object sender, object e) {
            // si el evento ha durado mas de 1 min llamar a myTimer.Stop()
            // si han pasado x segundos llamar al metodo que pinta el circulo con el codigo de abajo
            // generar de formar random entre colores rojo y azul

            // esto pinta el circulo
            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = brush,
                Fill = brush,
                StrokeThickness = 5,
                Height = 10,
                Width = 10
            };
            /* Definir metodo para manejar el evento de single tap y añadir despues del =
            newEllipse.Tapped = */
            this.canvas.Children.Add(newEllipse);
        }
        private void canvas_LayoutUpdated(object sender, object e)
        {
            
        }
    }
}
