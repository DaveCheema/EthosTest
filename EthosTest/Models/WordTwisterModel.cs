using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EthosTest.Models
{
    // WordTwisterModel is only two properties - Phrase and Twist option.
    // Enum Twist provided allowed twist options.   
    public class WordTwisterModel
    {
        [Required(ErrorMessage = "Phrase is required.")]
        [MinLength(2, ErrorMessage = "The minimum length must be at least 2.")]       
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "")]
        [Display(Name = "Phrase")]
        public string Text { get; set; } = "";

        [Required(ErrorMessage = "Twist option is required.")]
        [Range(1, 4)]
        [Display(Name = "Twist Option")]
        public Twist TwistAction { get; set; }
    }

    // Allowed Twist options.
    public enum Twist
    {
        ReverseWordOrder = 1,
        ReverseCharacters = 2,
        SortWordsAlphabetically = 3,
        EncryptInput = 4
    }
}