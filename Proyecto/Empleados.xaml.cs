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
using System.Data;
using System.Data.SqlClient;


namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : Page
    {

        private Conexion con = new Conexion();
        private SqlCommand consulta = new SqlCommand();
        private SqlDataAdapter da;
        private DataTable tabla;

        public Empleados()
        {
            InitializeComponent();
            ListarEmpleados();
        }

        public void ListarEmpleados()
        {
            try
            {
                consulta.Connection = con.AbrirConexion();
                consulta.CommandText = "SELECT * FROM tbusuarios";
                consulta.CommandType = CommandType.Text;
                da = new SqlDataAdapter(consulta);
                tabla = new DataTable();
                da.Fill(tabla);
                TablaEmpleados.ItemsSource = tabla.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("CrearEmpleado.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ModificarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new Uri("Paginas/ModificarEmpleado.xaml", UriKind.RelativeOrAbsolute));
            DataRowView registro = this.TablaEmpleados.SelectedItem as DataRowView;
            NavigationService.Navigate(new ModificarEmpleado(registro));
        }

        private void EliminarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            DataRowView registro = this.TablaEmpleados.SelectedItem as DataRowView;
            try
            {
                consulta.Connection = con.AbrirConexion();
                consulta.CommandText = "DELETE FROM tbusuarios   WHERE Login=@id";
                consulta.CommandType = CommandType.Text;
                consulta.Parameters.AddWithValue("@id", registro["Login"].ToString());
                da = new SqlDataAdapter(consulta);
                tabla = new DataTable();
                da.Fill(tabla);                
                NavigationService.Navigate(new Uri("Empleados.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }
        }
    }
}
