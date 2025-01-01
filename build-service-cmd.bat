@echo off

REM Initialize variables
setlocal enabledelayedexpansion

REM Define base path
set "basePath=D:\1.BE\MyBlog.Api"

REM Load service paths and build each service
for /f "tokens=1,2,3 delims== " %%A in (servicePaths.txt) do (
    if "%%C"=="true" (
        REM Replace initial part of the path with base path
        set "fullPath=!basePath!%%B"
        echo Building %%A
        cd /d "!fullPath!"
        dotnet build
        cd /d ..
    ) else (
        echo Skipping %%A
    )
)

pause
