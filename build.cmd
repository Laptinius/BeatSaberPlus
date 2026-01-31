@echo off
setlocal
set script=%~dp0build.ps1
powershell -NoProfile -ExecutionPolicy Bypass -File "%script%" %*
endlocal
