# COSC 3380 Team 4 Project - Theme Park

This is the group project by Team 4 of COSC 3380, Fall 2022 semester at the University of Houston.

# App Dependencies

The app can be built on Windows, macOS (both Intel and Apple Silicon), or Linux. In each case, you will need:

* [Microsoft .NET 6 with ASP.NET Core Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) This is included in recent versions of Visual Studio and Visual Studio for Mac, but Visual Studio is not required to build the app. The SDK installer bundles the ASP.NET Core Runtime and is preferred.
* [Microsoft's SQL client library for .NET](https://www.nuget.org/packages/Microsoft.Data.SqlClient/) This is probably included in your build of Visual Studio, but to be sure, one can run:

```
dotnet add package Microsoft.Data.SqlClient
```

in the terminal or command prompt inside of the `cosc3380-group4-themepark/cosc3380-group4-themepark` directory. This app was tested with version 5.0.1

# Database Installation and Configuration

## Platform options

The database used for this project was Microsoft SQL Server 2017 Developer Edition, as packaged for Docker Engine for running on Ubuntu Linux hosts. However, any other available deployment of SQL Server 2017 Developer Edition will work.

Regardless of platform, the machine or VM guest which is hosting SQL server requires at least 2 GB of RAM.

### Windows

Microsoft provides a free installer for the Windows version of SQL Server 2017 Developer Edition [here](https://www.microsoft.com/en-IN/sql-server/sql-server-downloads). This will be suitable. However, one must use [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) or [Azure Data Studio](https://azure.microsoft.com/en-us/products/data-studio/?cdn=disable) to connect and configure the server to use SQL authentication in addition to Windows authentication. The latter is included by default in recent versions of Visual Studio.

### Linux (bare-metal)

For bare-metal installations on Linux, Microsoft provides documentation for Red Hat Enterprise Linux (RHEL) 7.3, Ubuntu Linux 16.04, and SUSE Linux Enterprise Server (SLES) v12 SP. I have not tried to install it this way, as it is much easier to get working using Docker Engine.

### Docker

The easiest (in my experience) way to deploy is using Docker Engine. Besides the typical Docker Engine on Linux, Docker for Windows also works. Although we have not tested it, Docker for Mac will work on Intel Macs, but Microsoft SQL Server does not support Apple Silicon Macs.

* Install Docker Engine, Docker for Windows, or Docker for Mac
* Verify that Docker is running and that it has been allocated at least 2 GB of RAM
* In a terminal or command prompt, run `docker pull mcr.microsoft.com/mssql/server:2017-latest` to fetch the latest updated version of SQL Server 2017.
* Run `docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Passwordtesting" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest` to quickly create a new container running Microsoft SQL Server 2017

Further documentation regarding running Microsoft SQL Server on Docker can be found on [DockerHub](https://hub.docker.com/_/microsoft-mssql-server)

Our configuration of Docker Engine for running Microsoft SQL Server can be seen in the `docker-compose.yml` file, using Docker Compose.

## Importing from the dump and configuring logins

Before starting, the SQL server should be configured to allow SQL authentication in addition to Windows Authentication, and a system administrator account should be enabled to allow configuration of the schema.

The BACPAC file `themepark_dev.bacpac` includes a full database dump. The easiest means to set up the database on a local database server is Microsoft's SqlPackage.exe utility, using the `Import` option as documented [here](https://learn.microsoft.com/en-us/sql/tools/sqlpackage/sqlpackage?view=sql-server-ver16)

The invocation we used to produce this database dump:

```
./sqlpackage /Action:"Export" /SourceServerName:"cosc3380group4.moorman.xyz" /SourceDatabaseName:"themepark_dev" /SourceUser:"sa" /SourcePassword:"<password>" /TargetFile:"./themepark_dev-$(date --rfc-3339="date").bacpac"
```

The corresponding invocation to restore the backup to an SQL server running on localhost:

```
./sqlpackage /Action:"Import" /SourceFile:"./themepark_dev.bacpac" /TargetServerName:"localhost" /TargetDatabaseName:"themepark_dev" /TargetUser:"sa" /TargetPassword:"<password>" /TargetEncryptConnection:"False"
```

It will be possible to `TargetPassword` appropriately to the system administrator password set when you had installed SQL Server.

It is also possibly necessary to fix the user logins, as in my testing I had been unable to log in to the SQL server using the `webapp` login until I reset its password.
In order to be sure that the login is setup correctly, run the following in the Command Prompt or terminal (this assumes that `sqlcmd` is in your `$PATH` or `%PATH%` as it should be after installing SQL Server):

```
sqlcmd -S localhost -i fix-login.sql
```

## Additional Configuration for local install

Besides the code included in the repository, there were further configuration required for the application for its current deployment at [https://cosc3380group4.moorman.xyz](https://cosc3380group4.moorman.xyz)

## `db.json`

The app is configured to connect to the database using a configuration read from `db.json`. This file is not included in the repository for security reasons, but a copy of `db.json` relevant to our Docker-based deployment is included in the project submission. For usage of a different SQL database server, the file must be modified appropriately with the hostname, username, and password appropriate to that database. The database and application were served on the same Docker Engine in a shared network.

# Configuration only relevant to our production deployment

## `appsettings.json`

For production deployment using Docker it was necessary to modify Kestrel's configuration to accept an external SSL certificate and key in order for HTTPS hosting to be successful, as many browsers do not accept the developer SSL certificates. For the sake of completion, the requisite file has been included in the project submission. This relies on the existence of Let's Encrypt SSL certificates to be installed in the same directory. It is completely unnecesary for non-production deployment.

## `Dockerfile`

The `Dockerfile` included was used to build the application for deployment on Docker Engine. It is based almost exactly on the ASP.NET example Dockerfile from Microsoft's documentation.

## `docker-compose.yml`

The final combination of running both Microsoft SQL Server and our application on the production server was managed through [Docker Compose](https://docs.docker.com/compose/). The included `docker-compose.yml` provides the script definition for instantiating and configuring the containers. They share a private network. 

# Building and running the app

For localhost testing, it is possible to build and run the app from the command line, or through Visual Studio, once it has been configured to use the correct SQL server by editing `db.json` appropriately as shown above.

## Command-line

From the root directory of the repository, in the terminal enter:

* `dotnet build`
* `dotnet run --project cosc3380-group4-themepark`

This will fetch the NuGet library dependencies, build the app, and start a Kestrel server hosting the app locally at `localhost:7035`.

## Visual Studio

* Open the `cosc3380-group4-themepark.sln` file from the root directory in Visual Studio
* Build and run the application using the menu

The server should start automatically and a new browser tab will open to `localhost:7035`.

## Using the app

After opening the browser to the `localhost:7035` page, you will be greeted with a landing page with login options on the top-right corner. By logging into accounts with the customer role, users will be able to scroll to "Tickets" and enter the ticket reservation page, allowing them to view their ticket reservations, cancel their reservations, and place new ones for future dates.

Logins with employee roles will be able to access the "Internal" site and will be permitted access to pages with functionality specific to their role, such as maintenance request and completion forms, sales reports, and employee rosters and forms to add new employee.s
