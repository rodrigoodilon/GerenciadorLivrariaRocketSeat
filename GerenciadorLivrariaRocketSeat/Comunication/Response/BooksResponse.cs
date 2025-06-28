namespace GerenciadorLivrariaRocketSeat.Comunication.Response
{
    /// <summary>
    /// Representa a resposta retornada ao cliente com os dados de um livro.
    /// Contém informações como ID, título, autor, gênero, preço e quantidade.
    /// </summary>
    public class BooksResponse
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
