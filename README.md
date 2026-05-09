# Super Shop Management System

A simple desktop-based Super Shop Management System built with **C# Windows Forms** and **SQLite Database**.

## Features

- Login System
  - Admin Login
  - Employee Login

- Product Management
  - Add New Product (Admin Only)
  - Update Product Price (Admin Only)
  - Search Products

- Billing System
  - Select Products
  - Auto Price Calculation
  - Quantity Management
  - Grand Total Calculation

- Sales Features
  - Print Receipt
  - Sales History
  - Daily Sales Amount

- Role Based Access
  - Admin can manage products and prices
  - Employee can only create bills

## Technologies Used

- C#
- Windows Forms (.NET)
- SQLite
- Visual Studio

## Default Login

### Admin
```text
Username: admin
Password: 1234
Employee
Username: employee
Password: 1234
How to Run
Open the project in Visual Studio
Restore NuGet Packages
Build Solution
Run the project
Setup Installer

The project includes a setup installer.

Files:

setup.exe
SuperShopSetup.msi

Run:

setup.exe
Database

Database used:

SQLite

Database file:

supershop.db
