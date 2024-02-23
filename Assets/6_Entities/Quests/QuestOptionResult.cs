namespace Assets._6_Entities.Quests
{
    /// <summary>
    /// Результат решения квеста
    /// </summary>
    public class QuestOptionResult
    {
        /// <summary>
        /// Описание результа
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Вес результа
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Изменения параметров сущностей по результатам
        /// </summary>
        public QuestOptionResultEntity[] QuestOptionResultEntities { get; set; }
    }
}
