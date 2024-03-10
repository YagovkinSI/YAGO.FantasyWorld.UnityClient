﻿namespace YAGO.FantasyWorld.Domain.Quests
{
    /// <summary>
    /// Запрос выбора опции квеста
    /// </summary>
    public class SetQuestOptionRequest
    {
        /// <summary>
        /// Идентификатор квеста
        /// </summary>
        public long QuestId { get; set; }

        /// <summary>
        /// Идентификатор опции квеста
        /// </summary>
        public int QuestOptionId { get; set; }
    }
}
