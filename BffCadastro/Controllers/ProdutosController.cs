using Microsoft.AspNetCore.Mvc;

namespace BffCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://host.docker.internal:8081")
            };

            var response = await httpClient.GetAsync("Produtos");

            return Ok(await response.Content.ReadAsStringAsync());
        }
    }
}
