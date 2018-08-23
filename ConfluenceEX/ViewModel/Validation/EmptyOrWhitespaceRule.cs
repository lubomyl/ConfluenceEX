using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ConfluenceEX.ViewModel.Validation
{
    public class EmptyOrWhitespaceRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(String.IsNullOrWhiteSpace((string) value) || String.IsNullOrEmpty((string) value))
            {
                return new ValidationResult(false, "Cannot be empty");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

    }
}
