@echo off
echo Installing Sirus Patch Protector...

if not exist "nssm.exe" (
    echo ERROR: nssm.exe not found!
    pause
    exit /b
)

nssm stop SirusPatchProtector 2>nul
nssm remove SirusPatchProtector confirm 2>nul

nssm install SirusPatchProtector "%~dp0sirus-patch-protector.exe"

nssm set SirusPatchProtector Start SERVICE_AUTO_START
nssm set SirusPatchProtector DisplayName "Sirus Patch Protector"
nssm set SirusPatchProtector Description "Protects Data folder from modifications"
nssm set SirusPatchProtector AppPriority BELOW_NORMAL_PRIORITY_CLASS

nssm start SirusPatchProtector

echo.
echo SUCCESS: Service installed.
pause