using System;
using System.ComponentModel;
using System.Timers;
using Xamarin.Forms;

namespace Stopwatch
{
    public class ViewModel : INotifyPropertyChanged
    {
        private const int IntervalMilliseconds = 100;

        private Timer _timer;
        private TimeSpan _time;
        private string _hours, _minutes, _seconds, _milliseconds;

        public ViewModel()
        {
            Start = new Command(() =>
            {
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer.Elapsed -= Timer_Elapsed;
                }

                _timer = new Timer(IntervalMilliseconds);
                _timer.Elapsed += Timer_Elapsed;
                _timer.Start();
            });

            Stop = new Command(() =>
            {
                if (_timer is null)
                {
                    return;
                }

                _timer.Stop();
            });

            Reset = new Command(() =>
            {
                if (_timer is null)
                {
                    return;
                }

                _time = default;
                UpdateStopwatch();
            });
        }

        public string Hours
        {
            get => _hours;
            private set
            {
                _hours = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hours)));
            }
        }

        public string Minutes
        {
            get => _minutes;
            private set
            {
                _minutes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Minutes)));
            }
        }

        public string Seconds
        {
            get => _seconds;
            private set
            {
                _seconds = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Seconds)));
            }
        }

        public string Milliseconds
        {
            get => _milliseconds;
            private set
            {
                _milliseconds = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Milliseconds)));
            }
        }

        public Command Start { get; }
        public Command Stop { get; }
        public Command Reset { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _time += TimeSpan.FromMilliseconds(IntervalMilliseconds);
            UpdateStopwatch();
        }

        private void UpdateStopwatch()
        {
            Hours = _time.Hours.ToString("00");
            Minutes = _time.Minutes.ToString("00");
            Seconds = _time.Seconds.ToString("00");
            Milliseconds = _time.Milliseconds.ToString("000");
        }
    }
}