var nativePath = Path.Combine(AppContext.BaseDirectory, "native");

Environment.SetEnvironmentVariable(
    "PATH",
    nativePath + Path.PathSeparator + Environment.GetEnvironmentVariable("PATH")
);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.Inject(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Body Metrics Analyzer API",
        Version = "v1",
        Description = "API para análise e comparação de métricas corporais"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Body Metrics Analyzer API v1");
    c.RoutePrefix = "swagger";
});
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();