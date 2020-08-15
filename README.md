# ASPNetCore-Template
My own template to start an ASP .NET Core Project.

## Prerequisite Understanding
-------
Before you could edit this template properly, I suggest you to learn basics of these  materials/topics
1. Web/Internet
   * HTTP (structure, request method, status code, etc)
   * Json format
   * Serialize-deserialize
   * API
   * MVC (Model-View-Controller) architecture
   * SQL Database
2. [C# language](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/)
   * Syntax
   * Object-Oriented
   * Lambda function/delegate
   * LINQ
3. [ASP .NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-3.1&tabs=visual-studio)
   * Startup Class
   * MVC in ASP .NET Core Structure
   * Configuration
   * Dependency Injection
   * Routing
   * Middleware
   * Logging
   * View Rendering (.cshtml)
   * Tag Helper
   * Authentication (with Identity feature)
   * Localization - Globalization
4. [Entity Framework Core](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx) 
   * DBContext & DBSet
   * Connection
   * Querying
   * Migration
   * Data Seeding
5. Visual Studio basic functionality

## Tools Needed
---------
To make edits of these code easier, these are tools that you should have and mastered (at least the basics)
1. [Visual Studio](https://visualstudio.microsoft.com/) 
2. API Development App (such as [Postman](https://www.postman.com/) or [Insomnia](https://insomnia.rest/))
3. Web Browser (such Mozilla, Chrome, etc)

## Solution Structure
-------
I classify my program solution into four separate projects, they are
1. WebAPI - Built-in API project template from ASP .NET Core (don't need much extensions) 
2. ODataAPI - API project template based on OData (Open Data) protocol
3. MVCApp - Web App project template with UI that constructed by MVC architecture
4. XUnitTest - XUnit-based unit test project template

## Feature Used
--------
In these projects, I've included template for basic and additional features of an ASP .NET Core project, they are
1. Authentication [ *MVCApp* ]
2. Localization and globalization [ *MVCApp* ]
3. Custom Middleware [ *MVCApp* | *WebAPI* ]
4. Swagger [ *WebAPI* ]
5. OData [ *ODataAPI* ]
6. HTTP request [ *MVCApp* ]
7. XUnit unit test [ *XUnitTest* ]
8. Data Seeding [ *MVCApp* | *WebAPI* | *ODataAPI* ]
