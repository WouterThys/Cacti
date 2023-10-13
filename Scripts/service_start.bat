@echo off
set service_name=%1
set max_wait_time=20

if "%service_name%" == "" (
    set service_name=DEFAULT_SERVICE_NAME
)

echo Starting service %service_name%

sc query "%service_name%" | find "SERVICE_NAME" > nul
if %errorlevel% neq 0 (
    echo Service "%service_name%" does not exist. Error code: %errorlevel%
    exit /b 1
)

sc start "%service_name%" > nul
if %errorlevel% neq 0 (
    echo Failed to start service "%service_name%". Error code: %errorlevel%
    exit /b %errorlevel%
)

set /a counter=0
:wait
sc query "%service_name%" | find "STATE" | find "RUNNING" > nul
if not errorlevel 1 (
    echo Service started successfully.
    goto end
)
set /a counter=counter+1
if %counter% gtr %max_wait_time% (
    echo Timed out waiting for service "%service_name%" to start.
    exit /b 1
)
timeout /t 1 > nul
goto wait

:end
echo. 
