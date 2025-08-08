@echo off
echo Building XM.App.Editor...
dotnet build XM.App.Editor.csproj
set "BUILD_ERRORLEVEL=%ERRORLEVEL%"
if %BUILD_ERRORLEVEL% EQU 0 (
    echo Build successful!
    echo Starting XM.App.Editor...
    dotnet run --project XM.App.Editor.csproj
) else (
    echo Build failed with code %BUILD_ERRORLEVEL%!
    pause
    exit /b %BUILD_ERRORLEVEL%
) 