using GerenciadorLivrariaRocketSeat.Comunication.Request;
using GerenciadorLivrariaRocketSeat.Comunication.Response;
using GerenciadorLivrariaRocketSeat.DataAcess;
using GerenciadorLivrariaRocketSeat.Services;
using GerenciadorLivrariaRocketSeat.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorLivrariaRocketSeat.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar as operações relacionadas aos livros,
    /// incluindo inserção, atualização, exclusão e consulta.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        /// <summary>
        /// Retorna os detalhes de um livro com base no ID fornecido.
        /// </summary>
        /// <param name="Id">ID do livro a ser consultado.</param>
        /// <returns>Objeto contendo os dados do livro ou mensagem de erro se não encontrado.</returns>
        /// <response code="200">Sucesso - Livro encontrado e retornado com sucesso.</response>
        /// <response code="404">NotFound - Livro não encontrado para o ID informado.</response>
        /// <response code="500">InternalServerError - Erro inesperado ao buscar o livro.</response> 
        [HttpGet]
        [Route("SelectBooksById{Id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<object>> SelectBooksById([Required][FromRoute] int Id)
        {
            try
            {
                BooksResponse? Response = new BooksResponse();

                BooksDataAcess booksDataAcess = new BooksDataAcess();
                Response = booksDataAcess.SelectById(Id);

                if (Response == null)
                {
                    return NotFound($"Não foi possível localizar o livro para o id informado.");
                }

                return Ok(new { status = 200, Book = Response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retorna a lista de todos os livros registrados no sistema.
        /// </summary>
        /// <returns>Lista de livros ou mensagem de erro se nenhum livro for encontrado.</returns>
        /// <response code="200">Sucesso - Lista de livros retornada com sucesso.</response>
        /// <response code="404">NotFound - Nenhum livro registrado no sistema.</response>
        /// <response code="500">InternalServerError - Erro inesperado ao buscar os livros.</response>
        [HttpGet]
        [Route("SelectAllBooks")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<object>> SelectAllBooks()
        {
            try
            {
                List<BooksResponse> Response = new List<BooksResponse>();

                BooksDataAcess booksDataAcess = new BooksDataAcess();
                Response = booksDataAcess.SelectAll();

                if (Response.Count == 0)
                {
                    return NotFound($"Não existem livros registrados no sistema.");
                }

                return Ok(new { status = 200, Book = Response });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Insere um novo livro no sistema com base nos dados fornecidos na requisição.
        /// </summary>
        /// <param name="Request">Dados do livro a ser inserido.</param>
        /// <returns>Status da operação de inserção.</returns>
        /// <response code="201">Sucesso - O livro foi inserido com sucesso.</response>
        /// <response code="400">BadRequest - Dados inválidos na requisição.</response>
        /// <response code="500">InternalServerError - Erro inesperado durante a inserção do livro.</response>
        [HttpPost]
        [Route("InsertBooks")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [EnableRateLimiting("BooksInsertPolicy")]
        public async Task<ActionResult<object>> InsertBooks([FromBody] InsertBooksRequest Request)
        {
            try
            {
                var validationResult = await BooksValidator.ValidateInsert(Request);

                if (validationResult != null)
                {
                    return BadRequest(validationResult);
                }

                var bookEntity = await BooksService.CreateInsertBooksEntity(Request);

                BooksDataAcess booksDataAcess = new BooksDataAcess();

                if (booksDataAcess.Insert(bookEntity))
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Atualiza os dados de um livro existente com base no ID fornecido.
        /// </summary>
        /// <param name="Request">Dados atualizados do livro.</param>
        /// <returns>Status da operação de atualização.</returns>
        /// <response code="200">Sucesso - O livro foi atualizado com sucesso.</response>
        /// <response code="400">BadRequest - Dados inválidos na requisição.</response>
        /// <response code="404">NotFound - Livro não encontrado para o ID informado.</response>
        /// <response code="500">InternalServerError - Erro inesperado durante a atualização do livro.</response>
        [HttpPut]
        [Route("UpdateBooks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<object>> UpdateBooks( [FromBody] UpdateBooksRequest Request)
        {
            try
            {
                var validationResult = await BooksValidator.ValidateUpdate(Request);

                if (validationResult != null)
                {
                    return BadRequest(validationResult);
                }

                var bookEntity = await BooksService.CreateUpdateBooksEntity(Request);

                BooksDataAcess booksDataAcess = new BooksDataAcess();

                if (!booksDataAcess.Update(bookEntity))
                {
                    return NotFound($"Não foi possível localizar o livro para o id informado.");
                }

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Exclui um livro do sistema com base no ID fornecido.
        /// </summary>
        /// <param name="Id">ID do livro a ser excluído.</param>
        /// <returns>Status da operação de exclusão.</returns>
        /// <response code="200">Sucesso - O livro foi excluído com sucesso.</response>
        /// <response code="404">NotFound - Livro não encontrado para o ID informado.</response>
        /// <response code="500">InternalServerError - Erro inesperado durante a exclusão do livro.</response> 
        [HttpDelete]
        [Route("DeleteBooks{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<object>> DeleteBooks([Required][FromRoute] int Id)
        {
            try
            {
                BooksDataAcess booksDataAcess = new BooksDataAcess();

                if (!booksDataAcess.Delete(Id))
                {
                    return NotFound($"Não foi possível localizar o livro para o id informado.");
                }

                return Ok(new { status = 200, message = "Livro excluído com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
