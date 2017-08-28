using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EthosTest.Models;
using EthosTest.Engines;

namespace EthosTest.Controllers
{
    // WordTwisterController is used to twister user entered phrases based on user selected twist option.   
    public class WordTwisterController : Controller
    {
        // GET: WordTwister
        public ActionResult WordTwister()
        {
            return View();
        }

        // Receives a phrase and a twist option from the user and provides the twisted phrase.
        [HttpPost]
        public ActionResult WordTwister(WordTwisterModel twister)
        {
            if (ModelState.IsValid)
            {
                twister.Text = TwistIt(twister);
                ModelState.Clear();
            }

            return View(twister);

        }

        // Twist user entered phrase based on user selected option.
        private static string TwistIt(WordTwisterModel twister)
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

            return twistedPhrase;
        }
    }
}