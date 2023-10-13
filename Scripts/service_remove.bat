@echo off
set service_name=%1

if "%service_name%" == "" (
    set service_name=DEFAULT_SERVICE_NAME
)

echo Removing service %service_name%

sc query "%service_name%" | find "SERVICE_NAME" > nul
if %errorlevel% neq 0 (
    echo Service "%service_name%" does not exist. Error code: %errorlevel%
    exit /b 1
)

sc stop "%service_name%" > nul
if %errorlevel% neq 0 (
    echo Failed to stop service "%service_name%". Error code: %errorlevel%
)

sc delete "%service_name%"
if %errorlevel% neq 0 (
    echo Failed to remove service "%service_name%". Error code: %errorlevel%
    exit /b %errorlevel%
)

echo Service "%service_name%" removed successfully.
echo. 
