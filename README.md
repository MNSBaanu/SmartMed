# SmartMed — Pharmacy Management System

[![Platform](https://img.shields.io/badge/platform-Windows-0078D6?style=flat-square&logo=windows)]()
[![.NET](https://img.shields.io/badge/.NET_Framework-4.x-512BD4?style=flat-square&logo=dotnet)]()
[![Language](https://img.shields.io/badge/language-C%23-239120?style=flat-square&logo=csharp)]()
[![UI](https://img.shields.io/badge/UI-Windows_Forms-0078D6?style=flat-square)]()

---

## Overview

SmartMed Pharmacy is a chain of pharmacies providing prescription and over-the-counter medications, wellness products, and health services. This project delivers a **Windows Forms desktop application** to manage day-to-day pharmacy operations—inventory, customer orders, and service quality—for two user roles:

| Role | Purpose |
|------|---------|
| **Admin** | Inventory, customers, order fulfilment, reporting, and dashboard analytics |
| **Customer** | Medicine search, ordering, order tracking, and profile management |

The system is built in **C#** using an object-oriented design, with local data persistence and on-premise deployment on Windows workstations.

---

## Features

### Admin

- Secure login
- **Manage medicine details** — Add, update, delete (name, category, dosage, price, stock, supplier)
- **Manage customer details** — View and update customer records
- **Manage orders** — View all orders; update status (*Pending*, *Ready for Pickup*, *Delivered*)
- **Generate reports** — Sales, stock, and customer order history
- **Dashboard** — Total sales, medicines in stock, and active orders

### Customer

- Register and login
- **Search medicines** — By name, category, or price range
- **Place orders** — Add to cart and submit orders
- **Track orders** — View order status
- **Profile management** — Update personal and contact details

### Additional

- Discounts and promotions on medicines
- Medicine expiry tracking and notifications
- Prescription upload for restricted medicines
- Export order history to PDF or Excel

---

## Tech Stack

| Category | Technology |
|----------|------------|
| Language | C# |
| Framework | .NET Framework |
| UI | Windows Forms |
| IDE | Visual Studio 2015 or later |
| Data storage | SQL Server, or XML / JSON files |
| Core entities | `Medicine`, `Customer`, `Order`, `Admin` |
| Search | Linear search, filtering, sorting (binary search optional) |
| Export | PDF, Excel |

**Design principles:** OOP with inheritance/interfaces where suitable; input validation; exception handling; separate admin and customer form sets with navigation dashboard.

---

## Screenshots / Demo

> Screenshots will be added once the UI is implemented.

| Screen | Description |
|--------|-------------|
| Admin Dashboard | Sales, stock, and active orders overview |
| Medicine Management | CRUD interface for the product catalogue |
| Customer Portal | Search, cart, and order tracking |
| Reports | Sales, stock, and order history exports |

<!-- Replace placeholders below when assets are available -->
<!--
![Admin Dashboard](./docs/screenshots/admin-dashboard.png)
![Customer Search](./docs/screenshots/customer-search.png)
-->

**Demo:** Run `SmartMed.exe` after building the solution (see [Running the Project](#running-the-project)).

---

## Installation

### Prerequisites

| Requirement | Version |
|-------------|---------|
| OS | Windows 10 or later |
| .NET Framework | 4.x |
| Visual Studio | 2015+ (Desktop development workload) |
| Database (optional) | SQL Server Express / LocalDB |

### Steps

```powershell
# Clone the repository
git clone https://github.com/<organisation>/SmartMed.git
cd SmartMed

# Open in Visual Studio
start SmartMed.sln
```

1. Restore NuGet packages — **Build → Restore NuGet Packages**
2. Configure environment — update `App.config` (see [Environment Variables](#environment-variables))
3. Build — **Build → Build Solution** (`Ctrl+Shift+B`)

---

## Environment Variables

SmartMed uses **App.config** (not `.env`) for runtime configuration. Set these before first run:

| Key | Description | Example |
|-----|-------------|---------|
| `ConnectionStrings:SmartMedDb` | SQL Server connection string | `Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SmartMed;Integrated Security=True` |
| `AppSettings:DataStorageMode` | Persistence mode | `Database` or `File` |
| `AppSettings:DataFilePath` | Path to XML/JSON data (file mode) | `Data\smartmed.json` |
| `AppSettings:ExportPath` | Default folder for PDF/Excel exports | `Exports\` |
| `AppSettings:PrescriptionUploadPath` | Folder for uploaded prescriptions | `Uploads\Prescriptions\` |

**Example (`App.config`):**

```xml
<configuration>
  <connectionStrings>
    <add name="SmartMedDb"
         connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SmartMed;Integrated Security=True"
         providerName="System.Data.SqlClient" />
  </connectionStrings>
  <appSettings>
    <add key="DataStorageMode" value="Database" />
    <add key="DataFilePath" value="Data\smartmed.json" />
    <add key="ExportPath" value="Exports\" />
    <add key="PrescriptionUploadPath" value="Uploads\Prescriptions\" />
  </appSettings>
</configuration>
```

Ensure export and upload directories exist or are created at application startup.

---

## Running the Project

### Visual Studio (development)

1. Set **SmartMed** as the startup project.
2. Press **F5** (Debug) or **Ctrl+F5** (Run without debugging).
3. Log in as **Admin** or **Customer** using seeded or registered credentials.

### Executable (release)

```powershell
cd SmartMed\bin\Release
.\SmartMed.exe
```

### Workflows

**Admin:** Login → Dashboard → Manage medicines / customers / orders → Generate reports

**Customer:** Register or login → Search medicines → Add to cart → Place order → Track status → Update profile

---

## Folder Structure

```
SmartMed/
├── SmartMed.sln                  # Visual Studio solution
├── SmartMed/                     # Main WinForms project
│   ├── Forms/                    # Admin & customer UI forms
│   ├── Models/                   # Medicine, Customer, Order, Admin
│   ├── Services/                 # Business logic
│   ├── Data/                     # Data access & repositories
│   ├── Utils/                    # Validation, search, export helpers
│   ├── App.config                # Connection strings & app settings
│   └── Program.cs                # Entry point
├── Data/                         # Local XML/JSON data (file mode)
├── Exports/                      # Generated PDF/Excel reports
├── Uploads/                      # Prescription uploads
├── Diagrams/                     # Architecture, ER, use-case diagrams
├── docs/
│   └── screenshots/              # UI screenshots for README
├── Document.md                   # Full specification & coursework brief
└── README.md
```

---

## Deployment

### Release build

1. Set configuration to **Release**.
2. **Build → Publish** or build solution and copy output from `bin\Release\`.
3. Include `App.config`, data files, and required DLLs in the deployment folder.

### Distribution options

| Method | Notes |
|--------|-------|
| **Folder publish** | Copy `bin\Release\` contents to target machines |
| **ClickOnce** | Optional; configure publish profile in Visual Studio |
| **Installer** | Optional MSI/setup project for enterprise rollout |

### Target environment

- Windows 10+ with .NET Framework 4.x installed
- SQL Server / LocalDB if using database mode, or writable path for file-based storage
- Read/write access to `Exports\` and `Uploads\` directories

---

## Future Enhancements

- Online payment integration
- Email/SMS order status notifications
- Barcode scanning for stock intake
- Multi-branch inventory sync
- Role-based admin permissions (pharmacist vs manager)
- REST API for mobile or web clients
- Cloud backup and audit logging

---

## Author

MNSBaanu

---

## License

Redistribution and commercial use are subject to your institution's academic integrity and intellectual property policies.
