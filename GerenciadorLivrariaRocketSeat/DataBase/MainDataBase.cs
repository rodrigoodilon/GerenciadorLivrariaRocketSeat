using System.Data;

namespace GerenciadorLivrariaRocketSeat.DataBase
{
    /// <summary>
    /// Classe que simula o banco de dados principal em memória para armazenamento de livros.
    /// Contém uma tabela com os livros e um identificador incremental.
    /// </summary>
    public class MainDataBase
    {
        /// <summary>
        /// Tabela que armazena os registros dos livros.
        /// </summary>
        public static DataTable ListBooks = new DataTable();

        /// <summary>
        /// Identificador incremental para controle de IDs dos livros.
        /// </summary>
        public static int Id_Book_Identity { get; set; }
    }

}
