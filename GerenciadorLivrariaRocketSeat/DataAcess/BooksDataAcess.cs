using GerenciadorLivrariaRocketSeat.Comunication.Response;
using GerenciadorLivrariaRocketSeat.DataBase;
using GerenciadorLivrariaRocketSeat.Entities;
using System.Data;
using System.Net;

namespace GerenciadorLivrariaRocketSeat.DataAcess;

/// <summary>
/// Classe responsável pelo acesso e manipulação dos dados relacionados aos livros,
/// incluindo operações de inserção, atualização, exclusão e consulta.
/// </summary>
public class BooksDataAcess
{
    /// <summary>
    /// Insere um novo livro na base de dados em memória.
    /// </summary>
    /// <param name="book">Entidade do livro a ser inserida.</param>
    /// <returns>Retorna true se a inserção for bem-sucedida, caso contrário false.</returns>
    public bool Insert(BooksEntity book)
    {
        try
        {
            DataRow row = MainDataBase.ListBooks.NewRow();
            row["Title"] = book.Title;
            row["Author"] = book.Author;
            row["Gender"] = book.Gender;
            row["Price"] = book.Price;
            row["Quantity"] = book.Quantity;

            MainDataBase.Id_Book_Identity++;
            book.Id = MainDataBase.Id_Book_Identity;

            row["Id"] = book.Id;

            MainDataBase.ListBooks.Rows.Add(row);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Atualiza os dados de um livro existente com base no ID.
    /// </summary>
    /// <param name="book">Entidade do livro com os dados atualizados.</param>
    /// <returns>Retorna true se a atualização for bem-sucedida, caso contrário false.</returns>
    public bool Update(BooksEntity book)
    {
        try
        {
            DataRow? row = MainDataBase.ListBooks.Select().Where(x => Convert.ToInt32(x["Id"]) == book.Id).FirstOrDefault();

            if (row == null)
            {
                return false;
            }
            
            row["Title"] = book.Title;
            row["Author"] = book.Author;
            row["Gender"] = book.Gender;
            row["Price"] = book.Price;
            row["Quantity"] = book.Quantity;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Remove um livro da base de dados com base no ID fornecido.
    /// </summary>
    /// <param name="Id">ID do livro a ser removido.</param>
    /// <returns>Retorna true se a exclusão for bem-sucedida, caso contrário false.</returns>
    public bool Delete(int Id)
    {
        try
        {
            DataRow? row = MainDataBase.ListBooks.Select().Where(x => Convert.ToInt32(x["Id"]) == Id).FirstOrDefault();

            if (row == null)
            {
                return false;
            }

            MainDataBase.ListBooks.Rows.Remove(row);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Busca um livro na base de dados com base no ID.
    /// </summary>
    /// <param name="Id">ID do livro a ser localizado.</param>
    /// <returns>Objeto BooksResponse com os dados do livro ou null se não encontrado.</returns>
    public BooksResponse? SelectById(int Id)
    {
        try
        {
            DataRow? row = MainDataBase.ListBooks.Select().Where(x => Convert.ToInt32(x["Id"]) == Id).FirstOrDefault();

            if (row == null)
            {
                return null;
            }

            BooksResponse Response = new BooksResponse
            {
                id = Convert.ToInt32(row["Id"]),
                title = row["Title"].ToString()!,
                author = row["Author"].ToString()!,
                gender = row["Gender"].ToString()!,
                price = row["Price"].ToString()!,
                quantity = row["Quantity"].ToString()!
            };

            return Response;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Retorna todos os livros armazenados na base de dados em memória.
    /// </summary>
    /// <returns>Lista de objetos BooksResponse representando todos os livros cadastrados.</returns>
    public List<BooksResponse> SelectAll()
    {
        try
        {
            List<BooksResponse> listBooks = new List<BooksResponse>();

            foreach (DataRow row in MainDataBase.ListBooks.Rows)
            {
                BooksResponse booksEntity = new BooksResponse
                {
                    id = Convert.ToInt32(row["Id"]),
                    title = row["Title"].ToString()!,
                    author = row["Author"].ToString()!,
                    gender = row["Author"].ToString()!,
                    price = row["Price"].ToString()!,
                    quantity = row["Quantity"].ToString()!
                };

                listBooks.Add(booksEntity);
            }

            return listBooks;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
