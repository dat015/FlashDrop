@echo off
setlocal

if "%~1"=="" (
    echo [ERROR] Please provide service name.
    echo.
    echo Usage:
    echo   update-database ServiceName
    echo.
    echo Example:
    echo   update-database Catalog
    exit /b 1
)

set SERVICE=%~1

set INFRA_PROJECT=src\Services\%SERVICE%\FlashDrop.%SERVICE%.Infrastructure
set API_PROJECT=src\Services\%SERVICE%\FlashDrop.%SERVICE%.Api

echo.
echo ========================================
echo Updating Database
echo ========================================
echo Service : %SERVICE%
echo Project : %INFRA_PROJECT%
echo Startup : %API_PROJECT%
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

dotnet ef database update ^
    --project "%INFRA_PROJECT%" ^
    --startup-project "%API_PROJECT%"

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo [ERROR] Failed to update database.
    exit /b %ERRORLEVEL%
)

echo.
echo ========================================
echo Database updated successfully!
echo ========================================
echo Service : %SERVICE%
echo ========================================

endlocal    