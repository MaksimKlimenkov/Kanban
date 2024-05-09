using System.Reflection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Kanban.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureOptions(this IServiceCollection services)
    {
        var types = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.GetInterface(typeof(IConfigureOptions<>).Name) is not null)
            .Select(type => new { T = type, I = type.GetInterface(typeof(IConfigureOptions<>).Name)! })
            .ToList();
        foreach (var type in types) services.AddTransient(type.I, type.T);
    }
}