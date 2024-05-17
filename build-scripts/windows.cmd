@ECHO OFF

echo Starting to build resource...

echo [1/3] Bulding client project
"%MSBUILD_PATH%\msbuild.exe" /p:Configuration=Release /v:q ../src/Client/IndestructibleObjects.Client.csproj

echo [2/3] Copying license
copy /y "..\LICENSE" /a "..\output\LICENSE"

echo [3/3] Copying readme
copy /y "..\README.md" /a "..\output\README.md"

echo Finished building resource

pause