using QuizApp.Models;
using QuizApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private QuizDBEntities quizDB;
     

        public QuizController()
        {
            quizDB = new QuizDBEntities();



        }



        // GET: Quiz
        public ActionResult Index()
        {

            QuizCategoryViewModel quizCategoryViewModel = new QuizCategoryViewModel();
            quizCategoryViewModel.ListofCategory = (from obj in quizDB.Categories
                                                    select new SelectListItem()
                                                    {
                                                        Value = obj.CategoryId.ToString(),
                                                       
                                                        Text = obj.CategoryName
                                                         


                                                    }).ToList();


            return View(quizCategoryViewModel);




        }
        [HttpPost]
        public ActionResult Index(string CandidateName,int CategoryId)
        {

            User objuser = new User();
            objuser.UserName = CandidateName;
            objuser.LoginTime = DateTime.Now;
            objuser.CategoryId = CategoryId;
         
            quizDB.Users.Add(objuser);
            quizDB.SaveChanges();

            Session["CandidateName"] = CandidateName;
            Session["CategoryId"] = CategoryId;
            Session["UserId"] = objuser.UserId;
            return View("QuestionIndex");
        }
        
public PartialViewResult UserQuestionAnswer(CandidateAnswer candidateAnswer) {


           bool IsLast = false;
            if (candidateAnswer.AnswerText != null) {
                List<CandidateAnswer> objcandidateAnswers = Session["CadQuestionAnswer"] as List<CandidateAnswer>;

                if (objcandidateAnswers == null) {
                    objcandidateAnswers = new List<CandidateAnswer>();
                
                }
                objcandidateAnswers.Add(candidateAnswer);
                Session["CadQuestionAnswer"] = objcandidateAnswers;



            }
            
            int pageSize = 1;
            int pageNumber = 0;
          
            int CategoryId = Convert.ToInt32(Session["CategoryId"]);

            if (Session["CadQuestionAnswer"] == null) {

                pageNumber = pageNumber + 1;
            


            }
            else
            {

                List<CandidateAnswer> canAnswer = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
                pageNumber = canAnswer.Count + 1;

              
            }


            List<Question> listofQuestion = new List<Question>();
            listofQuestion = quizDB.Questions.Where(model => model.CategoryId == CategoryId).ToList();

            if (pageNumber == listofQuestion.Count)
            {
                IsLast = true;
            }
                QuizQuestionAnsweViewModel quizQuestionAnsweViewModel = new QuizQuestionAnsweViewModel();
            Question question = new Question();
     
            question = listofQuestion.Skip((pageNumber - 1) * pageSize).Take(pageSize).FirstOrDefault();


            quizQuestionAnsweViewModel.IsLast = IsLast;
            quizQuestionAnsweViewModel.QuestionId = question.QuestionId;
            quizQuestionAnsweViewModel.QuestionName = question.QuestionName;
            quizQuestionAnsweViewModel.ListofQuizOption = (from obj in quizDB.Options
                                                           where obj.QuestionId == question.QuestionId
                                                           select new QuizOption()
                                                           {
                                                               OptionName=obj.OptioName,
                                                               OptionId=obj.OptionId,

                                                           }).ToList();

           


            return PartialView("QuizQuestionOption", quizQuestionAnsweViewModel);
        
        }

        public JsonResult SaveCandidateAnswer(CandidateAnswer objcandidateanswer)
        {
            List<CandidateAnswer> canAnswer = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
            if(objcandidateanswer.AnswerText != null)
            {
                List<CandidateAnswer> objcanAnswer = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
                if(objcanAnswer == null)
                {
                    objcanAnswer = new List<CandidateAnswer>();
                    
                }
                objcanAnswer.Add(objcandidateanswer);
                Session["CadQuestionAnswer"] = objcanAnswer;

            }


            foreach (var item in canAnswer)
            {
                Result objresult = new Result();
                objresult.AnswerTest = item.AnswerText;
                objresult.QuestionId = item.QuestionId;
                objresult.UserId = Convert.ToInt32(Session["UserId"] );


                quizDB.Results.Add(objresult);
                quizDB.SaveChanges();


            }
            
            return Json(new { message="data added",success=true},JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFinalResult() {

            List<CandidateAnswer> canAnswer;
            canAnswer= Session["CadQuestionAnswer"] as List<CandidateAnswer>;

            var ans=quizDB.Answers.ToList();

            var UserResult = (from objResult in canAnswer
                              join objAnswer in quizDB.Answers on objResult.QuestionId equals objAnswer.QuestionId
                              join objQuestion in quizDB.Questions on objResult.QuestionId equals objQuestion.QuestionId
                              select new ResultModel()
                              {

                                  Question = objQuestion.QuestionName,
                                  Answer = objAnswer.AnswerTest,
                                  AnswerByUser = objResult.AnswerText,
                             Status =objAnswer.AnswerTest == objResult.AnswerText ? "Correct" : "Wrong"
                              
                              }).ToList();
            Session.Abandon();
            

            return View(UserResult);
        }

    }
}
