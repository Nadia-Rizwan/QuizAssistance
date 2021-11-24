using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.ViewModel
{
    public class CategoryViewModel
    {
     
        [Display(Name ="Category")]
        [Required(ErrorMessage ="Category is required")]
     public string  CategoryId { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "Question is required")]
        public string QuestionName { get; set; }
        public string CategoryName { get; set; }
        public string OptionName { get; set; }
        public IEnumerable<SelectListItem>  ListofCategory { get; set; }



    }
}