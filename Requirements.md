# System Requirements

The following requirements define what the SmartMed pharmacy management system must fulfil.

## Functional Requirements

### Admin

- Access the system using secure administrator credentials to perform and manage all pharmacy-related operations.
- Maintain medicine information by adding new medicines, updating existing records, removing discontinued medicines, and managing details such as name, category, dosage, price, stock quantity, supplier information, expiry dates, and prescription requirements.
- Monitor and manage customer information by viewing customer records and updating customer details when necessary.
- Oversee customer orders by viewing all orders placed through the system and updating order statuses from Pending to Ready for Pickup or Delivered.
- Monitor pharmacy performance through a dashboard that provides an overview of total sales, available medicine stock, active orders, and other key operational statistics.
- Generate and review reports related to sales performance, medicine inventory, and customer order history to support decision-making and operational monitoring.
- Create, update, and remove discounts or promotional offers that can be applied to medicines.
- Track medicine expiry dates and receive notifications for medicines approaching expiry in order to ensure inventory safety and compliance.

### Customer

- Create a personal account and securely access the system using registered credentials.
- Search and browse available medicines by name, category, or price range and view detailed medicine information before purchasing.
- Select medicines, add them to a shopping cart, review selected items, and place orders through the system.
- Monitor previously placed orders and track their current status throughout the order processing lifecycle.
- Manage personal profile information by viewing and updating personal and contact details.
- Upload prescription documents when purchasing medicines that require prescription verification.
- View available discounts and promotional offers that can be applied to eligible medicines.
- Export personal order history in PDF or Excel format for future reference and record keeping.

### Additional Features

1. **Low Stock Alert System** — The system will automatically monitor medicine inventory levels and notify administrators when the stock quantity of a medicine falls below a predefined threshold. This helps prevent stock shortages and ensures the availability of essential medicines.
2. **Medicine Expiry Warning Dashboard** — The system will identify medicines approaching their expiry dates and display warning notifications on the administrator dashboard. This assists in inventory management and reduces the risk of dispensing expired medicines.
3. **Prescription Verification System** — The system will require customers to upload a valid prescription when ordering medicines marked as prescription-required. Orders containing prescription medicines cannot be processed unless the required prescription document has been uploaded and verified.
4. **Password Change Functionality** — The system will allow both administrators and customers to securely change their account passwords after successful authentication, improving account security and user management.
5. **Order Cancellation** — The system will allow customers to cancel orders that are still in the Pending status. Once an order has been processed or marked as Ready for Pickup, cancellation will no longer be permitted.

## Catalog Scope

SmartMed Pharmacy offers prescription and over-the-counter medications and wellness products:

| Offering | Implementation |
|----------|----------------|
| Prescription / OTC medications | `Medicine` catalog; `RequiresPrescription` flag |
| Wellness products | `Medicine` with `Category = Wellness` |

## Non-Functional Requirements

- **Usability** — The system shall provide an intuitive and user-friendly interface with role-based navigation, allowing administrators and customers to easily access their respective functionalities through clearly organized forms and menus.
- **Reliability** — The system shall validate user inputs before processing and use parameterized SQL queries to ensure accurate data handling and prevent database-related errors.
- **Maintainability** — The system shall follow a three-tier architecture with separate Presentation, Business Logic, and Data Access layers to simplify future maintenance, testing, and enhancements.
- **Performance** — The system shall provide responsive operation for medicine searches, order processing, and report generation, with search and filtering functionality optimized for typical pharmacy inventory sizes.
- **Security** — The system shall authenticate users through secure login mechanisms, restrict access based on user roles, and maintain session information for authenticated users.
- **Availability** — The system shall allow authorized users to access pharmacy services whenever the application and database are operational.
- **Scalability** — The system shall support future enhancements such as additional user roles, advanced reporting features, and expanded inventory management without major architectural changes.
- **Data Integrity** — The system shall maintain consistent and accurate data through validation rules, database constraints, and controlled update operations.
