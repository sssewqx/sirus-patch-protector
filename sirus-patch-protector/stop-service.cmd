@echo off
echo Stopping Sirus Patch Protector...

if not exist "nssm.exe" (
    echo ERROR: nssm.exe not found!
    pause
    exit /b
)

nssm stop SirusPatchProtector
nssm remove SirusPatchProtector confirm

echo.
echo SUCCESS: Service stopped.
pause