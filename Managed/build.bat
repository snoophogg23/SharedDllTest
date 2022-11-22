set CURRENTDIR=%cd%
set BATDIR=%~dp0
set BUILD=%1

@REM call "%ProgramFiles%\Microsoft Visual Studio\2022\Professional\VC\Auxiliary\Build\vcvars32.bat"
@REM cmake %BATDIR%/../APSNative/ --preset x86-%BUILD%
@REM cmake --build %BATDIR%/../APSNative/out/build/x86-%BUILD%

call "%ProgramFiles%\Microsoft Visual Studio\2022\Professional\VC\Auxiliary\Build\vcvars64.bat"
cmake %BATDIR%/../ --preset x64-%BUILD%
cmake --build %BATDIR%/../out/build/x64-%BUILD%