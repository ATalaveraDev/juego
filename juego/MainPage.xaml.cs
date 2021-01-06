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
        int contR = 0, contA = 0, totR=0, totA=0;
        public MainPage()
        {
            this.InitializeComponent();
            myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromSeconds(1);
            myTimer.Tick += dispatcherTimer_Tick;
            myTimer.Start();
            startTime = DateTime.Now;
            this.Azul.Text = "0 / 0";
            this.Rojo.Text = "0 / 0";
        }

        private void dispatcherTimer_Tick(object sender, object e) {
            
            // si han pasado x segundos llamar al metodo que pinta el circulo con el codigo de abajo
            myTimer.Interval = TimeSpan.FromSeconds(rnd.Next(3,6));

            // si el evento ha durado mas de 1 min llamar a myTimer.Stop() 
            int elapsed = (DateTime.Now - startTime).Seconds;  //NO FUNCIONA BIEN
            if (elapsed == 59) {
                myTimer.Stop();
                this.canvas.Children.Clear();
                
                int azul = totA - contA;
                int rojo = totR - contR;

                if(azul < rojo) //gana el que tiene menos diferencia
                {
                    this.Rojo.IsEnabled = false;
                }
                else
                {
                    this.Azul.IsEnabled = false;
                }
                return;
            }
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
            if (nivel == 2)
            {
                newEllipse.Tag = "RR";
            } else
            {
                newEllipse.Tag = "R";
            }
            
            this.canvas.Children.Add(newEllipse);
            Canvas.SetTop(newEllipse, rnd.Next(10, 450));
            Canvas.SetLeft(newEllipse, rnd.Next(300, 1200));
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

    }

}
