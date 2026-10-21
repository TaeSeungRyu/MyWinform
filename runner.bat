@echo off
cd /d "%~dp0"
start "WinForms" cmd /k "dotnet run --project TestClient\WinFormsHello.csproj"