Write-Host "===================================================" -ForegroundColor Cyan
Write-Host "  Building and Running Elegant Housing System" -ForegroundColor Cyan
Write-Host "===================================================" -ForegroundColor Cyan

# Find MSBuild
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = $null

if (Test-Path $vswhere) {
    $msbuild = & $vswhere -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe
}

if (-not $msbuild) {
    $msbuild = Get-Command msbuild.exe -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Source
}

if (-not $msbuild) {
    Write-Error "MSBuild was not found. Please install Visual Studio or Build Tools."
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "Found MSBuild at: $msbuild" -ForegroundColor Green
Write-Host "Building the project..." -ForegroundColor Yellow

& $msbuild ElegantHousingSystem.sln /t:Build /p:Configuration=Debug /v:minimal

if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed. Please check the compiler output."
    Read-Host "Press Enter to exit"
    exit $LASTEXITCODE
}

Write-Host "`nBuild Succeeded! Starting the application..." -ForegroundColor Green
Start-Process "bin\Debug\ElegantHousingSystem.exe"
