using Assets._6_Entities.Quests.Enums;

namespace Assets._6_Entities.Quests
{
    /// <summary>
    /// Результат решения для параметра сущности
    /// </summary>
    public class QuestOptionResultEntityParameter
    {
        /// <summary>
        /// Параметр сущности
        /// </summary>
        public EntityParametres EntityParameter { get; set; }

        /// <summary>
        /// Изменение параметра
        /// </summary>
        public string Change { get; set; }
    }
}
