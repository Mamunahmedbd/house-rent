@echo off
setlocal enabledelayedexpansion

echo ===================================================
echo   Building and Running Elegant Housing System
echo ===================================================

:: Detect MSBuild using vswhere
set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
set "MSBUILD_PATH="

if exist "%VSWHERE%" (
    for /f "usebackq tokens=*" %%i in (`"%VSWHERE%" -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do (
        set "MSBUILD_PATH=%%i"
    )
)

if not defined MSBUILD_PATH (
    :: Fallback to searching PATH
    where msbuild.exe >nul 2>&1
    if !errorlevel! equ 0 (
        set "MSBUILD_PATH=msbuild.exe"
    ) else (
        echo ERROR: MSBuild was not found on your system.
        echo Please ensure Visual Studio or Build Tools are installed.
        pause
        exit /b 1
    )
)

echo Found MSBuild at: "%MSBUILD_PATH%"
echo Building the project...

"%MSBUILD_PATH%" ElegantHousingSystem.sln /t:Build /p:Configuration=Debug /v:minimal
if !errorlevel! neq 0 (
    echo.
    echo ERROR: Build failed. Please check the compilation errors above.
    pause
    exit /b !errorlevel!
)

echo.
echo Build Succeeded! Starting the application...
start "" "bin\Debug\ElegantHousingSystem.exe"
