# Payroll Engine Core
👉 This library is part of the [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine/wiki).

The Payroll Engine core library, used by any other component:
- Payroll exceptions
- Logger abstraction `ILogger`
- Document abstraction for reports `IDataMerge`
- Value conversion
- Common types and extension methods
- JSON and CSV serialization
- Payroll `DataSet` convertible to the ADO.NET [`DataSet`](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-datasets)
- Program Configuration from system environment and configuration files

## Configuration

### Http Client Configuration
The Payroll HTTP configuration includes the following data to connect to the backend:
- `BaseUrl` - the base API URL (required)
- `Port` - the base API port
- `Timeout` - the connection timeout
- `ApiKey` - the API access key

### Database connection string
The backed database connection string is determined by the following priority:

1. Environment variable `PayrollDatabaseConnection`.
2. Program configuration file `appsettings.json`.

## Build
Supported runtime environment variables:
- *PayrollEnginePackageDir* - the NuGet package destination directory (optional)