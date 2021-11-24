using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizApp.ViewModel
{
    public class AdminViewModel
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Enter Valid email.")]
        public string UserName { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password  is Required")]
        [DataType(DataType.Password)]

        public string UserPassword { get; set; }

        
    }
}