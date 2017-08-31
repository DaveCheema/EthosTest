using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using EthosTest.Models;
using EthosTest.Engines;

namespace EthosTest.Workers
{
    public class AmortizationProcessor
    {
        private static volatile AmortizationProcessor instance;
        private static object syncRoot = new Object();

        private AmortizationProcessor() { }

        // Create a singleton instance.
        public static AmortizationProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new AmortizationProcessor();
                    }
                }

                return instance;
            }
        }

        // GetPaymentModel is called asynchronously to avoid any blocking for improved performance. 
        public async Task<AmortizationModel> GetPaymentModel(double loanAMount
                                                                    , short loanTermInMonths
                                                                    , double interestRate)
        {
            // Generate amortization model asynchronously.
            return
                 await
                 AmortizationEngine.GeneratePaymentModel(loanAMount, loanTermInMonths, interestRate);
        }
    }
}