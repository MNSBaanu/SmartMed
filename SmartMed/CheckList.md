# SmartMed — Feature & Submission Checklist

Use this list against `Document.md` (assignment brief) and the marking scheme.  
Legend: **Done** | **Partial** | **Gap**

Last reviewed against the codebase: June 2026.

---

## How to use

1. Tick `[x]` when a item is fully complete and tested.
2. Leave `[ ]` for gaps — see **Action** column.
3. Re-run the app and database script before submission.

---

## 1. Admin features

| Status | Requirement | Implementation | Notes / Action |
|:------:|-------------|----------------|----------------|
| [x] | Secure admin login | `LoginForm`, `AuthService.AdminLogin`, `AdminRepository` | Quick-login button for demo |
| [x] | Manage medicines — add | `ManageMedicinesForm`, `MedicineService.Add` | Name, category, dosage, price, stock, supplier, expiry, Rx, promo |
| [x] | Manage medicines — update | `MedicineService.Update` | |
| [x] | Manage medicines — delete | `MedicineService.Delete` | Blocked if active order items exist |
| [x] | Manage customers — view | `ManageCustomersForm`, grid + search | Linear search via `SearchService.SearchCustomers` |
| [x] | Manage customers — update | `CustomerService.Update` | |
| [x] | Manage orders — view all | `ManageOrdersForm`, `OrderService` | Status filter + search |
| [x] | Manage orders — update status | Pending → Ready for Pickup → Delivered | |
| [x] | Generate reports — sales | `ReportsForm` → Sales Performance tab | Completed (Delivered) orders only |
| [x] | Generate reports — stock / inventory | Medicine Inventory tab | Current / low stock / expired / near expiry |
| [x] | Generate reports — customer order history | Customer Order History tab | Customer dropdown filter |
| [x] | Admin dashboard | `AdminDashboardForm` | Sales, stock, active orders, alerts, recent orders |

---

## 2. Customer features

| Status | Requirement | Implementation | Notes / Action |
|:------:|-------------|----------------|----------------|
| [x] | Registration | `RegistrationForm`, `AuthService.RegisterCustomer` | Validation on email, phone, address |
| [x] | Customer login | `AuthService.CustomerLogin` | Email + password |
| [x] | Search medicines — name | `SearchService.SearchByName` | Partial match (`Contains`) |
| [x] | Search medicines — category | `SearchService.FilterByCategory` | |
| [x] | Search medicines — price range | `SearchService.FilterByPriceRange` | Min / max on Browse Medicines |
| [ ] | Search medicines — live filter | `SearchMedicinesForm` | **Gap:** search runs on **Search button** only; add `TextChanged` on filter fields (like admin Manage Medicines) |
| [x] | Place orders — cart | `CartService`, `PlaceOrderForm` | Add from Browse, qty, remove, clear |
| [x] | Place orders — checkout | `OrderService.PlaceOrder` | Stock validation, order items saved |
| [x] | Track orders — view status | `TrackOrdersForm` | Order list + line items |
| [x] | Profile management | `ProfileManagementForm` | Update name, phone, address |

---

## 3. Additional features (brief)

| Status | Requirement | Implementation | Notes / Action |
|:------:|-------------|----------------|----------------|
| [x] | Discounts / promotions | `Medicine.IsOnPromotion`, `DiscountPercent`, promo dates | Date-aware pricing in `MedicineService.GetEffectivePrice` |
| [x] | Expiry tracking / notifications | `MedicineService.CheckExpiry`, dashboard alerts, Manage Medicines expiry panel | |
| [x] | Prescription upload (Rx medicines) | `PlaceOrderForm`, `PrescriptionService` | Required when cart has Rx items |
| [x] | Export order history | `TrackOrdersForm` → Export CSV | |
| [ ] | Export order history — PDF | — | **Gap:** spec mentions PDF or Excel; reports have PDF, **My Orders export is CSV only** — add PDF export or document CSV as Excel-compatible in report |
| [x] | Admin report export CSV / PDF | `ReportsForm`, `ExportHelper` | View report first, then export |
| [x] | Admin medicine export / print | `ManageMedicinesForm` | CSV + print preview |
| [x] | Admin customer export | `ManageCustomersForm` | CSV |
| [ ] | Forgot password (real reset) | `LoginForm.LnkForgot_LinkClicked` | **Partial:** shows “contact administrator” message only |
| [x] | Change password (customer) | `ChangePasswordForm`, profile / dashboard | |
| [x] | Change password (admin) | `ChangePasswordForm`, admin dashboard | |

---

## 4. Technical requirements

### 4.1 Platform & data

| Status | Requirement | Implementation | Notes / Action |
|:------:|-------------|----------------|----------------|
| [x] | C# Windows Forms | `.NET Framework 4.8`, `SmartMed.csproj` | |
| [x] | Local database | SQL Server, `Database/SmartMedDB.sql` | Run script before first use |
| [x] | Connection string | `App.config` | Adjust for LocalDB / named instance if needed |
| [ ] | DB migration on existing DB | `Database/Migrations/` | **Gap:** confirm promo/discount columns applied if DB created before latest schema |

### 4.2 Software design (classes)

| Status | Requirement | Implementation | Notes / Action |
|:------:|-------------|----------------|----------------|
| [x] | Medicine entity | `Models/Medicine.cs` | |
| [x] | Customer entity | `Models/Customer.cs` | |
| [x] | Order entity | `Models/Order.cs`, `OrderItem.cs` | |
| [x] | Admin entity | `Models/Admin.cs` | |
| [x] | Inheritance | `Admin : User`, `Customer : User` | |
| [ ] | Interfaces | — | **Gap:** brief asks for interfaces where suitable — consider `IValidatable`, `ISearchable`, or repository interface for report |
| [x] | Repository layer | `Data/*Repository.cs` | |
| [x] | Service / business layer | `Services/*Service.cs` | |
| [x] | Separation admin vs customer UI | `AdminShellForm` / `CustomerShellForm`, separate hosts | Single-window navigation per role |

### 4.3 User interface

| Status | Requirement | Implementation | Notes / Action |
|:------:|-------------|----------------|----------------|
| [x] | Window-based forms | All feature forms | |
| [x] | Separate admin / customer areas | `AdminHostForm`, `CustomerHostForm` | |
| [x] | Navigation menu / dashboard | Sidebar nav + overview pages | |
| [x] | Consistent theme | `UiTheme.cs` | |
| [x] | Search above tables (admin) | Manage Medicines / Customers / Orders | Search bar inside grid panel |
| [x] | Browse Medicines layout | Search row above grid | |
| [x] | Cart layout | Actions below cart table | |

### 4.4 Validation & exception handling

| Status | Requirement | Implementation | Notes / Action |
|:------:|-------------|----------------|----------------|
| [x] | Input validation | `ValidationService`, service-layer checks | Email, phone, numeric fields, empty inputs |
| [x] | User-friendly errors | `MessageBox`, thrown messages caught in forms | |
| [x] | Prevent crashes on bad input | Try/catch on order place, cart, exports | |
| [ ] | Password storage security | Plain-text compare in repositories | **Gap (optional polish):** hashing not required by brief but note in report as future improvement |

### 4.5 Search algorithms (for report & marks)

| Status | Algorithm | Where used | Notes / Action |
|:------:|-----------|------------|----------------|
| [x] | **Linear search** | `SearchService.SearchByName`, `SearchCustomers`, `SearchOrders` | Document in reflective essay |
| [x] | **Filtering** | Category, price, order status, expiry/stock categories | Chained filters in `SearchService.Search` |
| [x] | **Sorting** | SQL `ORDER BY`, LINQ `OrderBy` (expiry alerts, categories, reports) | |
| [ ] | **Binary search** (optional) | — | **Gap:** not implemented — add exact-name lookup on sorted medicine list (e.g. duplicate check) and describe **O(log n)** in report |

---

## 5. Marking scheme — implementation tasks

| Task | Weight | Status | Form / service |
|------|:------:|:------:|----------------|
| UI design | 2 | [x] | Themed shells, grids, stats tiles |
| Customer & admin login | 2 | [x] | `LoginForm` |
| Customer registration | 2 | [x] | `RegistrationForm` |
| Admin — manage medicines | 3 | [x] | `ManageMedicinesForm` |
| Admin — manage customers | 2 | [x] | `ManageCustomersForm` |
| Admin — manage orders | 3 | [x] | `ManageOrdersForm` |
| Customer — search medicines | 3 | [ ] | **Partial** — live filter gap on Browse Medicines |
| Customer — place orders | 2 | [x] | `PlaceOrderForm`, `CartService` |
| Customer — track orders | 2 | [x] | `TrackOrdersForm` |
| Admin — generate reports | 2 | [x] | `ReportsForm` |
| Admin dashboard | 2 | [x] | `AdminDashboardForm` |

---

## 6. Documentation & submission (PDF essay)

| Status | Deliverable | Location | Notes / Action |
|:------:|-------------|----------|----------------|
| [ ] | Run instructions | Report §1 | **Gap:** verify step-by-step in `Docs/Report/SmartMed Report.docx` |
| [ ] | Architecture / class diagram | `Docs/Diagrams/`, report | Export PNGs included; align diagram with current `CustomerHostForm` / services |
| [ ] | Class properties & methods | Report | Describe Models + Services + key forms |
| [ ] | Search algorithms explanation | Report | Linear + filter + sort **done in code**; add binary search section if implemented |
| [ ] | Personal reflection (1000+ words) | Report | Challenges: WinForms dock order, embedded hosts, DB migrations |
| [ ] | Cover sheet details | `Document.md` template | Module, student ID, date, signature |
| [ ] | Export report to PDF | — | File → Save As → PDF for submission |
| [ ] | Visual Studio project zip | `SmartMed.sln`, source, exe, `SmartMedDB.sql` | Include build output or instructions to build |

---

## 7. Programming style (self-review)

| Status | Criterion | Notes / Action |
|:------:|-----------|----------------|
| [x] | Clear algorithm structure | `SearchService` loops are readable |
| [x] | Sensible naming | Models, services, forms follow conventions |
| [ ] | Useful comments | **Partial** — add brief comments on search methods & binary search if added |
| [x] | Validation & exception handling | Services + forms |
| [x] | Usability | Single-window nav, search placement, logout vs exit |

---

## 8. Pre-submission test plan

Run through once and tick:

- [ ] Fresh DB: run `Database/SmartMedDB.sql`
- [ ] Admin login → dashboard loads figures
- [ ] Add / edit / delete medicine (promo dates, expiry alert)
- [ ] Search & filter medicines (admin grid)
- [ ] Update customer; search customer
- [ ] Change order status through full workflow
- [ ] View & export each report tab (CSV + PDF)
- [ ] Register new customer → login
- [ ] Browse medicines → add to cart → upload Rx if needed → place order
- [ ] Track order → export CSV
- [ ] Update profile & change password
- [ ] Logout returns to login; close exits app
- [ ] Build Release / Debug with no errors

---

## 9. Priority gaps to fill

| Priority | Gap | Suggested fix |
|:--------:|-----|----------------|
| **High** | Browse Medicines filters only on button click | Wire `TextChanged` / `SelectedIndexChanged` to `Search()` in `SearchMedicinesForm.cs` |
| **High** | Reflective essay & PDF not finalized | Complete `Docs/Report/SmartMed Report.docx`, export PDF |
| **Medium** | Binary search not in code | Add `SearchService.BinarySearchByName` for exact duplicate check; document in report |
| **Medium** | Order history PDF export | Reuse `ExportHelper.ExportDataTableToPdf` in `TrackOrdersForm` |
| **Low** | Interfaces for OOP marks | Add small interface(s) on repositories or entities |
| **Low** | Forgot password | Keep admin-contact message or add email reset stub |
| **Low** | DB migration script for old DBs | Add/run migration if `PromotionStartDate` columns missing |

---

## 10. Key file map

| Area | Files |
|------|--------|
| Entry | `SmartMed/Program.cs`, `UI/SmartMedApplicationContext.cs` |
| Auth | `Forms/LoginForm.cs`, `RegistrationForm.cs`, `Services/AuthService.cs` |
| Admin pages | `ManageMedicinesForm.cs`, `ManageCustomersForm.cs`, `ManageOrdersForm.cs`, `ReportsForm.cs`, `AdminDashboardForm.cs` |
| Customer pages | `SearchMedicinesForm.cs`, `PlaceOrderForm.cs`, `TrackOrdersForm.cs`, `ProfileManagementForm.cs` |
| Search | `Services/SearchService.cs` |
| Data | `Database/SmartMedDB.sql`, `Data/*Repository.cs` |

---

*Reference: assignment specification in `Document.md` (CS6004ES SmartMed coursework).*
