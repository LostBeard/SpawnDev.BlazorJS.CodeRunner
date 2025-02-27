﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using SpawnDev.BlazorJS;
using SpawnDev.BlazorJS.CodeRunner;
using SpawnDev.BlazorJS.CodeRunner.Demo;
using SpawnDev.BlazorJS.CodeRunner.Demo.Layout;
using SpawnDev.BlazorJS.CodeRunner.Demo.Layout.AppTray;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazorJSRuntime(out var JS);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

if (JS.IsWindow)
{
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<AppTrayService>();
builder.Services.AddScoped<MainLayoutService>();
builder.Services.AddScoped<ThemeTrayIconService>();

// Adds the CompilationService as a scoped service
builder.Services.AddCompilerService();

await builder.Build().BlazorJSRunAsync();
