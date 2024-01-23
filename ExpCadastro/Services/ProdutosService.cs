using ExpCadastro.Models;

namespace ExpCadastro.Services
{
    public class ProdutosService
    {
        private readonly ProdutoModel[] produtos = new[]
        {
            new ProdutoModel { Id = "1", Nome = "Banana", Descricao = "Fruta" },
            new ProdutoModel { Id = "2", Nome = "Maça", Descricao = "Fruta" },
            new ProdutoModel { Id = "3", Nome = "Mamão", Descricao = "Fruta" }
        };

        public ProdutosService() { }

        public IEnumerable<ProdutoModel> GetProdutos()
        {
            return produtos;
        }
    }
}
