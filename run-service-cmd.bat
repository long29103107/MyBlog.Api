@echo off

REM Initialize variables
setlocal enabledelayedexpansion

REM Define base path
set "basePath=D:\2.Backend\"

REM Load service paths and run each service
for /f "tokens=1,2,3 delims== " %%A in (servicePaths.txt) do (
    if "%%C"=="true" (
        REM Replace initial part of the path with base path
        set "fullPath=!basePath!%%B"
        echo Running %%A
        cd /d "!fullPath!"
        start cmd /k "dotnet run --configuration Debug --no-build"
        cd /d ..
    ) else (
        echo Skipping %%A
    )
)

pause
