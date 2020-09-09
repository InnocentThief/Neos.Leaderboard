using DataAccess.Entity.QuestEntity;
using DataTransfer.Dto.Dtos;
using Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransfer.Dto.Converter
{
    /// <summary>
    /// Handles conversion from <see cref="Quest"/> to <see cref="QuestDto"/> and vice versa.
    /// </summary>
    public static class QuestConverter
    {
        /// <summary>
        /// Converts the given <see cref="Quest"/> entity to a <see cref="QuestDto"/>.
        /// </summary>
        /// <param name="entity">The entity to transform into its data transfer object equivalent.</param>
        /// <exception cref="ArgumentNullException">Thrown if entity is null.</exception>
        public static QuestDto ToDto(this Quest entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new QuestDto
            {
                AccountKey = entity.AccountKey,
                Description = entity.Description,
                Name = entity.Name,
                QuestKey = entity.QuestKey,
                StepCount = entity.QuestSteps != null ? entity.QuestSteps.Count().ToString() : "0"
            };
        }

        /// <summary>
        /// Converts a given list of <see cref="Quest"/> to a list of <see cref="QuestDto"/>.
        /// </summary>
        /// <param name="entities">The entities to transform into their data transfer object equivalents.</param>
        /// <exception cref="ArgumentNullException">Thrown if entities is null.</exception>
        public static ICollection<QuestDto> ToDtos(this ICollection<Quest> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return entities.Select(ToDto).ToList();
        }

        /// <summary>
        /// Converts the given <see cref="QuestDto"/> to a <see cref="Quest"/> entity.
        /// </summary>
        /// <param name="dto">The dto to transform into its entity object equivalent.</param>
        /// <exception cref="ArgumentNullException">Thrown if dto is null.</exception>
        public static Quest ToEntity(this QuestDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Quest
            {
                AccountKey = dto.AccountKey,
                Description = dto.Description.JsonCleanUp(),
                Name = dto.Name.JsonCleanUp(),
                QuestKey = dto.QuestKey
            };
        }

        /// <summary>
        /// Converts a given list of <see cref="QuestDto"/> to a list of <see cref="Quest"/>.
        /// </summary>
        /// <param name="dtos">The dtos to transform into their entity object equvalents.</param>
        /// <exception cref="ArgumentNullException">Thrown if dtos is null.</exception>
        public static ICollection<Quest> ToEntities(this ICollection<QuestDto> dtos)
        {
            if (dtos == null) throw new ArgumentNullException(nameof(dtos));

            return dtos.Select(ToEntity).ToList();
        }
    }
}