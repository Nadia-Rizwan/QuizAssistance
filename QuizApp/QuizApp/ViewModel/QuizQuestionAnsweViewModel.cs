using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.ViewModel
{
    public class QuizQuestionAnsweViewModel
    {
        public bool IsLast{get; set;}

        public int OptionId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public List<QuizOption> ListofQuizOption { get; set; }

    }



    public class QuizOption {


        public int OptionId { get; set; }
        public string OptionName { get; set; }



    }
}