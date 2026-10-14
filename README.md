# Legacy eCommerce Platform (.NET Framework 4.7 · MVC 5 · Razor · jQuery)

A classic, monolithic **eCommerce web application** built on ASP.NET MVC 5 with server-side
Razor rendering and jQuery for AJAX interactivity. Data is stored in SQL Server Express and
the shopping cart / checkout wizard rely on server-side **Session state** — a deliberately
"legacy" design intended as a faithful reference of the .NET 4.7 era.

---

## Features

- **Product Catalog** — product listing, category tree, detail pages, and jQuery AJAX
  filtering (partial-view swap, no full reload)
- **Shopping Cart** — session-backed cart (`Session["CartItems"]`), AJAX add-to-cart, header
  mini-cart, cart page with quantity edit / remove
- **Checkout** — multi-step wizard (Address → Shipping → Payment → Confirmation) with
  server + client validation; on completion it persists an order to the database
- **User Account** — registration, login/logout via ASP.NET Identity 2.0 + OWIN cookie auth,
  and a protected order-history page (DataTables), with orders linked to the account by email

---

## Tech Stack

| Area | Technology |
|------|-----------|
| Framework | .NET Framework 4.7 |
| Web | ASP.NET MVC 5, Razor view engine |
| Client | jQuery 3.4.1, jQuery Validate + Unobtrusive, DataTables, Bootstrap 3 |
| Data | Entity Framework 6 (Database-First / EDMX) |
| Database | SQL Server Express (`LegacyEcommerceDb`) |
| Auth | ASP.NET Identity 2.0 + OWIN cookie authentication |
| DI | Unity |
| State | InProc Session |

---

## Architecture

A four-project solution with a strict one-way dependency chain:

```
Ecommerce.Web  ──►  Ecommerce.Services  ──►  Ecommerce.Data  ──►  Ecommerce.Core
   (MVC UI)           (business logic)         (EF6 / EDMX)        (models + interfaces)
```

- **Ecommerce.Core** — domain models, view models, and interfaces (no dependencies)
- **Ecommerce.Data** — the EDMX model + repositories that map EF entities to Core models
- **Ecommerce.Services** — business logic (web-agnostic; no `HttpContext`)
- **Ecommerce.Web** — controllers, Razor views, DI wiring, Identity/OWIN, scripts, styles

---

## Prerequisites

- Windows with **Visual Studio 2022** (ASP.NET & web workload) or MSBuild
- **.NET Framework 4.7** targeting pack
- **SQL Server Express** instance named `.\SQLEXPRESS` (Mixed Mode auth enabled)

---

## Getting Started

### 1. Create the database

Open the scripts in the `database/` folder in **SSMS** and run them **in order** against
`.\SQLEXPRESS`:

1. `01_create_database.sql` — creates `LegacyEcommerceDb` + the SQL login `legacy_app_user`
2. `02_create_tables.sql` — catalog / order tables
3. `03_seed_data.sql` — sample categories and products
4. `04_create_identity_tables.sql` — ASP.NET Identity (`AspNet*`) tables

> Requires SQL Server Mixed Mode authentication (Server → Properties → Security), then
> restart the `MSSQL$SQLEXPRESS` service.

### 2. Open and run

1. Open `src/LegacyEcommerce/LegacyEcommerce.sln` in Visual Studio 2022
2. Ensure **`Ecommerce.Web`** is the startup project
3. Press **Ctrl+F5** (NuGet packages restore automatically on build)
4. Browse to `http://localhost:51000/`

### 3. Test account

Register a new account, or use a seeded one if present:

```
Email:    alice@example.com
Password: Passw0rd
```

---

## Project Structure

```
src/LegacyEcommerce/
├── Ecommerce.Core/         Models, ViewModels, Interfaces
├── Ecommerce.Data/         EDMX model + Repositories
├── Ecommerce.Services/     Business logic services
├── Ecommerce.Web/          Controllers, Views, Scripts, Content, config
└── LegacyEcommerce.sln

database/                   SQL setup scripts (run in order 01 → 04)
```

---

## Configuration

Connection strings live in `src/LegacyEcommerce/Ecommerce.Web/Web.config`:

- `LegacyEcommerceDbEntities` — the EDMX (Entity Framework) connection
- `DefaultConnection` — used by ASP.NET Identity

Both point at `.\SQLEXPRESS` / `LegacyEcommerceDb` using the `legacy_app_user` login.

> **Note:** the local dev password is stored in `Web.config` for convenience. Change it (and
> the DB login) before using anywhere outside local development.

---

## Notes

- The shopping cart lives in **InProc Session**, so it is cleared when the app pool recycles —
  this is intentional and matches the legacy design.
- Payment is **mock only**: card fields are validated (Luhn check via `[CreditCard]`) but
  nothing is charged or stored. Test card: `4111111111111111`, expiry `12/29`, CVV `123`.
- Persisted orders store the header (customer + total); line items are not persisted.
