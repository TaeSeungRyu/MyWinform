@echo off
cd /d "%~dp0"
start "TCP Server" cmd /k "dotnet run --project TestTcpServer\TestTcpServer.csproj"
