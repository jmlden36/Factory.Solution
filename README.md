# _Dr. Sillystringz's Factory_

#### By _*Matt Luker*_

#### _A website to keep track of the machines and engineers that belong to each Dr. Sillystringz Factory location._

## Technologies Used
* C#
* .NET5
* ASP.NET Core MVC
* Razor
* NuGet
* git
* GitHub
* MySql
* Entity Framework

## Description
_A website where users can add factory locations, machines and engineers that repair machines.  You can license engineers to be able to repair machines and add machines and engineers to each factory location._

![Screenshot of database schema](/schema.jpeg)
## System Requirements
* _Install C# and .NET [here for Mac](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-5.0.401-macos-x64-installer)  or  [here for PC](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-5.0.401-windows-x64-installer) and follow the installation instructions_
* A text editor, such as [VS Code](https://code.visualstudio.com/)
* MySQL and MySQL Workbench

## Setup/Installation Instructions
* Open the terminal on your desktop
* Once in the terminal, use it to navigate to your desktop folder
* Once inside your desktop folder, use the command `git clone https://github.com/jmlden36/HairSalon.Solution.git`
* After cloning the project, navigate into it using the command `cd HairSalon.Solution/HairSalon`
* Create a file named "appsettings.json" in the `HairSalon` directory
* Add the following code to appsettings.json and add your MySQL user ID and password:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=james_luker;uid=[YOUR MYSQL USER ID];pwd=[YOUR PASSWORD];"
  }
}
```
* Please use your personal MySQL user ID where it says "[YOUR MYSQL USER ID] and use your personal password where it says [YOUR PASSWORD].  Do not include the square brackets.
* Open MySQL Workbench and log into your server
* Find the "Navigator" panel and select the "Administration" tab
* Click on "Data Import/Restore"
* Select "Import from Self-Contained File" and navigate to where you cloned the project
* Under "Default Schema to be Imported To" select "New..."
* Name the new schema "james_luker" and click "OK"
* Click "Start Import" in the bottom right corner
* After the import is finished, go back to the "Navigator" panel and switch to the "Schemas" tab
* Once in the "Schemas" tab, right-click and select "Refresh All" to see the imported database
* Then run the command `dotnet restore` to install project dependencies
* Then run the command `dotnet run` to run the project in the browser
* You will then command click on localhost:5000 in the terminal to open the project in your web browser
* Once on the webpage you can create stylists and clients and have create, read, update, and destroy functionality for both stylists and clients.

## Known Bugs
* _No known bugs_

## Contact Information

* _If you have any issues or notice any bugs please email [jamesmattluker@gmail.com](mailto:jamesmattluker@gmail.com)._

## License
_MIT License: https://opensource.org/licenses/MIT_

Copyright (c) _2022_ _Matt Luker_
