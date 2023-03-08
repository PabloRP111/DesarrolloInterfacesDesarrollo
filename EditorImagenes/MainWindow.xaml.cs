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
using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EditorImagenes
{

    public partial class MainWindow : System.Windows.Window
    {
        private double _scale = 1.0;
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
                    UpdateTransform();
                    break;

                case "bDerecha":
                    RotationTransform.Angle += 90;
                    UpdateTransform();
                    break;

                case "btnLupa":
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    dlg.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";
                    if (dlg.ShowDialog() == true)
                    {
                        // Acciones a realizar con la ruta del archivo seleccionado
                        string rutaArchivo = dlg.FileName;

                        // Carga la imagen seleccionada en el control de imagen
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.UriSource = new Uri(rutaArchivo);
                        image.EndInit();

                        _scale = 1.0;
                        RotationTransform.Angle = 0;

                        imagen.Source = image;
                        UpdateTransform();
                    }
                    break;

                case "btnGuia":
                    string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GuiaUsuario.pdf");
                    // Se inicia el proceso de la aplicación predeterminada para abrir el archivo PDF
                    try
                    {
                        Process.Start(pdfPath);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Error al abrir el archivo PDF: " + ex.Message);
                    }
                    break;

                case "bSalir":
                    this.Close();
                    break;

                case "bZoomMas":
                    if (_scale < 4.0)
                    {
                        _scale += 0.1;
                        UpdateTransform();
                    }
                    break;

                case "bZoomMenos":
                    if (_scale > 0.5)
                    {
                        _scale -= 0.1;
                        UpdateTransform();
                    }
                    break;
            }


        }

        private void UpdateTransform()
        {
            imagen.RenderTransform = new TransformGroup
            {
                Children =
                {
                    new ScaleTransform(_scale, _scale),
                    RotationTransform
                }
            };
        }

        private Point? lastPosition = null;
        private bool isDragging = false;

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                lastPosition = e.GetPosition(imagen);
                isDragging = true;
                Mouse.Capture(imagen);
            }
        }

        private void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newPosition = e.GetPosition(imagen);
                double deltaX = newPosition.X - lastPosition.Value.X;
                double deltaY = newPosition.Y - lastPosition.Value.Y;

                var transform = imagen.RenderTransform as TransformGroup;
                var translate = transform.Children.OfType<TranslateTransform>().FirstOrDefault();
                if (translate == null)
                {
                    translate = new TranslateTransform();
                    transform.Children.Insert(0, translate);
                }

                translate.X += deltaX;
                translate.Y += deltaY;

                lastPosition = newPosition;
            }
        }

        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = false;
                Mouse.Capture(null);
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            var rootDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")));
            var miCarpetaPath = Path.Combine(rootDirectory.FullName);

            var root = new Folder { Name = "MiCarpeta" };
            root.Children = new ObservableCollection<FileSystemItem>();

            foreach (var directory in Directory.GetDirectories(miCarpetaPath))
            {
                root.Children.Add(CreateFolder(directory));
            }

            foreach (var file in Directory.GetFiles(miCarpetaPath))
            {
                root.Children.Add(CreateFile(file));
            }

            treeView_Copy1.ItemsSource = new List<Folder> { root };
            treeView_Copy1.SelectedItemChanged+= new RoutedPropertyChangedEventHandler<object>(seleccionImagen);
        }

        public void seleccionImagen(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FileSystemItem selectedItem = treeView_Copy1.SelectedItem as FileSystemItem;
            if (selectedItem != null)
            {
                string selectedHeader = selectedItem.Name;
                var rootDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")));
                var miCarpetaPath = Path.Combine(rootDirectory.FullName, selectedHeader);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(miCarpetaPath);
                image.EndInit();

                imagen.Source = image;
            }
        }




        public class Root
        {
            public string Name { get; set; }
            public ObservableCollection<string> Children { get; set; }
        }

        private Folder CreateFolder(string path)
        {
            var folder = new Folder { Name = Path.GetFileName(path) };
            folder.Children = new ObservableCollection<FileSystemItem>();

            foreach (var directory in Directory.GetDirectories(path))
            {
                folder.Children.Add(CreateFolder(directory));
            }

            foreach (var file in Directory.GetFiles(path))
            {
                folder.Children.Add(CreateFile(file));
            }

            return folder;
        }

        private File CreateFile(string path)
        {
            return new File { Name = Path.GetFileName(path), Extension = Path.GetExtension(path) };
        }
        public class FileSystemItem
        {
            public string Name { get; set; }
            public ObservableCollection<FileSystemItem> Children { get; set; }
        }

        public class Folder : FileSystemItem
        {
            public Folder()
            {
                Children = new ObservableCollection<FileSystemItem>();
            }
        }

        public class File : FileSystemItem
        {
            public string Extension { get; set; }
        }
    }
}
