using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using questPlayer.App_Start;
using questPlayer.Models;

namespace questPlayer.Controllers
{
    public class QuestController : Controller
    {
        // GET: Quest
        public ActionResult Question(int questId,int questionId, bool startNew=false)
        {
            var quest = Session["quest"] as QuestModel;
            if (startNew||quest==null||quest.QuestId!=questId)
            {
                quest=CreateQuestAndStoreInSession(questId);
            }
            if (questionId==0||questionId==-1)
            {
                var finalMessage = questionId == 0 ? "You Win! " + quest.WinText : "You Lose! " + quest.LoseText;
                return RedirectToAction("WinOrLose", new {message=finalMessage});
            }
            var model = quest.QuestPages.FirstOrDefault(x => x.QuestionId == questionId);
            if (model == null)
            {
                model = quest.QuestPages.FirstOrDefault(x => x.QuestionId == 1); //redirect to 1 question if not found
            }
            model.QuestId = questId;
            return View(model);
        }

        private QuestModel CreateQuestAndStoreInSession(int questId)
        {
            var questFile = FileList.Quests.FirstOrDefault(x => x.Id == questId);
            var quest=new QuestModel();
            quest.QuestId = questId;
            var lines = System.IO.File.ReadLines(questFile.FileName);
            var questPage=new QuestPageModel();
            bool isReadingQuestions = true;
            foreach (var line in lines)
            {
                if (line.StartsWith("*END*"))
                {
                    isReadingQuestions = false;
                }
                else if (line.StartsWith("*Win*"))
                {
                    if (questPage.QuestionId != 0)
                    {
                        quest.QuestPages.Add(questPage);
                    }
                    else
                    {
                        throw new Exception("Quest has no questions but win");
                    }
                    quest.WinText = line.Replace("*Win*", "");
                }
                else if (line.StartsWith("*Lose*"))
                {
                    quest.LoseText = line.Replace("*Lose*", "");
                }
                else if (isReadingQuestions && line.StartsWith("???")) //quest line, change question
                {
                    if (questPage.QuestionId != 0)
                    {
                        quest.QuestPages.Add(questPage);
                        questPage = new QuestPageModel();
                    }
                    var lineSplit = line.Replace("???", "").Split(new[] {"***"}, StringSplitOptions.None);
                    questPage.QuestionId = int.Parse(lineSplit[0]);
                    questPage.Question = lineSplit[1];
                    if (lineSplit.Length > 2)
                    {
                        questPage.Image = lineSplit[2];
                    }
                    if (lineSplit.Length > 3)
                    {
                        questPage.Sound = lineSplit[3];
                    }



                }
                else if(isReadingQuestions && questPage.QuestionId != 0 && !string.IsNullOrEmpty(line))
                {
                    var lineSplit= line.Split(new[] { "***" }, StringSplitOptions.None);
                    questPage.Answers.Add(new Answer()
                    {
                        AnswerText = lineSplit[0],
                        AnswerResult = lineSplit[1],
                        RedirectId = int.Parse(lineSplit[2])
                    });
                }

            }
            Session["quest"] = quest;
            return quest;
        }

        public ActionResult WinOrLose(string message)
        {
            return View(model:message);
        }
    }
}