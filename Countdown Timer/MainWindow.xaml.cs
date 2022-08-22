using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Lib_Countdown_Timer;

namespace Countdown_Timer
{
    public partial class MainWindow : Window
    {
        public CountdownCollection CountdownCollection { get; set; } = new CountdownCollection();
        private readonly DispatcherTimer? timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            Closed += MainWindow_Closed;
        }
        private void Countdown_StatusChanged(object? sender, CountdownStatusChangedEventArgs e)
        {
            if (e.Status == Countdown.CountdownStatus.Finished)
            {
                MessageBox.Show($"Contagem de {e.CountdownName} foi Finalizado", "Contagem Regressiva", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            if (timer != null)
            { 
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
            foreach (Countdown? countdown in CountdownCollection.Countdowns)
            {
                if (countdown != null)
                {
                    countdown.StatusChanged -= Countdown_StatusChanged;
                }
            }
                Closed -= MainWindow_Closed;
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            Task.Run(() =>
            {
                foreach (Countdown? countdown in CountdownCollection.Countdowns)
                {
                    if (countdown != null)
                    {
                        countdown.StatusChanged -= Countdown_StatusChanged;
                        countdown.StatusChanged += Countdown_StatusChanged;
                        if (countdown.Status == Countdown.CountdownStatus.Active)
                        {
                            countdown.CountdownSpan = countdown.CountdownSpan.Subtract(TimeSpan.FromSeconds(1));
                        }
                    }
                }
            });
        }
        private void BtnNewCount_Click(object sender, RoutedEventArgs e)
        {
            if (CountdownCollection.Countdowns.Count >= 4)
            { 
                MessageBox.Show("Maximo de contagems atigindo", "Limite alcançado", MessageBoxButton.OK, MessageBoxImage.Warning); 
            }
            else
            {
                NewCountDownWindow NewCountDown = new NewCountDownWindow();
                NewCountDown.ShowDialog();
            }
        }
        private void BtnRemoveCount_Click(object sender, RoutedEventArgs e)
        {
            var SelectedItem = LbCountdowns.SelectedItem as Countdown;
            if (SelectedItem != null)
            {
                if (SelectedItem.Status == Countdown.CountdownStatus.Finished || SelectedItem.Status == Countdown.CountdownStatus.Disable)
                {
                    CountdownCollection.Countdowns.Remove(SelectedItem);
                }
                else
                {
                    string? message = $"Contagem {SelectedItem.CountdownName} sera removido da lista - Você tem certeza ?";
                    MessageBoxResult userDecision = MessageBox.Show(message, "Remover Contagem", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (userDecision == MessageBoxResult.Yes) CountdownCollection.Countdowns.Remove(SelectedItem);
                }
            }
        }
    }
}