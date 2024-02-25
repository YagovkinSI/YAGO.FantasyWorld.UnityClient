﻿using Assets._6_Entities.Quests.Enums;
using System;

namespace Assets._6_Entities.Quests
{
    /// <summary>
    /// Данные квеста
    /// </summary>
    public class Quest
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
        /// Дата создания квеста
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Тип квеста
        /// </summary>
        public QuestType Type { get; set; }

        /// <summary>
        /// Первая сущность квеста
        /// </summary>
        public long QuestEntity1Id { get; set; }

        /// <summary>
        /// Статус квеста
        /// </summary>
        public QuestStatus Status { get; set; }
    }
}
