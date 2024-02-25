using System;

namespace Assets._6_Entities.Quests
{
    /// <summary>
    /// Данные квеста
    /// </summary>
    public class QuestData
    {
        /// <summary>
        /// Флаг готовности квеста
        /// </summary>
        public bool IsQuestReady { get; set; }

        /// <summary>
        /// Время готовности квеста
        /// </summary>
        public DateTimeOffset? QuestReadyDateTime { get; set; }

        /// <summary>
        /// Данные квеста
        /// </summary>
        public QuestWithDetails QuestWithDetails { get; set; }
    }
}
