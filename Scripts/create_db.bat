@echo off

:: Backup database schema to specified location
echo Starting create.db script
:: --------------------------------- VARIABLES --------------------------------- ::
:: ----------------------------------------------------------------------------- ::
set database=%~1
set create_script=%~2
set update_script=%~3

set server=localhost
set mysqlshow="C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqlshow.exe"
set mysql="C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe"

:: Need to escape underscores!!
echo Parsing database name
set "escaped_name=%database:_=\_%"
%mysqlshow% -uroot -proot "%escaped_name%" > nul 2>&1
if errorlevel 1 (
    echo Database %database% does not exist, creating new database now
    
    echo Updating schema in SQL file...
    powershell -Command "(Get-Content %create_script%) -replace '\{SCHEMA\}', '%database%' | Set-Content output.sql"
    
    echo Creating database...
    %mysql% -uroot -proot < ".\output.sql"

    echo Database created!!

    echo Insert update scripts into updatescripts table
    DatabaseHelper.exe EXECUTE config.json -s %server% -d %database% -u root -p root --exec %update_script%

    echo Set all updatescripts executed
    echo UPDATE %database%.updatescripts SET state = 3 WHERE id ^> 1; > update.sql
    DatabaseHelper.exe EXECUTE config.json -s %server% -d %database% -u root -p root --exec update.sql

    echo Cleaning up
    del output.sql
    del update.sql

) else (
    echo Database %database% exists. Nothing to do.
)