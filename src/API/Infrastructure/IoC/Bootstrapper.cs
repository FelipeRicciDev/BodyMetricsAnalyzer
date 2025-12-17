namespace API.Infrastructure.IoC;

public static class Bootstrapper
{
    public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
    {
        InjectMediator(services);
        //InjectRepositories(services);
        InjectApplicationServices(services);

        return services;
    }

    public static void InjectMediator(IServiceCollection services)
    {
        var assemblies = new Assembly[]
        {
            typeof(ProcessBodyMetricsFilesCommandHandler).Assembly,
            typeof(AnalyzeBodyMetricsCommandHandler).Assembly
        };

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                assemblies
            );
        });
    }

    public static void InjectApplicationServices(IServiceCollection services)
    {
        services.AddScoped<PdfImageExtractor>();
        services.AddScoped<OcrService>();
    }

    //public static void InjectRepositories(IServiceCollection services)
    //{
    //    services.AddSingleton<IEntityRepository>(sp =>
    //    {
    //        //return new EntityRepository("entity");
    //    });
    //}
}