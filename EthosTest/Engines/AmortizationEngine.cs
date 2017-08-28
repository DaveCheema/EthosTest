using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EthosTest.Models;

namespace EthosTest.Engines
{
    // AmortizationEngine is the engine to generate the loan amortization models.   
    public class AmortizationEngine
    {
        // GeneratePaymentModel method creates the loan amortization models.
        // loanAmount, loanTermInMonths, and interestRate parameters are required. 
        // The starting date parameter is optional.
        // This method is called asynchronously for better performance.
        public static Task<AmortizationModel> GeneratePaymentModel(double loanAmount, short loanTermInMonths, double interestRate, DateTime? startingDate = null)
        {
            try
            {
                if (loanAmount <= 0 || loanTermInMonths <= 0 || interestRate <= 0)
                {
                    throw
                        new ArgumentException("Loan amount, loan term (months), and interest rate must be greater than zero.");
                }


                // declare local variables           
                double monthlyInterest = 0;
                short amortizationTerm = 0;
                double currentBalance;
                double cummulativeInterest = 0;
                double monthlyPrincipal = 0;
                double cummulativePrincipal = 0;

                AmortizationModel amortizationModel = new AmortizationModel()
                    {  LoanAMount = loanAmount, LoanTermInMonths = loanTermInMonths, InterestRate = interestRate};

                // If no starting data, assume it to be today's date.
                DateTime payDate = DateTime.Today;
                if (startingDate != null)
                {
                    payDate = (DateTime)startingDate;
                }

                currentBalance = loanAmount;
                interestRate = interestRate * 0.01;
                amortizationTerm = loanTermInMonths;

                // Calculate the monthly payment and round it to 2 decimal places.
                var monthlyinterestRate = interestRate / 12;
                var monthlyPayment = (monthlyinterestRate / (1 - (Math.Pow((1 + monthlyinterestRate), -(amortizationTerm))))) * loanAmount;
                monthlyPayment = Math.Round(monthlyPayment, 2);

                // Save the starting date and the monthly payment the result set.
                amortizationModel.StartingDate = payDate;
                amortizationModel.MonthlyPayment = monthlyPayment;


                // Loop for amortization term (number of monthly payments).
                for (int j = 0; j < amortizationTerm; j++)
                {
                    // Calculate monthly cycle
                    monthlyInterest = currentBalance * interestRate / 12;
                    monthlyPrincipal = monthlyPayment - monthlyInterest;
                    currentBalance = currentBalance - monthlyPrincipal;

                    // Adjust the last payment to make sure the final balance is 0.
                    if (j == amortizationTerm - 1 && currentBalance != monthlyPayment)
                    {
                        monthlyPayment += currentBalance;
                        currentBalance = 0;
                    }

                    // Update the Due Date.
                    payDate = payDate.AddMonths(1);

                    // Add to cummulative totals.
                    cummulativeInterest += monthlyInterest;
                    cummulativePrincipal += monthlyPrincipal;

                    // Add a MonthlyPaymentDetail to the MonthlyPaymentDetails collection.
                    amortizationModel.MonthlyPaymentDetails.Add
                        (new MonthlyPaymentDetail
                        {
                            PaymentNumber = j + 1,
                            DueDate = payDate,
                            PaymentAmount = Math.Round(monthlyPayment, 2),
                            InterestAmount = Math.Round(monthlyInterest, 2),
                            PrincipleAmount = Math.Round(monthlyPayment - monthlyInterest, 2),
                            RemainingBalance = Math.Round(currentBalance, 2)
                        });
                }

                // Add cummulative total interest and total repayment to the result set.
                amortizationModel.TotalInterest = cummulativeInterest;
                amortizationModel.TotalRepayment = amortizationModel.LoanAMount + amortizationModel.TotalInterest;
                                
                return Task.FromResult(amortizationModel);
            }
            catch (Exception ex)
            {
                return Task.FromException<AmortizationModel>(ex);
            }

        }
    }
}