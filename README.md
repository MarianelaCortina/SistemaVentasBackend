El backend del Sistema de ventas lo realicé utilizando .Net Core 8. El sistema cuenta con un sistema de login, administración de roles, menú que se mostrará completo o no según el tipo de rol.

El proyecto está conformado por siete capas: API, Negocio, Dal, DTO, Model, Ioc, Utility
* Las funcionalidades de los menues las realicé creando APISRest con ASP.Net Core 8.
* Como motor de base de datos, utilicé Microsoft SQL SERVER
* Realicé los modelos: Menu, MenuRol, NumeroDocumento, Rol, Usuario, Venta, DetalleVenta, Categoría, Producto, en SQL SERVER.
* Plasmé la DB en el Sistema mediante el uso del comando Scaffold-DBContext...más el connection string 
* Para proteger algunas propiedades de los modelos, y que no se muestren en la interfaz gráfica (IG) use modelos DTO.
* También agregué Automapper, para el manejo de tipo de datos entre la IG y el backend

  En el Home se podrá visualizar:
- Menú Productos: que incluye categoría, stock y precio de cada producto. Que consiste en un ABM
- Menú Ventas: Desde el formulario se puede buscar un producto, el cual se puede agregar, tipo de pago. Se puede registrar la venta y nos da un número de venta.
- Menú Historial de Ventas: Que muestra un listado de ventas, que se puede buscar por un rango de fechas o por el número de ventas. También se puede ver el detalle de la venta
- Menú Reportes: muestra toda la información de las ventas, que se puede buscar por rango de fechas.
