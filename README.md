## Learn-Entity-Framework-with-CSharp
# Overview
This project is designed to help developers learn how to use Entity Framework (EF) Core with C#. It demonstrates how to create, read, update, and delete (CRUD) records in a MySQL database using EF Core, focusing on a simple student management system.

Features
Basic CRUD operations with a Student entity.
Configuration of database connection using DbContext.
Use of option classes to manage database connection details.
Support for generic methods to handle different entity types.
Getting Started
Prerequisites
.NET SDK (version 5.0 or later)
MySQL Server (version 6.0.2 or later)
Entity Framework Core and MySQL packages. You can install these via NuGet:

## Configuration
Before running the application, you need to configure your database connection in the OptionLogin class within the Program.cs file. Modify the following properties according to your MySQL server configuration:

Usage Example
The following is an example of how the application operates:

A new Student entity is created and added to the database.
All students are retrieved and displayed in the console.
The application finds a specific student by name, updates their last name, and saves the changes to the database.

## Introduction
Entity Framework (EF) is a powerful Object-Relational Mapping (ORM) framework for .NET applications. It enables developers to work with databases using .NET objects, simplifying data access and manipulation. In this post, we will explore the benefits of using EF and walk through a practical example using a school database to manage student information.

## What is Entity Framework?
Entity Framework is an ORM that allows developers to interact with databases using .NET objects. It abstracts the complexities of database interactions, allowing for faster and cleaner code development. EF supports various database systems, including SQL Server, MySQL, and PostgreSQL.

## Advantages of Using Entity Framework
Productivity: EF allows developers to focus on business logic rather than database interactions. The code is cleaner and easier to read.
Strongly Typed Queries: With LINQ (Language Integrated Query), developers can write strongly typed queries, reducing runtime errors.
Database Migrations: EF supports database migrations, allowing you to evolve your database schema over time without losing data.
Change Tracking: EF automatically tracks changes to your entities, simplifying the process of updating records.
Relationship Management: EF simplifies managing relationships between entities, making it easier to work with complex data models.

## Practical Implementation
#Setting Up the Context
In our example, we have defined two entities: Student and StudentAddress. Here's how the context is set up using DbContext:

# Create Entity (POCO Class) 
<img width="416" alt="image" src="https://github.com/user-attachments/assets/13d4c619-b400-4393-9c7a-6258e168554d">

# Interact with the database [Using Entity Framework]
This class defines configuration settings, relationships between entities, and sets up the database tables using DbSet properties.
<img width="604" alt="image" src="https://github.com/user-attachments/assets/ccc6c5ba-a141-46fc-ae19-ed61e92d9f88">

# What is Fluent API in Entity Framework ?
The Fluent API in Entity Framework Core is a way to configure entity properties and relationships using method chaining instead of data annotations.
OnModelCreating Method :

<img width="507" alt="image" src="https://github.com/user-attachments/assets/a393cf91-0e0b-4abd-a608-68fb621f4b6c">

## Apply CRUD Operation using Entity Framework ?
# Insert
<img width="446" alt="image" src="https://github.com/user-attachments/assets/833cb1d6-0eae-4729-a278-c45d690a3cc9">

# Select : Get All Record
<img width="893" alt="image" src="https://github.com/user-attachments/assets/9ecafb0a-ddf9-4986-8a7d-2e5c93426abb">

# Update Record
<img width="590" alt="image" src="https://github.com/user-attachments/assets/1ddaa7e4-6174-46a9-b80e-61645a4b3b7b">

# Delete Record
<img width="687" alt="image" src="https://github.com/user-attachments/assets/6dbece66-3433-48f0-9872-92802703299b">

## What is DataBase Migration in EF Core ?
# Database Migrations
# Advantage:
EF’s support for database migrations makes it easier to manage changes to the database schema as your application evolves.

# Explanation: 
With EF migrations, you can alter the database schema without losing data. Migrations allow you to incrementally apply changes to the database, keeping it in sync with your model classes. This is especially useful in collaborative projects where the database may undergo frequent updates.

# Example: 
If you add a new field to your Student model, a migration can automatically update the database schema to include this new field. EF handles the generation of migration scripts, making it straightforward to apply these changes to your database.

## Change Tracking
# Advantage:
EF automatically tracks changes made to entities in memory, which simplifies the process of updating records.

# Explanation:
EF’s change tracking feature keeps track of the current and original values of entity properties. When SaveChanges is called, EF generates the necessary SQL to update only those fields that were modified, which is efficient and reduces data redundancy.

# Example:
If you retrieve a Student entity, modify its Name property, and then call SaveChanges, EF will detect the modification and only update the Name field in the database, leaving other fields untouched.

# Conclusion
Entity Framework simplifies database interactions, making it an invaluable tool for developers working with C#. The ability to perform CRUD operations efficiently and manage relationships between entities allows for rapid application development. 







