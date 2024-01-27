@echo off

:: Backup database schema to specified location

:: --------------------------------- VARIABLES --------------------------------- ::
:: ----------------------------------------------------------------------------- ::
set schema=%~1
set target=%~2
set mysql="C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe"
set mysqldump="C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe"

echo "Backing up %schema%"
echo "to %target%"
%mysqldump% -uroot -proot -hlocalhost -P3306 --opt %schema% > "%target%"
echo DONE!