# C# Generic Misc Library

This repository covers C# snippet misc library to be used in different projects. Inspiration from the repository developed through my trainee at DSV company.

## Table of Contents

- [Libraries Dependencies](#libraries-dependencies)
- [Versions](#versions)
- [Build Process](#build-process)
- [How to Use](#how-to-use)
- [Changelog](#changelog)
- [Documentation](https://github.com/davikawasaki/csharp-misc-library/browse/CSharpMiscLibrary/CSharpMiscLibrary.xml)
- [Authors](#authors)

## Libraries Dependencies

- NetStandard.Library 2.0.3
- Bundle.Microsoft.Office.Interop 15.0.4569
- Dapper 1.50.5
- ExcelDataReader.DataSet 3.4.2

## Versions

All compiled versions of this library is released as a NuGet C# library project package, which contains the mapping of methods and the respective assembly binary file.

Details of build process can be found at [build process topic](#build-process).

## Build Process

The build process of this library uses two batches processes for a pre-build and a post-build. The basic process can be seen in [CSharpMiscLibrary.csproj](https://github.com/davikawasaki/csharp-misc-library/browse/CSharpMiscLibrary/CSharpMiscLibrary.csproj) file in the following lines:

```
<PropertyGroup>
    <PostBuildEventDependsOn>
        $(PostBuildEventDependsOn);
        PostBuildMacros;
    </PostBuildEventDependsOn>
</PropertyGroup>

<Target Name="PreBuild" BeforeTargets="PreBuildEvent" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Exec Command="call &quot;$(SolutionDir)scripts\backupCSharpOldPackages.bat&quot; $(SolutionDir) &quot;CSharpMiscLibrary\&quot; &quot;bin\Release\&quot; $(TargetFramework) &quot;Versions\&quot; $(PreviousVersion)" />
</Target>

<Target Name="CopyPackage" AfterTargets="Pack" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Copy SourceFiles="$(OutputPath)..\$(PackageId).$(PackageVersion).nupkg" DestinationFolder="\\145.218.248.62\filesvcs-01$\Custom\DK\DK.AirSeaHorsens\XpExportHorsens\Arbejdsark\ScriptsLibrary\packages\CSharpMiscLibrary\$(Version)\" />
</Target>

<Target Name="PostBuild" AfterTargets="PostBuildEvent" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Exec Command="call &quot;$(SolutionDir)scripts\buildCopyCSharpPackages.bat&quot; $(SolutionDir) &quot;CSharpMiscLibrary\&quot; &quot;bin\Release\&quot; $(TargetFramework) &quot;\\145.218.248.62\filesvcs-01$\Custom\DK\DK.AirSeaHorsens\XpExportHorsens\Arbejdsark\ScriptsLibrary\packages\CSharpMiscLibrary\&quot; $(Version) &quot;docs\Versions\&quot;" />
</Target>
```

The steps are the following:

1\. Before building any **release** version, it's necessary to make a backup version of the previous version. Using a property *<PreviousVersion>* in the main .csproj file, all release files are copied to a /Versions/X.X.X folder inside CSharpMiscLibrary/ folder (not synced with Git). For more details, check the [backupCSharpOldPackages.bat file](https://github.com/davikawasaki/csharp-misc-library/browse/scripts/backupCSharpOldPackages.bat).

```
<Target Name="PreBuild" BeforeTargets="PreBuildEvent" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Exec Command="call &quot;$(SolutionDir)scripts\backupCSharpOldPackages.bat&quot; $(SolutionDir) &quot;CSharpMiscLibrary\&quot; &quot;bin\Release\&quot; $(TargetFramework) &quot;Versions\&quot; $(PreviousVersion)" />
</Target>
```

2\. After that, the build is performed.

3\. After the build step, a post-build event is triggered, which calls the [buildCopyCSharpPackages.bat script](https://github.com/davikawasaki/csharp-misc-library/browse/scripts/buildCopyCSharpPackages.bat). This batch script copies the standard2.0/ folder from release to the local feed repository in the specific version created folder for the library. After that, the generated XML code documentation is copied to the docs version folder docs/Versions/X.X.X/.

```
<Target Name="PostBuild" AfterTargets="PostBuildEvent" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Exec Command="call &quot;$(SolutionDir)scripts\buildCopyCSharpPackages.bat&quot; $(SolutionDir) &quot;CSharpMiscLibrary\&quot; &quot;bin\Release\&quot; $(TargetFramework) &quot;...\packages\CSharpMiscLibrary\&quot; $(Version) &quot;docs\Versions\&quot;" />
</Target>
```

4\. In the end, the library code is packed into a .nupkg file and copied to the local feed repository (don't forget to enable the package generation in the Package tab inside Project properties:

```
<Target Name="CopyPackage" AfterTargets="Pack" Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
    <Copy SourceFiles="$(OutputPath)..\$(PackageId).$(PackageVersion).nupkg" DestinationFolder="...\packages\CSharpMiscLibrary\$(Version)\" />
</Target>
```

After you get these details, follow the next topic to add and sign the package **(STEP TODO AUTOMATICALLY)** into a local feed repository, which will be available for all other projects.

## How to Use

1\. Get the NuGet package file and the binary files (netstandard20 folder) - check [versions topic](#versions) for more details

2\. Since this is a private package, you won't find it in nuget public repository. The way it's being used is adding to a local feed repository in a shared drive folder. To configure the local feed repository in Visual Studio, go through the following steps:

- Go to Tools > NuGet Package Manager > Package Manager Settings. In there add the folder you will store your local NuGet package repositories:

![Package Manager Settings](https://github.com/davikawasaki/csharp-misc-library/browse/raw/docs/img/nuget_local_repository_management.JPG)
    
- Add the nuget package to repository using [nuget CLI](https://dist.nuget.org/win-x86-commandline/latest/nuget.exe), which will copy/sign the nupkg file and other auxiliary ones:

``` bash
$ nuget add CSharpMiscLibrary.<PUT_THE_VERSION_HERE>.nupkg -source <FULL_DRIVE_LOCAL_FEED_PATH>
```

- Copy manually the binary files (netstandard20 folder) from the desired release folder to the library version folder inside specific local feed repository. So, for a 1.2.0 version, the feed repository organization would according to the following structure:

```
\\local\feed
    CSharpMiscLibrary\
        1.0.0\
        1.1.0\
        1.2.0\
            netstandard2.0\
            CSharpMiscLibrary.1.2.0.nupkg
            CSharpMiscLibrary.1.2.0.nupkg.sha512
            CSharpMiscLibrary.nuspec
```

- Go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution. Select the local drive you setted and install the CSharpMiscLibrary as following:

![CSharpMiscLibrary NuGet Installation](https://github.com/davikawasaki/csharp-misc-library/browse/raw/docs/img/local_nuget_package_installation_visual_studio_process_v0.1.0.gif)

3\. After installation of the package and respective nested ones, import the desired folders into your project:

```csharp
using CSharpMiscLibrary.APIs;
using CSharpMiscLibrary.Classes;
using CSharpMiscLibrary.Exceptions;
using CSharpMiscLibrary.Repositories;
using CSharpMiscLibrary.Services;
```

Methods documentation for each one of the folders can be found with CSharpMiscLibrary XML file [inside docs folder](https://github.com/davikawasaki/csharp-misc-library/browse/docs/versions).

## Changelog

- 1.0.0 [03/01/19]: Started repository with classes, exceptions, DB repository, services, APIs, nuget, build steps.