using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Forms;
using System.Diagnostics;

namespace EditorImagenes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button clickedButton = (System.Windows.Controls.Button)sender;
            String imgRuta = "";

            switch (clickedButton.Name)
            {
                case "bIzquierda":
                    RotationTransform.Angle -= 90;
                    break;
 
                case "bDerecha":
                    RotationTransform.Angle += 90;
                    break;

                case "btnLupa":

                    using (var fd = new FolderBrowserDialog())
                    {
                        if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fd.SelectedPath))
                        {
                            imgRuta = fd.SelectedPath;
                        }
                    }
                    break;
            }


            

        }

        private void boton_guia_Click(object sender, RoutedEventArgs e)
        {

            Process.Start("C:\\Users\\USUARIO\\Documents\\interfaces\\DesarrolloInterfacesDesarrollo\\EditorImagenes\\GuiaUsuario.pdf");

        }
    }
}
