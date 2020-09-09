using DataAccess.Entity.QuestEntity;
using DataTransfer.Dto.Dtos;
using Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransfer.Dto.Converter
{
    /// <summary>
    /// Handles conversion from <see cref="QuestStep"/> to <see cref="QuestStepDto"/> and vice versa.
    /// </summary>
    public static class QuestStepConverter
    {
        /// <summary>
        /// Converts the given <see cref="QuestStep"/> entity to a <see cref="QuestStepDto"/>.
        /// </summary>
        /// <param name="entity">The entity to transform into its data transfer object equivalent.</param>
        /// <exception cref="ArgumentNullException">Thrown if entity is null.</exception>
        public static QuestStepDto ToDto(this QuestStep entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new QuestStepDto
            {
                Description = entity.Description,
                Name = entity.Name,
                QuestKey = entity.QuestKey,
                QuestStepKey = entity.QuestStepKey,
                SortOrder = entity.SortOrder
            };
        }

        /// <summary>
        /// Converts a given list of <see cref="QuestStep"/> to a list of <see cref="QuestStepDto"/>.
        /// </summary>
        /// <param name="entities">The entities to transform into their data transfer object equivalents.</param>
        /// <exception cref="ArgumentNullException">Thrown if entities is null.</exception>
        public static ICollection<QuestStepDto> ToDtos(this ICollection<QuestStep> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return entities.Select(ToDto).ToList();
        }

        /// <summary>
        /// Converts the given <see cref="QuestStepDto"/> to a <see cref="QuestStep"/>.
        /// </summary>
        /// <param name="dto">The dto to transform into its entity object equivalent.</param>
        /// <exception cref="ArgumentNullException">Thrown if dto is null.</exception>
        public static QuestStep ToEntity(this QuestStepDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new QuestStep
            {
                Description = dto.Description.JsonCleanUp(),
                Name = dto.Name.JsonCleanUp(),
                QuestKey = dto.QuestKey,
                QuestStepKey = dto.QuestStepKey,
                SortOrder = dto.SortOrder
            };
        }

        /// <summary>
        /// Converts a given list of <see cref="QuestStepDto"/> to a list of <see cref="QuestStep"/>.
        /// </summary>
        /// <param name="dtos">The dtos to transform into their entity object equivalents.</param>
        /// <exception cref="ArgumentNullException">Thrown if dtos is null.</exception>
        public static ICollection<QuestStep> ToEntities(this ICollection<QuestStepDto> dtos)
        {
            if (dtos == null) throw new ArgumentNullException(nameof(dtos));

            return dtos.Select(ToEntity).ToList();
        }
    }
}