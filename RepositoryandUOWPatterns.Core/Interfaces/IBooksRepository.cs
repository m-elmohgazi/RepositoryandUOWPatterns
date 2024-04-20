using RepositoryandUOWPatterns.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryandUOWPatterns.Core.Interfaces
{
    public interface IBooksRepository :IBaseRepository<Book>
    {
        IEnumerable<Book> SpecialMethod();
    }
}
