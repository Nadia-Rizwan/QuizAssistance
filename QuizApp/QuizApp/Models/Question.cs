//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string QuestionName { get; set; }
        public Nullable<bool> IsMultiple { get; set; }
        public bool IsActive { get; set; }
    }
}
