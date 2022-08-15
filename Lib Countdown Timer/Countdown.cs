using System;
using System.Collections.Generic;
using System.Text;

namespace Lib_Countdown_Timer
{
    public class Countdown : ObservableObject
    {
        //Implements: if needed to notify OnCountdownBegins add "Started" on CountdownStatus enum
        public enum CountdownStatus { Disable, Active, Finished }
        #pragma warning disable CS8618 
        public event EventHandler<CountdownStatusChangedEventArgs> StatusChanged;
        #pragma warning restore CS8618 
        private CountdownStatus status;
        public CountdownStatus Status
        {
            get { return status; }
            private set 
            { 
                status = value;
                if (value == CountdownStatus.Finished) OnStatusChanged(new CountdownStatusChangedEventArgs(CountdownName,Status));
            }
        }
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
        private DateTime countdownDateTime;
        public DateTime CountdownDateTime
        {
            get { return countdownDateTime; }
            set
            {
                countdownDateTime = value;
                SetCountdownSpan();
                OnPropertyChanged();
            }
        }
        private TimeSpan countdownSpan;
        public TimeSpan CountdownSpan
        {
            get { return countdownSpan; }
            set
            {
                countdownSpan = value;
                SetStatus();
                OnPropertyChanged();
            }
        }
        private bool HasTime() => CountdownSpan.CompareTo(TimeSpan.Zero) > 0;
        private void SetCountdownSpan() => CountdownSpan = CountdownDateTime.Subtract(DateTime.Now);
        private void SetStatus()
        {
            if (HasTime()) Status = CountdownStatus.Active;
            else if (Status == CountdownStatus.Active && !HasTime()) Status = CountdownStatus.Finished;
            else Status = CountdownStatus.Disable;
        }
        protected virtual void OnStatusChanged (CountdownStatusChangedEventArgs e)
        {
            StatusChanged?.Invoke(this, new CountdownStatusChangedEventArgs(CountdownName,Status));
        }
    }
}
