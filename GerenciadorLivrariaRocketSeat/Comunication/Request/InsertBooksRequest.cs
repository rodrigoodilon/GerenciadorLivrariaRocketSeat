using static GerenciadorLivrariaRocketSeat.Entities.Enums;

namespace GerenciadorLivrariaRocketSeat.Comunication.Request
{
    /// <summary>
    /// Representa a requisição para operações de inserção ou atualização de livros.
    /// Contém os dados necessários como título, autor, gênero, preço e quantidade.
    /// </summary>
    public class InsertBooksRequest
    {
        /// <summary>
        /// Título do livro.
        /// </summary>
        public string title { get; set; } = string.Empty;

        /// <summary>
        /// Autor do livro.
        /// </summary>
        public string author { get; set; } = string.Empty;

        /// <summary>
        /// Gênero do livro.
        /// </summary>
        public string gender { get; set; } = string.Empty;

        /// <summary>
        /// Preço do livro.
        /// </summary>
        public string price { get; set; } = string.Empty;

        /// <summary>
        /// Quantidade disponível do livro.
        /// </summary>
        public string quantity { get; set; } = string.Empty;

    }

}
