# 🏢 BuildingCharge – Unit-Based Charge Management System

BuildingCharge is a backend system designed to manage residential units, assign shared charges (like water, gas, maintenance), and generate financial reports per unit. It supports proportional charge distribution, debt tracking, and payment management.

---

## 🚀 Project Workflow

1. **Register Units**  
   Admin creates all residential units in the building.

2. **Create Charges**  
   Monthly or one-time charges are created (e.g., water bill, gas bill).

3. **Assign Shares to Units**  
   Each unit receives a share of the charge, either equally or based on a coefficient.

4. **Generate Reports**  
   The system calculates how much each unit owes, considering its share, previous debts, and credits.

5. **Track Payments**  
   Payments made by units are recorded and reflected in their financial status.

---

## 📘 Key Concepts

### 🔹 Unit
A residential or commercial space in the building. Each unit has:
- A unique ID
- A name or number
- Associated owners or tenants

### 🔹 Charge
A cost that needs to be distributed among units. Examples:
- Water bill
- Gas bill
- Building maintenance

Each charge includes:
- Type (e.g., "Water", "Gas")
- Total amount
- List of shares assigned to units

### 🔹 Share
A **Share** represents how much of a charge is assigned to a specific unit.  
It can be:
- **Coefficient-based** (e.g., water usage)
- **Equal split** among all units

Each share includes:
- `UnitId`: the unit receiving the share  
- `ChargeId`: the charge being distributed  
- `Coefficient`: optional value used for proportional distribution

---

## 📡 API Reference

| Purpose                         | Endpoint                            | Method | Description                                 |
|----------------------------------|-------------------------------------|--------|---------------------------------------------|
| Create a unit                    | `/api/units`                        | POST   | Add a new unit                              |
| Get all units                    | `/api/units`                        | GET    | Retrieve all units                          |
| Create a charge with shares      | `/api/charges`                      | POST   | Add a charge and assign shares              |
| Get all charges with shares      | `/api/charges/with-shares`          | GET    | Retrieve charges including unit shares      |
| Get unit-based charge report     | `/api/reports/unit-based-report`    | GET    | Generate financial report per unit          |
| Get financial status of a unit   | `/api/units/{unitId}/financial-status` | GET | View debt, credit, and unpaid charges       |

---

## 🧾 Example: Charge Distribution

If a water bill of 1,000,000 is created and assigned to 4 units:

- If **equal split**: each unit pays 250,000  
- If **coefficient-based**: each unit pays based on its usage (e.g., Unit A with coefficient 2 pays more than Unit B with coefficient 1)

---

## 🧩 Swagger Integration

To enable XML comments in Swagger UI:

### 1. In `BuildingCharge.WebAPI.csproj`, add:

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
</PropertyGroup>


📂 Folder structure
BuildingCharge/
├── Core/
│   ├── Domain/               # Entities, value objects, base classes
│   └── Application/          # Interfaces, DTOs, services
├── Infrastructure/
│   ├── Persistence/          # AppDbContext, configurations, migrations
│   └── Repositories/         # RepositoryBase, UnitRepository, ChargeRepository
└── WebAPI/
    ├── Controllers/          # UnitController, ChargeController, ReportController
    ├── Program.cs
    └── appsettings.json







👤 Author
Ahmad Backend Developer – Tehran, Iran 
Focused on clean architecture, scalable APIs, and practical solutions.
 Contributions and feedback are welcome.