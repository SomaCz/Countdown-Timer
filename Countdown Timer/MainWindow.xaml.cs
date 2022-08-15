using System;
using System.Collections.Generic;
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
        public Countdown NewCountdown { get; set; } = new Countdown();
        public DateTime CurrentlyDate { get; private set; } = DateTime.Now;
        private DispatcherTimer? timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            Closed += MainWindow_Closed;
            NewCountdown.StatusChanged += Countdown_StatusChanged;
        }

        private void Countdown_StatusChanged(object? sender, CountdownStatusChangedEventArgs e)
        {
            if (e.Status == Countdown.CountdownStatus.Finished) MessageBox.Show($"{e.CountdownName} Finalizado");
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            if (timer != null)
            { 
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
            if (NewCountdown != null)
            {
                NewCountdown.StatusChanged -= Countdown_StatusChanged;
            }
            Closed -= MainWindow_Closed;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (NewCountdown.Status == Countdown.CountdownStatus.Active)
            {
                NewCountdown.CountdownSpan = NewCountdown.CountdownSpan.Subtract(TimeSpan.FromSeconds(1));    
            }
        }

        private void BtnNewCount_Click(object sender, RoutedEventArgs e)
        {
            NewCountDownWindow NewCountDown = new NewCountDownWindow();
            NewCountDown.ShowDialog();
        }
    }
}
