using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PZ1.Validators
{
    public class StringRangeValidationRule : ValidationRule
    {
        private int _minimumLength = -1;
        private int _maximumLength = -1;

        public int MinimumLength
        {
            get { return _minimumLength; }
            set { _minimumLength = value; }
        }

        public int MaximumLength
        {
            get { return _maximumLength; }
            set { _maximumLength = value; }
        }

        // public string ErrorMessage
        // {
        //     get { return _errorMessage; }
        //    set { _errorMessage = value; }
        // }

        public override ValidationResult Validate(object value,
            CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string inputString = (value ?? string.Empty).ToString();
            int radiusValue;
            if (int.TryParse(inputString, out radiusValue))
            {
                if (radiusValue < MinimumLength ||
                   (radiusValue > 0 &&
                    radiusValue > this.MaximumLength))
                {
                    result = new ValidationResult(false, "Please enter a number in the range:" + MinimumLength + "-" + MaximumLength + ".");
                }
            }
            else
            {
                result = new ValidationResult(false, "Please enter a value in all the fields:" + MinimumLength + "-" + MaximumLength + ".");
            }
            
            return result;
        }
    }
}
