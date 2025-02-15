# SpawnDev.BlazorJS.CodeRunner

[![NuGet](https://img.shields.io/nuget/dt/SpawnDev.BlazorJS.CodeRunner.svg?label=SpawnDev.BlazorJS.CodeRunner)](https://www.nuget.org/packages/SpawnDev.BlazorJS.CodeRunner) 

Provides a C# code compiler service for compiling and running C# code and Blazor components at runtime. 99% of the code compiling work was already completed by [StefH](https://github.com/StefH/) and [MudBlazor/TryMudBlazor](https://github.com/MudBlazor/TryMudBlazor). This repo just packages it in a way I find useful for my projects.

The demo also uses [BlazorMonaco](https://github.com/serdarciplak/BlazorMonaco). A Blazor component for Microsoft Monaco Editor which powers Visual Studio Code. 

## Demo
[Live Demo](https://lostbeard.github.io/SpawnDev.BlazorJS.CodeRunner/)

## Getting started
Add `SpawnDev.BlazorJS.CodeRunner` package reference to your `Project.csproj`
```xml
<ItemGroup>
	<PackageReference Include="SpawnDev.BlazorJS.CodeRunner" Version="0.0.17" />
</ItemGroup>
```

CodeRunner uses Microsoft.CodeAnalysis.CSharp. Microsoft now publishes that library one of their Azure Nuget package hosts, referred to as `dotnet-tools`. That source has to be added to your project's `.csproj` file inside of a `<RestoreAdditionalProjectSources>` node.

Add the package package source to your `Project.csproj`  
https://github.com/dotnet/aspnetcore/blob/main/NuGet.config  
```xml
<!-- CodeRunner dependency - package source for Microsoft.CodeAnalysis.CSharp -->
<PropertyGroup>
	<RestoreAdditionalProjectSources>
		https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet-tools/nuget/v3/index.json;
	</RestoreAdditionalProjectSources>
</PropertyGroup>
```

### Thanks to:
- [StefH](https://github.com/StefH/ProtoBufJsonConverter/tree/main/src-webcil) for their Webcil to MetadataReference work.
- [MudBlazor/TryMudBlazor](https://github.com/MudBlazor/TryMudBlazor) for their compiling Blazor/Razor code.
- [CompileBlazorInBlazor](https://github.com/BlazorComponents/CompileBlazorInBlazor) for their compiling Blazor/Razor code (older work).
- [BlazorMonaco](https://github.com/serdarciplak/BlazorMonaco) for their code editor.
