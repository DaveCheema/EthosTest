using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EthosTest.Engines
{
    // WordTwisterEngine is the engine that generates the twisted phrase based on user selected twist option.    
    public class WordTwisterEngine
    {
        // Reverse word order: e.g. ‘the brown fox’ becomes ‘fox brown the’.
        public static string ReverseWordOrder(string phrase)
        {
            return String.Join(" ", phrase.Split(' ').Reverse());
        }

        // Reverse characters while maintaining word order: e.g. ‘the brown fox’ becomes ‘eht nworb xof’.
        public static string ReverseCharacters(string phrase)
        {
            return
                String.Join(" ",
                phrase.Split(' ').ToArray()
                .Select(word => string.Join("", word.ToCharArray().Reverse().ToArray())));
        }

        // Alphabetically sort words: e.g. ‘the brown fox’ becomes ‘brown fox the’.
        public static string SortWordsAlphabetically(string phrase)
        {
            return
                String.Join(" ",
                phrase.Split(' ').ToArray()
                .OrderBy(n => n)
                );
        }

        // Encrypt the paragraph using a salted SHA-384 algorithm.
        // This is called asynchronously to avoid any blocking for improved performance.
        public async static Task<string> EncryptInput(string phrase)
        {
            return
                await
                HashingEngine.Instance.HashWithSalt(phrase);
        }
    }
}