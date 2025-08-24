# 🔧 ServiceLog - Device Service Management System

## 📋 Project Description

ServiceLog is a modern device service management system built with REST API architecture. The application enables comprehensive management of service tickets, devices, service history, and system users. The system is designed for service companies that need an efficient tool for tracking and managing device repair processes.

## 🚀 Main Features

- **🎫 Service Ticket Management** - Create, update, and track ticket status
- **📱 Device Management** - Device catalog with categories and statuses
- **🔧 Service History** - Complete documentation of all repairs and maintenance
- **👥 User Management** - Role-based system and permissions
- **🏷️ Categorization** - Organization of devices by types and categories

## 📓🔧 Architecture

### Design Patterns
- **Repository Pattern** - Data access abstraction
- **Service Layer** - Business logic in service layer
- **DTO Pattern** - Data transfer between layers
- **Dependency Injection** - Loose coupling between components

## 🧰🔧 Technologies Used

### Backend
- **.NET 9.0** - Latest Microsoft framework version
- **ASP.NET Core Web API** - Modern framework for building APIs
- **Entity Framework Core 9.0** - ORM for SQL Server database management
- **MongoDB.Driver 3.4.0** - Driver for NoSQL MongoDB database

### Databases
- **SQL Server** - Relational database for authorization and basic data
- **MongoDB** - NoSQL database for dynamic documents (tickets, service history)

### Authentication & Security
- **JWT Bearer Token** - Secure API authorization
- **ASP.NET Core Identity** - User and role management system
- **Role-based Access Control (RBAC)** - Role-based access control

### Validation & Logging
- **FluentValidation 12.0.0** - Model and DTO validation
- **Serilog 4.3.0** - Advanced logging with file and console output

### Documentation & Testing
- **Swagger/OpenAPI** - Automatic API documentation
- **xUnit** - Unit testing framework
- **Functional and Integration Tests** - Comprehensive test coverage

### Containerization
- **Docker** - Application containerization
- **Multi-stage builds** - Docker image optimization

## 📝 API Endpoints

### Auth
- **POST** `/api/Auth/register` – Register a new user  
- **POST** `/api/Auth/login` – Log in an existing user  

### Category
- **POST** `/api/Category` – Create a new category  
- **GET** `/api/Category` – Display all categories  
- **GET** `/api/Category/{id}` – Display a category by ID  
- **PUT** `/api/Category/{id}` – Update an existing category  
- **DELETE** `/api/Category/{id}` – Delete a category by ID  

### Device
- **POST** `/api/Device` – Create a new device  
- **GET** `/api/Device` – Get all devices  
- **GET** `/api/Device/{id}` – Display a device by ID  
- **PUT** `/api/Device/{id}` – Update an existing device  
- **DELETE** `/api/Device/{id}` – Delete a device by ID  
- **GET** `/api/Device/{id}/service-history` – Display service history for a device  

### ServiceHistory
- **POST** `/api/ServiceHistory` – Create a new service history  
- **GET** `/api/ServiceHistory` – Get all service histories  
- **GET** `/api/ServiceHistory/{id}` – Display a service history by ID  
- **PUT** `/api/ServiceHistory/{id}` – Update a service history  
- **DELETE** `/api/ServiceHistory/{id}` – Delete a service history  

### Ticket
- **POST** `/api/Ticket` – Create a new ticket  
- **GET** `/api/Ticket` – Display all tickets  
- **GET** `/api/Ticket/{id}` – Get a ticket by ID  
- **PUT** `/api/Ticket/{id}` – Update a ticket  
- **DELETE** `/api/Ticket/{id}` – Delete a ticket  
- **GET** `/api/Ticket/my-tickets` – Get all tickets for the authenticated user  
- **POST** `/api/Ticket/{id}/devices` – Add devices to a ticket  
- **PATCH** `/api/Ticket/{id}/change-status` – Change ticket status  
- **PATCH** `/api/Ticket/{ticketId}/assign-technican` – Assign a technician to a ticket  

### User
- **GET** `/api/User` – Display all users  
- **GET** `/api/User/{id}` – Get a user by ID  
- **PUT** `/api/User/{id}` – Update a user  
- **DELETE** `/api/User/{id}` – Delete a user  

## 🔒 Security Features

### Authentication & Authorization
- **JWT Token Authentication** - Secure tokens with defined lifetime
- **Identity Framework** - Password management with security requirements
- **Role-based Authorization** - Endpoint-level access control

### API Security
- **Rate Limiting** - Request limit enforcement (10 requests/minute per user)
- **HTTPS Redirection** - Enforced encrypted connections
- **CORS Policy** - Cross-origin request control
- **Input Validation** - Validation of all input data

### Password Security
- **Minimum length**: 8 characters
- **Required**: digits, lowercase and uppercase letters
- **Unique characters**: minimum 3

