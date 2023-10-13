@echo off

if "%1"=="" goto usage
if "%2"=="" goto usage

echo Copying folders

if not exist "%1" (
    echo Error: Source folder "%1" does not exist.
    goto end
)

if not exist "%2" (
    echo Error: Destination folder "%2" does not exist.
    goto end
)

robocopy "%1" "%2" /E

:end
echo Done.
goto :eof

:usage
echo Usage: copyfolder.bat [source_folder] [destination_folder]
echo.
echo Example: copyfolder.bat "C:\Folder1" "D:\Folder2"
echo.
goto :eof
