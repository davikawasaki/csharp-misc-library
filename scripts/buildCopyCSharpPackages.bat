@echo off
setlocal

REM Script name: buildCopyCSharpPackages
REM Version:     1.0.0
REM @TODO: Error handling
REM @TODO: Unit testing 

REM ------- ARGS NECESSARY TO RUN SCRIPT (NEEDS TO BE SENT IN ORDER) -------
REM %~1 as SOLUTION FOLDER: $(SolutionDir) Ex. csharp-misc-library\
REM %~2 as PROJECT FOLDER: CSharpMiscLibrary\
REM %~3 as RELEASE OUTPUT: bin\Release\
REM %~4 as TARGET OUTPUT FOLDER: $(TargetFramework) Ex. netstandard2.0
REM %~5 as DESTINATION COPY PATH: Ex. ...\packages\CSharpMiscLibrary\
REM %~6 as PACKAGE VERSION: $(Version) Ex. 0.2.0
REM %~7 as DESTINATION DOCS COPY PATH: docs\versions\
REM CMD prompt command line example:
REM "buildCopyCSharpPackages.bat" $(SolutionDir) "CSharpMiscLibrary\" "bin\Release\" $(TargetFramework) "...\packages\CSharpMiscLibrary\" $(Version) "docs\versions\"

REM rd \s \q ...\packages\CSharpMiscLibrary\$(Version)
del /f /s /q "%~5%~6" 1>nul
rd \s \q "%~5%~6"

REM mkdir ...\packages\CSharpMiscLibrary\$(Version)
mkdir "%~5%~6"

REM echo D | xcopy $(SolutionDir)CSharpMiscLibrary\bin\Release\$(TargetFramework) ...\packages\CSharpMiscLibrary\$(Version)\$(TargetFramework)
echo D | xcopy "%~1%~2%~3%~4" "%~5%~6\%~4"

REM copy $(SolutionDir)CSharpMiscLibrary\bin\Release\*$(Version).nupkg ...\packages\CSharpMiscLibrary\$(Version)
REM copy "%~1%~2%~3*%~6.nupkg" "%~5%~6"

REM mkdir $(SolutionDir)docs\versions\$(Version)
mkdir "%~1%~7%~6"

REM copy $(SolutionDir)CSharpMiscLibrary\*.xml $(SolutionDir)docs\versions\$(Version)\
copy "%~1%~2*.xml" "%~1%~7%~6\"