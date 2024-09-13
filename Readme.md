EcomMVC Project

Overview

This project is a .NET Core MVC eCommerce web application. The project allows users to browse products, add them to a cart, and purchase them. Admins have additional capabilities to add, edit, or remove products from the catalog.

Prerequisites
Ensure that you have the following tools installed on your machine:

- .NET 6 SDK or a compatible version
- SQLite (if you're using SQLite as the database)
- An IDE such as Visual Studio or Visual Studio Code

Step 1: Clone the repository or Download and extract the zip file on your pc.

Step 2: Install Required Packages
//Run the following command inside the project directory: 

    dotnet restore

// This will download all the necessary NuGet packages for the project.
//Or you can manually install all the packages provided in the Requirement/packages.txt

    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.AspNetCore.Identity
    dotnet add package Rotativa.AspNetCore
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite

Step 3: Update the Database
//The project uses Entity Framework for database interactions. Before running the application, ensure the database is up to date:

    dotnet ef database update

Step 4: Build the Project
To ensure everything is set up correctly, build the project using the following command:

    dotnet build

Step 5: Run the project

    dotnet run

Step 6: After running the project, open your web browser and go to the localhost URL provided in the terminal (e.g., http://localhost:5000).

Step 7: Admin Account
On first run, an admin account is created automatically with the following credentials:

Email: admin@gmail.com
Password: Admin@123

Step 8: Make sure you have a folder for storing images. If necessary, create a directory where product images will be uploaded. 
    By default, images are uploaded to the wwwroot/images folder 

Step 9:Ensure that the Rotativa.AspNetCore package is configured correctly in the Program.cs file:


// Running with Visual Studio
1. Open the solution file (.sln) in Visual Studio.
2. Ensure all NuGet packages are restored.
3. Set the EcomMVC project as the startup project.
4. Press F5 to build and run the project in debug mode.