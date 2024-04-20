using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryandUOWPatterns.Core.Entities;
using RepositoryandUOWPatterns.Core.Interfaces;
using RepositoryandUOWPatterns.EF;

namespace RepositoryandUOWPatterns.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
            var author = _unitOfWork.Authors.GetById(idValue);
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
            var author = await _unitOfWork.Authors.GetByIdAsync(idValue);
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
            var authors = await _unitOfWork.Authors.GetAllAsync();
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
            var author = await _unitOfWork.Authors.Find(a => a.Name == name);
            return Ok(author);
        }

        [Route("AddAuthor")]
        [HttpGet]
        public async Task<IActionResult> AddAuthor()
        {
            var author = _unitOfWork.Authors.Add(new Author { Name = "author 5" });
            _unitOfWork.Complete();
            return Ok(author);
        }
    }
}
