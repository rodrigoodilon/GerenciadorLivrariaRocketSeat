using GerenciadorLivrariaRocketSeat.Comunication.Request;
using GerenciadorLivrariaRocketSeat.Utilities;

namespace GerenciadorLivrariaRocketSeat.Validators
{
    /// <summary>
    /// Classe responsável pela validação dos dados de requisição relacionados aos livros.
    /// </summary>
    public class BooksValidator
    {
        /// <summary>
        /// Valida os dados fornecidos em uma requisição de livro.
        /// </summary>
        /// <param name="request">Objeto contendo os dados do livro a serem validados.</param>
        /// <returns>Objeto com mensagens de erro se houverem, ou null se os dados forem válidos.</returns>
        public async static Task<object?> ValidateInsert(InsertBooksRequest request)
        {
			try
			{
                if (string.IsNullOrEmpty(request.title?.Trim()))
                {
                    return new { status = 400, message = "Título não informado" };
                }

                if (string.IsNullOrEmpty(request.author?.Trim()))
                {
                    return new { status = 400, message = "Autor não informado" };
                }

                if (string.IsNullOrEmpty(request.gender?.Trim()))
                {
                    return new { status = 400, message = "Genêro não informado" };
                }

                if (string.IsNullOrEmpty(request.price?.Trim()) || Functions.IsNumeric(request.price) == false)
                {
                    return new { status = 400, message = "Preço não informado ou inválido" };
                }

                if (string.IsNullOrEmpty(request.quantity?.Trim()) || Functions.IsNumeric(request.quantity) == false)
                {
                    return new { status = 400, message = "Quantidade não informada ou inválida" };
                }

                return null; // Retorne null se todas as validações passarem
            }
			catch (Exception ex)
			{
				throw ex;
			}
        }

        /// <summary>
        /// Valida os dados fornecidos em uma requisição de atualização de livro.
        /// </summary>
        /// <param name="request">Objeto contendo os dados do livro a serem validados para atualização.</param>
        /// <returns>
        /// Objeto contendo mensagens de erro caso existam, ou null se os dados forem considerados válidos.
        /// </returns>
        public async static Task<object?> ValidateUpdate(UpdateBooksRequest request)
        {
            try
            {
                if (request.id <= 0)
                {
                    return new { status = 400, message = "Id inválido" };
                }

                if (string.IsNullOrEmpty(request.title?.Trim()))
                {
                    return new { status = 400, message = "Título não informado" };
                }

                if (string.IsNullOrEmpty(request.author?.Trim()))
                {
                    return new { status = 400, message = "Autor não informado" };
                }

                if (string.IsNullOrEmpty(request.gender?.Trim()))
                {
                    return new { status = 400, message = "Genêro não informado" };
                }

                if (string.IsNullOrEmpty(request.price?.Trim()) || Functions.IsNumeric(request.price) == false)
                {
                    return new { status = 400, message = "Preço não informado ou inválido" };
                }

                if (string.IsNullOrEmpty(request.quantity?.Trim()) || Functions.IsNumeric(request.quantity) == false)
                {
                    return new { status = 400, message = "Quantidade não informada ou inválida" };
                }

                return null; // Retorne null se todas as validações passarem
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
