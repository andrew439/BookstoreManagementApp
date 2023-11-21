using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Book
    {
        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "Please enter an ISBN.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Choose a category:")]
        [Display(Name = "Category")]
        public string BookCategoryID { get; set; }

        [Display(Name = "Condition")]
        [Required(ErrorMessage = "Choose a condition:")]
        public string BookConditionID { get; set; }

        [Required(ErrorMessage = "Choose a genre:")]
        [Display(Name = "Genre")]
        public string BookGenreID { get; set; }

        [Display(Name = "Publisher")]
        [Required(ErrorMessage = "Please enter a publisher.")]
        public string Publisher { get; set; }

        [Display(Name = "Publication Date")]
        [Required(ErrorMessage = "Please enter a publication date.")]
        public DateTime PublicationDate { get; set; }

        [Display(Name = "Wholesale Price")]
        [Required(ErrorMessage = "Please enter a wholesale price.")]
        public decimal WholesalePrice { get; set; }

        [Display(Name = "Sale Price")]
        [Required(ErrorMessage = "Please enter a sale price.")]
        public decimal SalePrice { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please enter a quantity.")]
        public int Quantity { get; set; }

        [Display(Name = "Quantity By Title")]
        [Required(ErrorMessage = "Please enter a quantity by title.")]
        public int QuantityByTitle { get; set; }

        [Display(Name = "Location ID")]
        [Required(ErrorMessage = "Please enter a location ID.")]
        public string LocationID { get; set; }
        public bool Active { get; set; }
    }

    public class BookVM : Book
    {
        [Display(Name = "Author(s)")]
        [Required(ErrorMessage = "Please enter an author.")]
        public List<Author> Authors { get; set; }
    }
}
