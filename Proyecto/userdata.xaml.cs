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
        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost; Initial Catalog=ElectivaIVFinal; Integrated Security=True;");
        
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
            

            edit.Visibility = Visibility.Visible;
            save.Visibility = Visibility.Hidden;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string con = "SELECT * FROM tbusuarios WHERE Login=@MyUser";
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
           

            edit.Visibility = Visibility.Hidden;
            save.Visibility = Visibility.Visible;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string con = "UPDATE tbusuarios SET Nombre=@name, Apellido=@lastname, Email=@email, Telefono=@phone, Direccion=@address WHERE Login=@id";
                SqlCommand sqlCmd = new SqlCommand(con, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@ID", user_id);
                sqlCmd.Parameters.AddWithValue("@name", nombreTxt.Text);
                sqlCmd.Parameters.AddWithValue("@lastname", apellidoTxt.Text);
                sqlCmd.Parameters.AddWithValue("@email", emailTxt.Text);
                sqlCmd.Parameters.AddWithValue("@phone", telefonoTxt.Text);                
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string clave = "";
            try
            {
                
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string con = "SELECT Password FROM tbusuarios WHERE Login=@id";
                SqlCommand sqlCmd = new SqlCommand(con, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@ID", user_id);

                SqlDataReader rd = sqlCmd.ExecuteReader();

                while (rd.Read())
                {
                    clave=rd["Password"].ToString();
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

            if (clavenueva1.Password != clavenueva2.Password)
            {
                clavedesigualn.Visibility = Visibility.Visible;
                exito.Visibility = Visibility.Hidden;
            }
            if (clave != calveantigua.Password)
            {
                clavedesiguala.Visibility = Visibility.Visible;
                exito.Visibility = Visibility.Hidden;
            }

            if (clavenueva1.Password == clavenueva2.Password && clave == calveantigua.Password)
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string conex = "UPDATE tbusuarios SET Password=@clave WHERE Login=@id";
                        SqlCommand sqlCmdn = new SqlCommand(conex, sqlCon);
                        sqlCmdn.CommandType = CommandType.Text;
                        sqlCmdn.Parameters.AddWithValue("@ID", user_id);
                        sqlCmdn.Parameters.AddWithValue("@clave", clavenueva1.Password);

                        SqlDataReader red = sqlCmdn.ExecuteReader();

                        clavedesiguala.Visibility = Visibility.Hidden;
                        clavedesigualn.Visibility = Visibility.Hidden;

                        exito.Visibility = Visibility.Visible;

                        calveantigua.Password = "";
                        clavenueva1.Password = "";
                        clavenueva2.Password = "";
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

        private void Calveantigua_KeyDown(object sender, KeyEventArgs e)
        {
            clavedesiguala.Visibility = Visibility.Hidden;
        }

        private void Clavenueva1_KeyDown(object sender, KeyEventArgs e)
        {
            clavedesigualn.Visibility = Visibility.Hidden;
        }

        private void Clavenueva2_KeyDown(object sender, KeyEventArgs e)
        {
            clavedesigualn.Visibility = Visibility.Hidden;
        }
    }
}
