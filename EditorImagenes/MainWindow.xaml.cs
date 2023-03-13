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

    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ObservableCollection<Alumno> Alumnos { get; set; }
    }

    public class Alumno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int GrupoId { get; set; }
    }

    /// <summary>
    /// 
    /// Esta clase se encarga de controlar todos los elementos que modifican la imagen
    /// 
    /// </summary>

    public partial class MainWindow : System.Windows.Window
    {
        private ObservableCollection<Grupo> grupos;

        // Variables de instancia para escala, movimiento, posición y transformación
        private double _scale = 1.0;
        private bool _isMoving;
        private Point? position;
        private double deltaX;
        private double deltaY;
        private TranslateTransform _currentTT;

        /// <summary>
        /// 
        /// Esta funcion se encarga de controlar los botones
        /// 
        /// </summary>
        /// <param name="sender">Componente por defecto</param>
        /// <param name="e">Componente por defecto</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button clickedButton = (System.Windows.Controls.Button)sender;
            String imgRuta = "";

            //Este switch se encarga de diferenciar los botones por su x:Name
            switch (clickedButton.Name)
            {
                case "bIzquierda":
                    //En este caso gira 90º hacia la izquierda
                    RotationTransform.Angle -= 90;
                    UpdateTransform();
                    break;

                case "bDerecha":
                    //En este caso gira 90º hacia la derecha
                    RotationTransform.Angle += 90;
                    UpdateTransform();
                    break;

                case "btnLupa":
                    //En este caso hace zoom a la imagen
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

                //En este caso abre una gia de la aplicacion para el usuario
                case "btnGuia":
                    string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GuiaUsuario.pdf");
                    // Se inicia el proceso de la aplicación predeterminada para abrir el archivo PDF
                    try
                    {
                        Process.Start("explorer.exe", "\"" + pdfPath + "\"");
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Error al abrir el archivo PDF: " + ex.Message);
                    }
                    break;

                case "bSalir":
                    //En este caso cierra la aplicacion
                    this.Close();
                    break;

                case "bZoomMas":
                    //En este caso aumenta el tamaño de la imagen
                    if (_scale < 4.0)
                    {
                        _scale += 0.1;
                        UpdateTransform();
                    }
                    break;

                case "bZoomMenos":
                    //En este caso disminuye el tamaño de la imagen
                    if (_scale > 0.5)
                    {
                        _scale -= 0.1;
                        UpdateTransform();
                    }
                    break;
            }


        }

        // Este método es privado y se encarga de actualizar la transformación de la imagen
        private void UpdateTransform()
        {
            // Se crea un nuevo TranslateTransform utilizando las variables deltaX y deltaY
            var transformGroup = new TransformGroup();
            var translateTransform = new TranslateTransform(deltaX, deltaY);

            // Se añade el translateTransform al transformGroup
            transformGroup.Children.Add(translateTransform);
            
            // Se añade un nuevo ScaleTransform al transformGroup utilizando la variable _scale
            transformGroup.Children.Add(new ScaleTransform(_scale, _scale));

            // Se añade un objeto RotationTransform al transformGroup
            transformGroup.Children.Add(RotationTransform);

            // Se establece la propiedad RenderTransform de la imagen con el transformGroup
            imagen.RenderTransform = transformGroup;

            // Se establece la variable _currentTT con el translateTransform
            _currentTT = translateTransform;
        }

        // Estas variables privadas se utilizan para el evento MouseDown
        private Point? lastPosition = null;
        private bool isDragging = false;

        // Estas variables privadas se utilizan para el evento MouseDown
        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Si el botón presionado es el botón izquierdo
            if (e.ChangedButton == MouseButton.Left)
            {
                // Se establece la variable lastPosition con la posición del mouse en la imagen
                lastPosition = e.GetPosition(imagen);
                // Se establece la variable isDragging en true
                isDragging = true;
                // Se establece la propiedad Capture del mouse 
                Mouse.Capture(imagen);
            }
        }

        // Este método se llama cuando se mueve el mouse sobre la imagen
        private void MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
{
            // Si la variable isDragging es verdadera
            if (isDragging)
            {
                // Si la imagen tiene una imagen de origen establecida
                if (imagen.Source != null)
                {
                    // Se obtiene la nueva posición del mouse en la imagen
                    Point newPosition = e.GetPosition(imagen);

                    // Se calcula la diferencia entre la nueva posición y la última posición almacenada en lastPosition
                    deltaX = newPosition.X - lastPosition.Value.X;
                    deltaY = newPosition.Y - lastPosition.Value.Y;

                    // Se obtiene la transformación actual de la imagen
                    var transform = imagen.RenderTransform as TransformGroup;

                    // Si la transformación es nula, se crea una nueva TransformGroup con una TranslateTransform
                    if (transform == null)
                    {
                        transform = new TransformGroup();
                        var translate = new TranslateTransform();
                        transform.Children.Add(translate);
                        imagen.RenderTransform = transform;
                    }

                    // Se obtiene el TranslateTransform de la transformación actual de la imagen
                    var translateTransform = transform.Children.OfType<TranslateTransform>().FirstOrDefault();

                    // Si el TranslateTransform no es nulo, se actualizan sus valores X e Y
                    if (translateTransform != null)
                    {
                        translateTransform.X += deltaX / 2;
                        translateTransform.Y += deltaY / 2;
                    }

                    // Se establece lastPosition con la nueva posición del mouse
                    lastPosition = newPosition;
                }
    }
}

        // Este método se llama cuando se suelta el botón izquierdo del mouse sobre la imagen
        private void MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Si el botón presionado es el botón izquierdo
            if (e.ChangedButton == MouseButton.Left)
            {
                // Se establece la variable isDragging en false
                isDragging = false;

                // Se establece la propiedad Capture del mouse en null para liberar la captura de eventos del mouse
                Mouse.Capture(null);
            }
        }

        // Este es el constructor de la clase MainWindow
        public MainWindow()
        {
            // Se inicializa la interfaz de usuario
            InitializeComponent();

            // Se crea una nueva instancia de DirectoryInfo que representa el directorio raíz del programa
            var rootDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")));

            // Se obtiene la ruta completa de la carpeta "MiCarpeta" dentro del directorio raíz
            var miCarpetaPath = Path.Combine(rootDirectory.FullName);

            // Se crea una nueva instancia de Folder que representa la carpeta raíz "MiCarpeta"
            var root = new Folder { Name = "MiCarpeta" };

            // Se crea una nueva ObservableCollection de FileSystemItem que representa los elementos dentro de la carpeta "MiCarpeta"
            root.Children = new ObservableCollection<FileSystemItem>();

            // Se itera a través de todos los subdirectorios dentro de "MiCarpeta" y se crea un Folder correspondiente para cada uno
            foreach (var directory in Directory.GetDirectories(miCarpetaPath))
            {
                root.Children.Add(CreateFolder(directory));
            }

            // Se itera a través de todos los archivos dentro de "MiCarpeta" y se crea un File correspondiente para cada uno
            foreach (var file in Directory.GetFiles(miCarpetaPath))
            {
                root.Children.Add(CreateFile(file));
            }

            // Se establece el origen de datos del TreeView con una lista que contiene la carpeta raíz "MiCarpeta"
            treeView_Copy1.ItemsSource = new List<Folder> { root };

            // Se suscribe al evento SelectedItemChanged del TreeView para que se llame al método "seleccionImagen" cada vez que se selecciona un elemento
            treeView_Copy1.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(seleccionImagen);
        
            //Acceso a datos

        
        }

        // Este método se llama cada vez que se selecciona un elemento en el TreeView
        public void seleccionImagen(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Se obtiene el elemento FileSystemItem seleccionado
            FileSystemItem selectedItem = treeView_Copy1.SelectedItem as FileSystemItem;

            // Si el elemento seleccionado no es nulo
            if (selectedItem != null)
            {
                // Se obtiene el nombre del elemento seleccionado
                string selectedHeader = selectedItem.Name;

                // Si el elemento seleccionado es una imagen
                if (selectedHeader.EndsWith(".jpg") || selectedHeader.EndsWith(".jpeg") || selectedHeader.EndsWith(".png"))
                {
                    // Se obtiene la ruta completa de la imagen seleccionada
                    var rootDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")));
                    var miCarpetaPath = Path.Combine(rootDirectory.FullName, selectedHeader);

                    // Se crea una nueva instancia de BitmapImage y se establece su UriSource a la ruta de la imagen seleccionada
                    BitmapImage image = new BitmapImage();
                }
            }
        }

        // Esta es la clase principal llamada "Root"
        public class Root
        {
            // Propiedad pública llamada "Name" que representa el nombre de la raíz
            public string Name { get; set; }

            // Propiedad pública llamada "Children" que es una colección observable de strings que representa los hijos de la raíz
            public ObservableCollection<string> Children { get; set; }
        }

        // Esta es una clase privada que se utiliza para crear un objeto de tipo "Folder" a partir de una ruta de archivo
        private Folder CreateFolder(string path)
        {
            // Se crea un nuevo objeto de tipo "Folder" con el nombre del archivo extraído de la ruta
            var folder = new Folder { Name = Path.GetFileName(path) };

            // Se crea una nueva colección observable de FileSystemItem para los hijos de la carpeta
            folder.Children = new ObservableCollection<FileSystemItem>();

            // Se itera sobre cada subdirectorio de la carpeta y se añade un objeto de tipo "Folder" a la colección de hijos
            foreach (var directory in Directory.GetDirectories(path))
            {
                folder.Children.Add(CreateFolder(directory));
            }

            // Se itera sobre cada archivo de la carpeta y se añade un objeto de tipo "File" a la colección de hijos
            foreach (var file in Directory.GetFiles(path))
            {
                folder.Children.Add(CreateFile(file));
            }

            // Se devuelve la carpeta
            return folder;
        }

        // Esta es una clase privada que se utiliza para crear un objeto de tipo "File" a partir de una ruta de archivo
        private File CreateFile(string path)
        {
            // Se crea un nuevo objeto de tipo "File" con el nombre y la extensión extraídos de la ruta
            return new File { Name = Path.GetFileName(path), Extension = Path.GetExtension(path) };
        }

        // Esta es una clase base llamada "FileSystemItem"
        public class FileSystemItem
        {
            // Propiedad pública llamada "Name" que representa el nombre del archivo o carpeta
            public string Name { get; set; }

            // Propiedad pública llamada "Children" que es una colección observable de FileSystemItem que representa los hijos del archivo o carpeta
            public ObservableCollection<FileSystemItem> Children { get; set; }
        }

        // Esta es una clase que hereda de FileSystemItem llamada "Folder"
        public class Folder : FileSystemItem
        {
            // Constructor que inicializa la colección de hijos
            public Folder()
            {
                Children = new ObservableCollection<FileSystemItem>();
            }
        }

        // Esta es una clase que hereda de FileSystemItem llamada "File"
        public class File : FileSystemItem
        {
            // Propiedad pública llamada "Extension" que representa la extensión del archivo
            public string Extension { get; set; }
        }
    }
}