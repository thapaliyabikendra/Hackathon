@echo off

REM Get configurations
call config.cmd

REM Create the server site in IIS
echo Creating server site in IIS...
%SystemRoot%\System32\inetsrv\appcmd.exe add site /name:"%SERVER_SITE_NAME%" /bindings:http/*:"%SERVER_SITE_PORT%": /physicalPath:"%SERVER_SITE_PATH%"

REM Create the client site in IIS
echo Creating client site in IIS...
%SystemRoot%\System32\inetsrv\appcmd.exe add site /name:"%CLIENT_SITE_NAME%" /bindings:http/*:"%CLIENT_SITE_PORT%": /physicalPath:"%CLIENT_SITE_PATH%"

echo IIS Web Sites created.