# Student-Database-Console-Application-with-CRUD-Operations

# StudentCRUD

A simple C# console application that connects to a MySQL database and performs basic **CRUD operations** (Create, Read, Update) on a `students` table.

##  Features

- Create a new student record
- Read a student record by ID
- Update existing student details
- (Delete functionality can be added easily)
- Interactive console input for dynamic data
- Uses MySQL as the backend database


##  Technologies Used

- C# (.NET Core / .NET Framework)
- MySQL
- MySql.Data NuGet package


##  MySQL Database Setup

Before running the app, create the database and table:

```sql
CREATE DATABASE taskdb;
USE taskdb;

CREATE TABLE students (
    id INT PRIMARY KEY,
    Stuname VARCHAR(100),
    Department VARCHAR(100)
);
