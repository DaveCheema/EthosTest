using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EthosTest.Models;
using EthosTest.Engines;

namespace EthosTest.Workers
{
    public class WordTwisterProcessor
    {
        private static volatile WordTwisterProcessor instance;
        private static object syncRoot = new Object();

        private WordTwisterProcessor() { }

        // Create a singleton instance.
        public static WordTwisterProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new WordTwisterProcessor();
                    }
                }

                return instance;
            }
        }

        // Twist user entered phrase based on user selected option.
        public Task<string> TwistIt(WordTwisterModel twister)
        {
            try
            {
                string twistedPhrase = null;

                // Produce result based on Twist Action selecteed.
                switch (twister.TwistAction)
                {
                    case Twist.ReverseWordOrder:
                        twistedPhrase = WordTwisterEngine.ReverseWordOrder(twister.Text);
                        break;
                    case Twist.ReverseCharacters:
                        twistedPhrase = WordTwisterEngine.ReverseCharacters(twister.Text);
                        break;
                    case Twist.SortWordsAlphabetically:
                        twistedPhrase = WordTwisterEngine.SortWordsAlphabetically(twister.Text);
                        break;
                    case Twist.EncryptInput:
                        // .Result is used to extract the result.
                        twistedPhrase = WordTwisterEngine.EncryptInput(twister.Text).Result;
                        break;
                }

                // Return the result.          
                return Task.FromResult(twistedPhrase);
            }
            catch (Exception ex)
            {
                return Task.FromException<string>(ex);
            }
        }
    }
}