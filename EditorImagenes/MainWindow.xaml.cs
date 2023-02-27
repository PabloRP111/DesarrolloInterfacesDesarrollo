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
        private bool _isMoving;
        private Point? position;
        private double deltaX;
        private double deltaY;
        private TranslateTransform _currentTT;

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

        //Si se pulsa el ratón
        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Se coge la posición del ratón para mover la imagen con él y se setea "isMoving" a true para que MouseMove haga cosas
            if (position == null)
                position = imagen.TransformToAncestor(scroll).Transform(new Point(0, 0));
            var mousePosition = Mouse.GetPosition(scroll);
            deltaX = mousePosition.X - position.Value.X;
            deltaY = mousePosition.Y - position.Value.Y;
            _isMoving = true;
        }

        //Si se deja de pulsar el ratón
        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Se guarda la posición y se setea el "isMoving" a false para que no la líe si se mueve el ratón
            _currentTT = imagen.RenderTransform as TranslateTransform;
            _isMoving = false;
        }

        //Al mover el ratón
        private void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Si "isMoving" está en false no hace nada
            if (!_isMoving) return;
            //En caso contrario, cambia la posición de la imagen
            var mousePoint = Mouse.GetPosition(scroll);

            var offsetX = (_currentTT == null ? position.Value.X : position.Value.X - _currentTT.X) + deltaX - mousePoint.X;
            var offsetY = (_currentTT == null ? position.Value.Y : position.Value.Y - _currentTT.Y) + deltaY - mousePoint.Y;

            this.imagen.RenderTransform = new TranslateTransform(-offsetX, -offsetY);
        }

    }
}
