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

namespace MenuDesplegable
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void Button1_Clicked(object sender, RoutedEventArgs e)
        {
            if (Despliegue1.IsVisible)
            {
                Despliegue1.Visibility = Visibility.Hidden;
                Despliegue2.Visibility = Visibility.Hidden;
                Despliegue3.Visibility = Visibility.Hidden;

                Bttn1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));

                Bttn1.Margin = new Thickness(0, 140, 0, 418);
                Bttn2.Margin= new Thickness(0, 197, 0, 359);
                Bttn3.Margin = new Thickness(0, 256, 0, 298);
                //0,140,0,418
                //0,197,0,359
                //0,256,0,298
            }
            else
            {
                Despliegue1.Visibility = Visibility.Visible;
                Despliegue2.Visibility = Visibility.Hidden;
                Despliegue3.Visibility = Visibility.Hidden;

                Bttn1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF4987F3"));
                Bttn2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));

                Bttn1.Margin = new Thickness(0, 140, 0, 418);
                Bttn2.Margin = new Thickness(0, 337, 0, 219);
                Bttn3.Margin = new Thickness(0, 393, 0, 161);
                //0,140,0,418
                //0,337,0,219
                //0,393,0,161
            }
        }

        private void Button2_Clicked(object sender, RoutedEventArgs e)
        {
            if (Despliegue2.IsVisible)
            {
                Despliegue1.Visibility = Visibility.Hidden;
                Despliegue2.Visibility = Visibility.Hidden;
                Despliegue3.Visibility = Visibility.Hidden;

                Bttn1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));

                Bttn1.Margin = new Thickness(0,140,0,418);
                Bttn2.Margin = new Thickness(0,197,0,359);
                Bttn3.Margin = new Thickness(0,256,0,298);
                //0,140,0,418
                //0,197,0,359
                //0,256,0,298
            }
            else
            {
                Despliegue1.Visibility = Visibility.Hidden;
                Despliegue2.Visibility = Visibility.Visible;
                Despliegue3.Visibility = Visibility.Hidden;

                Bttn1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF4987F3"));
                Bttn3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));

                Bttn1.Margin = new Thickness(0, 140, 0, 418);
                Bttn2.Margin = new Thickness(0, 197, 0, 359);
                Bttn3.Margin = new Thickness(0, 393, 0, 161);
                //0,140,0,418
                //0,337,0,219
                //0,393,0,161
            }
        }
        private void Button3_Clicked(object sender, RoutedEventArgs e)
        {
            if (Despliegue3.IsVisible)
            {
                Despliegue1.Visibility = Visibility.Hidden;
                Despliegue2.Visibility = Visibility.Hidden;
                Despliegue3.Visibility = Visibility.Hidden;

                Bttn1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));

                Bttn1.Margin = new Thickness(0, 140, 0, 418);
                Bttn2.Margin = new Thickness(0, 197, 0, 359);
                Bttn3.Margin = new Thickness(0, 256, 0, 298);
                //0,140,0,418
                //0,197,0,359
                //0,256,0,298
            }
            else
            {
                Despliegue1.Visibility = Visibility.Hidden;
                Despliegue2.Visibility = Visibility.Hidden;
                Despliegue3.Visibility = Visibility.Visible;

                Bttn1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D80D1"));
                Bttn3.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF4987F3"));

                Bttn1.Margin = new Thickness(0, 140, 0, 418);
                Bttn2.Margin = new Thickness(0, 197, 0, 359);
                Bttn3.Margin = new Thickness(0, 256, 0, 298);
                //0,140,0,418
                //0,337,0,219
                //0,393,0,161
            }
        }
    }
}
