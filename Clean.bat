@ECHO OFF

IF EXIST .vs RMDIR /S /Q .vs
IF EXIST packages RMDIR /S /Q packages

SET PROJ=AppHostCefSharp
IF EXIST %PROJ%\bin RMDIR /S /Q %PROJ%\bin
IF EXIST %PROJ%\obj RMDIR /S /Q %PROJ%\obj
IF EXIST %PROJ%\%PROJ%.csproj.user DEL %PROJ%\%PROJ%.csproj.user
IF EXIST %PROJ%\%PROJ%.csproj.DotSettings DEL %PROJ%\%PROJ%.csproj.DotSettings

SET PROJ=AppHostCefSharp.Services
IF EXIST %PROJ%\bin RMDIR /S /Q %PROJ%\bin
IF EXIST %PROJ%\obj RMDIR /S /Q %PROJ%\obj
IF EXIST %PROJ%\%PROJ%.csproj.user DEL %PROJ%\%PROJ%.csproj.user
IF EXIST %PROJ%\%PROJ%.csproj.DotSettings DEL %PROJ%\%PROJ%.csproj.DotSettings

SET PROJ=AppHostCefSharp.WebBrowser
IF EXIST %PROJ%\bin RMDIR /S /Q %PROJ%\bin
IF EXIST %PROJ%\obj RMDIR /S /Q %PROJ%\obj
IF EXIST %PROJ%\%PROJ%.csproj.user DEL %PROJ%\%PROJ%.csproj.user
IF EXIST %PROJ%\%PROJ%.csproj.DotSettings DEL %PROJ%\%PROJ%.csproj.DotSettings

SET PROJ=ExcelDnaExample
IF EXIST %PROJ%\bin RMDIR /S /Q %PROJ%\bin
IF EXIST %PROJ%\obj RMDIR /S /Q %PROJ%\obj
IF EXIST %PROJ%\%PROJ%.csproj.user DEL %PROJ%\%PROJ%.csproj.user
IF EXIST %PROJ%\%PROJ%.csproj.DotSettings DEL %PROJ%\%PROJ%.csproj.DotSettings

ECHO Done cleaning
ECHO.
PAUSE