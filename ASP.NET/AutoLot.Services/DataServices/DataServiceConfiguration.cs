namespace AutoLot.Services.DataServices;

public static class DataServiceConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICarRepo, CarRepo>();
        services.AddScoped<ICarDriverRepo, CarDriverRepo>();
        services.AddScoped<IMakeRepo, MakeRepo>();
        services.AddScoped<IRadioRepo, RadioRepo>();
        services.AddScoped<IOrderRepo, OrderRepo>();
        services.AddScoped<ICustomerRepo, CustomerRepo>();
        services.AddScoped<IDriverRepo, DriverRepo>();
        services.AddScoped<ICustomerOrderViewModelRepo, CustomerOrderViewModelRepo>();
        services.AddScoped<ICreditRiskRepo, CreditRiskRepo>();

        return services;
    }

    public static IServiceCollection AddDataServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        if (configuration.GetValue<bool>("UseApi"))
        {
            services.AddScoped<ICarDataService, CarApiDataService>();
            services.AddScoped<IMakeDataService, MakeApiDataService>();
        }
        else
        {
            services.AddScoped<ICarDataService, CarDalDataService>();
            services.AddScoped<IMakeDataService, MakeDalDataService>();
        }

        return services;
    }
}
