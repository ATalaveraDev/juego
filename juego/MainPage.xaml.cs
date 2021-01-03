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
        }

        private void dispatcherTimer_Tick(object sender, object e) {
            SolidColorBrush brush = new SolidColorBrush(Windows.UI.Colors.Red);
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = brush,
                Fill = brush,
                StrokeThickness = 5,
                Height = 10,
                Width = 10
            };
            this.canvas.Children.Add(newEllipse);
        }
        private void canvas_LayoutUpdated(object sender, object e)
        {
            
        }
    }
}
