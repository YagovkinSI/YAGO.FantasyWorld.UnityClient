namespace Assets._6_Entities.Quests
{
    /// <summary>
    /// Данные квеста для пользователя
    /// </summary>
    public class QuestForUser
    {
        /// <summary>
        /// Идентификатор квеста
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентифкатор организации
        /// </summary>
        public long OrganizationId { get; set; }

        /// <summary>
        /// Текст квеста
        /// </summary>
        public string QuestText { get; set; }

        /// <summary>
        /// Варианты решения
        /// </summary>
        public QuestOption[] QuestOptions { get; set; }

    }
}
