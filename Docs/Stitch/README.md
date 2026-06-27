# Stitch — SmartMed UI Reference

Project: **SmartMed Pharmacy Management System** (`13162339874056363525`)

## Downloaded screens

| Slug | Screen ID | WinForms form |
|------|-----------|---------------|
| `admin-dashboard` | `8a1ef21c68f54f98b110ba2bdbd42659` | `AdminDashboardForm` |
| `manage-inventory` | `fd5b56f4515b4199bb8070662bdc5b21` | `ManageMedicinesForm` |
| `manage-customers` | `c46c137330e64465a9df2292674547b1` | `ManageCustomersForm` |
| `manage-orders` | `7e936a0aaed44267b68855ebfced5101` | `ManageOrdersForm` |
| `reports-analytics` | `8158597c7b814767ab9b09f4e029de3e` | `ReportsForm` |
| `home` | `f7007caf4e6a48d1962ce2e95ddee7b8` | `CustomerDashboardForm` |
| `browse-medicine` | `7cf550b09037449a809c015b1f1c96cd` | `SearchMedicinesForm` |
| `my-cart` | `6a4f5607492049b28347c238c87bd5a3` | `PlaceOrderForm` |
| `my-orders` | `97fc709c2569442aa203bb02a1340d9c` | `TrackOrdersForm` |
| `my-profile` | `d707d5003a8745ecbc5edf8be7d86af6` | `ProfileManagementForm` |
| `login` | `df3064256ad24a729d3b0f3a100fc51f` | `LoginForm` (SmartMedNew) |
| `registration` | `719984e1cd6e4c5cb8b3f16793014c49` | `RegistrationForm` (SmartMedNew) |

Assets live in `13162339874056363525/` (`.html`, `.png`, `screens-index.json`).

## Re-fetch all screens

```powershell
node Docs/Stitch/fetch-all-screens.mjs
```

Requires `STITCH_API_KEY` in the environment.

## WinForms implementation

- Shared theme: `SmartMed/UI/UiTheme.cs` (`AdminTeal`, `AdminSurface`, `ApplyAdminClinicalShell`, `ApplyClinicalGrid`)
- Shared page helpers: `SmartMed/UI/ClinicalUi.cs`
- Admin shell: `AdminShellForm` + `AdminHostForm` (light sidebar, profile header)
- Customer shell: `CustomerShellForm` + `CustomerHostForm` (same clinical chrome)
