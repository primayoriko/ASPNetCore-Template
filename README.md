# ASPNetCore-Template

My own project template to start an ASP .NET Core Project.

## Prerequisite Understanding

---------
Before you could edit this template properly, I suggest you to learn basics of these  materials/topics

1. Web/Internet
   * HTTP (about their structure, request method, status code, etc)
   * JSON Data Format
   * Data Serialization & Deserialization
   * API (Application Programming Interface)
   * MVC (Model-View-Controller) Architecture
   * SQL Database
2. [C# language](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/)
   * Syntax
   * Object-Oriented
   * Lambda Function & Delegate
   * LINQ (Language-Integrated Query)
3. [ASP .NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-3.1&tabs=visual-studio)
   * Startup Class
   * Configuration
   * Dependency Injection
   * Routing
   * Middleware
   * Logging
   * Controller Class
   * HTTP Context Class
   * Data Binding & Model Validation
   * Tag Helper
   * View Rendering (with .cshtml file format)
   * Authentication (with Identity library)
   * Localization & Globalization
4. [Entity Framework Core](https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx) 
   * DBContext & DBSet Class
   * Connection
   * Querying
   * Migration
   * Data Seeding
5. Visual Studio Basic Functionality/Usage

## Tools Needed

---------
To make edits of these code easier, these are tools that you should have and mastered (at least the basics)

1. [Visual Studio](https://visualstudio.microsoft.com/)
2. API Development Application (such as [Postman](https://www.postman.com/) or [Insomnia](https://insomnia.rest/))
3. Web Browser (such as Mozilla, Chrome, etc)

## Solution Structure

---------
I classify my program solution into four separate projects, they are

1. WebAPI - Built-in/basic API project template from ASP .NET Core (don't need much extensions) 
2. ODataAPI - API project template based on OData (Open Data) protocol
3. MVCApp - Web Application project template with UI that constructed by MVC architecture
4. XUnitTest - XUnit-based unit test project template

## Feature Used

---------
In these projects, I've included template for basic and additional features of an ASP .NET Core project, they are

1. Authentication (with ASP .NET Core Identity Library) [ *MVCApp* ]
2. Localization & Globalization [ *MVCApp* ]
3. Custom Middleware [ *MVCApp* | *WebAPI* ]
4. Swagger (an API documenting library) [ *WebAPI* ]
5. OData (Open Data) Protocol [ *ODataAPI* ]
6. HTTP Request (API's data fetching) [ *MVCApp* ]
7. XUnit (an unit test library) [ *XUnitTest* ]
8. Data Seeding [ *MVCApp* | *WebAPI* | *ODataAPI* ]
