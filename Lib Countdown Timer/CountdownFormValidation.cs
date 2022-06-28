using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lib_Countdown_Timer
{
    // Temp //
    // Validation for testing
    public class CountdownFormValidation
    {
        public UserCountdown User { get; private set; }
        private static readonly Regex _regexNumberOnly = new Regex("[^0-9]+$");
        public List<string?> Fields { get; private set; } = new List<string?>();

        public CountdownFormValidation(UserCountdown user)
        {
            User = user;
            Fields.Add(user.CountdownName);
            Fields.Add(user.CountdownDay);
            Fields.Add(user.CountdownMonth);
            Fields.Add(user.CountdownYear);
        }
        public bool IsLengthValid(string? text, int length)
        {
            if (text?.Length <= length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsTextOnlyNumbers(string? text)
        {
            return !_regexNumberOnly.IsMatch(text);
        }
        private bool IsListFieldsEmpty()
        {
            List<bool> results = new List<bool>();

            foreach (string? field in Fields)
            {
                if (string.IsNullOrWhiteSpace(field))
                {
                    results.Add(false);
                }
                else
                {
                    results.Add(true);
                }
            }
            return results.Any(x => x == false);
        }

        public bool IsFormValid(UserCountdown countdown, out List<string?>? invalidFields)
        {
            List<bool> results = new List<bool>();
            invalidFields = new List<string?>();

            if (IsListFieldsEmpty())
            {
                invalidFields.Add("Campos não podem estar vazios");
                results.Add(false);
            }
            if (!IsLengthValid(User.CountdownName, 30))
            {
                invalidFields.Add("Nome do contador não pode ser maior que 30 caracteres");
                results.Add(false);
            }

            if (results.Any(x => x == false))
            {
                return false;
            }
            else
            {
                invalidFields.Clear();
                return true;
            }
        }
    }
}
