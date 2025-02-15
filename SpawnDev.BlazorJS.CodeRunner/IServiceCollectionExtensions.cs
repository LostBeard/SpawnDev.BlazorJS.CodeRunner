using MetadataReferenceService.BlazorWasm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SpawnDev.BlazorJS.CodeRunner
{
    /// <summary>
    /// IServiceCollection extension methods
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Add the CompilerService service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCompilerService(this IServiceCollection services)
        {
            services.TryAddScoped<BlazorWasmMetadataReferenceService>();
            //services.TryAddScoped<CompileService>();
            services.TryAddScoped<CompilationService>();
            return services;
        }
    }
}
