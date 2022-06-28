using Lib_Countdown_Timer;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Countdown_Timer
{
    public partial class NewCountDownWindow : Window
    {
        private static readonly Regex _regexNumberOnly = new Regex("[^0-9]+$");
        public UserCountdown Countdown { get; set; } = new UserCountdown();
        public NewCountDownWindow()
        {
            InitializeComponent();
        }
        public bool IsTextOnlyNumbers(string text)
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
            if (Countdown.HasErrors)
            {
                MessageBox.Show("Campos invalidos");
            }
            else
            {
                if (Countdown.SetDateTime() == false)
                {
                    MessageBox.Show($"Data Invalida. Verifique se o mês possui {Countdown.CountdownDay} dias");
                }
                else
                {
                    if (Countdown.FullDateCountdown <= System.DateTime.Now == true)
                    {
                        MessageBox.Show("Data Invalida. Verifique se a data não esta no passado");
                    }
                    else
                    {
                        MessageBox.Show("Enviado com sucesso");
                    }
                }
            }
        }
    }
}
