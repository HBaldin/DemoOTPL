using ExpCadastro;
using ExpCadastro.Services;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<Instrumentation>();
builder.Services.AddSingleton<ProdutosService>();
builder.Services.AddSingleton<PessoasService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//OTEL
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
        resource.AddService(
            serviceName: "ExpCadastro",
            serviceVersion: "1.0.0"))
    .WithTracing(tracing => tracing
        .AddSource(Instrumentation.ActivitySourceName)
        .AddAspNetCoreInstrumentation(InstrumentationConfiguration.Configure)
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new("http://otel-collector:4317");
        }))
    .WithMetrics(metrics => metrics
        .AddMeter(Instrumentation.MeterName)
        .AddAspNetCoreInstrumentation()
        .AddRuntimeInstrumentation()
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new("http://otel-collector:4317");
        }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
