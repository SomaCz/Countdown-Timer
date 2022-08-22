using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lib_Countdown_Timer
{
    public class UserCountdownValidation
    {
        UserCountdown UserCountdown { get;}
        public bool IsValidDateTime(string? field, string? format) => DateTime.TryParseExact(field, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
        public bool IsValidCountdown(out string errorMessage)
        {
            errorMessage = string.Empty;
            if (UserCountdown.HasErrors)
            {
                errorMessage = "Campos invalidos";
                return false;
            }
            if (!UserCountdown.TrySetDateTime())
            {
                errorMessage = $"Data Invalida - Verifique se o mês possui {UserCountdown.CountdownDay} dias";
                return false;
            }
            if (UserCountdown.FullDateCountdown <= System.DateTime.Now)
            {
                errorMessage = "Data Invalida - Verifique se a data inserida ja não foi percorrida";
                return false;
            }
            return true;
        }
        public UserCountdownValidation(UserCountdown userCountdown)
        {
            UserCountdown = userCountdown;  
        }
    }
}
