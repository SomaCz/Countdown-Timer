using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lib_Countdown_Timer
{
    public class UserCountdown : ObservableObject, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();
        private string countdownName;
        public string CountdownName
        {
            get { return countdownName; }
            set 
            {
                countdownName = value;

                ClearErrors(nameof(CountdownName));
                if (countdownName.Length > 40)
                {
                    AddError(nameof(CountdownName), "Nome não pode conter mais que 40 caracteres");
                }
                if (string.IsNullOrWhiteSpace(countdownName))
                {
                    AddError(nameof(CountdownName), "É obrigatorio preencher nome");
                }
                OnPropertyChanged(nameof(CountdownName));
            }
        }
        //Date
        private string countdownDay;
        public string CountdownDay
        {
            get { return countdownDay; }
            set
            {
                countdownDay = value;
                ClearErrors(nameof(CountdownDay));

                if (string.IsNullOrWhiteSpace(countdownDay))
                {
                    AddError(nameof(CountdownDay), "É obrigatorio preencher Dia");
                }
                else
                {
                    if (IsValidDateTime(value,"dd") == false)
                    {
                        AddError(nameof(CountdownDay), "Dia Invalido");
                    }
                }
                OnPropertyChanged();
            }
        }
        private string countdownMonth;
        public string CountdownMonth
        {
            get { return countdownMonth; }
            set 
            {
                countdownMonth = value;
                ClearErrors(nameof(CountdownMonth));
                if (string.IsNullOrWhiteSpace(countdownMonth))
                {
                    AddError(nameof(CountdownMonth), "É obrigatorio preencher Mês");
                }
                else
                {
                    if (IsValidDateTime(value, "MM") == false)
                    {
                        AddError(nameof(CountdownMonth), "Mês Invalido");
                    }
                }
                OnPropertyChanged();
            }
        }
        private string countdownYear;
        public string CountdownYear
        {
            get { return countdownYear; }
            set
            {
                countdownYear = value;
                ClearErrors(nameof(CountdownYear));
                if (string.IsNullOrWhiteSpace(countdownYear))
                {
                    AddError(nameof(CountdownYear), "É obrigatorio preencher Ano");
                }
                else
                {
                    if (IsValidDateTime(value, "yyyy") == false)
                    {
                        AddError(nameof(CountdownYear), "Ano Invalido");
                    }
                }
                OnPropertyChanged();
            }
        }
        //Time
        private string? countdownHour;
        public string? CountdownHour
        {
            get { return countdownHour; }
            set 
            { 
                countdownHour = value;
                ClearErrors(nameof(CountdownHour));
                if (!string.IsNullOrWhiteSpace(countdownHour))
                {
                    if (IsValidDateTime(value, "HH") == false)
                    {
                        AddError(nameof(CountdownHour), "Hora Invalida");
                    }
                }
                OnPropertyChanged();
            }
        }
        private string? countdownMinute;
        public string? CountdownMinute
        {
            get { return countdownMinute; }
            set
            {
                countdownMinute = value;
                ClearErrors(nameof(CountdownMinute));
                if (!string.IsNullOrWhiteSpace(countdownMinute))
                {
                    if (IsValidDateTime(value, "mm") == false)
                    {
                        AddError(nameof(CountdownMinute), "Minuto Invalido");
                    }
                }
                OnPropertyChanged();
            }
        }
        public DateTime FullDateCountdown { get; private set; }
        public UserCountdown()
        {
            countdownName = string.Empty;
            countdownDay = string.Empty;
            countdownMonth = string.Empty;
            countdownHour = string.Empty;
            countdownMinute = string.Empty;
            AddError(nameof(CountdownName), "");
            AddError(nameof(CountdownDay), "");
            AddError(nameof(CountdownMonth), "");
            countdownYear = DateTime.Now.Year.ToString();
        }
        public bool SetDateTime()
        {
            CountdownHour = OpcionalField(CountdownHour);
            CountdownMinute = OpcionalField(CountdownMinute);

            string? fullDateTimeStr = $"{countdownDay}/{countdownMonth}/{countdownYear}/{countdownHour}/{countdownMinute}";
            if (DateTime.TryParseExact(fullDateTimeStr, "d/M/yyyy/H/m", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateParseOutput))
            {
                FullDateCountdown = dateParseOutput;
                return true;
            }
            return false;
        }
        private bool IsValidDateTime(string? field, string? format) => DateTime.TryParseExact(field, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);
        private string OpcionalField(string? field) => string.IsNullOrWhiteSpace(field) ? "00" : field;
        // INotifyDataErrorInfo
        public bool HasErrors => _propertyErrors.Any();
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable? GetErrors(string propertyName) => _propertyErrors.GetValueOrDefault(propertyName);
        
        public void AddError(string propertyName, string errorMessage)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors.Add(propertyName, new List<string>());
            }
            _propertyErrors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void ClearErrors(string propertyName)
        {
            if (_propertyErrors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }
    }
}
