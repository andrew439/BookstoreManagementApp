using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces.Abstract
{
    public interface IBookRepository
    {
        IEnumerable<BookVM> Books { get; }
    }
}
