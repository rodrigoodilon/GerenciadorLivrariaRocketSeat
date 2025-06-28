using GerenciadorLivrariaRocketSeat.Comunication.Request;
using GerenciadorLivrariaRocketSeat.Entities;
using GerenciadorLivrariaRocketSeat.Utilities;

namespace GerenciadorLivrariaRocketSeat.Services
{
    /// <summary>
    /// Serviço responsável pela criação da entidade BooksEntity a partir de um BooksRequest.
    /// </summary>
    public class BooksService
    {
        /// <summary>
        /// Converte os dados da requisição em uma entidade de livro.
        /// </summary>
        /// <param name="request">Dados da requisição do livro.</param>
        /// <returns>Objeto BooksEntity preenchido.</returns>
        public static async Task<BooksEntity> CreateInsertBooksEntity(InsertBooksRequest request)
        {
            try
            {
                BooksEntity book = new BooksEntity
                {
                    Title = request.title.Trim(),
                    Author = request.author.Trim(),
                    Gender = request.gender.Trim(),
                    Price = Convert.ToDecimal(request.price),
                    Quantity = Convert.ToInt32(request.quantity)
                };

                return book;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cria uma entidade BooksEntity com base nos dados fornecidos em uma requisição de atualização.
        /// </summary>
        /// <param name="request">Objeto UpdateBooksRequest contendo os dados atualizados do livro.</param>
        /// <returns>Objeto BooksEntity preenchido com os dados da requisição.</returns>
        public static async Task<BooksEntity> CreateUpdateBooksEntity(UpdateBooksRequest request)
        {
            try
            {
                BooksEntity book = new BooksEntity
                {
                    Id = request.id,
                    Title = request.title.Trim(),
                    Author = request.author.Trim(),
                    Gender = request.gender.Trim(),
                    Price = Convert.ToDecimal(request.price),
                    Quantity = Convert.ToInt32(request.quantity)
                };

                return book;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
