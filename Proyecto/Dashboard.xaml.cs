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
using System.Windows.Shapes;

namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public string usr;
        public string id;
        public bool update=false;
        public Dashboard()
        {
            InitializeComponent();
        }

        private void PopUpSalir_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void AbrirMenu_Click(object sender, RoutedEventArgs e)
        {
            AbrirMenu.Visibility = Visibility.Collapsed;
            CerrarMenu.Visibility = Visibility.Visible;
        }

        private void CerrarMenu_Click(object sender, RoutedEventArgs e)
        {
            AbrirMenu.Visibility = Visibility.Visible;
            CerrarMenu.Visibility = Visibility.Collapsed;
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame.Content = new Ingreso();            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textusr.Text = usr;
            if (update)
            {
                update = false;
                Account_Click(sender,e);
            }
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            frame.Content = new Salida();
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            userdata data = new userdata();
            data.user_id = id;
            frame.Content = data;
        }

        private void StackPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            frame.Content = new Empleados();
        }
    }
}
