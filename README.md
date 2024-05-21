El backend del Sistema de ventas lo realicé utilizando .Net Core 8. El sistema cuenta con un sistema de login, administración de roles, menú que se mostrará completo o no según el tipo de rol.

![Api-Swagger](https://github.com/MarianelaCortina/SistemaVentasBackend/assets/73797352/f8359e55-fb26-497f-bd35-09919d352dba)


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
