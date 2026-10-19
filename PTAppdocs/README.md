# PTAppdocs

This directory holds documentation, schema exports, Progress 4GL source, and mapping notes for migrating the legacy PTApp (Progress 4GL) application into the RP123 solution as a Razor Pages app.

Structure
- schema/       : SQL DDL/seed data or CSV exports from Progress DB
- progress-src/ : Progress 4GL source files (procedures, .p, .r, etc.)
- mappings/     : Entity and field mapping notes (Progress -> EF Core)
- ui-flows/     : Screens, flows, and UI mockups or notes
- extras/       : Helpful scripts, sample connection strings (no secrets)

How to use
1. Place any DDL exports and CSVs in schema/.
2. Add Progress 4GL source files to progress-src/.
3. Fill mappings/entity-mapping.md with table -> model mappings.
4. Once files are present, notify the developer assistant to scaffold EF models and Razor Pages from the provided schema.
