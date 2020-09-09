using DataAccess.Entity.GameEntity;
using DataTransfer.Dto.Dtos;
using Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransfer.Dto.Converter
{
    /// <summary>
    /// Handles conversion from <see cref="Game"/> to <see cref="GameDto"/> and vice versa.
    /// </summary>
    public static class GameConverter
    {
        /// <summary>
        /// Converts a <see cref="Game"/> to its data transfer object equivalent.
        /// </summary>
        /// <param name="entity">The entity to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if entity is null.</exception>
        public static GameDto ToDto(this Game entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new GameDto
            {
                AccountKey = entity.AccountKey,
                GameKey = entity.GameKey,
                Name = entity.Name
            };
        }

        /// <summary>
        /// Converts a list of <see cref="Game"/> to a list of <see cref="GameDto"/>.
        /// </summary>
        /// <param name="entities">The list of entity to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if entities is null.</exception>
        public static ICollection<GameDto> ToDtos(this ICollection<Game> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            return entities.Select(ToDto).ToList();
        }

        /// <summary>
        /// Converts a <see cref="GameDto"/> to its entity object equivalent.
        /// </summary>
        /// <param name="dto">The dto to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if dto is null.</exception>
        public static Game ToEntity(this GameDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Game
            {
                AccountKey = dto.AccountKey,
                GameKey = dto.GameKey,
                Name = dto.Name.JsonCleanUp()
            };
        }

        /// <summary>
        /// Converts a list of<see cref="GameDto"/> to a list of <see cref="Game"/>.
        /// </summary>
        /// <param name="dtos">The list of dtos to convert.</param>
        /// <exception cref="ArgumentNullException">Thrown if dtos is null.</exception>
        public static ICollection<Game> ToEntities(this ICollection<GameDto> dtos)
        {
            if (dtos == null) throw new ArgumentNullException(nameof(dtos));

            return dtos.Select(ToEntity).ToList();
        }
    }
}