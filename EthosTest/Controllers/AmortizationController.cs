using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EthosTest.Models;
using EthosTest.Workers;

namespace EthosTest.Controllers
{
    // AmortizationController is the central focal point for loan amortization model.    
    public class AmortizationController : Controller
    {
        // GET: Amortization
        public ActionResult Amortizer()
        {
            
            // Create a data model instance.
            var amortizationModel = new AmortizationModel();

            // Create a tuple to pass multiple data models.
            var tuple = new Tuple<AmortizationModel, List<MonthlyPaymentDetail>>
                (amortizationModel, amortizationModel.MonthlyPaymentDetails);

            return View(tuple);
        }

        // Accepts user entered parameters, generates amortization model and presents to the user.
        [HttpPost]
        public ActionResult Amortizer(AmortizationModel amortizationModel)
        {

            // If no validation errors, create the amortization model.
            if (ModelState.IsValid)
            {                
                amortizationModel =
                    AmortizationProcessor.Instance                    
                    .GetPaymentModel(  amortizationModel.LoanAMount
                                    , amortizationModel.LoanTermInMonths
                                    , amortizationModel.InterestRate)
                    .Result;
               
                ModelState.Clear();
            }

            //Tuple is used to pass multiple data models.
            var tuple = new Tuple<AmortizationModel, List<MonthlyPaymentDetail>>
                        (amortizationModel, amortizationModel.MonthlyPaymentDetails);

            return View(tuple);

        }
        
    }
}