namespace BffCadastro.Models
{
    public record CreatePessoaModel
    {
        public required string Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Telefone { get; set; }
        public required string ProdutoContratado { get; set; }
    }
}
