using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RepositoryandUOWPatterns.Core.Entities;
using RepositoryandUOWPatterns.Core.Interfaces;
using RepositoryandUOWPatterns.EF.Repositories;

namespace RepositoryandUOWPatterns.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IBooksRepository _booksRepository;
        public BooksController(IUnitOfWork unitOfWork, IBooksRepository booksRepository)
        {
            _unitOfWork = unitOfWork;
            _booksRepository = booksRepository;
        }

        /// <summary>
        /// GetBookById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBookById(int id)
        {
            var idValue = id <= 0 ? 1 : id;
            //var author = _unitOfWork.Books.GetById(idValue);
            var author = _booksRepository.GetById(idValue);
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
            //var books = await _unitOfWork.Books.GetAllAsync();
            var books = await _booksRepository.GetAllAsync();
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
            //var book = await _unitOfWork.Books.Find(b => b.Title == title);
            var book = await _booksRepository.Find(b => b.Title == title);
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
            //var book = await _unitOfWork.Books.Find(b => b.Title == title, new[] { "Author" });
            var book = await _booksRepository.Find(b => b.Title == title, new[] { "Author" });
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
            //var book = await _unitOfWork.Books.FindAll(b => b.Title.Contains(title), new[] { "Author" });
            var book = await _booksRepository.FindAll(b => b.Title.Contains(title), new[] { "Author" });
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
            //var book = await _unitOfWork.Books.FindAll(b => b.Title.Contains(title), new[] { "Author" }, 5, 10);
            var book = await _booksRepository.FindAll(b => b.Title.Contains(title), new[] { "Author" }, 5, 10);

            return Ok(book);
        }
        /// <summary>
        /// GetBooksOrdered
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [Route("GetBooksOrdered")]
        [HttpGet]
        //public async Task<IActionResult> GetBooksOrdered(string title, int? skipe, int? take, string orderByDirection)
        public async Task<IActionResult> GetBooksOrdered(string title)
        {
            //var book = await _unitOfWork.Books.FindAllAsync(b => b.Title.Contains(title), null,null, b => b.Id);
            var book = await _booksRepository.FindAllAsync(b => b.Title.Contains(title), null, null, b => b.Id);
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
            //var b = _unitOfWork.Books.Add(book);
            var b = _booksRepository.Add(book);


            _unitOfWork.Complete();

            return Ok(b);
        }

        /// <summary>
        /// AddBookAndAuthor
        /// </summary>
        /// <returns></returns>
        [Route("AddBookAndAuthor")]
        [HttpPost]
        public async Task<IActionResult> AddBookAndAuthor()
        {
            var author = _unitOfWork.Authors.Add(new Author { Name = "author 5" });
            var book = new Book { AuthorId = author.Id, Title = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum" };
            var b = _unitOfWork.Books.Add(book);
            //var b = _booksRepository.Add(book);
            
            _unitOfWork.Complete();

            return Ok(b);
        }
    }
}
