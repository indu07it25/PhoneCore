@echo off
pushd

:: CD to script's directory
cd /D %~dp0

if '%1'=='' ( 
	Set Mode=Debug
) else (
	Set Mode=%1
)
set proxyTargetProject="%~dp0PhoneCore.Framework.UnitTests"
echo %proxyTargetProject%
set doPause=1
if not "%2" == "" set doPause=0
%systemroot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe "PhoneCore.Framework.sln" /t:Build /p:Configuration=%Mode%;TargetFrameworkVersion=v4.0
echo run proxy gen utility
PhoneCore.Framework.ProxyGen\bin\%Mode%\PhoneCore.Framework.ProxyGen.exe %proxyTargetProject% %proxyTargetProject%\bin\%Mode% %proxyTargetProject%\Proxies
%systemroot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe  PhoneCore.Framework.UnitTests\PhoneCore.Framework.UnitTests.csproj /t:Test /p:Configuration=%Mode%;TargetFrameworkVersion=v4.0
@if ERRORLEVEL 1 goto fail

:fail
if "%doPause%" == "1" pause
popd
exit /b 1

:end
popd
if "%doPause%" == "1" pause
exit /b 0