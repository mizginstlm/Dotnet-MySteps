<!-- # Game Store API

## Starting SQL server
```powershell

docker run -e 'ACCEPT_EULA=Y' microsoft/mssql-server-linux -e 'MSSQL_SA_PASSWORD=password123' -p 1433:1433 -v sqlvolume:/var/opt/mssql  -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest 
```

## Setting the connection string to secret manager
```powershell
dotnet user-secrets set 'ConnectionStrings:DefaultConnection' 'Server=localhost; Database=dotnet-steps ;User Id=sa; Password=password123; Trusted_Connection=false; TrustServerCertificate=true;'
``` -->