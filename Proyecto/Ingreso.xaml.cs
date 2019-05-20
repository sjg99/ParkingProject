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

namespace Proyecto
{
    /// <summary>
    /// Lógica de interacción para Ingreso.xaml
    /// </summary>
    public partial class Ingreso : Page
    {
        public Ingreso()
        {
            InitializeComponent();
        }


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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Jpeg files (*.jpeg)|*.jpeg|Jpg files (*.jpg)|*.jpg|Png files (*.png)|*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                String path = openFileDialog.FileName;

                image.Source = new BitmapImage(new Uri(path));
                Task<string> recognizeTask = Task.Run(() => ProcessImage(path));
                recognizeTask.Wait();
                string task_result = recognizeTask.Result;

                task_result = task_result.Substring(task_result.IndexOf('['));
                task_result = task_result.Substring(12, 6);

                label.Content = task_result;
            }
        }
    }
}
