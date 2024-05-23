@echo off

REM Get configurations
call config.cmd

echo Publishing a client
echo stop a client
%SystemRoot%\System32\inetsrv\appcmd.exe stop site /site.name:"%CLIENT_SITE_NAME%"

cmd /c "cd %ANGULAR_SRC_PATH% && npm i && npm run build:prod"

echo start a client
%SystemRoot%\System32\inetsrv\appcmd.exe start site /site.name:"%CLIENT_SITE_NAME%"

echo Client published.