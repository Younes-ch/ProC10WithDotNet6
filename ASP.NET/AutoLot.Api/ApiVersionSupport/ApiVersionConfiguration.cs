namespace AutoLot.Api.ApiVersionSupport;

public static class ApiVersionConfiguration
{
    public static IServiceCollection AddAutoLotApiVersionConfiguration(this IServiceCollection services, ApiVersion defaultVersion = null)
    {
        defaultVersion ??= ApiVersion.Default;

        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = defaultVersion;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new QueryStringApiVersionReader(), // default to "api-version"
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("v"),
                    new HeaderApiVersionReader("api-version"),
                    new MediaTypeApiVersionReader(), // default to "v"
                    new MediaTypeApiVersionReader("api-version"));
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = defaultVersion;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}
