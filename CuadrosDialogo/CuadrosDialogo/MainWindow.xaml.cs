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
using System.IO;

namespace CuadrosDialogo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            lblPregunta.Content = question;
            txtRespuesta.Text = defaultAnswer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //en su forma más simple
            MessageBox.Show("Electiva Profesional IV");
            //MessageBox con título
            MessageBox.Show("Electiva Profesional IV", "Sistema");
            //MessageBox con botones
            MessageBox.Show("Quiere mostrar.\n\nBotones adicionales?", "Sistema",
           MessageBoxButton.YesNoCancel);
            //MessageBox con ícono
            MessageBox.Show("Electiva Profesional IV", "Sistema", MessageBoxButton.OK,
           MessageBoxImage.Information);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //Los filtros para los tipos de archivos
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //El directorio inicial
            openFileDialog.InitialDirectory = @"c:\";
            if (openFileDialog.ShowDialog() == true)
                txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //Los filtros para los tipos de archivos
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //El directorio inicial
            saveFileDialog.InitialDirectory = @"c:\";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, txtEditor.Text);
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void Window_ContentRendered(object sender, RoutedEventArgs e)
        {
            txtRespuesta.SelectAll();
            txtRespuesta.Focus();
        }
        public string Answer
        {
            get { return txtRespuesta.Text; }
        }
    }
}
