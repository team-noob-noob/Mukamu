# Sinuka (Sinuka ka/Sinuka v2)

## Requirements
- Dotnet Core
- MySQL
- Dotnet-ef CLI
    - To install <code>dotnet tool install --global dotnet-ef</code>

## Setup
1. Create Database 'sinukaka'
3. Change Connection String if required
    - Either use envvars (SINUKA_DB_CONN_STR) or update Sinuka.Infrastructure.Configurations.DbConfig.DbConnectionString
2. Run migrations
    - <code>dotnet ef database update --project ./src/Sinuka.Infrastructure</code>
3. Run App
    - <code>dotnet run --project ./src/Sinuka.WebAPIs</code>
