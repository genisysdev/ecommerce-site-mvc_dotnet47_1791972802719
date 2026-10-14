# LegacyEcommerceDb — setup

Run these against `.\SQLEXPRESS` in SSMS (or `sqlcmd`), in order:

1. Enable Mixed Mode auth (Server → Properties → Security → SQL Server and Windows
   Authentication mode), then restart the `MSSQL$SQLEXPRESS` service.
2. `01_create_database.sql`
3. `02_create_tables.sql`
4. `03_seed_data.sql`

Verify: `SELECT * FROM Product;` returns 6 rows.

App connection (Web.config): Server=.\SQLEXPRESS; Database=LegacyEcommerceDb;
User Id=legacy_app_user; Password=Legacy!Pass123; MultipleActiveResultSets=True;
