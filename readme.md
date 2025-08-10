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

