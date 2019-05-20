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
using System.Data.SqlClient;
using System.Data;

namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para userdata.xaml
    /// </summary>
    public partial class userdata : Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost; Initial Catalog=ElectivaIV; Integrated Security=True;");
        
        public string user_id;

        public userdata()
        {
            InitializeComponent();
            setFalse();
        }

        private void setFalse()
        {

            nombreTxt.IsEnabled = false;
            apellidoTxt.IsEnabled = false;
            emailTxt.IsEnabled = false;
            telefonoTxt.IsEnabled = false;
            direccionTxt.IsEnabled = false;
            date.IsEnabled = false;

            edit.Visibility = Visibility.Visible;
            save.Visibility = Visibility.Hidden;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string con = "SELECT * FROM tbusuarios WHERE ID=@MyUser";
                SqlCommand sqlCmd = new SqlCommand(con, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@MyUser", user_id);
                SqlDataReader rd = sqlCmd.ExecuteReader();
                while (rd.Read())
                {
                    nombreTxt.Text = rd["Nombre"].ToString();
                    apellidoTxt.Text = rd["Apellido"].ToString();
                    emailTxt.Text = rd["Email"].ToString();
                    telefonoTxt.Text = rd["Telefono"].ToString();
                    direccionTxt.Text = rd["Direccion"].ToString();
                    date.Text = rd["FechaNacido"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            //index.id_user;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            nombreTxt.IsEnabled = true;
            apellidoTxt.IsEnabled = true;
            emailTxt.IsEnabled = true;
            telefonoTxt.IsEnabled = true;
            direccionTxt.IsEnabled = true;
            date.IsEnabled = true;

            edit.Visibility = Visibility.Hidden;
            save.Visibility = Visibility.Visible;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string con = "UPDATE tbusuarios SET Nombre=@name, Apellido=@lastname, Email=@email, Telefono=@phone, FechaNacido=@date, Direccion=@address WHERE ID=@id";
                SqlCommand sqlCmd = new SqlCommand(con, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@ID", user_id);
                sqlCmd.Parameters.AddWithValue("@name", nombreTxt.Text);
                sqlCmd.Parameters.AddWithValue("@lastname", apellidoTxt.Text);
                sqlCmd.Parameters.AddWithValue("@email", emailTxt.Text);
                sqlCmd.Parameters.AddWithValue("@phone", telefonoTxt.Text);
                sqlCmd.Parameters.AddWithValue("@date", date.Text);
                sqlCmd.Parameters.AddWithValue("@address", direccionTxt.Text);

                Dashboard main = new Dashboard();
                main.usr = nombreTxt.Text + " " + apellidoTxt.Text;
                main.update = true;
                main.id = user_id;
                main.Show();

                SqlDataReader rd = sqlCmd.ExecuteReader();

                setFalse();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
