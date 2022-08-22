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
        public NewCountDownWindow()
        {
            InitializeComponent();
        }
        public static bool IsTextOnlyNumbers(string text)
        {
            return !_regexNumberOnly.IsMatch(text);
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
            if (!UserCountdown.CountdownValidation.IsValidCountdown(out string errorMessage))
            {
                MessageBox.Show(errorMessage,"Contagem Invalida", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Countdown NewCountdown = new Countdown
                {
                    CountdownName = UserCountdown.CountdownName,
                    CountdownDateTime = UserCountdown.FullDateCountdown
                };
                ((MainWindow)Application.Current.MainWindow).CountdownCollection.Countdowns.Add(NewCountdown);
                MessageBox.Show("Enviado com sucesso","Nova Contagem");
                Close();
            }
        }
    }
}
