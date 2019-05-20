﻿using System;
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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SqlConnection sqlCon = new SqlConnection(@"Data Source=SJJGPC\SJGSERVER; Initial Catalog=ElectivaIV; Integrated Security=True;");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string consulta = "SELECT COUNT(1) FROM tbusuarios WHERE Login=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(consulta, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtUsuario.Text);
                sqlCmd.Parameters.AddWithValue("@Password", txtContrasena.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if (count == 1)
                {
                    Dashboard menu = new Dashboard();
                    menu.usr = txtUsuario.Text;
                    menu.Show();
                    Close();
                }
                else {
                    MessageBox.Show("Usuario o contraseña incorrecta.");
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
        }
    }
}