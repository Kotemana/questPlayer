using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace questPlayer.Models
{
    public class QuestPageModel
    {
        public QuestPageModel()
        {
            Answers=new List<Answer>();
        }
        public int QuestId { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public List<Answer> Answers { get; set; }

    }
    public class Answer
    {
        public int RedirectId { get; set; }
        public string AnswerText { get; set; }
        public string AnswerResult { get; set; }
    }
}