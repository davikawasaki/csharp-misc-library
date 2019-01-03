@echo off
setlocal

REM Script name: backupCShrapOldPackages
REM Version:     1.0.0
REM @TODO: Error handling
REM @TODO: Unit testing 

REM ------- ARGS NECESSARY TO RUN SCRIPT (NEEDS TO BE SENT IN ORDER) -------
REM %~1 as SOLUTION FOLDER: $(SolutionDir) Ex. csharp-misc-library\
REM %~2 as PROJECT FOLDER: CSharpMiscLibrary\
REM %~3 as RELEASE OUTPUT: bin\Release\
REM %~4 as TARGET OUTPUT FOLDER: $(TargetFramework) Ex. netstandard2.0
REM %~5 as DESTINATION COPY PATH: Ex. Versions\
REM %~6 as PREVIOUS PACKAGE VERSION: $(PreviousVersion) Ex. 0.1.0
REM CMD prompt command line example:
REM "backupCShrapOldPackages.bat" $(SolutionDir) "CSharpMiscLibrary\" "bin\Release\" $(TargetFramework) "Versions\" $(PreviousVersion)

REM rd \s \q $(SolutionDir)CSharpMiscLibrary\Versions\$(PreviousVersion)
IF EXIST "%~1%~2%~5%~6" del /f /s /q "%~1%~2%~5%~6" 1>nul
IF EXIST "%~1%~2%~5%~6" rd /s /q "%~1%~2%~5%~6"

REM mkdir $(SolutionDir)CSharpMiscLibrary\Versions\$(PreviousVersion)
IF NOT EXIST "%~1%~2%~5%~6" mkdir "%~1%~2%~5%~6"

REM echo D | xcopy $(SolutionDir)CSharpMiscLibrary\bin\Release\$(TargetFramework)\ $(SolutionDir)CSharpMiscLibrary\Versions\$(PreviousVersion)\$(TargetFramework)\
IF EXIST "%~1%~2%~3%~4" echo D | xcopy "%~1%~2%~3%~4\" "%~1%~2%~5%~6\%~4\"

REM copy $(SolutionDir)CSharpMiscLibrary\bin\Release\*$(PreviousVersion).nupkg $(SolutionDir)CSharpMiscLibrary\Versions\$(PreviousVersion)
IF EXIST "%~1%~2%~3*%~6.nupkg" copy "%~1%~2%~3*%~6.nupkg" "%~1%~2%~5%~6\"

REM del $(ProjectDir)\bin\Release\*.nupkg
REM del /f "%~1%~2%~3*%~6.nupkg"