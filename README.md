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
2. Run `Database/SmartMedDB.sql`.
3. Default credentials after seed:
   - **Admin:** `admin` / `admin123`
   - **Customer:** `john@email.com` / `customer123`

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
│   ├── Models/                 # All entity classes (Person, Medicine, Order, …)
│   ├── Services/               # Business logic
│   ├── Data/                   # DatabaseHelper + repositories
│   └── App.config
├── Database/
│   └── SmartMedDB.sql          # Schema + seed data
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

## Documentation

**Edit the report in Word:** `Report/SmartMed Report.docx` (~2400+ words, all sections filled).

1. Open the file in Microsoft Word.
2. Fill in cover details (Module, Student ID, Date).
3. Export to PDF when required for submission: **File → Save As → PDF**.

To regenerate section text from the project (optional):

```powershell
python scripts/fill_report.py
```

This overwrites body content in the docx; keep a backup if you have manual edits.

---

## Author

MNSBaanu
