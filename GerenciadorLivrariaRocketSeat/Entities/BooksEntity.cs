using static GerenciadorLivrariaRocketSeat.Entities.Enums;

namespace GerenciadorLivrariaRocketSeat.Entities
{
    /// <summary>
    /// Representa a entidade de um livro utilizada internamente para manipulação e persistência dos dados.
    /// </summary>
    public class BooksEntity
    {
        /// <summary>
        /// Identificador único do livro.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Título do livro.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Autor do livro.
        /// </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// Gênero do livro.
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Preço do livro.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Quantidade disponível do livro.
        /// </summary>
        public int Quantity { get; set; }
    }

}
