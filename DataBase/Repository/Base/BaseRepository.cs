using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Base
{
    public class BaseRepository<T> where T : DbContext
    {
        protected BaseRepository(T context)
        {
            this._context = context;
        }

        public T _context { get; }
    }
}
