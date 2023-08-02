# Payroll Engine Core
👉 This repository is part of the [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine/wiki).

The Payroll Engine core library, used by any other component:
- Payroll exceptions
- Logger abstraction `ILogger`
- Document abstraction for reports `IDataMerge`
- Value conversion
- Common types and extension methods
- JSON and CSV serialization
- Payroll `DataSet` convertible to the ADO.NET `DataSet`
- Program Configuration

## Build
Supported runtime environment variables:
- *PayrollEnginePackageDir* - the NuGet package destination directory (optional)