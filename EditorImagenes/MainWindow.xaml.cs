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
using Path = System.IO.Path;
using Application = System.Windows.Forms.Application;

namespace EditorImagenes
{

    public partial class MainWindow : Window
    {
        private double _scale=1.0;

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

                case "btnGuia":
                    string pdfPath = Path.Combine(Application.StartupPath, "GuiaUsuario.pdf");
                    Process.Start(pdfPath);
                    break;
                case "bSalir":
                    this.Close();
                    break;
                case "bZoomMas":
                    _scale += 0.1;
                    ScaleTransform scaleTransform = new ScaleTransform(_scale, _scale);
                    imagen.RenderTransform = scaleTransform;
                    break;
                case "bZoomMenos":
                    _scale -= 0.1;
                    ScaleTransform scaleTransform2 = new ScaleTransform(_scale, _scale);
                    imagen.RenderTransform = scaleTransform2;
                    break;
            }


        }

    }
}
