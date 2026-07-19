@echo off
setlocal

if "%~1"=="" (
    echo [ERROR] Please provide service name.
    echo.
    echo Usage:
    echo   create-migration ServiceName MigrationName
    echo.
    echo Examples:
    echo   create-migration Identity InitialCreate
    echo   create-migration Catalog InitialCreate
    echo   create-migration Order InitialCreate
    echo   create-migration Payment InitialCreate
    echo   create-migration FlashSale InitialCreate
    echo   create-migration Notification InitialCreate
    exit /b 1
)

if "%~2"=="" (
    echo [ERROR] Please provide migration name.
    echo.
    echo Usage:
    echo   create-migration ServiceName MigrationName
    exit /b 1
)

set SERVICE=%~1
set MIGRATION=%~2

set INFRA_PROJECT=src\Services\%SERVICE%\FlashDrop.%SERVICE%.Infrastructure
set API_PROJECT=src\Services\%SERVICE%\FlashDrop.%SERVICE%.Api

echo.
echo ========================================
echo Creating Migration
echo ========================================
echo Service   : %SERVICE%
echo Migration : %MIGRATION%
echo Project   : %INFRA_PROJECT%
echo Startup   : %API_PROJECT%
echo ========================================
echo.

if not exist "%INFRA_PROJECT%" (
    echo [ERROR] Infrastructure project not found:
    echo %INFRA_PROJECT%
    exit /b 1
)

if not exist "%API_PROJECT%" (
    echo [ERROR] API project not found:
    echo %API_PROJECT%
    exit /b 1
)

dotnet ef migrations add %MIGRATION% ^
    --project "%INFRA_PROJECT%" ^
    --startup-project "%API_PROJECT%" ^
    --output-dir Migrations

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [ERROR] Failed to create migration.
    exit /b %ERRORLEVEL%
)

echo.
echo ========================================
echo Migration created successfully!
echo ========================================
echo Service   : %SERVICE%
echo Migration : %MIGRATION%
echo ========================================

endlocal