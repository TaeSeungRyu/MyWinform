@echo off
cd /d "%~dp0"
start "WinForms" cmd /k "dotnet watch run --project TestClient\WinFormsHello.csproj"