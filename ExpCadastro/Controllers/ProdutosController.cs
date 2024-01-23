using ExpCadastro.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpCadastro.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly Instrumentation _instrumentation;
        private readonly ProdutosService _produtosService;

        public ProdutosController(
            ILogger<ProdutosController> logger,
            Instrumentation instrumentation,
            ProdutosService produtosService)
        {
            _logger = logger;
            _instrumentation = instrumentation;
            _produtosService = produtosService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using var activity = _instrumentation.ActivitySource.StartActivity("ProdutosController GET");
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok);
            return Ok(new { data = _produtosService.GetProdutos() });
        }
    }
}
