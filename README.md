# Payroll Engine Core

> Part of the [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine) open-source payroll automation framework.
> Full documentation at [payrollengine.org](https://payrollengine.org).

The Payroll Engine core library, used by every other component:
- Payroll exceptions (`PayrollException`, `PayrunException`, `PersistenceException`, `QueryException`)
- Logger abstraction `ILogger` with static `Log` facade and `PayrollNullLogger` fallback
- Document abstraction for reports (`IDataMerge`)
- Value conversion (`ValueConvert`, `ValueType`, `ValueBaseType`)
- Common types and extension methods
- JSON and CSV serialization (`DefaultJsonSerializer`, `CsvSerializer`, `ClientJsonSerializer`)
- Payroll `DataSet` convertible to the ADO.NET [`DataSet`](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-datasets)
- Program configuration from system environment and configuration files
- Password hashing with SHA-256 and PBKDF2 (`HashSaltExtensions`, `UserPassword`)
- OData query support (`Query`, `QueryFactory`, `QuerySpecification`)
- Date and period calculations (`Date`, `DatePeriod`, `IPayrollPeriod`)
- Action scripting framework (`ActionSpecification`, `MarkerType`)
- Object comparison and copy tools (`CompareTool`, `CopyTool`)
- System and scripting specification constants (`SystemSpecification`, `ScriptingSpecification`)

---

## Project Structure

```
PayrollEngine.Core/
├── Core/                         # Library source
│   ├── Action/                   # Scripting action framework
│   ├── Data/                     # Payroll DataSet, DataTable, DataRow, DataColumn
│   ├── Document/                 # Document merge abstraction
│   ├── IO/                       # File utilities
│   ├── Serialization/            # JSON and CSV serializers
│   └── *.cs                      # Core types, extensions and tools
├── Directory.Build.props         # Shared build properties
├── PayrollEngine.Core.sln        # Solution file
└── README.md
```

---

## Key Namespaces

| Namespace | Description |
|---|---|
| `PayrollEngine` | Core types, enums, extensions, logging, configuration |
| `PayrollEngine.Data` | Payroll data model (`DataSet`, `DataTable`, `DataRow`, `DataColumn`) with ADO.NET conversion |
| `PayrollEngine.Serialization` | JSON and CSV serialization (`DefaultJsonSerializer`, `CsvSerializer`) |
| `PayrollEngine.Document` | Document merge abstraction and metadata |
| `PayrollEngine.Action` | Scripting action specifications and markers |
| `PayrollEngine.IO` | File extensions and tools |

---

## Value Types

The `ValueType` enum defines all supported case field value types:

| Value | Base Type | Description |
|---|---|---|
| `String` | string | Text value |
| `Boolean` | boolean | True/false |
| `Integer` | numeric | Integer number |
| `NumericBoolean` | numeric | Non-zero = true |
| `Decimal` | numeric | Decimal number |
| `DateTime` | string | Date and time (ISO 8601) |
| `Date` | string | Date only |
| `None` | null | No value |
| `Weekday` | int 0–6 | Day of week |
| `Month` | int 0–11 | Month of year |
| `Year` | int | Calendar year |
| `Money` | decimal | Monetary amount |
| `Percent` | decimal | Percentage |
| `WebResource` | string | URL or web resource |

`ValueConvert` serializes and deserializes any `ValueType` to and from its JSON representation, with culture-aware datetime parsing.

---

## Function Types

The `FunctionType` flags enum identifies all payroll scripting function types. Composite values allow targeting groups of functions:

| Value | Description |
|---|---|
| `CaseAvailable` | Case availability check |
| `CaseBuild` | Case build |
| `CaseValidate` | Case input validation |
| `CaseRelationBuild` / `CaseRelationValidate` | Case relation functions |
| `CollectorStart` / `CollectorApply` / `CollectorEnd` | Collector lifecycle |
| `WageTypeValue` / `WageTypeResult` | Wage type calculation and result |
| `PayrunStart` / `PayrunEnd` | Payrun lifecycle |
| `PayrunEmployeeAvailable` / `PayrunEmployeeStart` / `PayrunEmployeeEnd` | Employee payrun lifecycle |
| `PayrunWageTypeAvailable` | Wage type availability during payrun |
| `ReportBuild` / `ReportStart` / `ReportEnd` | Report lifecycle |
| `Case`, `CaseRelation`, `Collector`, `WageType`, `PayrunBase` | Composite groups |
| `Payroll`, `Report`, `All` | Top-level composite groups |

---

## Date & Period Calculations

The static `Date` class provides UTC-based date helpers:

```csharp
// Boundaries
Date.MinValue        // DateTime.MinValue as UTC
Date.MaxValue        // DateTime.MaxValue as UTC
Date.Now             // DateTime.UtcNow
Date.Today           // UTC date only

// Period boundaries
Date.FirstMomentOfMonth(2026, 3)   // 2026-03-01 00:00:00
Date.LastMomentOfMonth(2026, 3)    // 2026-03-31 23:59:59.9999999
Date.IsLastMomentOfDay(moment)     // tick-precise boundary check

// Date expression parser (also used by Input Attributes)
Date.Parse("today", culture)            // today
Date.Parse("previousmonth", culture)    // first day of last month
Date.Parse("offset:3m", culture)        // 3 months from today
Date.Parse("offset:-5d", culture)       // 5 days ago
```

`DatePeriod` represents a closed or open interval. An open end defaults to `Date.MaxValue`; an open start defaults to `Date.MinValue`:

```csharp
var period = new DatePeriod(start, end);
bool open = period.IsOpen;       // true if start or end is unbounded
bool anytime = period.IsAnytime; // true if both are unbounded
double days = period.TotalDays;
```

---

## Logging

The static `Log` class provides a global logging facade. It is safe to use before configuration — the default `PayrollNullLogger` silently discards all messages until a logger is set:

```csharp
// configure logging
Log.SetLogger(myLogger);

// use logging
Log.Trace("Verbose detail");
Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
Log.Information("Processed {Count} items", items.Count);
Log.Warning("Skipped {Count} records.", skipped);
Log.Error(exception, "Processing failed");
Log.Critical("Process terminating.");

// check if a logger has been configured
if (Log.HasLogger) { ... }
```

---

## Password Security

Password hashing uses PBKDF2 with SHA-256 (100,000 iterations) and constant-time comparison to prevent timing attacks:

```csharp
// create hash + salt
var hashSalt = "myPassword".ToHashSalt();

// verify password
bool isValid = hashSalt.VerifyPassword("myPassword");
```

---

## OData Query Support

`Query` carries OData-style query parameters used across all GET endpoints:

```csharp
var query = new Query
{
    Filter = "Status eq 'Active'",
    OrderBy = "Name asc",
    Top = 25,
    Skip = 0,
    Result = QueryResultType.Items
};
```

`QueryFactory` builds typed query instances; `QuerySpecification` validates and normalizes query parameters before execution.

---

## Payroll Data Model

The `PayrollEngine.Data` namespace provides a serializable data model that converts to/from ADO.NET:

```csharp
// create payroll data set
var dataSet = new DataSet { Name = "MyData" };

// convert to ADO.NET
System.Data.DataSet systemDataSet = dataSet.ToSystemDataSet();

// convert back
DataSet payrollDataSet = systemDataSet.ToPayrollDataSet();
```

---

## System Specification

`SystemSpecification` defines the global constants shared by all components:

| Constant | Value | Description |
|---|---|---|
| `PayrollApiConfiguration` | `PayrollApiConfiguration` | Env var: path to API config JSON file |
| `PayrollApiConnection` | `PayrollApiConnection` | Env var: API connection string |
| `PayrollApiKey` | `PayrollApiKey` | Env var: static API key |
| `PayrollDatabaseConnection` | `PayrollDatabaseConnection` | Config key: database connection string |
| `TextAttributePrefix` | `TA_` | Prefix for text attribute columns |
| `DateAttributePrefix` | `DA_` | Prefix for date attribute columns |
| `NumericAttributePrefix` | `NA_` | Prefix for numeric attribute columns |
| `DecimalPrecision` | 28 | SQL decimal precision |
| `DecimalScale` | 6 | SQL decimal scale |
| `PayrunMaxExecutionCount` | 100 | Maximum payrun retro execution depth |

---

## Scripting Specification

`ScriptingSpecification` defines the constants that govern the C# scripting subsystem:

| Constant | Value | Description |
|---|---|---|
| `ScriptingVersion` | `1.0.0` | Current scripting API version |
| `CSharpLanguageVersion` | `CSharp14` | Roslyn language version used for compilation |
| `SealedTag` | `#sealed` | Script tag preventing derivation |
| `ActionRegion` | `Action` | C# region name for action code |
| `FunctionRegion` | `Function` | C# region name for function code |

---

## Configuration

### HTTP Client Configuration
The Payroll HTTP configuration includes the following data to connect to the backend:
- `BaseUrl` — the base API URL (required)
- `Port` — the base API port
- `Timeout` — the connection timeout
- `ApiKey` — the API access key

### Database Connection String
The backend database connection string is determined by the following priority:

1. Environment variable `PayrollDatabaseConnection`
2. Program configuration file `appsettings.json`

### Program Configuration Options
The `ProgramConfiguration<TApp>` class supports:
- `AppSettings` — load from `appsettings.json` (optional, no exception if missing)
- `UserSecrets` — .NET user secrets

---

## NuGet Package

Available on [NuGet.org](https://www.nuget.org/profiles/PayrollEngine):

```sh
dotnet add package PayrollEngine.Core
```

---

## Build

Environment variable used during build:

| Variable | Description |
|:--|:--|
| `PayrollEnginePackageDir` | Output directory for the NuGet package (optional) |

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## See Also

- [Client Core](https://github.com/Payroll-Engine/PayrollEngine.Client.Core) — client-side model and service layer built on top of this library
- [Client Scripting](https://github.com/Payroll-Engine/PayrollEngine.Client.Scripting) — scripting function API
- [Backend](https://github.com/Payroll-Engine/PayrollEngine.Backend) — uses this library for domain types and query infrastructure
