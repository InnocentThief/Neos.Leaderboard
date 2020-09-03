using DataAccess.Entity.AccountEntity;
using DataTransfer.Dto.Dtos;
using System;

namespace DataTransfer.Dto.Converter
{
    /// <summary>
    /// Handles conversion from <see cref="Account"/> to <see cref="AccountDto"/> and vice versa.
    /// </summary>
    public static class AccountConverter
    {
        /// <summary>
        /// Converts the given <see cref="Account"/> entity to a <see cref="AccountDto"/>.
        /// </summary>
        /// <param name="entity">The entity to transform into its data transfer object equivalent.</param>
        /// <exception cref="ArgumentNullException">Thrown if entity is null.</exception>
        public static AccountDto ToDto(this Account entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new AccountDto
            {
                AccountKey = entity.AccountKey,
                Username = entity.Username
            };
        }

        /// <summary>
        /// Converts the given <see cref="AccountDto"/> to a <see cref="Account"/> entity.
        /// </summary>
        /// <param name="dto">The dto to transform into its entity object equivalent.</param>
        /// <exception cref="ArgumentNullException">Thrown if dto is null.</exception>
        public static Account ToEntity(this AccountDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new Account
            {
                AccountKey = dto.AccountKey,
                Username = dto.Username,
            };
        }
    }
}