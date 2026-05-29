# SmartMed

**CS6004ES Individual Coursework** — Windows Forms pharmacy management system for SmartMed Pharmacy (30% module mark).

**Phase:** Design only (per module guidance). No Visual Studio implementation until reviewed in class.

**Authoritative specification:** [Document.md](Document.md)

---

## Business context

SmartMed Pharmacy manages prescription and OTC medicines, wellness products, and health services. A **desktop Windows Forms** application supports **Admin** and **Customer** roles for inventory, orders, and customer service.

---

## Functional requirements (from brief)

### Admin

| Feature | Requirement |
|---------|-------------|
| Login | Secure admin login |
| Manage medicine | Add, update, delete — **name, category, dosage, price, stock, supplier** |
| Manage customer | View and update customer information |
| Manage orders | View all orders; status: **Pending**, **Ready for Pickup**, **Delivered** |
| Reports | Sales, stock, customer order history |
| Dashboard | Total sales, medicines in stock, active orders |

### Customer

| Feature | Requirement |
|---------|-------------|
| Register / login | New registration and login |
| Search medicines | By **name**, **category**, or **price range** |
| Place orders | Cart → place order |
| Track orders | View order status |
| Profile | Update personal and contact details |

### Additional (optional extensions)

- Discounts / promotions on medicines  
- Medicine expiry notifications  
- Prescription upload (certain medicines)  
- Export order history (PDF or Excel)

---

## Technical requirements (mandatory)

| Area | Standard |
|------|----------|
| Language | C# — **.NET Framework**, **Windows Forms** |
| IDE / submission | **Visual Studio 2015+** project (source, compiled classes, `.exe`, data files) |
| Storage | Local **SQL Server** *or* **XML/JSON** files |
| Core classes | **`Medicine`**, **`Customer`**, **`Order`**, **`Admin`** — properties, methods, inheritance/interfaces where suitable |
| UI | Window-based forms; **separate Admin and Customer** areas; **dashboard / navigation menu** |
| Quality | Input validation; exception handling (no crashes) |
| Search | Linear search, filtering, sorting; optional binary search on sorted lists |

---

## Implementation tasks (marking scheme)

| # | Task | Weight |
|---|------|:------:|
| — | Application UI design | 2 |
| 1 | Customer & Admin login | 2 |
| 2 | Customer registration | 2 |
| 3 | Admin — manage medicine details | 3 |
| 4 | Admin — manage customer details | 2 |
| 5 | Admin — manage orders | 3 |
| 6 | Customer — search medicines | 3 |
| 7 | Customer — place orders | 2 |
| 8 | Customer — track orders | 2 |
| 9 | Admin — generate reports | 2 |
| 10 | Admin dashboard | 2 |

Documentation (reflective essay, 1000+ words) and programming style are assessed separately — see [Document.md](Document.md#marking-scheme-for-the-cs6004es-individual-coursework).

---

## Design documentation

All diagrams trace requirements in `Document.md`. View Mermaid in GitHub or VS Code (Mermaid extension).

| File | Purpose | Brief / marking alignment |
|------|---------|---------------------------|
| [01-architecture.md](Docs/design/01-architecture.md) | Layers, solution structure, UI/navigation | Technical requirements; Task UI design |
| [02-er-diagram.md](Docs/design/02-er-diagram.md) | Data model | Core entities + optional extensions |
| [03-class-diagram.md](Docs/design/03-class-diagram.md) | Classes, properties, methods, search design | Core OOP; essay sections (b)(c)(d) |
| [04-use-case.md](Docs/design/04-use-case.md) | Actors and use cases | All functional + marking tasks |
| [05-sequence-diagrams.md](Docs/design/05-sequence-diagrams.md) | Interaction flows | Tasks 1–10 flows |

---

## Planned solution structure (Visual Studio)

```
SmartMed/
├── SmartMed.sln
├── SmartMed/                    # WinForms app (.NET Framework)
│   ├── Program.cs
│   ├── Forms/                   # Separate Admin & Customer forms
│   ├── Models/                  # Medicine, Customer, Order, Admin
│   ├── Services/                # Business logic
│   ├── Data/                    # Repositories (SQL or JSON)
│   └── Utils/                   # Validation, exceptions
├── Data/                        # JSON/XML or DB scripts (if used)
├── Document.md
├── README.md
└── Docs/design/
```

---

## Deliverables checklist

| Deliverable | Design | Build | Essay |
|-------------|:------:|:-----:|:-----:|
| Visual Studio project + `.exe` + data | — | Pending | — |
| PDF documentation | — | — | Pending |
| Run instructions | — | — | Pending |
| Architecture & class diagrams | Done | — | Draft from `Docs/design/` |
| Class properties & methods | Done | — | Draft from `03-class-diagram.md` |
| Search algorithm explanation | Done | — | Draft from `03-class-diagram.md` |
| Reflection (C# / VS experience) | — | — | Pending |

---

## Repository layout

```
SmartMed/
├── Document.md
├── README.md
└── Docs/
    └── design/
        ├── 01-architecture.md
        ├── 02-er-diagram.md
        ├── 03-class-diagram.md
        ├── 04-use-case.md
        └── 05-sequence-diagrams.md
```
