using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EthosTest.Models
{
    // AmortizationModel provides the data structure for the loan amortization models.
    // Loan amount, loan term (months), and interest rate are the required properties to generate an amortization model.
    // Rest of the properties are generated.   
    public class AmortizationModel
    {
        [Required(ErrorMessage = "Loan amount is required.")]
        // Made an assumption.
        [Range(1, 999999999999.99, ErrorMessage = "Loan amount must be between 1 and 999999999999.99")]                
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = false)]        
        [Display(Name = "Loan Amount")]        
        public double LoanAMount { get; set; }

        [Required(ErrorMessage = "Loan term (months) is required.")]
        // Made an assumption.
        [Range(1, 1200, ErrorMessage = "Loan term (months) must be between 1 and 1200")]                            
        [DisplayFormat(DataFormatString = "{0:###,###}", ApplyFormatInEditMode = false)]
        [Display(Name = "Loan Term (months)")]        
        public short LoanTermInMonths { get; set; }

        [Required(ErrorMessage = "Interest rate is required.")]
        // Made an assumption.
        [Range(0.01, 100, ErrorMessage = "Interest rate must be between 0.01 and 100")]                          
        [DisplayFormat(DataFormatString = "{0:#,###.###}",
               ApplyFormatInEditMode = false)]
        [Display(Name = "Interest Rate (%)")]        
        public double InterestRate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Starting Date")]
        public DateTime? StartingDate { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Monthly Payment")]
        public double? MonthlyPayment { get; set; } = null;

        [DataType(DataType.Currency)]
        [Display(Name = "Total Interest Amt.")]
        public double? TotalInterest { get; set; } = null;

        [DataType(DataType.Currency)]
        [Display(Name = "Total Repayment Amt.")]
        public double? TotalRepayment { get; set; } = null;

        public List<MonthlyPaymentDetail> MonthlyPaymentDetails { get; set; } = new List<MonthlyPaymentDetail>();
    }

    public class MonthlyPaymentDetail
    {
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        [Display(Name = "Payment #")]
        public int PaymentNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Payment Date")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Payment Amt.")]
        public double PaymentAmount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Interest Amt.")]
        public double InterestAmount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Principle Amt.")]
        public double PrincipleAmount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Remaining Balance Amt.")]
        public double RemainingBalance { get; set; }
    }
}