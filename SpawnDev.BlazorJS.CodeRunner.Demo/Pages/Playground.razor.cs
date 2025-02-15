using BlazorMonaco.Editor;
using Microsoft.AspNetCore.Components;

namespace SpawnDev.BlazorJS.CodeRunner.Demo.Pages
{
    public partial class Playground
    {
        [Inject]
        BlazorJSRuntime JS { get; set; }

        [Inject]
        CompilationService CompilationService { get; set; } = default!;

        Type? CompiledType = null;

        StandaloneCodeEditor _Editor;

        string _textarea = @"@using System
@using System.Threading.Tasks
@using Microsoft.AspNetCore.Components
@using Microsoft.AspNetCore.Components.Rendering
@using SpawnDev.BlazorJS
@using SpawnDev.BlazorJS.JSObjects
@implements IDisposable

<div>
    This component was compiled at runtime
</div>
<div>Type: @this.GetType().FullName</div>
<div>Status: @Status</div>
<div>
    <button @onclick=Clicked @ref=buttonEl>Click Me</button>
</div>

@code {

    ElementReference buttonEl;

    string Status = """";

    [Inject]
    BlazorJSRuntime JS { get; set; } = default!;

    bool _beenInit = false;

    protected override void OnAfterRender(bool firstRender)
    {
        JS.Log(""OnAfterRender"", firstRender, buttonEl);
        if (!_beenInit && !string.IsNullOrEmpty(buttonEl.Id))
        {
            _beenInit = true;
            JS.Log(""_beenInit"");
            Status = ""Rendered!"";
            StateHasChanged();
        }
    }
    void Clicked()
    {
        Status = ""Hello world! "" + DateTime.Now.ToString(""s"");
        StateHasChanged();
        JS.Log(""Clicked"");
    }
    public void Dispose()
    {
        if (_beenInit)
        {
            _beenInit = false;
        }
        JS.Log(""Disposed"");
    }
}
";
        bool _busy = false;
        private StandaloneEditorConstructionOptions EditorConstructionOptions(StandaloneCodeEditor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "csharp",
                Value = _textarea,
                Theme = "vs-dark",
            };
        }

        List<string> CompileLog = new List<string>();
        bool _compileFailed = false;
        async Task CompileAndRun()
        {
            CompiledType = null;
            if (_busy) return;
            CompileLog.Clear();
            _compileFailed = false;
            try
            {
                _busy = true;
                StateHasChanged();
                _textarea = await _Editor.GetValue();
                CompiledType = await CompilationService.CompileRazorCodeToBlazorComponentType(_textarea, async msg =>
                {
                    JS.Log($"Compiler: {msg}");
                    CompileLog.Add(msg);
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
                _compileFailed = CompiledType == null;
                _busy = false;
            }
            StateHasChanged();
        }
    }
}
