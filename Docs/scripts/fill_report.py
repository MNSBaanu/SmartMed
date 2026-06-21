"""Restructure SmartMed Report.docx to match Docs/Report/breakdown.docx."""
from docx import Document
from docx.shared import Pt
from docx.enum.text import WD_LINE_SPACING
import os

REPORT_PATH = os.path.normpath(
    os.path.join(os.path.dirname(__file__), "..", "Report", "SmartMed Report.docx")
)

# (heading_level, title, body) — level 1 = Heading 1, 2 = Heading 2, 0 = body only
SECTIONS = [
    (1, "Introduction", """SmartMed Pharmacy Management System is a desktop Windows Forms application developed in C# on .NET Framework 4.8. The system supports pharmacy operations for two roles: administrators who manage inventory, customers, and orders, and customers who register, log in, search medicines, and place orders.

The main objectives are to digitise medicine stock control, customer records, and order fulfilment; provide searchable medicine catalogues using linear search and filtering; and present sales and stock information through admin dashboards and reports. Data is stored in Microsoft SQL Server using a layered design: Forms (presentation), Services (business logic), Data (repositories), and Models (entity classes)."""),

    (1, "System Requirements", None),

    (2, "Functional Requirements", """Admin Features
• Login – Secure login for admins.
• Manage Medicine Details – Add, update, delete medicine details (name, category, dosage, price, stock, supplier).
• Manage Customer Details – View and update customer information.
• Manage Orders – View all orders, update order status (Pending, Ready for Pickup, Delivered).
• Generate Reports – Sales reports, stock reports, and customer order history.
• Dashboard – Overview of total sales, medicines in stock, and active orders.

Customer Features
• Register/Login – New user registration and login.
• Search Medicines – Search by name, category, or price range.
• Place Orders – Add medicines to cart and place orders.
• Track Orders – View status of orders.
• Profile Management – Update personal details and contact information.

Additional Features
• Apply discounts or promotions on medicines.
• Include medicine expiry tracking notifications.
• Include prescription upload functionality for certain medicines.
• Export order history to PDF or Excel."""),

    (2, "Non-Functional Requirements", """• Usability — role-based navigation via AdminShellForm sidebar; standard WinForms controls for data entry and grids.
• Reliability — parameterized SQL queries prevent injection; validation runs before database access.
• Maintainability — separation of UI, services, and data access into distinct folders and namespaces.
• Performance — linear search is acceptable for typical pharmacy catalogue sizes.
• Security — passwords validated at login; session state held in Session service; admin and customer roles separated."""),

    (2, "Hardware Requirements", """• PC or laptop running Windows 10 or later.
• Minimum 4 GB RAM recommended for Visual Studio and SQL Server Express/LocalDB.
• Display resolution 1280×720 or higher for admin dashboard layout."""),

    (2, "Software Requirements", """• Microsoft Windows 10 or later.
• .NET Framework 4.8.
• Microsoft SQL Server (Express, LocalDB, or full edition) with Windows Authentication.
• Visual Studio 2019 or later with .NET desktop development workload (for building from source)."""),

    (2, "Development Tools and Technologies", """• Language: C#
• UI: Windows Forms (System.Windows.Forms)
• Data access: ADO.NET (System.Data.SqlClient), parameterized commands via DatabaseHelper
• Database: Microsoft SQL Server — script Database/SmartMedDB.sql
• IDE: Visual Studio 2022; builds also supported via dotnet CLI
• Diagrams: draw.io files in Docs/Diagrams/ (Architecture, Use Case, ER, Class, Sequence)"""),

    (1, "Design Diagrams", None),

    (2, "Architecture Diagram", """The application follows a layered architecture documented in Docs/Diagrams/Architecture.drawio:

Presentation layer (SmartMed.UI — Forms/): LoginForm, RegistrationForm, AdminShellForm, AdminDashboardForm, ManageMedicinesForm, ManageCustomersForm, ManageOrdersForm, ReportsForm.

Service layer (SmartMed.Services): AuthService, ValidationService, SearchService, ReportService, Session.

Data layer (SmartMed.Data): DatabaseHelper, AdminRepository, CustomerRepository, MedicineRepository, OrderRepository.

Database layer: SQL Server database SmartMedDB.

Forms call Services or Data repositories; repositories use DatabaseHelper for all SQL. Session stores the logged-in Admin or Customer after authentication."""),

    (2, "Use Case Diagram", """Docs/Diagrams/Usecase.drawio defines actors Admin and Customer.

Admin use cases: login, dashboard overview, manage medicines, manage customers, manage orders, generate reports.

Customer use cases: register, login, search medicines, place order, track order, manage profile.

Include relationships show that placing an order includes searching medicines. Implemented WinForms map directly to these use cases."""),

    (2, "ER Diagram", """Docs/Diagrams/ER.drawio models six entities: Admin, Customer, Medicine, Order, OrderItem, and Prescription.

Customer places Orders (one-to-many). Order contains OrderItems (one-to-many). Medicine appears in OrderItems. Customer may upload Prescriptions (one-to-many). Primary keys are identity integers. Database/SmartMedDB.sql implements this schema with foreign keys and a CHECK constraint on Order.Status."""),

    (2, "Class Diagram", """Docs/Diagrams/Class.drawio shows Person as the base class with Name, Email, Phone, Password. Customer and Admin inherit Person. Customer adds CustomerID and Address; Admin adds AdminID and Username.

Entity classes Medicine, Order, OrderItem, and Prescription model pharmacy domain objects. Services coordinate validation and repository calls; repositories map rows to Models."""),

    (2, "Sequence Diagram", """Docs/Diagrams/Sequence.drawio illustrates key interactions such as admin login and medicine CRUD: the user interacts with a Form, which calls AuthService or a Repository, which uses DatabaseHelper to execute SQL against SmartMedDB, then results flow back to update the UI (DataGridView or labels)."""),

    (2, "Database Design", """Database SmartMedDB contains tables Admin, Customer, Medicine, Order, OrderItem, and Prescription. Seed data includes a default admin (admin / admin123) and sample customers and medicines. Order.Status is limited to Pending, Ready for Pickup, or Delivered. OrderItem stores quantity, unit price, and subtotal at order time."""),

    (1, "Description of Classes, Properties and Methods", """The Models folder contains entity classes aligned with the class diagram.

Person — base class: Name, Email, Phone, Password.

Customer : Person — CustomerID, Address; methods Register(), Login(), SearchMedicine(), PlaceOrder(), TrackOrder(), UpdateProfile() document intended behaviour; AuthService and forms implement the workflows.

Admin : Person — AdminID, Username; Login() via AuthService.AdminLogin.

Medicine — MedicineID, MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription; CRUD methods AddMedicine(), UpdateMedicine(), DeleteMedicine(), CheckExpiry().

Order — OrderID, CustomerID, OrderDate, Status, TotalAmount.

OrderItem — links Order and Medicine with Quantity, UnitPrice, Subtotal.

Prescription — PrescriptionID, CustomerID, PrescriptionFile, UploadDate, Status (table created; upload UI planned).

Services: AuthService (AdminLogin, CustomerLogin, RegisterCustomer), ValidationService (input rules), SearchService (linear search and filters), ReportService (dashboard and report statistics), Session (CurrentAdmin, CurrentCustomer, Clear).

Data repositories expose GetAll, GetById, Insert, Update, Delete, and query methods using parameterized SQL.

[Insert screenshot: Person, Customer, and Admin classes in Visual Studio Class View or Solution Explorer with properties visible.]

[Insert screenshot: Medicine and Order classes showing properties and methods.]

[Insert screenshot: AuthService and MedicineRepository class structure.]"""),

    (1, "Implementation", None),

    (2, "Brief Description of Functions", """Program.Main — starts the application and opens LoginForm.

AuthService.AdminLogin / CustomerLogin — validate credentials through AdminRepository or CustomerRepository after ValidationService checks.

AuthService.RegisterCustomer — validate customer fields and insert via CustomerRepository.

SearchService.SearchByName — linear O(n) scan comparing MedicineName to keyword (case-insensitive Contains).

SearchService.FilterByCategory / FilterByPriceRange — additional linear passes for filtering.

MedicineRepository.GetAll / Insert / Update / Delete — CRUD for medicine inventory.

OrderRepository — list orders, update status, and transactional order placement with stock updates.

ReportService — aggregate totals for dashboard and ReportsForm.

Admin forms — LoadMedicines, LoadCustomers, grid selection handlers, and Save/Delete button handlers bind UI to repositories."""),

    (2, "Code Snippets", """Application entry point (Program.cs):
Application.EnableVisualStyles();
Application.Run(new LoginForm());

Linear search by medicine name (SearchService.cs):
foreach (var medicine in medicines)
{
    if (medicine.MedicineName.ToLower().Contains(key))
        results.Add(medicine);
}

Admin login validation (AuthService.cs):
if (ValidationService.IsNullOrWhiteSpace(username) || ValidationService.IsNullOrWhiteSpace(password))
    throw new ArgumentException("Username and password are required.");
return _adminRepo.GetByCredentials(username.Trim(), password);

Parameterized insert (MedicineRepository.cs):
DatabaseHelper.ExecuteNonQuery(
    "INSERT INTO Medicine (MedicineName, Category, ...) VALUES (@n, @c, ...)",
    new SqlParameter("@n", item.MedicineName), ...);"""),

    (2, "Output Screens", """[Insert screenshot: Login form — role selection, username/email, password.]

[Insert screenshot: Admin Dashboard — statistics cards, recent activity, alerts.]

[Insert screenshot: Manage Medicines — grid, add/update/delete form, stat tiles.]

[Insert screenshot: Manage Customers — customer grid and edit panel.]

[Insert screenshot: Manage Orders — order list and status update.]

[Insert screenshot: Generate Reports — sales/stock/history tabs.]

Design mock PNG files are available under Docs/DesignMocks/ for reference when capturing screenshots."""),

    (1, "User Manual", None),

    (2, "Installation Guide", """Prerequisites: Windows 10+, .NET Framework 4.8, SQL Server, Visual Studio 2019+ (optional for running the built executable).

Database setup:
1. Open SQL Server Management Studio.
2. Execute Database/SmartMedDB.sql from the project folder.
3. Confirm database SmartMedDB is created with seed data.

Connection string:
1. Open SmartMed/App.config.
2. Set the SmartMedDB connection string to match your SQL Server instance (e.g. local instance or LocalDB).

Build and run:
1. Open SmartMed.sln in Visual Studio.
2. Set SmartMed as the startup project.
3. Build the solution (Ctrl+Shift+B) or run: dotnet build SmartMed.sln
4. Press F5. The Login form appears.
5. Admin login: username admin, password admin123.
6. Customer: register via Register button or use sample credentials from the SQL seed script."""),

    (2, "Using the Application", """Login — select Admin or Customer role, enter credentials, click Login.

Admin navigation — use the sidebar: Dashboard Overview, Manage Medicines, Manage Customers, Manage Orders, Generate Reports, Logout.

Manage Medicines — select a row in the grid to edit fields; use Add, Update, Delete buttons; view stock statistics at the top.

Manage Customers — select a customer to edit name, email, phone, address; save or delete as needed.

Manage Orders — select an order, choose a new status from the dropdown, click Update Status.

Reports — switch tabs for Sales, Stock, and Customer Order History; summary tiles show key figures.

Registration — customers complete the registration form with password confirmation; return to login after success.

Logout — use Logout in the sidebar or close (X) on the top bar to return to the login screen."""),

    (1, "Reflection", """Developing SmartMed improved my understanding of C# object-oriented programming and event-driven Windows Forms applications. Moving from console programs to a multi-form desktop app required thinking about navigation (AdminShellForm), session state (Session service), and keeping SQL out of the UI layer.

I valued the folder structure (Forms, Models, Data, Services) because changes to MedicineRepository automatically benefited every form that loads medicines. Debugging login across LoginForm, AuthService, and AdminRepository with breakpoints showed how requests flow through layers.

Challenges included configuring App.config so DatabaseHelper reads the connection string at runtime, and keeping WinForms layout manageable when building page content in code. Using DataGridView for CRUD screens and ValidationService for shared rules reduced duplication.

SearchService linear search was straightforward to implement and document, matching the coursework requirement. Future work includes completing the customer portal (cart, order tracking, profile), prescription upload, and PDF export for reports.

Working in Visual Studio 2022 and building with dotnet CLI gave flexibility. Overall the project strengthened skills in ADO.NET, layered design, and technical documentation aligned with UML diagrams in Docs/Diagrams/."""),

    (1, "Conclusion", """SmartMed Pharmacy Management System delivers a working admin desktop application for pharmacy inventory, customers, orders, dashboards, and reports. The implementation uses C# WinForms, SQL Server, and a clear layered architecture documented with architecture, use case, ER, class, and sequence diagrams.

Mandatory admin requirements are met: login, medicine CRUD, customer management, order status updates, reports, and dashboard metrics. Customer registration and login are implemented; extended customer features are planned. Linear search and filtering satisfy the search-algorithm requirement.

The report follows the coursework breakdown: requirements, design diagrams, class descriptions, implementation details, user manual, reflection, and conclusion. The solution is runnable against SmartMedDB seed data and suitable for demonstration and assessment."""),
]


def set_body_format(paragraph):
    for run in paragraph.runs:
        run.font.name = "Times New Roman"
        run.font.size = Pt(12)
    paragraph.paragraph_format.line_spacing_rule = WD_LINE_SPACING.ONE_POINT_FIVE


def add_paragraph(doc, text, style="BodyText"):
    p = doc.add_paragraph(text, style=style)
    set_body_format(p)
    return p


def find_intro_index(doc):
    for i, p in enumerate(doc.paragraphs):
        if p.text.strip() == "Introduction" and p.style.name.startswith("Heading"):
            return i
    return None


def remove_from_index(doc, index):
    while len(doc.paragraphs) > index:
        el = doc.paragraphs[index]._element
        el.getparent().remove(el)


def restructure_report():
    doc = Document(REPORT_PATH)
    intro_idx = find_intro_index(doc)
    if intro_idx is None:
        raise RuntimeError("Could not find Introduction heading in report.")

    remove_from_index(doc, intro_idx)

    for level, title, body in SECTIONS:
        if level == 0:
            if body:
                add_paragraph(doc, body)
            continue
        style = f"Heading {level}"
        doc.add_paragraph(title, style=style)
        if body:
            for part in body.split("\n\n"):
                add_paragraph(doc, part)

    doc.save(REPORT_PATH)
    words = sum(len(body.split()) for _, _, body in SECTIONS if body)
    print(f"Saved {REPORT_PATH}")
    print(f"Approximate word count (body): {words}")


if __name__ == "__main__":
    restructure_report()
