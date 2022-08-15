using Lib_Countdown_Timer;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Countdown_Timer
{
    public partial class NewCountDownWindow : Window
    {
        private static readonly Regex _regexNumberOnly = new Regex("[^0-9]+$");
        public UserCountdown UserCountdown { get; set; } = new UserCountdown();
        public Countdown NewCountdown { get; private set; } = new Countdown();
        public NewCountDownWindow()
        {
            InitializeComponent();
        }
        public bool IsTextOnlyNumbers(string text)
        {
            return !_regexNumberOnly.IsMatch(text);
        }
        public bool IsValidCountdown (UserCountdown userCountdown, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (userCountdown.HasErrors)
            { 
                errorMessage = "Campos invalidos";
                return false; 
            }
            if (!userCountdown.SetDateTime()) 
            {
                errorMessage = $"Data Invalida. Verifique se o mês possui {userCountdown.CountdownDay} dias";
                return false;
            }
            if (UserCountdown.FullDateCountdown <= System.DateTime.Now)
            {
                errorMessage = "Data Invalida. Verifique se a data não esta no passado";
                return false;
            }
            return true;
        }
        private void TBPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextOnlyNumbers(e.Text);
        }
        private void TbPasting(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }
        private void AddCountdown_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidCountdown(UserCountdown, out string errorMessage))
            {
                MessageBox.Show(errorMessage);
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).NewCountdown.CountdownDateTime = UserCountdown.FullDateCountdown;
                ((MainWindow)Application.Current.MainWindow).NewCountdown.CountdownName = UserCountdown.CountdownName;
                MessageBox.Show("Enviado com sucesso");
                Close();
            }
        }
    }
}
