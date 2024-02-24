using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._4_Widgets.Quest.Models
{
    internal class SetQuestOptionRequest
    {
        public SetQuestOptionRequest(long questId, int questOptionId)
        {
            QuestId = questId;
            QuestOptionId = questOptionId;
        }

        public long QuestId { get; set; }

        public int QuestOptionId { get; set; }
        
    }
}
