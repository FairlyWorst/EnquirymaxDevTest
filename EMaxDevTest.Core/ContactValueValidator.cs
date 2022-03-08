namespace EMaxDevTest.Core
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    internal class ContactValueValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int phoneNumber;
            var emailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            if (int.TryParse(value.ToString(), out phoneNumber))
            {
                if (value.ToString().Length == 11)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if(Regex.IsMatch(value.ToString(),emailPattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
