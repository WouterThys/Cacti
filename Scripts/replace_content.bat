@echo off

set source_file=%~1
set replace_what=%2
set replace_with=%3
set destination_file=%4

@REM \{SCHEMA\}

echo Replacing content '%replace_what%' with '%replace_with%' in %source_file%
powershell -Command "(Get-Content %source_file%) -replace '%replace_what%', '%replace_with%' | Set-Content %destination_file%"