@echo off

REM Get configurations
call config.cmd

cmd /c "cd %DB_MIGRATOR_PROJECT_PATH% && dotnet run"
echo db migrated