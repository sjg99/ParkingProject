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
    /// Lógica de interacción para ModificarEmpleado.xaml
    /// </summary>
    public partial class ModificarEmpleado : Page
    {

        private Conexion con = new Conexion();
        private SqlCommand consulta = new SqlCommand();
        private SqlDataAdapter da;
        private DataTable tabla;
        private DataRowView registro;

        public ModificarEmpleado(DataRowView registro)
        {
            InitializeComponent();
            this.registro = registro;
            LlenarCampos();
        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            if (!CamposVacios())
            {
                try
                {
                    consulta.Connection = con.AbrirConexion();
                    consulta.CommandText = "UPDATE tbusuarios " +
                                           "SET Nombre=@nombre, Apellido=@apellido, Telefono=@telefono, Direccion=@direccion, Email=@email, Password=@password " +
                                           "WHERE Login=@login";
                    consulta.CommandType = CommandType.Text;
                    consulta.Parameters.AddWithValue("@nombre", campoNombre.Text);
                    consulta.Parameters.AddWithValue("@apellido", campoApellido.Text);
                    consulta.Parameters.AddWithValue("@telefono", Convert.ToInt32(campoTelefono.Text));
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

        public void LlenarCampos()
        {
            try
            {
                campoNombre.Text = registro["Nombre"].ToString();
                campoApellido.Text = registro["Apellido"].ToString();
                campoTelefono.Text = registro["Telefono"].ToString();
                campoDireccion.Text = registro["Direccion"].ToString();
                campoEmail.Text = registro["Email"].ToString();
                campoLogin.Text = registro["Login"].ToString();
                campoPassword.Text = registro["Password"].ToString();
            }
            catch
            {
                MessageBox.Show("Seleccione Algo Maricon");
            }
        }
    }
}
