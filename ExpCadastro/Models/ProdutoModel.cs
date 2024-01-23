namespace ExpCadastro.Models
{
    public record ProdutoModel
    {
        public required string Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
    }
}
