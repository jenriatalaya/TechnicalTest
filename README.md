![image](https://github.com/jenriatalaya/TechnicalTest/assets/81283568/067736bf-9def-41f3-ab66-317bf538e0b9)

Proyecto TechnicalTest

Este repositorio contiene un proyecto desarrollado en .NET 8 utilizando una arquitectura limpia (Clean Architecture) con el patrón CQRS (Command Query Responsibility Segregation). La aplicación se integra con una base de datos PostgreSQL utilizando Entity Framework Core y el cliente oficial de PostgreSQL para .NET.
Configuración de la Base de Datos

El proyecto está configurado para utilizar una base de datos PostgreSQL en la nube. Si se desea ejecutar las migraciones en otra base de datos, siga los siguientes pasos:

    Ubicación del Proyecto: Asegúrese de estar en el directorio del proyecto TechnicalTest.Api.

    Ejecución de Migraciones:

    bash

    dotnet ef database update --context ApplicationDbContext --project ../TechnicalTest.Infrastructure

    Este comando aplicará las migraciones al contexto ApplicationDbContext utilizando el proyecto TechnicalTest.Infrastructure donde están definidas las configuraciones de Entity Framework.

Estructura del Proyecto

El proyecto está estructurado siguiendo la arquitectura limpia y el patrón CQRS:

    TechnicalTest.Api: Proyecto de API principal. Contiene los controladores y la configuración de inicio de la aplicación.

    TechnicalTest.Application: Capa de aplicación que contiene los casos de uso (use cases) de la aplicación implementados con CQRS.

    TechnicalTest.Domain: Capa de dominio que define las entidades, los repositorios y los servicios del dominio de la aplicación.

    TechnicalTest.Infrastructure: Capa de infraestructura que implementa la lógica de acceso a datos utilizando Entity Framework Core y gestiona las migraciones de base de datos.

Requisitos del Sistema

    .NET SDK 8
    PostgreSQL cliente y servidor instalados o acceso a una base de datos PostgreSQL en la nube

Ejecución del Proyecto

Para ejecutar el proyecto, asegúrese de tener configurado el entorno adecuadamente con las variables de conexión a la base de datos PostgreSQL.

