using Lib_Countdown_Timer;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Countdown_Timer
{
    public partial class NewCountDownWindow : Window
    {
        public UserCountdown Countdown { get; set; } = new UserCountdown();
        private static readonly Regex _regexNumberOnly = new Regex("[^0-9]");
        public NewCountDownWindow()
        {
            InitializeComponent();
        }
        // Methods
        private static bool IsTextNumber(string text)
        {
            return _regexNumberOnly.IsMatch(text);
        }
        // Events
        private void TBPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextNumber(e.Text);
        }

        private void TbPasting(object sender, DataObjectPastingEventArgs e)
        {
            // implement condition to only let int to be pasting
            e.CancelCommand();
        }

        private void AddCountdown_Click(object sender, RoutedEventArgs e)
        {
            //Test
            Countdown.SetDate();
            MessageBox.Show(Countdown.FullDateCountdown.ToString());
            MessageBox.Show(Countdown.FullDateCountdown.Kind.ToString());
            //Test
        }
    }
}
