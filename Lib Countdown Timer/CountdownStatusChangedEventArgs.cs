using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Countdown_Timer
{
    public class CountdownStatusChangedEventArgs : EventArgs
    {
        public string? CountdownName { get; private set; }
        public Countdown.CountdownStatus Status { get; private set; }
        public CountdownStatusChangedEventArgs(string? countdownName, Countdown.CountdownStatus status)
        {
            CountdownName = countdownName;
            Status = status;
        }
    }
}
