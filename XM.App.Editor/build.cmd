@echo off
echo Building XM.App.Editor...
dotnet build XM.App.Editor.csproj
if %ERRORLEVEL% EQU 0 (
    echo Build successful!
    echo Starting XM.App.Editor...
    dotnet run --project XM.App.Editor.csproj
) else (
    echo Build failed!
    pause
) 