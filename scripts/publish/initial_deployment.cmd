@echo off

REM Get configurations
call config.cmd

call publish_all.cmd

REM Copy appsettings to published folder
echo Copy appsettings to published server folder
copy %SERVER_APPSETTINGS_PATH% %PUBLISHED_SERVER_APPSETTINGS_PATH%

call run_db_migrator.cmd

call setup_IIS_web_sites.cmd