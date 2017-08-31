using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EthosTest.Models;
using EthosTest.Workers;

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
                twister.Text = 
                    WordTwisterProcessor.Instance
                    .TwistIt(twister).Result;

                ModelState.Clear();
            }

            return View(twister);

        }

       
    }
}