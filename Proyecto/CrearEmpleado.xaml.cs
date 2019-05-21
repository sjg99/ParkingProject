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
    /// Lógica de interacción para CrearEmpleado.xaml
    /// </summary>
    public partial class CrearEmpleado : Page
    {

        private Conexion con = new Conexion();
        private SqlCommand consulta = new SqlCommand();
        private SqlDataAdapter da;
        private DataTable tabla;

        public CrearEmpleado()
        {
            InitializeComponent();
        }

        private void Crear_Click(object sender, RoutedEventArgs e)
        {
            if (!CamposVacios())
            {
                try
                {
                    consulta.Connection = con.AbrirConexion();
                    consulta.CommandText = "INSERT INTO tbusuarios (Nombre, Apellido, Telefono, Direccion, Email, Login, Password) VALUES(@nombre, @apellido, @telefono, @direccion, @email, @login, @password)";
                    consulta.CommandType = CommandType.Text;
                    consulta.Parameters.AddWithValue("@nombre", campoNombre.Text);
                    consulta.Parameters.AddWithValue("@apellido", campoApellido.Text);
                    consulta.Parameters.AddWithValue("@telefono", campoTelefono.Text);
                    consulta.Parameters.AddWithValue("@direccion", campoDireccion.Text);
                    consulta.Parameters.AddWithValue("@email", campoEmail.Text);
                    consulta.Parameters.AddWithValue("@login", campoLogin.Text);
                    consulta.Parameters.AddWithValue("@password", campoPassword.Text);
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
            else
                MessageBox.Show("Complete todos los campos!");
        }

        public Boolean CamposVacios()
        {
            return (campoNombre.Text.Equals("") || campoApellido.Text.Equals("") || campoTelefono.Text.Equals("") || campoDireccion.Text.Equals("") ||
                    campoEmail.Text.Equals("") || campoLogin.Text.Equals("") || campoPassword.Text.Equals(""));
        }
    }
}

