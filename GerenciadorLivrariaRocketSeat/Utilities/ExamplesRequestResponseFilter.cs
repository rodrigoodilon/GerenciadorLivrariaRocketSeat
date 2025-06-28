using GerenciadorLivrariaRocketSeat.Comunication.Request;
using GerenciadorLivrariaRocketSeat.Comunication.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace GerenciadorLivrariaRocketSeat.Utilities;

public class ExamplesRequestResponseFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var request = operation.RequestBody;

        var requestType = context.MethodInfo.GetParameters().FirstOrDefault()?.ParameterType;

        List<Dictionary<int, OpenApiExample>> openApiExamples = new List<Dictionary<int, OpenApiExample>>();
        int countExample = 0;

        // BooksController
        if (requestType == typeof(InsertBooksRequest))
        {
            openApiExamples = new List<Dictionary<int, OpenApiExample>>()
                {
                new Dictionary<int, OpenApiExample>()
                {
                    {
                        countExample++,
                        new OpenApiExample()
                        {
                            Summary = "Exemplo de Inclusão de livro",
                            Description = "Exemplo completo de inclusão de um novo livro.",
                            Value = new OpenApiString(
                                JsonSerializer.Serialize(new InsertBooksRequest()
                                {
                                    title = "O Silêncio do 7º Andar",
                                    author = "Rodrigo Odilon",
                                    gender = "Suspense",
                                    price = "35,90",
                                    quantity = "15"
                                }))
                        }
                    }
                }
            };
        }
        else if (requestType == typeof(UpdateBooksRequest))
        {
            openApiExamples = new List<Dictionary<int, OpenApiExample>>()
                {
                new Dictionary<int, OpenApiExample>()
                {
                    {
                        countExample++,
                        new OpenApiExample()
                        {
                            Summary = "Exemplo de Alteração de um livro",
                            Description = "Exemplo completo de alteração de um livro existente.",
                            Value = new OpenApiString(
                                JsonSerializer.Serialize(new UpdateBooksRequest()
                                {
                                    id = 3,
                                    title = "Além do Horizonte Perdido",
                                    author = "Carla Monteiro",
                                    gender = "Aventura",
                                    price = "42,50",
                                    quantity = "20"
                                }))
                        }
                    }
                }
            };
        }

        foreach (var example in openApiExamples)
        {
            foreach (var kvp in example)
            {
                request.Content["application/json"].Examples.Add(kvp.Key.ToString(), kvp.Value);
            }
        }
    }
}
