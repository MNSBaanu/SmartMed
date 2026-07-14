# SmartMed — Pharmacy Management System

[![Platform](https://img.shields.io/badge/platform-Windows-0078D6?style=flat-square&logo=windows)]()
[![.NET](https://img.shields.io/badge/.NET_Framework-4.8-512BD4?style=flat-square&logo=dotnet)]()
[![Language](https://img.shields.io/badge/language-C%23-239120?style=flat-square&logo=csharp)]()
[![UI](https://img.shields.io/badge/UI-Windows_Forms-0078D6?style=flat-square)]()

---

## Overview

SmartMed Pharmacy is a **Windows Forms** desktop application for pharmacy operations. Two roles are supported:

| Role | Purpose |
|------|---------|
| **Admin** | Inventory, customers, order fulfilment, reporting, dashboard |
| **Customer** | Medicine search, ordering, order tracking, profile management |

Built in **C# / .NET Framework 4.8** as a single WinForms project (forms, services, and data access in one folder, like a standard lecture app).

---

## Quick Start

### 1. Database

1. Open **SQL Server Management Studio**.
2. Run `Database/SmartMedDB.sql` (schema only — no sample rows).
3. Create an admin user in SSMS (the app has no admin registration screen), for example:

```sql
INSERT INTO Admin (Username, Password, Email)
VALUES (
  N'admin',
  N'100000$AAAAAAAAAAAAAAAAAAAAAQ==$yWsCnUmJjkvSHVoBAzJtW4j3Vl7wobUZIS5YvUKjMpU=',
  N'admin@smartmed.lk'
);
-- Login: admin / admin123 (PBKDF2 hash; same format as PasswordHasher)
```

4. Customers register in the app (or use the registration form).

### 2. Connection string

Edit `SmartMed/App.config` if needed:

```xml
<add name="SmartMedDB"
     connectionString="Data Source=.;Initial Catalog=SmartMedDB;Integrated Security=True"
     providerName="System.Data.SqlClient" />
```

For LocalDB: `Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SmartMedDB;Integrated Security=True`

### 3. Build and run

```powershell
cd SmartMed
dotnet build SmartMed.sln
```

Or open `SmartMed.sln` in Visual Studio, set **SmartMed** as startup project, press **F5**.

Executable: `SmartMed/bin/Debug/net48/SmartMed.exe`

---

## Solution Structure

```
SmartMed/
├── SmartMed.sln
├── SmartMed/                   # Single WinForms project (all C# code)
│   ├── LoginForm.cs, ...       # Forms and UI controls
│   ├── Models/                 # All entity classes (User, Medicine, Order, …)
│   ├── Services/               # Business logic
│   ├── Data/                   # DatabaseHelper + repositories
│   └── App.config
├── Database/
│   └── SmartMedDB.sql          # Schema only (no seed data)
├── Docs/                       # Diagrams, report, scripts
└── Document.md                 # Assignment specification
```

---

## Features

### Admin
- Login, dashboard (sales, stock, active orders)
- Medicine CRUD
- Customer management
- Order status updates (*Pending*, *Ready for Pickup*, *Delivered*)
- Reports (sales, stock, order history)

### Customer
- Registration and login
- Medicine search (linear search + filter by name, category, price)
- Place orders (cart)
- Track orders
- Profile management

---



## Author

MNSBaanu
