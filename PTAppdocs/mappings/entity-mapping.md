# Entity Mapping (Progress -> EF Core)

Describe how Progress tables and data types map to EF Core models and C# types.

Template:

- Table: <ProgressTableName>
  - Primary Key: <field>
  - Columns:
	- <FieldName> (<ProgressType>) -> <C# Type>, Notes
  - Foreign Keys:
	- <FKField> -> <ReferencedTable>(<ReferencedField>)
  - Remarks: <business rules, special encodings, enums>

Repeat for each table.
