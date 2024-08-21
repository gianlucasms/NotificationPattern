# Layered Architecture with Notification Pattern, EF Core, PostgreSQL, and xUnit Testing

## Overview

This project implements a **Layered Architecture** using the **Notification Pattern**, **Entity Framework Core** (EF Core) for data persistence with **PostgreSQL**, and **xUnit** for unit testing. It demonstrates how to build a scalable, testable, and maintainable .NET application.

## Table of Contents

1. [Technologies](#technologies)
2. [Architecture](#architecture)
3. [Setup Instructions](#setup-instructions)
4. [Running Tests](#running-tests)
5. [Contributing](#contributing)
6. [License](#license)

## Technologies

- **.NET 8**: Main framework
- **Entity Framework Core**: ORM for database interaction
- **PostgreSQL**: Relational database
- **xUnit**: Unit testing framework
- **FluentAssertions**: For better test assertions
- **AutoMapper**: Object-Object Mapping
- **Notification Pattern**: For handling validation and business rule notifications

## Architecture

This project follows a **Layered Architecture** with the following layers:

1. **Domain Layer**: Contains core business logic and domain entities.
2. **Application Layer**: Handles application services, notification validation, and DTOs.
3. **Infrastructure Layer**: Handles database access using EF Core, and any external services like repositories.
4. **Presentation Layer**: (Optional for API or UI) Could handle the user-facing interaction if required.

### Notification Pattern

The **Notification Pattern** is applied in the domain layer to validate business rules without throwing exceptions. It collects all validation errors and provides them in a single place, allowing for more flexible handling of errors.

### Example Diagram
```plaintext
+------------------+
|  Presentation    |
+------------------+
         |
+------------------+
|  Application     |
+------------------+
         |
+------------------+
|    Domain        |
+------------------+
         |
+------------------+
| Infrastructure   |
+------------------+

```

## Setup Instructions

### Prerequisites

- **.NET 8 SDK** installed on your machine.
- **PostgreSQL server** installed and running.
- **Entity Framework Core CLI tools** for migrations.

### Step 1: Clone the Repository

First, clone the repository to your local machine:

```bash
git clone https://github.com/gianlucasms/NotificationPattern.git
cd NotificationPattern
```

### Step 2: Configure PostgreSQL Database

Create a PostgreSQL database and configure the connection string in the appsettings.json file, located in the root of the project.

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=your-db;Username=your-user;Password=your-password"
  }
}
```
Make sure to replace the Database, Username, and Password with your actual database credentials.

### Step 3: Apply Migrations
After setting up the database, apply the EF Core migrations to create the necessary database schema.

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

This will generate and apply the initial migrations to your PostgreSQL database.

### Step 4: Running Tests
This project uses xUnit for unit testing. To run all the tests, execute the following command:

```
dotnet test
```
If you want to view detailed test output, you can run:
```
dotnet test --logger "console;verbosity=detailed"
```

## Contributing

Contributions are welcome! If you'd like to contribute, feel free to open issues or submit pull requests for new features, bug fixes, or improvements.

### Contribution Guidelines

- Ensure new features or bug fixes are properly tested.
- Follow the architecture and coding style defined by the project.
- Run all unit tests before submitting a pull request.



