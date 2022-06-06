# Shisha flavor website
Shisha flavor website built using [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) and [Angular](https://angular.io/). The database is local made using
[SQL Server Manager Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15). It features a list of flavors
that can be viewed, the users being able to write comments about them. It features authentication and authorization.



# Front-end
Made using [Angular CLI 13.0.2](https://www.npmjs.com/package/@angular/cli/v/13.0.2) with [Angular Material](https://material.angular.io/) and some help from
[Animate.css](https://animate.style/).

## Prerequisites
Download and install [Node.js](https://nodejs.org/en/download/).

Download and install [Angular](https://angular.io/guide/setup-local). Using the following command will do:

```bash
npm install -g @angular/cli
```

## Installation
[Clone](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository) or [download](https://www.itprotoday.com/development-techniques-and-management/how-do-i-download-files-github) the application.

Run the following command using the command prompt in the main directory of the front-end part, in our case /Frontend/Shisha.

```bash
npm install
```

## Usage
Use the following command in the main directory of the front-end part, in our case /Frontend/Shisha. The website will be available to use at [localhost:4200](http://localhost:4200/).

```bash
ng serve
```


# Back-end
Made using [ASP.NET 5.0.402 Web API](https://dotnet.microsoft.com/en-us/apps/aspnet) with [Entity Framework](https://docs.microsoft.com/en-us/ef/) and [Web API Authentication and Authorization](https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/authentication-and-authorization-in-aspnet-web-api).

## Prerequisites
Download and install [Dotnet v5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0).

Download and install [Visual studio](https://visualstudio.microsoft.com/). Make sure you select the .NET package while installing.

## Installation
[Clone](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository) or [download](https://www.itprotoday.com/development-techniques-and-management/how-do-i-download-files-github) the application.

## Usage
Open the Shisha.sln file in order to open the solution in Visual Studio, and hit run. It'll open up [Swagger](https://swagger.io/) which can be used to test the back-end.
