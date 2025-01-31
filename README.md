El backend del Sistema de ventas lo realicé utilizando .Net Core 8. 

El sistema cuenta con:
* Un sistema de login, administración de roles, menú que se mostrará completo o no según el tipo de rol logueado (Admin, Empleado o Supervisor).
* Sección Dashboard: en el cual se muestran las ventas realizadas en la ultima semana, en formato de gráfico de barras verticales. Cada barra representa el total de ventas de cada día.
* Listado de productos.
* Sección Ventas, donde el usuario podrá seleccionar los productos y registrar la venta, la cual se guardará y te brindará un numero de operación.
* Sección Historial ventas: en donde se puede buscar las ventas realizadas, por un rango de fechas o por el número de ventas y luego te las muestra en forma de listado. También te permite acceder al detalle de cada venta.
* Sección Reporte de ventas: en esta sección se puede seleccionar el reporte de las ventas, seleccionando una fecha de inicio y una fecha de fin y luego se mostrarán en un listado. Además, cuenta con un botón que te permite exportar la información en formato Excel.
  


![Captura-backend-somee](https://github.com/MarianelaCortina/SistemaVentasBackend/assets/73797352/d296b481-4821-4ca1-a766-d1e610100b58)



El proyecto está conformado por siete capas: API, Negocio, Dal, DTO, Model, Ioc, Utility.


![image](https://github.com/MarianelaCortina/SistemaVentasBackend/assets/73797352/b2980652-389c-4195-9e0f-a52155a640d8)


* Las funcionalidades de los menues las realicé creando APISRest con ASP.Net Core 8.
  
* Como motor de base de datos, utilicé Microsoft SQL SERVER.
  
* Realicé los modelos: Menu, MenuRol, NumeroDocumento, Rol, Usuario, Venta, DetalleVenta, Categoría, Producto, en SQL SERVER.
  
* Plasmé la DB en el Sistema mediante el uso del comando Scaffold-DBContext...más el connection string.


![Diagrama_Entidad_Relacion](https://github.com/MarianelaCortina/SistemaVentasBackend/assets/73797352/799d7185-5523-4c2f-b465-ae7414640880)


  
* Para proteger algunas propiedades de los modelos, y que no se muestren en la interfaz gráfica (IG) use modelos DTO.

* También agregué Automapper, para el manejo de tipo de datos entre la IG y el backend.

  En el Home se podrá visualizar:
  
- Menú Productos: que incluye categoría, stock y precio de cada producto. Que consiste en un ABM.
  
- Menú Ventas: Desde el formulario se puede buscar un producto, el cual se puede agregar, tipo de pago. Se puede registrar la venta y nos da un número de venta.
  
- Menú Historial de Ventas: Que muestra un listado de ventas, que se puede buscar por un rango de fechas o por el número de ventas. También se puede ver el detalle de la venta.
  
- Menú Reportes: muestra toda la información de las ventas, que se puede buscar por rango de fechas.
