﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;

namespace EthosTest.Engines
{
    // HashingEngine is used to hash a salted phrase using SHA384.
    // At this point in point, it is only uses SHA384 hash.
    public class HashingEngine
    {
        // HashWithSalt is the heart of Hashing engine.
        // This method is called asynchronously to avoid blocking and provide improved performance.
        public static Task<string> HashWithSalt(  string phraseToHash,                                         
                                            byte[] salt = null)
        {
            try
            {
                // If not salt provided, generate it.
                if (salt == null)
                {
                    int minSaltSize = 4;
                    int maxSaltSize = 16;

                    // Generate a random number for the size of the salt.
                    Random random = new Random();
                    int saltSize = random.Next(minSaltSize, maxSaltSize);

                    // Allocate a byte array to hold the salt.
                    salt = new byte[saltSize];

                    // Initialize a random number generator.
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                    // Fill the salt with cryptographically strong byte values.
                    rng.GetNonZeroBytes(salt);
                }

                // Convert plain text into a byte array.
                byte[] strTextBytes = Encoding.UTF8.GetBytes(phraseToHash);

                // Allocate array to hold plain text and salt.
                byte[] strWithsalt =
                        new byte[strTextBytes.Length + salt.Length];

                // Copy strTextBytes and salt arrays into strWithsalt.
                Array.Copy(strTextBytes, strWithsalt, strTextBytes.Length);
                Array.Copy(salt, 0, strWithsalt, strTextBytes.Length, salt.Length);

                HashAlgorithm hash = new SHA384Managed();

                // Compute hash value of str with appended salt.
                byte[] hashBytes = hash.ComputeHash(strWithsalt);

                // Create array which will hold hash and original salt bytes.
                byte[] hashWithsalt = new byte[hashBytes.Length +
                                                    salt.Length];

                // Copy hashBytes and salt arrays into hashWithsalt.
                Array.Copy(hashBytes, hashWithsalt, hashBytes.Length);
                Array.Copy(salt, 0, hashWithsalt, hashBytes.Length, salt.Length);

                // Convert result into a base64-encoded string.
                string hashedPhrase = Convert.ToBase64String(hashWithsalt);

                // Return the result.          
                return Task.FromResult(hashedPhrase);
            }
            catch (Exception ex)
            {
                return Task.FromException<string>(ex);
            }
        }
    }
}
