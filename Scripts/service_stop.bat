@echo off
set service_name=%1

if "%service_name%" == "" (
    set service_name=DEFAULT_SERVICE_NAME
)

echo Stopping service %service_name%

sc query "%service_name%" | find "SERVICE_NAME" > nul
if %errorlevel% neq 0 (
    echo Service "%service_name%" does not exist. Error code: %errorlevel%
    exit /b 1
)

sc stop "%service_name%" > nul
if %errorlevel% neq 0 (
    echo Failed to stop service "%service_name%". Error code: %errorlevel%
    exit /b %errorlevel%
)

:wait
sc query "%service_name%" | find "STATE" | find "STOPPED" > nul
if not errorlevel 1 (
    echo Service stopped successfully.
    goto end
)
timeout /t 1 > nul
goto wait

:end
echo. 

