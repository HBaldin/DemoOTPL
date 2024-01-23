using BffCadastro.Models;
using Microsoft.AspNetCore.Mvc;

namespace BffCadastro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly ILogger<PessoaController> logger;

        public PessoaController(ILogger<PessoaController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPessoas()
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://host.docker.internal:8081")
            };

            HttpResponseMessage response = await httpClient
                .GetAsync("Pessoas");

            return Ok(await response.Content.ReadAsStringAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] CreatePessoaModel model)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://host.docker.internal:8081")
            };

            HttpResponseMessage response = await httpClient
                .PostAsJsonAsync("Pessoas", model);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Erro ao cadastrar nova pessoa na camada de experiência");
                throw new Exception(await response.Content.ReadAsStringAsync());
            }

            return Created();
        }
    }
}
