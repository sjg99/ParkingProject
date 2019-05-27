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
using Microsoft.Win32;
using System.Net.Http;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para Ingreso.xaml
    /// </summary>
    public partial class Ingreso : Page
    {
        public bool existenDispositivos = false;
        public bool fotoHecha = false;
        private FilterInfoCollection dispositivosVideo;
        private VideoCaptureDevice fuenteVideo = null;
        SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost; Initial Catalog=ElectivaIVFinal; Integrated Security=True;");
        public Ingreso()
        {
            InitializeComponent();
            BuscarDispositivos();
        }
        private void BuscarDispositivos()
        {
            dispositivosVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (dispositivosVideo.Count == 0)
            {
                existenDispositivos = false;
            }
            else
            {
                existenDispositivos = true;
            }
        }

        private void MostrarImagen(object sender, NewFrameEventArgs eventArgs)
        {
            System.Drawing.Image img = (Bitmap)eventArgs.Frame.Clone();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();

            bi.Freeze();

            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                image.Source = bi;
            }));
        }
        string placa = null;

        string path = Directory.GetCurrentDirectory();
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> ProcessImage(string image_path)
        {
            string SECRET_KEY = "sk_bec6d270263f6ce8936190f2";

            byte[] bytes = File.ReadAllBytes(image_path);
            string imagebase64 = Convert.ToBase64String(bytes);

            var content = new StringContent(imagebase64);

            var response = await client.PostAsync("https://api.openalpr.com/v2/recognize_bytes?recognize_vehicle=1&country=us&secret_key=" + SECRET_KEY, content).ConfigureAwait(false);

            var buffer = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var byteArray = buffer.ToArray();
            var responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);

            return responseString;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            string filepath = path.Replace(@"\bin\Debug", @"\Matriculas\Placa.png");
            image1.Source = image.Source;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image1.Source));
            using (FileStream stream = new FileStream(filepath, FileMode.Create))
                encoder.Save(stream);            
            Task<string> recognizeTask = Task.Run(() => ProcessImage(filepath));
            recognizeTask.Wait();
            string task_result = recognizeTask.Result;

            task_result = task_result.Substring(task_result.IndexOf('['));
            task_result = task_result.Substring(12, 6);
            placa = task_result;
            RegistrarIS();
            
        }
       
        private int CantidadIngresos()
        {
            int cingreso = 0;
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT * FROM tbingreso Where Placa=@Placa";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Placa", placa);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        cingreso++;
                    }
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
            return cingreso;
        }
        private int CantidadSalidas()
        {
            int csalida = 0;
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT * FROM tbsalida Where Placa=@Placa";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Placa", placa);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        csalida++;
                    }
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
            return csalida;
        }
        private DateTime ConsultarIngreso()
        {
            DateTime fingreso = DateTime.MinValue;
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT top 1 * FROM tbingreso Where Placa=@Placa order by Fecha DESC";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Placa", placa);
                SqlDataReader reader = sqlCmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        fingreso = reader.GetDateTime(reader.GetOrdinal("Fecha"));
                    }
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
            return fingreso;
        }
        private void RegistrarIS()
        {
            var Match = Regex.IsMatch(placa, @"([A-Z]{3})+([0-9]{3})");            
            if (placa != null && Match)
            {
                DateTime Fecha = DateTime.Now;
                if (CantidadIngresos() == CantidadSalidas())
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "INSERT INTO tbingreso VALUES (@Placa,@Fecha)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.AddWithValue("@Placa", placa);
                        sqlCmd.Parameters.AddWithValue("@Fecha", Fecha);
                        if (sqlCmd.ExecuteNonQuery() == 1)
                        {
                            //MessageBox.Show("El vehiculo con placa " + placa + " ha ingresado a las " + Fecha);
                            PlacaES.Content = "Matricula:";
                            Placa.Content = placa;
                            HoraES.Content = "Fecha Ingreso:";
                            Hora.Content = Fecha;

                            PagoES.Visibility = Visibility.Visible;
                            Pago.Visibility = Visibility.Visible;
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
                else if (CantidadIngresos() - CantidadSalidas() == 1)
                {
                    DateTime Fechas = DateTime.Now;
                    DateTime Fechai = ConsultarIngreso();
                    var diferencia = Fechas - Fechai;
                    int minutos = (int)diferencia.TotalMinutes;
                    int precio = minutos * 60;
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();
                        string query = "INSERT INTO tbsalida VALUES (@Placa,@Fecha,@Precio)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCmd.Parameters.AddWithValue("@Placa", placa);
                        sqlCmd.Parameters.AddWithValue("@Fecha", Fechas);
                        sqlCmd.Parameters.AddWithValue("@Precio", precio.ToString());
                        if (sqlCmd.ExecuteNonQuery() == 1)
                        {
                            //MessageBox.Show("El vehiculo con placa " + placa + " ha salido a las " + Fechas + " y debe pagar $" + precio);
                            PlacaES.Content = "Matricula:";
                            Placa.Content = placa;
                            HoraES.Content = "Fecha Salida:";
                            Hora.Content = Fecha;
                            PagoES.Content = "Valor:";
                            Pago.Content = "$" + precio;

                            PagoES.Visibility = Visibility.Visible;
                            Pago.Visibility = Visibility.Visible;
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
                else 
                {
                    MessageBox.Show("Error para registrar ingreso o salida");
                }
            }
            else
            {
                MessageBox.Show("No se ha reconocido una placa valida");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (existenDispositivos)
            {
                fuenteVideo = new VideoCaptureDevice(dispositivosVideo[0].MonikerString);
                fuenteVideo.NewFrame += new NewFrameEventHandler(MostrarImagen);
                fuenteVideo.Start();
            }
            else
            {
                MessageBox.Show("No hay dispositivo de video", "Informacion");
                
            }
        }
    }
}
