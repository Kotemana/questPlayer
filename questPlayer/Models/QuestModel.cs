using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace questPlayer.Models
{
    public class QuestModel
    {
        public QuestModel()
        {
            QuestPages=new List<QuestPageModel>();
        }
        public List<QuestPageModel> QuestPages { get; set; } 
        public int QuestId { get; set; }
        public string WinText { get; set; }
        public string LoseText { get; set; }
        public int HP { get; set; }
        public int Mana { get; set; }
        public int SuperMana { get; set; }
    }
}