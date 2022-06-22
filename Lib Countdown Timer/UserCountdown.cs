using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Countdown_Timer
{
    public class UserCountdown : ObservableObject
    {
        private string? countdownName;

        public string? CountdownName
        {
            get { return countdownName; }
            set 
            {
                countdownName = value; 
                OnPropertyChanged();
            }
        }
        private string? countdownDay;
        public string? CountdownDay
        {
            get { return countdownDay; }
            set
            {
                countdownDay = value;
                OnPropertyChanged();
            }
        }

        private string? countdownMonth;
        public string? CountdownMonth
        {
            get { return countdownMonth; }
            set 
            {
                countdownMonth = value;
                OnPropertyChanged();
            }
        }

        private string? countdownYear;
        public string? CountdownYear
        {
            get { return countdownYear; }
            set
            {
                countdownYear = value;
                OnPropertyChanged();
            }
        }
            
        private DateTime fullDateCountdown;

        public DateTime FullDateCountdown
        {
            get { return fullDateCountdown; }
            set 
            {
                fullDateCountdown = value;
                OnPropertyChanged();
            }
        }

        public void SetDate()
        {
            string? fulldate = $"{countdownDay}/{countdownMonth}/{countdownYear}";
            DateTime date;
            //dateCountdown = DateTime.ParseExact(fulldate, "dd-MM-yyyy",null);
            if (DateTime.TryParse(fulldate, out date))
            {
                fullDateCountdown = date;
            }
        }
    }
}
