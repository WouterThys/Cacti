@echo off

:: Backup database schema to specified location

:: --------------------------------- VARIABLES --------------------------------- ::
:: ----------------------------------------------------------------------------- ::
set database=%~1
set server=localhost
set version=%~2
set updatescript=%~3
set translationscript=%~4

echo Dating up %database%

echo Insert update scripts into updatescripts table
DatabaseHelper.exe EXECUTE config.json -s %server% -d %database% -u root -p root --exec %updatescript%

echo Execute update scripts
DatabaseHelper.exe UPDATESCRIPTS_EXECUTE config.json -s %server% -d %database% -u root -p root %version%

echo Create stored procedures
DatabaseHelper.exe PROCEDURES config.json -s %server% -d %database% -u root -p root --procDir=.\procedures\

echo Execute stored procedures script
DatabaseHelper.exe EXECUTE config.json -s %server% -d %database% -u root -p root --exec .\output\procedures.sql

echo DONE!