@echo off

set basedir=TestServer
set source=".NET\CactiService\CactiService\bin\Debug\net7.0\"
set clientSource="Release\client\"

echo "%basedir%\Server\"
if not exist "%basedir%" mkdir "%basedir%"
if not exist "%basedir%\Server\" mkdir "%basedir%\Server"
if not exist "%basedir%\Server\Config\" mkdir "%basedir%\Server\Config"
if not exist "%basedir%\Client\" mkdir "%basedir%\Client"
if not exist "%basedir%\DOCS\" mkdir "%basedir%\DOCS"

echo stop server if running
taskkill /F /FI "IMAGENAME eq CactiServer.exe"

echo copy server files
xcopy /s %source%*.* %basedir%\Server\. /Y
rem xcopy test.lic %basedir%\Server\Config\ /Y
rem xcopy /s %clientSource%*.*  %basedir%\Client\. /Y

echo start new instance
cd /d %basedir%\Server
start "" CactiServer.exe "--console"