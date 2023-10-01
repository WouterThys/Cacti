@echo off

set /P version=Release Version:
set stored_procedure_dir=Database
set stored_procedure_file=Database/procedures.sql
set database_server=localhost

set database=%~1
goto :databaseCheck

:setdatabase
set database=cacti

:databaseCheck
if "%database%" == "" goto :setdatabase

rem set stored_procedure_file=Database\\output\\procedures.sql

echo Execute update scripts for %database%
cd .\Database\helper\ & DatabaseHelper.exe UPDATESCRIPTS_EXECUTE ..\config.json -s %database_server% -d %database% -u waldo -p waldow %version%
rem Scripts\\databasehelper\\DatabaseHelper.exe UPDATESCRIPTS_EXECUTE %cd%\local.json %version%

echo Create stored procedures
cd .\Database\helper\ & DatabaseHelper.exe PROCEDURES ..\config.json -s %database_server% -d %database% -u waldo -p waldow --procDir=..\procedures\

echo Execute stored procedures script
cd .\Database\helper\ & DatabaseHelper.exe EXECUTE ..\config.json -s %database_server% -d %database% -u waldo -p waldow --exec .\output\procedures.sql

echo DONE!
pause