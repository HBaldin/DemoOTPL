using ExpCadastro.Models;
using ExpCadastro.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpCadastro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly ILogger<PessoasController> logger;
        private readonly Instrumentation instrumentation;
        private readonly PessoasService pessoasService;
        private readonly ProdutosService produtosService;

        public PessoasController(
            ILogger<PessoasController> logger,
            Instrumentation instrumentation,
            PessoasService pessoasService,
            ProdutosService produtosService)
        {
            this.logger = logger;
            this.instrumentation = instrumentation;
            this.produtosService = produtosService;
            this.pessoasService = pessoasService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using var activity = instrumentation.ActivitySource.StartActivity("PessoasController Start Get Request");
            
            return Ok(new
            {
                data = pessoasService.GetPessoas()
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] PessoaModel model)
        {
            using var activity = instrumentation.ActivitySource.StartActivity("PessoasController Start POST Request");
            logger.LogInformation("Recebida request de cadastro de pessoa");

            bool existeProduto = produtosService.GetProdutos()
                .Any(x => x.Id == model.Id);

            //Caso não exista o produto
            if (!existeProduto)
            {
                logger.LogWarning("Produto informado é inválido para o cadastro");
                activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok);
                return BadRequest(new { error = "Produto informado é inválido para o cadastro" });
            }

            //Adiciona pessoas a lista
            pessoasService.SavePessoa(model);

            //Atualiza as métricas
            instrumentation.PessoasCadastradas.Add(1);

            logger.LogInformation("Pessoa cadastrada com sucesso");

            //Retorna ok
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok);
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
