using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataObjects;
using LogicLayer;

namespace MVCPresentation.Models
{
    public class BooksViewModel
    {
        public BookSortOptions? Sort { get; set; }
        public BookCategoryFilterOptions? CategoryFilterOptions { get; set; }
        public IEnumerable<Classification> ConditionOptions { get; set; }
        public IEnumerable<Classification> GenreOptions { get; set; }
        public IEnumerable<BookVM> BookVMs { get; set; }
        public BookVM BookVM { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public string Genre { get; set; }
        public int Count { get; set; }
        
    }

    public class CreateBookViewModel
    {
        public Book Book { get; set; }

        [Display(Name = "Authors (Separate with commas)")]
        [Required(ErrorMessage = "Please enter an author.")]
        public string Authors { get; set; }
    }

    public class CustomerBooksViewModel
    {
        public IEnumerable<BookVM> BookVMs { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public int Count { get; set; }
    }


    public enum BookSortOptions
    {
        [Display(Name = "Sort by Title")]
        Title,
        //[Display(Name = "Sort by Author")]
        //Author,
        [Display(Name = "Sort by Price")]
        Price
    }

    public enum BookCategoryFilterOptions
    {
        Fiction,
        Nonfiction
    }
}