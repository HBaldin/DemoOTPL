using ExpCadastro.Models;

namespace ExpCadastro.Services
{
    public class PessoasService
    {
        private readonly Instrumentation instrumentation;

        public PessoasService(Instrumentation instrumentation)
        {
            this.instrumentation = instrumentation;
        }

        private readonly IList<PessoaModel> Pessoas = new List<PessoaModel>();

        public void SavePessoa(PessoaModel pessoaModel)
        {
            using var activity = instrumentation.ActivitySource.StartActivity("PessoasService - SavePessoa");
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok);
            Pessoas.Add(pessoaModel);
        }
        public IList<PessoaModel> GetPessoas()
        {
            using var activity = instrumentation.ActivitySource.StartActivity("PessoasService - GetPessoas");
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Ok);
            return Pessoas;
        }
    }
}
