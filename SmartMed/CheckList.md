# SmartMed — Feature Checklist

Legend: `[x]` Done · `[ ]` Gap / to do  
Last reviewed: June 2026 · Reference: `Document.md`

---

## Admin

**Login**
- [x] Secure admin login (`LoginForm`, `AuthService`)

**Dashboard**
- [x] Sales, stock, active orders, alerts, recent orders (`AdminDashboardForm`)

**Manage Medicines**
- [x] Add medicine (name, category, dosage, price, stock, supplier, expiry, Rx, promo)
- [x] Update medicine
- [x] Delete medicine (blocked if linked to active orders)
- [x] Search & filter grid (live)
- [x] Expiry alerts panel
- [x] Export CSV / print

**Manage Customers**
- [x] View customers (grid + search)
- [x] Update customer details
- [x] Export CSV

**Manage Orders**
- [x] View all orders (filter + search)
- [x] Update status: Pending → Ready for Pickup → Delivered
- [x] View order line items

**Generate Reports**
- [x] Sales performance (Delivered orders)
- [x] Medicine inventory (stock / expiry status)
- [x] Customer order history
- [x] Export CSV / PDF (`ReportsForm`, `ExportHelper`)

**Account**
- [x] Change password (`ChangePasswordForm`)

---

## Customer

**Register & Login**
- [x] Registration with validation (`RegistrationForm`)
- [x] Login with email + password
- [ ] Forgot password — message only; no real reset

**Browse Medicines**
- [x] Search by name, category, price range
- [x] Discount & promo columns; effective price
- [ ] Live filter on type (search runs on button click only)

**Cart & Checkout**
- [x] Add to cart, quantity, remove, clear (`CartService`, `PlaceOrderForm`)
- [x] Show list price, unit price, discount, promo, applied offer
- [x] Place order — stock check, order saved, stock reduced
- [x] Prescription upload for Rx medicines (`Prescription` linked to `OrderID`)
- [ ] **Payments** — order total (`TotalAmount`) is recorded only; no payment method, payment status, or gateway (assumed **pay on pickup** at pharmacy)

**My Orders**
- [x] View orders and line items
- [x] Prescription file column (from `Prescription` table)
- [x] Cancel pending order
- [x] Export order history CSV
- [ ] Export order history PDF (spec mentions PDF or Excel)

**Profile**
- [x] Update name, phone, address
- [x] Change password

---

## Additional features

- [x] Discounts & promotions (date-aware `GetEffectivePrice`)
- [x] Expiry tracking & dashboard alerts
- [x] Prescription upload for Rx items
- [x] Admin & customer CSV exports
- [x] Consistent UI theme & customer page margins

---

## Technical (brief)

- [x] C# WinForms (.NET 4.8), SQL Server (`SmartMedDB.sql`)
- [x] Models, repositories, services, admin/customer shells
- [x] Linear search, filtering, sorting (`SearchService`)
- [ ] Binary search (optional — for report)
- [ ] Repository/entity interfaces (optional OOP polish)
- [ ] Run DB migrations on existing databases (`Database/Migrations/`)

---

## Submission

- [ ] Complete reflective report (`Docs/Report/SmartMed Report.docx`) → export PDF
- [ ] Cover sheet (module, student ID, date)
- [ ] Zip: solution, source, `SmartMedDB.sql`, run instructions
- [ ] Pre-submission test run (admin + customer flows end-to-end)

---

## Priority gaps

1. **Payments** — document pay-on-pickup in report, or add `PaymentStatus` / `PaymentMethod` on order (optional enhancement)
2. Browse Medicines live search
3. Finalize report PDF & test plan
4. Customer order export PDF (optional)
5. Binary search + interfaces (optional marks polish)
