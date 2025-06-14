JWT Authentication with .NET 9
This project demonstrates how to implement JWT (JSON Web Token) authentication in an ASP.NET 9 Web API. It provides user registration and login functionality, secure password hashing, JWT token generation, and protected API routes using the [Authorize] attribute. Role-based authorization is also supported, with roles such as Admin and User.

The project is built using ASP.NET Core 9 for the API, Entity Framework Core for data access, and SQL Server as the database. JWT is used to secure endpoints, and Swagger (OpenAPI) is integrated to provide interactive API documentation and testing with support for Bearer tokens.

The solution is organized into several folders. The Controllers folder contains API endpoint definitions, while the Services folder contains the business logic for authentication and token handling. The Data folder includes the EF Core DbContext and migration files, and the Models folder holds entity and DTO classes. Configuration settings such as the database connection string and JWT parameters are stored in the appsettings.json file.

To run the project, clone the repository from GitHub at https://github.com/ArafatYilmaz01/JWT-Authentication-with-.NET-9. Create your own appsettings.json file based on the appsettings.Template.json file and update it with your database connection string and JWT secret. Apply the database migrations using the command dotnet ef database update. Start the application with dotnet run. Open the Swagger UI, usually available at https://localhost:<port>/swagger, to test and explore the API.

For authentication testing, first register a new user via the /api/Auth/register endpoint. Then log in through /api/Auth/login to receive a JWT token. Copy this token, use the Authorize button in Swagger UI, and paste the token with the Bearer prefix. This will grant access to protected endpoints such as /api/Auth which require authentication.

Passwords are stored securely using hashing algorithms. JWT tokens include claims for user ID and username, and are signed using a symmetric key defined in appsettings.json.

This project is open-source and free to use.
