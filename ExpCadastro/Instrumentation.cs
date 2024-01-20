using OpenTelemetry.Instrumentation.AspNetCore;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace ExpCadastro
{
    public class Instrumentation : IDisposable
    {
        internal const string ActivitySourceName = "DemoOTPL.ExpCadastro";
        internal const string MeterName = "DemoOTPL.ExpCadastro";
        private readonly Meter meter;
        public ActivitySource ActivitySource { get; }
        public Counter<long> PessoasCadastradas { get; }

        public Instrumentation()
        {
            string? version = typeof(Instrumentation).Assembly
                .GetName()
                .Version?
                .ToString();
            ActivitySource = new ActivitySource(ActivitySourceName, version);
            meter = new Meter(MeterName, version);
            PessoasCadastradas = meter.CreateCounter<long>(
                name: "expcadastro.pessoas.cadastradas",
                description: "Número de pessoas cadastradas");
        }

        public void Dispose()
        {
            ActivitySource.Dispose();
            meter.Dispose();
        }
    }

    public static class InstrumentationConfiguration
    {
        public static Action<AspNetCoreTraceInstrumentationOptions> Configure => (options) =>
        {
            options.EnrichWithHttpRequest = async (activity, httpRequest) =>
            {
                httpRequest.EnableBuffering();
                httpRequest.Body.Position = 0;
                StreamReader reader = new StreamReader(httpRequest.Body, System.Text.Encoding.UTF8);
                string requestBody = await reader.ReadToEndAsync();
                httpRequest.Body.Position = 0;
                activity.SetTag("http.request.headers", JsonSerializer.Serialize(httpRequest.Headers));
                activity.SetTag("http.request.body", requestBody);
            };
        };
    }
}
