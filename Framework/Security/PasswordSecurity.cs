using System;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Security
{
    /// <summary>
    /// Provides password security by employing salted password hashing.
    /// </summary>
    public static class PasswordSecurity
    {
        private const int saltLength = 32;

        /// <summary>
        /// Creates password hash and salt value to save in database.
        /// </summary>
        /// <param name="password">The chosen password in plaintext.</param>
        /// <returns>The security credentials to store in database.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="password"/> is null.</exception>
        public static SecurityCredentials CreateSecurityCredentials(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            // Create a salt value.
            var saltValue = new byte[saltLength];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltValue);

            // Return the user's security credentials
            var plainText = Encoding.ASCII.GetBytes(password);
            var createdPassword = CreatePassword(saltValue, plainText);
            return new SecurityCredentials
            {
                PasswordHash = createdPassword,
                SaltValue = saltValue
            };
        }

        /// <summary>
        /// Compares the hashed password against the stored password.
        /// </summary>
        /// <param name="credentials">The stored security credentials from database.</param>
        /// <param name="password">The password to compare against the stored credentials.</param>
        /// <returns>True if the password matches the stored credentials; otherwise false.</returns>
        public static bool VerifyPassword(SecurityCredentials credentials, string password)
        {
            // Extract and validate parameters
            var storedPassword = credentials?.PasswordHash;
            var storedSaltValue = credentials?.SaltValue;
            if (storedPassword == null || storedSaltValue == null || password == null)
            {
                return false;
            }

            // Compare the values
            var plainText = Encoding.ASCII.GetBytes(password);
            var createdPassword = CreatePassword(storedSaltValue, plainText);
            return CompareByteArray(storedPassword, createdPassword);
        }

        #region Helper methods

        private static byte[] CreatePassword(byte[] saltValue, byte[] plainText)
        {
            // Add the salt to the hash
            var rawSalted = new byte[plainText.Length + saltValue.Length];
            plainText.CopyTo(rawSalted, 0);
            saltValue.CopyTo(rawSalted, plainText.Length);

            // Create the salted hash    
            var sha256 = SHA256.Create();
            return sha256.ComputeHash(rawSalted);
        }

        private static bool CompareByteArray(byte[] array1, byte[] array2)
        {
            // Compare thes contents of two byte arrays
            if (array1.Length != array2.Length)
            {
                return false;
            }
            var mismatch = 0;
            for (int i = 0; i < array1.Length; i++)
            {
                mismatch |= array1[i] ^ array2[i];
            }
            return mismatch == 0;
        }

        #endregion
    }
}