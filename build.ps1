param(
    [ValidateSet("Debug","Release")]
    [string]$Configuration = "Release",

    [string]$Solution = "BeatSaberPlus.sln",

    [string]$Project = "",

    [switch]$Restore,

    [switch]$Diag
)

$ErrorActionPreference = "Stop"

function Get-MSBuildPath {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vswhere) {
        $found = & $vswhere -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe"
        if ($found) { return $found }
    }

    $candidates = @(
        "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
        "C:\Program Files\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe",
        "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe",
        "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
    )

    foreach ($path in $candidates) {
        if (Test-Path $path) { return $path }
    }

    return $null
}

$msbuild = Get-MSBuildPath
if (-not $msbuild) {
    Write-Error "MSBuild.exe не знайдено. Встанови Visual Studio/Build Tools з компонентом MSBuild."
}

$target = if ($Project) { $Project } else { $Solution }
if (-not (Test-Path $target)) {
    Write-Error "Файл не знайдено: $target"
}

$args = @(
    $target,
    "/t:Build",
    "/m",
    "/p:Configuration=$Configuration"
)

if ($Restore) { $args += "/restore" }
if ($Diag) { $args += "/v:diag" }

$logDir = Join-Path $PSScriptRoot "build\logs"
New-Item -ItemType Directory -Force -Path $logDir | Out-Null

$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
$logPath = Join-Path $logDir "build_${timestamp}.log"
$args += "/flp:logfile=$logPath;verbosity=normal"

Write-Host "MSBuild: $msbuild"
Write-Host "Target : $target"
Write-Host "Config : $Configuration"
Write-Host "Log    : $logPath"
Write-Host ""

& $msbuild @args
