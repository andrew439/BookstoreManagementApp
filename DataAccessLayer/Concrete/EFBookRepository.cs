using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces.Abstract;
using DataObjects;

namespace DataAccessLayer
{
    public class EFBookRepository : IBookRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<BookVM> Books
        {
            get { return context.Books; }
        }
    }
}
