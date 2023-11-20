namespace HttpClientTestProject;

public static class EndpointDefinitionExtension
{
    public static IServiceCollection AddEndpointDefinitions(this IServiceCollection services, params Type[] assemblyMarkers)
    {
        List<Type> endpointDefinitions = new();

        foreach (Type marker in assemblyMarkers)
        {
            endpointDefinitions.AddRange(marker.Assembly.ExportedTypes
                .Where(
                    type => typeof(IEndpointDefinition).IsAssignableFrom(type) == true
                    && type.IsInterface == false
                    && type.IsAbstract == false)
                .ToList());
        }

        foreach (Type endpointDefinition in endpointDefinitions)
        {
            services.AddScoped(typeof(IEndpointDefinition), endpointDefinition);
        }

        return services;
    }

    public static IApplicationBuilder UseEndpointDefinitions(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();

        IEnumerable<IEndpointDefinition> definitions = scope.ServiceProvider.GetServices<IEndpointDefinition>();

        foreach (IEndpointDefinition endpointDefinition in definitions)
        {
            endpointDefinition.DefineEndpoints(app);
        }

        return app;
    }
}
