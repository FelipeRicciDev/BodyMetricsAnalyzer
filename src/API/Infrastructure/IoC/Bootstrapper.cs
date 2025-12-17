namespace API.Infrastructure.IoC;

public static class Bootstrapper
{
    public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
    {
        InjectMediator(services);
        InjectApplicationServices(services);

        return services;
    }

    public static void InjectMediator(IServiceCollection services)
    {
        var assemblies = new Assembly[]
        {
            typeof(CompareBodyCompositionCommandHandler).Assembly
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
        //Interface
        services.AddScoped<IPdfTextExtractor, PdfTextOcrExtractor>();

        //Service
        services.AddScoped<OcrService>();
        services.AddScoped<PdfImageExtractor>();
    }
}