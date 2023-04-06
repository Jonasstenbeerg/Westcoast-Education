# Westcoast-Education
Two ASP.NET Core Web App projects with MVC pattern that handles frontend and one ASP.NET Core Web API(RESTful standard) project for backend that uses repository design pattern and SQLite relational database.

# Requirements
1 Build database
 * Install [ef-tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
 ```sh
dotnet tool install --global dotnet-ef
```
 * Navigate to EducationApi folder and run 
```sh
dotnet ef database update
```

# Common issues
 1 Not trusted SSL certificate for localhost
  * The issue gets resolved by running
```sh
dotnet dev-certs https --clean;
dotnet dev-certs https --trust;
```
