using System;
using System.Timers;
using Xamarin.Forms;

namespace Stopwatch
{
    public partial class MainPage : ContentPage
    {
        private const int IntervalMilliseconds = 100;

        private Timer _timer;
        private TimeSpan _time;

        public MainPage()
        {
            InitializeComponent();
            
            // This is required for the data-binding example.
            BindingContext = new ViewModel();
        }

        void Start_Clicked(object sender, EventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Elapsed -= Timer_Elapsed;
            }

            _timer = new Timer(IntervalMilliseconds);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        void Stop_Clicked(object sender, EventArgs e)
        {
            if (_timer is null)
            {
                return;
            }

            _timer.Stop();
        }

        void Reset_Clicked(object sender, EventArgs e)
        {
            if (_timer is null)
            {
                return;
            }

            _time = default;
            UpdateStopwatch();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _time += TimeSpan.FromMilliseconds(IntervalMilliseconds);
            UpdateStopwatch();
        }

        private void UpdateStopwatch()
        {
            // Without data binding, any UI elements must be updated on the main thread (aka the 'UI thread').
            // This is needed since Timer runs on the ThreadPool.
            Device.BeginInvokeOnMainThread(() =>
            {
                Hours.Text = _time.Hours.ToString("00");
                Minutes.Text = _time.Minutes.ToString("00");
                Seconds.Text = _time.Seconds.ToString("00");
                Milliseconds.Text = _time.Milliseconds.ToString("000");
            });
        }
    }
}
