using System.Text;
using System.Text.Json;

namespace BffCadastro.Middlewares
{
    public class RequestResponseTracerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly Instrumentation instrumentation;

        public RequestResponseTracerMiddleware(
            RequestDelegate next,
            Instrumentation instrumentation)
        {
            this.next = next;
            this.instrumentation = instrumentation;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using var activity = instrumentation.ActivitySource.StartActivity("Start Request");

            var httpRequest = context.Request;

            //Pega as informações da request
            httpRequest.EnableBuffering();
            httpRequest.Body.Position = 0;
            var reader = new StreamReader(httpRequest.Body, Encoding.UTF8);
            var requestBody = await reader.ReadToEndAsync();
            httpRequest.Body.Position = 0;

            activity?.SetTag("http.request.headers", JsonSerializer.Serialize(httpRequest.Headers));
            activity?.SetTag("http.request.body", requestBody);

            //Executa a request
            await next(context);
        }
    }
}
