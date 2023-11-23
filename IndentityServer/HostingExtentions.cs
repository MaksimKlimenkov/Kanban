using Microsoft.IdentityModel.Logging;

namespace Kanban.IndentityServer;

internal static class HostingExtentions
{
    public static void ConfigureIndentityServices(this WebApplicationBuilder builder)
    {
        IdentityModelEventSource.ShowPII = true;
        builder.Services.AddIdentityServer()
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients);
    }
}