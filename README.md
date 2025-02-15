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

CodeRunner uses Microsoft.CodeAnalysis.CSharp. Microsoft now publishes that library on one of their Azure Nuget package hosts, referred to as `dotnet-tools`. That source has to be added to your project's `.csproj` file inside of a `<RestoreAdditionalProjectSources>` node.

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

Add the service to your `Program.cs`
```cs
// Adds the CompilationService as a scoped service
builder.Services.AddCompilerService();
```

BasicBlazorExample from the demo project in this repo.  
```razor
@page "/basic"

<h3>BasicBlazorExample</h3>

<div>
    <button @onclick=CompileAndRun>Run</button>
</div>
<div>
    <textarea style="width: 100%; min-height: 400px;" @bind-value="_textArea" @bind-value:event="oninput"></textarea>
</div>
@if (_compiledType != null)
{
    <DynamicComponent Type="@_compiledType"></DynamicComponent>
}
else if (_busy && _compileLog.Count == 0)
{
    <span style="padding: 4px;">Compiling...</span>
}
else if (_compileLog.Count > 0)
{
    <pre style="padding: 4px;">
        @string.Join("\r\n", _compileLog)
    </pre>
}
else
{
    <div>Click `Run` to compile the code in the text area.</div>
}

@code {
    [Inject]
    CompilationService _compilationService { get; set; } = default!;

    Type? _compiledType = null;
    List<string> _compileLog = new List<string>();
    bool _busy = false;
    string _textArea = @"<div>Hello World! It is @DateTime.Now.ToString(""s"")</div>";

    async Task CompileAndRun()
    {
        _compiledType = null;
        if (_busy) return;
        _compileLog.Clear();
        try
        {
            _busy = true;
            StateHasChanged();
            _compiledType = await _compilationService.CompileRazorCodeToBlazorComponentType(_textArea, async msg =>
            {
                _compileLog.Add(msg);
                StateHasChanged();
                await Task.Delay(100);
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
        finally
        {
            _busy = false;
        }
        StateHasChanged();
    }
}
```

### Thanks to:
- [StefH](https://github.com/StefH/ProtoBufJsonConverter/tree/main/src-webcil) for their Webcil to MetadataReference work.
- [MudBlazor/TryMudBlazor](https://github.com/MudBlazor/TryMudBlazor) for their compiling Blazor/Razor code.
- [CompileBlazorInBlazor](https://github.com/BlazorComponents/CompileBlazorInBlazor) for their compiling Blazor/Razor code (older work).
- [BlazorMonaco](https://github.com/serdarciplak/BlazorMonaco) for their code editor.
