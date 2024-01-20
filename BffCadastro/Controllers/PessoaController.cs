using BffCadastro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace BffCadastro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> logger;
        private readonly Instrumentation instrumentation;

        public PessoaController(
            ILogger<PessoaController> logger,
            Instrumentation instrumentation)
        {
            this.logger = logger;
            this.instrumentation = instrumentation;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] CreatePessoaModel model)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://host.docker.internal:8081")
            };

            var response = await httpClient.PostAsJsonAsync("Pessoa", model);

            return Created();
        }
    }
}
