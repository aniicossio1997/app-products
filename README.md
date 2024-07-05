# app-products

## Descripción
Este proyecto es una aplicación ASP.NET Core 8 que utiliza Entity Framework Core para la gestión de la base de datos. A continuación, se detallan los pasos para configurar y ejecutar las migraciones de la base de datos.

## Requisitos previos
Antes de comenzar, asegúrate de tener instaladas las siguientes herramientas:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [MySql](https://www.mysql.com/downloads/)

## Configuración del Proyecto

1. Clonar el repositorio:
    ```bash
    git clone https://github.com/tu-usuario/app-products.git
    cd app-products
    ```

2. Configurar la base de datos:
    - Crear una base de datos en MySql para el proyecto.
    - Actualizar el archivo `appsettings.json` con tu cadena de conexión de MySql:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=app-products;User=root;Password=yourpassword;"
      }
    }
    ```

## Inicializar Migraciones de la Base de Datos en .NET Core 8
Para configurar y aplicar las migraciones de la base de datos en .NET Core 8, sigue estos pasos:

1. Añadir una nueva migración:
    ```bash
    dotnet ef migrations add InitialCreate
    ```


2. Aplicar la migración a la base de datos:
    ```bash
    dotnet ef database update
    ```
3. Tambien se puede usar: 
        Tambien se puede usar: 
    ```bash
    Add-Migration nombreMigracion
    ```
    ```bash
    Update-Database
    ```


### Categorías
En el proyecto, los productos se organizan en dos categorías específicas:
- **Indumentaria**: Representa la categoría de ropa y vestimenta, y tiene un ID de 1.
- **Accesorios**: Representa la categoría de artículos complementarios como joyas, bolsos, etc., y tiene un ID de 2.

Estas categorías se crean automáticamente durante el proceso de inicialización de la base de datos como parte del seed.


### Productos
Cada producto en el sistema pertenece a una de estas dos categorías.
Ejemplo de cómo se Veria un producto:

```csharp
{
  "category": {
    "name": "Indumentaria",
    "id": 1
  },
  "date": "05/07/2024",
  "price": 30,
  "name": "dede back",
  "id": 2
}
```
 - **Ir al swaggger**
 -     https://localhost:44361/swagger/index.html



