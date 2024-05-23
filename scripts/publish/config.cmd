@echo off

REM Set the IIS site names and ports
set APP_NAME=hackathon
set CLIENT_SITE_NAME=%APP_NAME%-client
set SERVER_SITE_NAME=%APP_NAME%-server
set CLIENT_SITE_PORT=5100
set SERVER_SITE_PORT=5101

REM Set projects details
set PUBLISHED_APP_PATH=C:\Amnil\published
set PROJECT_NAME=Hackathon
set ANGULAR_SRC_PATH=..\..\angular
set ASPNET_SRC_PATH=..\..\aspnet-core\src

REM Set the paths for site directories
set CLIENT_SITE_PATH=%PUBLISHED_APP_PATH%\%APP_NAME%\client
set SERVER_SITE_PATH=%PUBLISHED_APP_PATH%\%APP_NAME%\server
set MQ_CLIENT_SITE_PATH=%PUBLISHED_APP_PATH%\%APP_NAME%\mq-client

REM Set projects path
set SERVER_PROJECT_NAME=%PROJECT_NAME%.HttpApi.Host
set DB_MIGRATOR_PROJECT_NAME=%PROJECT_NAME%.DbMigrator

set SERVER_PROJECT_PATH=%ASPNET_SRC_PATH%\%SERVER_PROJECT_NAME%
set DB_MIGRATOR_PROJECT_PATH=%ASPNET_SRC_PATH%\%DB_MIGRATOR_PROJECT_NAME%

REM Set projects csproj file path
set SERVER_PROJECT_FILE_PATH=%SERVER_PROJECT_PATH%\%SERVER_PROJECT_NAME%.csproj

REM Set the appsettings paths
set SERVER_APPSETTINGS_PATH=configs\hackathon-server-appsettings.json
set PUBLISHED_SERVER_APPSETTINGS_PATH=%SERVER_SITE_PATH%\appsettings.json