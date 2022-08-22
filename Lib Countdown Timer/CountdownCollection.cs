using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Lib_Countdown_Timer
{
    public class CountdownCollection : ObservableObject
    {
        private ObservableCollection<Countdown?> countdowns;

        public ObservableCollection<Countdown?> Countdowns
        {
            get { return countdowns; }
            set 
            { 
                countdowns = value;
            }
        }
        public CountdownCollection()
        {
            Countdowns = new ObservableCollection<Countdown?>();
        }
    }
}
