// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;

Console.WriteLine("Iniciando processo de cadastro para os exemplos");

CreatePessoaModel[] models =
[
    //Modelo Certo 1
    new CreatePessoaModel
    {
        Id = "1", Email = "teste@email.com", Nome = "José", ProdutoContratado = "1", Telefone = "12345"
    },
    //Modelo Certo 2
    new CreatePessoaModel
    {
        Id = "2", Email = "teste2@email.com", Nome = "Joaquim", ProdutoContratado = "2", Telefone = "12345"
    },
    //Modelo Certo 3
    new CreatePessoaModel
    {
        Id = "3", Email = "teste3@email.com", Nome = "Maria", ProdutoContratado = "3", Telefone = "12345"
    },
    //Modelo Errado 1
    new CreatePessoaModel
    {
        Id = "4", Email = "teste4@email.com", Nome = "Jim", ProdutoContratado = "4", Telefone = "12345"
    },
    //Modelo Errado 2
    new CreatePessoaModel
    {
        Id = "4", Nome = "Bob", ProdutoContratado = "4"
    }
];

//Dorme 5 segundos
Thread.Sleep(5000);

foreach(var model in models)
{
    Console.WriteLine($"Processando {model.Id}");

    HttpClient httpClient = new HttpClient
    {
        BaseAddress = new Uri("http://host.docker.internal:8080")
    };

    HttpResponseMessage response = await httpClient
        .PostAsJsonAsync("Pessoa", model);

    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}

Console.WriteLine("Finalizado");
