# TPI-SYSTEMCODE -> CURSO 3K5 -> GRUPO 20
Repositorio para el Trabajo Práctico Integrador de la materia PAV1
- Nombre del Dominio: SYSTEMCODE 
- Modulo a usar: Número 4 - Facturación de Proyectos y Productos
- Integrantes: 
  * Acosta, Facundo Leonel [Legajo: 72100]
  * Diaz Mac William, Rodrigo Tomas [Legajo: 77020]

# INFORMES DEL PROYECTO - ¿QUÉ CONTIENE?
- Ventana de Login
- Ventana de Principal
- Ventana ABMC de Usuarios
- Ventana ABMC de Perfiles
- Ventana ABMC de Clientes
- Ventana ABMC de Barrios
- Ventana ABMC de Proyectos
- Validaciones de DNI repetidos
- Validaciones de Perfiles repetidos
- Validaciones de Perfiles (Cada perfil, tiene permitida determinada actividad)
- Validaciones de Control de Perfiles (No se permite borrar un perfil si aún siguen usuarios asociados)
- Validaciones de Clientes repetidos
- Validaciones de Barrios repetidos
- Validaciones de Control de Barrios (No se permite borrar un barrio si aún siguen clientes asociados)
- Validaciones de Proyectos repetidos

# BASE DE DATOS
Se debe cargar la base de datos con el script almacenado en el repositorio, por los siguientes motivos:
- Se modificó la tabla de Usuarios con el fin de agregar la columna DNI
- Se quitaron los usuarios existentes para agregar los acordes al dominio
- Se quitaron los perfiles existentes para agregar los acordes al dominio
- Se actualizó cantidad de caracteres en la columna cuit de la tabla Clientes para pasar de 50 a 11
- Se actualizó tipo de fecha en la columna fecha_alta de la tabla Clientes para pasar de DateTime a Date
- Se agregó el campo cuit a la tabla de Contactos
- Se agregaron datos de barrios a la tabla de Barrios para agregar los acordes al dominio
- Se modificó el campo id_proyecto de la tabla Proyectos para ser un identity
- Se modificó el campo borrado de la tabla Proyectos pasando de tipo nchar a bit

# DATOS DE INICIO DE SESIÓN
- Usuario: general --> Clave: general --> Permisos: Encargado General
- Usuario: administracion --> Clave: administracion --> Permisos: Encargado de Administración
- Usuario: ventas --> Clave: ventas --> Permisos: Encargado de Ventas
