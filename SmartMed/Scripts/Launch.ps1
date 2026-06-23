# SmartMed launcher — use when Visual Studio F5 is blocked by Application Control.
$ErrorActionPreference = 'Stop'
$projectDir = Split-Path -Parent $PSScriptRoot
Set-Location $projectDir

$sac = Get-ItemProperty -Path 'HKLM:\SYSTEM\CurrentControlSet\Control\CI\Policy' -Name 'VerifiedAndReputablePolicyState' -ErrorAction SilentlyContinue
if ($sac -and $sac.VerifiedAndReputablePolicyState -eq 1) {
    Write-Warning @"
Windows Smart App Control is ON and may block SmartMed.exe.

One-time fix:
  Windows Security → App & browser control → Smart App Control settings → Off
  (restart may be required)

Alternative: move the project to C:\Dev\SmartMed and rebuild.
"@
}

dotnet build
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

$exe = Join-Path $projectDir 'bin\Debug\net48\SmartMed.exe'
if (Test-Path $exe) {
    Unblock-File -LiteralPath $exe -ErrorAction SilentlyContinue
}

dotnet run
