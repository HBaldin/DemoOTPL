using ExpCadastro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpCadastro.Controllers
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
        public IActionResult Post(
            [FromBody] CreatePessoaModel model)
        {
            instrumentation.PessoasCadastradas.Add(1);
            
            return Ok(new
            {
                data = new
                {
                    Mensagem = "Pessoa Salva com sucesso"
                }
            });
        }
    }
}
