namespace Assets._6_Entities.Quests
{
    /// <summary>
    /// Результат решения для одной сущности
    /// </summary>
    public class QuestOptionResultEntity
    {
        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long EntityId { get; set; }

        /// <summary>
        /// Изменнеия параметров
        /// </summary>
        public QuestOptionResultEntityParameter[] QuestOptionResultEntityParameters { get; set; }
    }
}
