using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLayer;

namespace Services
{
    public class InputValidator : IInputValidator
    {
        public bool isPhoneValidation(string phoneNumber)
        {
            string regex = @"^(?:\+84|84|0)(3|5|7|8|9)\d{8}$";

            if (Regex.IsMatch(phoneNumber, regex)) {
                return true;
            }

            return false;
        }
    }
}
