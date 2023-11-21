using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;

namespace LogicLayerInterfaces
{
    public interface IBookRepository
    {
        IEnumerable<BookVM> BookVM { get; }
        void SaveBook(BookVM bookVM);
        BookVM DeleteBook(string ISBN);
    }
}
