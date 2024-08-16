Project Description
This project is a microservices-based application designed to manage employee data with secure authentication. The system consists of two primary services:

Employee Service: This service handles all employee-related operations, including creating, updating, retrieving, and deleting employee records. It interacts with a database to store and manage employee information.

AuthGuard Service: This service is responsible for securing the Employee Service by issuing and validating tokens. It uses the OAuth 2.0 Client Credentials flow to authenticate requests and ensure that only authorized clients can access the Employee Service.

Workflow:

Client: Initiates requests to perform operations on employee data.
Employee Service: Receives requests from clients, validates the tokens with the AuthGuard Service, and performs the requested operations on the database.
AuthGuard Service: Provides token-based authentication. When the Employee Service needs to validate a token, it requests a token from AuthGuard and verifies it.
The interaction between these services ensures that only authenticated and authorized clients can perform operations, maintaining data security and integrity.

Features
Secure token-based authentication using OAuth 2.0.
CRUD operations for employee data management.
Integration between employee management and authentication services.
Error handling for unauthorized access attempts.
This setup provides a robust framework for secure and scalable employee management within a microservice architecture.

Setup
Requirements
.NET 8.0
PostgreSQL
Docker (Optional)

Steps
1. Clone the Repository
  git clone https://github.com/username/repository.git
  cd repository
2. Install Dependencies
dotnet restore

3. Start the Database
Configure PostgreSQL and create the necessary tables.

4. Run the Applications
  dotnet run --project EmployeeService.csproj
  dotnet run --proejct AuthGuard.csproj

 Usage
 ***
API Usage
***
Get Token
Endpoint: /connect/token
Method: POST
Parameters:
grant_type: client_credentials
client_id: [any clientid in config at authGuard]
client_secret: employee
**
Add Employee
Endpoint: /api/employees/
Method: POST
Body: Employee information in JSON format
**
Get Employee
Endpoint: /api/employees/getbyid/{id}
Method: GET
Parameters:
id: The ID of the employee to retrieve
**
Get All Employees
Method: GET
Endpoint: /api/employees/get-all
***
Get a Employee
Method: GET
Endpoint: /api/employees/{id}
***
Update a Employee
Method: PUT
Parameters:
id: The ID of the employee to update
Body: Updated employee information in JSON format
***
Delete Employee
Endpoint: /api/employees/{id}
Method: DELETE
Parameters:
id: The ID of the employee to delete
***
To access certain endpoints, you need to include a token in the Authorization header of your requests. Ensure that the token has the required scopes for the operation you are performing.

