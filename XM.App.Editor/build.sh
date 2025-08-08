#!/bin/bash
echo "Building XM.App.Editor..."
dotnet build XM.App.Editor.csproj
if [ $? -eq 0 ]; then
    echo "Build successful!"
    echo "Starting XM.App.Editor..."
    dotnet run --project XM.App.Editor.csproj
else
    echo "Build failed!"
    read -p "Press Enter to continue..."
fi 