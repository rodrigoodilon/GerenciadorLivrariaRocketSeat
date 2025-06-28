namespace GerenciadorLivrariaRocketSeat.Comunication.Request
{
    /// <summary>
    /// Representa a requisição utilizada para atualizar os dados de um livro existente.
    /// Contém os campos que podem ser modificados, como título, autor, gênero, preço e quantidade.
    /// </summary>
    public class UpdateBooksRequest
    {
        /// <summary>
        /// Identificador único do livro.
        /// </summary>
        public int id { get; set; }
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
