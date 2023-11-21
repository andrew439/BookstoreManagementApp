using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(BookVM bookVM, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.BookVM.ISBN == bookVM.ISBN)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    BookVM = bookVM,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(BookVM bookVM)
        {
            lineCollection.RemoveAll(l => l.BookVM.ISBN == bookVM.ISBN);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.BookVM.SalePrice * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
        public class CartLine
        {
            public BookVM BookVM { get; set; }
            public int Quantity { get; set; }
        }
    }
}
