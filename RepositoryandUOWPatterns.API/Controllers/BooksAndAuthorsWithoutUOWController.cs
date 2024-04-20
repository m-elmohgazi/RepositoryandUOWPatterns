using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryandUOWPatterns.Core.Entities;
using RepositoryandUOWPatterns.Core.InterfacesWithoutUOW;

namespace RepositoryandUOWPatterns.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksAndAuthorsWithoutUOWController : ControllerBase
    {
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IBaseRepository<Author> _authorRepository;

        public BooksAndAuthorsWithoutUOWController(IBaseRepository<Book> bookRepository, IBaseRepository<Author> authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }
        #region Authors
        /// <summary>
        /// GetAuthorById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAuthorById")]
        [HttpGet]
        public IActionResult GetAuthorById(int id)
        {
            var idValue = id <= 0 ? 1 : id;
            var author = _authorRepository.GetById(idValue);
            return Ok(author);
        }
        /// <summary>
        /// GetAuthorByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetAuthorByIdAsync")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var idValue = id <= 0 ? 1 : id;
            var author = await _authorRepository.GetByIdAsync(idValue);
            return Ok(author);
        }
        /// <summary>
        /// GetAllAuthors
        /// </summary>
        /// <returns></returns>
        [Route("GetAllAuthors")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorRepository.GetAllAsync();
            return Ok(authors);
        }
        /// <summary>
        /// GetAuthorByName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("GetAuthorByName")]
        [HttpGet]
        public async Task<IActionResult> GetAuthorByName(string name)
        {
            var author = await _authorRepository.Find(a => a.Name == name);
            return Ok(author);
        }

        [Route("AddAuthor")]
        [HttpGet]
        public async Task<IActionResult> AddAuthor()
        {
            var author = _authorRepository.Add(new Author { Name = "author 5" });
            return Ok(author);
        }
        #endregion

        #region Books

        /// <summary>
        /// GetBookById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            var idValue = id <= 0 ? 1 : id;
            var author = _bookRepository.GetById(idValue);
            return Ok(author);
        }
        /// <summary>
        /// GetAllBooks
        /// </summary>
        /// <returns></returns>
        [Route("GetAllBooks")]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }
        /// <summary>
        /// GetBookByTitle
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("GetBookByTitle")]
        [HttpGet]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            var book = await _bookRepository.Find(b => b.Title == title);
            return Ok(book);
        }
        /// <summary>
        /// GetBookByTitleIncludeAuthor
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("GetBookByTitleIncludeAuthor")]
        [HttpGet]
        public async Task<IActionResult> GetBookByTitleIncludeAuthor(string title)
        {
            var book = await _bookRepository.Find(b => b.Title == title, new[] { "Author" });
            return Ok(book);
        }
        /// <summary>
        /// GetAllBooksIncludeAuthor
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("GetAllBooksIncludeAuthor")]
        [HttpGet]
        public async Task<IActionResult> GetAllBooksIncludeAuthor(string title)
        {
            var book = await _bookRepository.FindAll(b => b.Title.Contains(title), new[] { "Author" });
            return Ok(book);
        }
        /// <summary>
        /// GetBooksPagedIncludeAuthor
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("GetBooksPagedIncludeAuthor")]
        [HttpGet]
        public async Task<IActionResult> GetBooksPagedIncludeAuthor(string title)
        {
            var book = await _bookRepository.FindAll(b => b.Title.Contains(title), new[] { "Author" }, 5, 10);
            return Ok(book);
        }
        /// <summary>
        /// GetBooksPagedIncludeAuthor
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("GetBooksOrdered")]
        [HttpGet]
        //public async Task<IActionResult> GetBooksOrdered(string title, int? skipe, int? take, string orderByDirection)
        public async Task<IActionResult> GetBooksOrdered(string title)
        {
            var book = await _bookRepository.FindAllAsync(b => b.Title.Contains(title), null, null, b => b.Id);
            return Ok(book);
        }

        /// <summary>
        /// AddBook
        /// </summary>
        /// <returns></returns>
        [Route("AddBook")]
        [HttpPost]
        public async Task<IActionResult> AddBook()
        {
            var book = new Book { AuthorId = 1, Title = "Book 15" };
            return Ok(_bookRepository.Add(book));
        }

        /// <summary>
        /// AddBookAndAuthor
        /// This will save author and fail to save book in the database.
        /// </summary>
        /// <returns></returns>
        [Route("AddBookAndAuthor")]
        [HttpPost]
        public async Task<IActionResult> AddBookAndAuthor()
        {
            var author = _authorRepository.Add(new Author { Name = "author 5" });
            var book = new Book { AuthorId = author.Id, Title = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum" };
            return Ok(_bookRepository.Add(book));
        }
        #endregion
    }
}
