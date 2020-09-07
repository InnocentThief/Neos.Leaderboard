using DataAccess.Entity.QuestEntity;
using DataTransfer.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransfer.Dto.Converter
{
    /// <summary>
    /// Handles conversion from <see cref="QuestLeaderboardEntry"/> to <see cref="QuestLeaderboardEntryDto"/> and vice versa.
    /// </summary>
    public static class QuestLeaderboardEntryConverter
    {
        /// <summary>
        /// Converts a <see cref="QuestLeaderboardEntry"/> to its data transfer object equivalent.
        /// </summary>
        /// <param name="entity">The entity to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if entity is null.</exception>
        public static QuestLeaderboardEntryDto ToDto(this QuestLeaderboardEntry entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new QuestLeaderboardEntryDto
            {
                QuestStepsDone = entity.QuestStepsDone,
                Username = entity.Username
            };
        }

        /// <summary>
        /// Converts a list of <see cref="QuestLeaderboardEntry"/> to a list of <see cref="QuestLeaderboardEntryDto"/>.
        /// </summary>
        /// <param name="entities">The list of entity to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if entities is null.</exception>
        public static ICollection<QuestLeaderboardEntryDto> ToDtos(this ICollection<QuestLeaderboardEntry> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return entities.Select(ToDto).ToList();
        }
    }
}