using Microsoft.Identity.Client;
using RepositoryandUOWPatterns.Core.Entities;
using RepositoryandUOWPatterns.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryandUOWPatterns.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IBaseRepository<Author> Authors { get; private set; }
        public IBaseRepository<Book> Books { get; private set; }
        
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Authors = new BaseRepository<Author>(_dbContext);
            Books = new BaseRepository<Book>(_dbContext);
        }
            
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
