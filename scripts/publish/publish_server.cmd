@REM @echo off

REM Get configurations
call config.cmd

echo Publishing a server
echo stop a server
%SystemRoot%\System32\inetsrv\appcmd.exe stop site /site.name:"%SERVER_SITE_NAME%"

dotnet publish %SERVER_PROJECT_FILE_PATH% -c Release -o %SERVER_SITE_PATH%

echo start a server
%SystemRoot%\System32\inetsrv\appcmd.exe start site /site.name:"%SERVER_SITE_NAME%"

echo Server published.