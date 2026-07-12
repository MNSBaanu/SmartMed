### ![](./image1.png){width="2.046527777777778in" height="1.1861111111111111in"}**Course Submission Cover Sheet** 

### Module: xxx

### Assignment no: 001 

### Weighting: 100% of the module mark

### Deadline: 3PM Wednesday the xxth of January 2025

Module Leader: xx Student ID:

Please note that there are specific regulations concerning **the use of
AI and Academic Misconduct**. Below are extracts from these regulations.
By signing, you acknowledge that you have read and understood these
extracts.

(signature:) Date:

**\
**

**Introduction**

This individual coursework requires developing and documenting a small
application in C# using an object oriented approach and Visual Studio.
Your software artefact must be submitted as a Visual Studio project. It
will be assessed using Visual Studio 2015 or any higher version and any
features not working in the standard installation of Visual Studio 2015
or any higher version will not be assessed.

The coursework carries 30% of the module mark.

**Set:**

**Submission Deadlines: ...**

This individual coursework has 2 parts, both of the soft copies which
are to be submitted before 11.59pm on

**.....**

(1) **The software application to be developed in .NET platform with
    C#**

(2) **The documentation in PDF format.**

*[Please note the rules on plagiarism]{.underline}*

The application should be implemented individually. This is not a
group/team effort. Any material which is a direct copy from someone else
(student or other source) or a close paraphrase/code must be indicated
where it is quoted i.e., it must be made clear what material is a
quotation or close paraphrase e.g. by showing the text in italics or in
quotation marks. It is not sufficient to show the source in a list of
references or bibliography. If you are unclear, please discuss your
examples with your seminar tutor or the module leader. Plagiarism is a
serious offence and conviction for plagiarism may lead to suspension
from the University, even for a first offence (please see the section on
Academic Misconduct in the Student Handbook).

**Software Development Task**

**SmartMed** Pharmacy is a chain of pharmacies that provides
prescription and over-the-counter medications and wellness products.
They aim to manage their inventory efficiently, process
customer orders, and maintain high-quality customer service through a
desktop-based application.

They require a Windows Forms application to manage pharmacy operations
for Admins and Customers.

**[Functional Requirements]{.underline}**

**Admin Features**

-   Login -- Secure login for admins.

-   Manage Medicine Details -- Add, update, delete medicine details
    (name, category, dosage, price, stock, supplier) and optional
    customer-facing product info (description, usage, warnings, pack size).

-   Manage Customer Details -- View and update customer information.

-   Manage Orders -- View all orders, update order status (Pending,
    Ready for Pickup, Delivered).

-   Generate Reports -- Sales reports, stock reports, and customer order
    history.

-   Dashboard -- Overview of total sales, medicines in stock, and active
    orders.

**Customer Features**

-   Register/Login -- New user registration and login.

-   Search Medicines -- Search by name, category, or price range; view
    product details before purchase.

-   Place Orders -- Add medicines to cart and place orders.

-   Track Orders -- View status of orders.

-   Profile Management -- Update personal details and contact
    information.

**Additional Features**

-   Apply discounts or promotions on medicines.

-   Include medicine expiry tracking notifications.

-   Include prescription upload functionality for certain medicines.

-   Export order history to PDF or Excel.

**[Technical Requirements]{.underline}**

-   Programming Language: C# (.NET Framework, Windows Forms Application)

-   Data Storage: Local database (SQL Server or XML/JSON files for
    simplicity).

-   Software Design:

```{=html}
<!-- -->
```
-   Use classes for core entities: Medicine, Customer, Order, Admin.

-   Implement appropriate properties and methods.

Include inheritance or interfaces where suitable.

-   User Interface:

```{=html}
<!-- -->
```
-   Window-based forms for all functionalities.

-   Separate forms for Admin and Customer tasks.

-   Navigation menu/dashboard for easy access.

```{=html}
<!-- -->
```
-   Validation & Exception Handling:

```{=html}
<!-- -->
```
-   Validate user inputs (e.g., numeric fields, empty inputs).

-   Handle exceptions to prevent application crashes.

```{=html}
<!-- -->
```
-   Search Functionality:

```{=html}
<!-- -->
```
-   Implement search algorithms (e.g., linear search, filtering, or
    sorting) for medicine.

-   Optional: implement more efficient search methods if desired (e.g.,
    binary search for sorted lists).

***Note**:* Additionally, you may add extra features (data,
functionality, and technical) to the application, if you wish.

**[Deliverables]{.underline}**

**Software Project:**

-   Complete Visual Studio project with source code, compiled classes,
    executable, and data files.

**Reflective Essay (1000+ words):**

-   Detailed instructions to run the program.

-   Software architecture: class diagrams, relationships, and sources of
    code.

-   Description of classes' properties and methods.

-   Explanation of search algorithms used.

-   Personal reflection on C# and Visual Studio experience, challenges
    faced, and solutions applied.

**Deliverables**

Your submission should include the software project and a reflective
essay as described below.

1.  Your software artefact in the form of a Visual Studio 2015 project,
    which should include the program's source code, compiled classes,
    the executable file and data file (if any).

2.  A reflective essay (1000 or more words), which concisely documents:

    a.  Detailed instructions to run the program

    b.  The architecture of your software in terms of software classes,
        clearly indicating which classes to be of your own work and
        which classes from other sources (e.g. From textbooks, online
        sources such as MSDN etc.).

    c.  Detailed description of the classes' properties and methods

    d.  Your reflection of own experience of using c# and visual studio
        for the development task, which feature you like and why, what
        issues you experienced and your solution to overcome it.

## Marking Scheme for the CS6004ES Individual Coursework

This individual coursework counts for **30%** of the module mark.

**How marks are calculated:** Mark each item below on a scale of **0 to 5** (see scale table). Multiply each mark by its **weight**, add all weighted marks, then **divide by 2** to get the total mark (maximum **200**).

### Marking scale (0–5)

| Mark | Characterised by |
|:----:|------------------|
| 0 | No work or work totally irrelevant |
| 1 | Work started on right lines but no result |
| 2 | Some result, with major lack and/or errors |
| 3 | Acceptable result but incomplete, or some good result with minor errors |
| 4 | Good result but can be further polished |
| 5 | Excellent result |

### Implementation

| # | Item | Weight | Max mark (Weight × 5) |
|---|------|:------:|:---------------------:|
| 1 | The application UI Design | 2 | 10 |
| 2 | Task 1: Customer, Admin Login | 2 | 10 |
| 3 | Task 2: Customer Registration | 2 | 10 |
| 4 | Task 3: Admin — Manage Medicine Details | 3 | 15 |
| 5 | Task 4: Admin — Manage Customer Details | 2 | 10 |
| 6 | Task 5: Admin — Manage Orders | 3 | 15 |
| 7 | Task 6: Customer — Search Medicines | 3 | 15 |
| 8 | Task 7: Customer — Place Orders | 2 | 10 |
| 9 | Task 8: Customer — Track Orders | 2 | 10 |
| 10 | Task 9: Admin — Generate Reports | 2 | 10 |
| 11 | Task 10: Admin Dashboard | 2 | 10 |

### Documentation

| # | Item | Weight | Max mark (Weight × 5) |
|---|------|:------:|:---------------------:|
| 1 | Detailed instructions to run the program | 2 | 10 |
| 2 | The software architecture | 2 | 10 |
| 3 | Detailed description of the classes' properties and methods | 2 | 10 |
| 4 | Explanation about search algorithms used in the project | 2 | 10 |
| 5 | Own reflection of own experience | 2 | 10 |

### Programming style

| # | Item | Weight | Max mark (Weight × 5) |
|---|------|:------:|:---------------------:|
| 1 | Clarity of code which shows the underlying algorithm | 1 | 5 |
| 2 | Sensible naming of programmer-defined variables, classes, properties and methods | 1 | 5 |
| 3 | Useful comments in code | 1 | 5 |
| 4 | Data validation and exception handling | 1 | 5 |
| 5 | Interface design and usability of the system | 1 | 5 |

### Total

| | Max mark |
|---|:--------:|
| **All sections combined** | **200** |
