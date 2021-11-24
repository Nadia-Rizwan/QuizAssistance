
using QuizApp.Models;
using QuizApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizApp.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {


        private QuizDBEntities quizDB;
        // GET: Account

        public AdminController()
        {
            quizDB = new QuizDBEntities();



        }

        // GET: Admin
        public ActionResult Index()
        {

            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.ListofCategory = (from objcat in quizDB.Categories
                                                select new SelectListItem()
                                                {
                                                    Value = objcat.CategoryId.ToString(),
                                                    Text=objcat.CategoryName



                                                }



                                  ).ToList();


            return View(categoryViewModel);
        }
        [HttpPost]
        public JsonResult Index(QuestionOptionViewModel questionOptionViewModel)
        {
            Question question = new Question();
            question.QuestionName = questionOptionViewModel.QuestionName;
            question.CategoryId= questionOptionViewModel.CategoryId;
            question.IsActive= true;
            question.IsMultiple = false;

            quizDB.Questions.Add(question);
            quizDB.SaveChanges();
            int questionId = question.QuestionId;

            foreach (var item in questionOptionViewModel.ListOfOptions) {

                Option option = new Option();
                option.OptioName = item;
                option.QuestionId = questionId;
                quizDB.Options.Add(option);

                quizDB.SaveChanges();
            }
            Answer answer = new Answer();
            answer.AnswerTest = questionOptionViewModel.AnswerText;
            answer.QuestionId = questionId;
            quizDB.Answers.Add(answer);
            quizDB.SaveChanges();



            return Json(data :new  {message="Data successfully added",success=true } ,JsonRequestBehavior.AllowGet);

        }


        }
    }
