   M S B A M L   `   `   `  ????I  EEditorImagenes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:EditorImagenesEditorImagenes  5   	       EditorImagenes.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:EditorImagenes     5   	     ? Title$  
MainWindow??5   	   $	??500??6   $	??900??6)    ? 
ResizeMode$ CanMinimize=?65   .??5	      ? ???	#FF96A1FF ? ColumnDefinitions 5
   
   ?? 5      $	??367 ?6   ?? 5      .+?5   
   R? -   5         R?Name$ treeView_Copy??5      $??	0,64,0,32q?6*   $??Center=?6=   $	??402??6Z   $??1??6f   ???	#FFB6EDFF5      ?? 5   
   -   5         ??Name$ imagen??5      $	??142??6    $	??Top=?6-   $??1??6E   $??103,141,100,0q?6V   $&?? /Placeholder_view_vector.svg.png??6m    J? RenderTransformOrigin$ 0.5,0.5&?6?   z?5      ?? 5      -   5        RotationTransform?? ?? CenterX$ 0Q?5   =    ?? CenterY$ 0Q?6I   5      ?? 5   
   -   5         ??Name$ 
bIzquierda??5      $??Rotate??6%   $	??Top=?6L   $??33??6d   $ 1.572,3.157&?6p   $??1??6?   $??276,379,211,0q?6?   ??5      ?? 5      	 ?? ImageSource$	 /girar-a-la-izquierda.png??6   5      ?? 5   
   -   5        $ bDerecha??5      $??Rotate??6#   $	??Top=?6J   $??33??6b   $ 1.572,3.157&?6n   $??1??6?   $??322,379,169,0q?6?   ??5      ?? 5      $	 /gira-a-la-derecha.png??6   5      ?? 5    
   $	??Top=?6   $??33??6*   $ 1.572,3.157&?66   $??1??6Z   $??364,379,127,0q?6j   ??5!      ?? 5"      $	 /acercarse.png??6   5$      ?? 5%   
   $	??Top=?6   $??33??6*   $ 1.572,3.157&?66   $??1??6Z   $??404,379,87,0q?6j   ??5&      ?? 5'      $	 /buscar.png??6   5)      ?? 5*   
   $	??Top=?6   $??33??6*   $ 1.572,3.157&?66   $??1??6Z   $??476,10,10,0q?6j   ??5+      ?? 5,      $	 /cerrar-sesion.png??6   5.      ?? 5/   
   -   5        $ btnLupa??5/      $	??Top=?6#   $??33??6;   $ 1.572,3.157&?6G   $??1??6k   $??374,10,112,0q?6{   ??50      ?? 51      $	 /busqueda.png??6   53      R? 54   
   -   5        $ treeView_Copy1??54      $??	0,69,0,27q?6+   $??Center=?6>   $	??286??6[   
???????6g   ?? 55   
   $??SELECCIONADOR DE IMAGENES??6   $??28,10,95,420q?64   $
??Bold"?6J   $$??Bahnschrift SemiBold Condensed)?6\   $??24(?6?   ??56      ?? 57      
 ?? EndPoint$
 0.5,1&?6&    ?? 
StartPoint$ 0.5,0&?67   .??58      ?  ? Color$ White??6#   ? 59      $ White??6#    ? Offset$ 1Q?61   ? 5:      $ 	#FF4D9CE6??6#   $ 0.988Q?65   5;      5=      ?? 5>   
   -   5        $ 
boton_guia??5>      $??1??6%   $?? ??65   $
??Left=?6@   $??426,10,10,0q?6[   $	??Top=?6p   $??33??6?   $??Cambria)?6?   $??47??6?   $ 
0.574,0.91&?6?   ??5?      ?? 5@      $	 
/ayuda.png??6   5B      5F      5H      