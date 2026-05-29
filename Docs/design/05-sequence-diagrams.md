# Sequence Diagrams — SmartMed

**Module:** CS6004ES  
**Source of truth:** [Document.md](../../Document.md)  
**Status:** Design phase — one diagram per major **marking task**

Forms and services match [01-architecture.md](01-architecture.md). Validation and exception handling shown where required by brief.

---

## Task 1 — Admin login (UC-A1)

```mermaid
sequenceDiagram
    actor Admin
    participant F as frmAdminLogin
    participant V as ValidationHelper
    participant S as AuthenticationService
    participant R as IAdminRepository

    Admin->>F: Enter username, password
    F->>V: ValidateNotEmpty(fields)
    alt Invalid input
        V-->>F: false
        F-->>Admin: Show validation error
    else Valid input
        F->>S: LoginAdmin(username, password)
        S->>R: GetByUsername(username)
        R-->>S: Admin or null
        alt Credentials OK
            S-->>F: Admin
            F->>F: Open frmAdminDashboard
            F-->>Admin: Dashboard displayed
        else Credentials fail
            S-->>F: null
            F-->>Admin: Invalid login message
        end
    end
```

---

## Task 1 — Customer login (UC-C2)

```mermaid
sequenceDiagram
    actor Customer
    participant F as frmCustomerLogin
    participant S as AuthenticationService
    participant R as ICustomerRepository

    Customer->>F: Enter email, password
    F->>F: Validate inputs
    F->>S: LoginCustomer(email, password)
    S->>R: GetByEmail(email)
    R-->>S: Customer
    S->>S: ValidatePassword
    alt OK
        S-->>F: Customer
        F->>F: Show frmCustomerHome
    else Fail
        F-->>Customer: Error message
    end
```

---

## Task 2 — Customer registration (UC-C1)

```mermaid
sequenceDiagram
    actor Customer
    participant F as frmRegister
    participant V as ValidationHelper
    participant S as AuthenticationService
    participant R as ICustomerRepository

    Customer->>F: Enter details + password
    F->>V: ValidateEmail, required fields
    alt Validation fails
        F-->>Customer: Field errors
    else Valid
        F->>S: Register(...)
        S->>R: GetByEmail(email)
        alt Email exists
            S-->>F: Error duplicate
            F-->>Customer: Email already registered
        else New customer
            S->>R: Save(customer)
            F-->>Customer: Success → frmCustomerLogin
        end
    end
```

---

## Task 3 — Admin manage medicine (UC-A2)

```mermaid
sequenceDiagram
    actor Admin
    participant F as frmMedicineManagement
    participant S as MedicineService
    participant R as IMedicineRepository

  Note over Admin,R: Add / Update / Delete — name, category, dosage, price, stock, supplier

    Admin->>F: Save medicine
    F->>F: Validate numeric price, stock
    F->>S: Add(medicine) or Update(medicine)
    S->>R: Save(medicine)
    R-->>S: OK
    S-->>F: Success
    F->>S: GetAll()
    S->>R: GetAll()
    R-->>F: List
    F-->>Admin: Refresh grid

    Admin->>F: Delete selected
    F->>S: Delete(medicineId)
    S->>R: Delete(medicineId)
    F-->>Admin: Grid updated
```

---

## Task 4 — Admin manage customer (UC-A3)

```mermaid
sequenceDiagram
    actor Admin
    participant F as frmCustomerManagement
    participant S as CustomerService
    participant R as ICustomerRepository

    Admin->>F: Open form
    F->>S: GetAll()
    S->>R: GetAll()
    R-->>F: Customer list
    Admin->>F: Select + edit + Save
    F->>S: Update(customer)
    S->>R: Save(customer)
    F-->>Admin: Confirmation
```

---

## Task 5 — Admin manage orders (UC-A4)

```mermaid
sequenceDiagram
    actor Admin
    participant F as frmOrderManagement
    participant S as OrderService
    participant R as IOrderRepository

    Admin->>F: Open form
    F->>S: GetAll()
    S->>R: GetAll()
    R-->>F: Orders

    Admin->>F: Change status to Ready for Pickup / Delivered
    F->>S: UpdateStatus(orderId, newStatus)
    S->>R: GetById(orderId)
    S->>S: order.UpdateStatus (Pending → ReadyForPickup → Delivered)
    S->>R: Save(order)
    F-->>Admin: Updated grid
```

---

## Task 6 — Customer search medicines (UC-C3)

Uses **linear search**, **filtering**, and **sorting** per brief.

```mermaid
sequenceDiagram
    actor Customer
    participant F as frmSearchMedicine
    participant MS as MedicineService
    participant SS as MedicineSearchService
    participant R as IMedicineRepository

    Customer->>F: Enter name / category / price range
    F->>MS: GetAll()
    MS->>R: GetAll()
    R-->>MS: List Medicine
    MS-->>F: source list
  opt Category filter
        F->>SS: FilterByCategory(source, category)
        SS-->>F: filtered
    end
  opt Price range
        F->>SS: FilterByPriceRange(source, min, max)
        SS-->>F: filtered
    end
  opt Name search
        F->>SS: SearchByName(filtered, name)
        Note right of SS: Linear search O(n)
        SS-->>F: results
    end
  opt Sort by price
        F->>SS: SortByPrice(results, ascending)
        SS-->>F: sorted list
    end
    F-->>Customer: DataGridView results
```

---

## Task 7 — Customer place order (UC-C4)

```mermaid
sequenceDiagram
    actor Customer
    participant Search as frmSearchMedicine
    participant Cart as frmCart
    participant S as OrderService
    participant MR as IMedicineRepository
    participant OR as IOrderRepository

    Customer->>Search: Add to cart
    Customer->>Cart: Place Order
    Cart->>Cart: Validate cart not empty
    loop Each cart item
        Cart->>S: CheckStock(medicineId, qty)
        S->>MR: GetById()
        MR-->>S: Medicine
        alt Insufficient stock
            S-->>Cart: false
            Cart-->>Customer: Error — insufficient stock
        end
    end
    Cart->>S: PlaceOrder(customer, cart)
    S->>S: Create Order, Status=Pending, OrderLines
    S->>S: Reduce medicine stock
    S->>OR: Save(order)
    Cart-->>Customer: Order confirmed
```

---

## Task 8 — Customer track orders (UC-C5)

```mermaid
sequenceDiagram
    actor Customer
    participant F as frmTrackOrders
    participant S as OrderService
    participant R as IOrderRepository

    Customer->>F: Open track orders
    F->>S: GetByCustomer(currentCustomerId)
    S->>R: GetByCustomerId()
    R-->>S: Orders with status
    S-->>F: List Order
    F-->>Customer: Grid: OrderId, Date, Status, Total
```

---

## Task 9 — Admin generate reports (UC-A5)

```mermaid
sequenceDiagram
    actor Admin
    participant F as frmReports
    participant S as ReportService
    participant OR as IOrderRepository
    participant MR as IMedicineRepository

    Admin->>F: Select report type

    alt Sales report
        F->>S: GetSalesReport(from, to)
        S->>OR: Query by date
        OR-->>S: Orders
        S->>S: Aggregate sales
    else Stock report
        F->>S: GetStockReport()
        S->>MR: GetAll()
        MR-->>S: Medicines
    else Customer order history
        F->>S: GetCustomerOrderHistory(customerId)
        S->>OR: GetByCustomerId()
    end

    S-->>F: Report data
    F-->>Admin: Display (optional PDF/Excel export)
```

---

## Task 10 — Admin dashboard (UC-A6)

```mermaid
sequenceDiagram
    actor Admin
    participant F as frmAdminDashboard
    participant S as ReportService
    participant OR as IOrderRepository
    participant MR as IMedicineRepository

    Admin->>F: Open dashboard (after login)
    F->>S: GetTotalSales()
    S->>OR: Load orders
    OR-->>S: data
    S-->>F: totalSales
    F->>S: GetMedicinesInStockCount()
    S->>MR: GetAll()
    S-->>F: stockCount
    F->>S: GetActiveOrdersCount()
    Note right of S: Pending + ReadyForPickup
    S-->>F: activeOrders
    F-->>Admin: Show KPIs + menu navigation
```

---

## Cross-cutting — Exception handling

```mermaid
sequenceDiagram
    participant F as Any Form
    participant S as Service
    participant H as ExceptionHelper

    F->>S: Operation()
    alt Business rule error
        S-->>F: BusinessRuleException
        F-->>F: MessageBox warning
    else Data / IO error
        S-->>F: Exception
        F->>H: Handle(ex)
        H-->>F: User-friendly message
        F-->>F: MessageBox error (no crash)
    end
```

---

## Search algorithms summary

| Diagram | Algorithm |
|---------|-----------|
| Task 6 | Linear search (name), filter (category, price), sort (price) |
| Optional | Binary search on name-sorted list — see [03-class-diagram.md](03-class-diagram.md) §5 |

Document implementation and Big-O analysis in the **reflective essay** (marking: documentation item 4).
