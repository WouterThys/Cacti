@echo off
set service_name=%1
set display_name=%2
set exe_path=%3

echo Creating service %service_name%

sc query "%service_name%" | find "SERVICE_NAME" > nul
if %errorlevel% == 0 (
    echo Service "%service_name%" already exist.
    goto params
) else ( echo Checked that service does not exist yet )

sc query MySQL80 | find "SERVICE_NAME" > nul
if %errorlevel% neq 0 (
    echo Service "MySQL80" does not exist. "MySQL80" is required to be installed for server to work..
    exit /b 1
) else ( echo MySQL80 service exists! )

sc create "%service_name%" binPath='"%exe_path%"' DisplayName=%display_name% start=auto depend=MySQL80
if %errorlevel% neq 0 (
    echo Failed to create service "%service_name%". Error code: %errorlevel%
    exit /b %errorlevel%
)

sc description "%service_name%" "Cacti-Server Windows Service"
if %errorlevel% neq 0 (
    echo Failed to set description for service "%service_name%".
    exit /b %errorlevel%
)

:params
sc failure "%service_name%" reset= 0 actions= restart/60000/restart/60000/restart/60000
if %errorlevel% neq 0 (
    echo Failed to configure recovery settings for service "%service_name%". Error code: %errorlevel%
    exit /b %errorlevel%
)

echo Service "%service_name%" created successfully.
echo. 
