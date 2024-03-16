using System;
using System.Threading;

namespace DTimeManagerService
{
    public class CountdownTimerManagerService
    {
        private Thread timerThread;
        private TimeSpan duration;
        private bool isRunning;
        private bool isPaused; // Biến để kiểm soát việc tạm dừng luồng
        private object lockObject = new object(); // Đối tượng khóa để đồng bộ hóa trạng thái tạm dừng

        public event EventHandler<string> TimerTick;
        public event EventHandler TimerElapsed;

        public bool IsRunning => isRunning;
        public bool IsPaused => isPaused;

        public CountdownTimerManagerService(TimeSpan duration)
        {
            this.duration = duration;
        }

        public void Start()
        {
            if (!isRunning)
            {
                timerThread = new Thread(CountdownThread);
                timerThread.IsBackground = true;
                timerThread.Start();
                isRunning = true;
            }
        }

        public void Start(TimeSpan duration)
        {
            this.duration = duration;
            if (!isRunning)
            {
                timerThread = new Thread(CountdownThread);
                timerThread.IsBackground = true;
                timerThread.Start();
                isRunning = true;
            }
        }

        public void Pause()
        {
            if (isRunning && !isPaused)
            {
                lock (lockObject)
                {
                    isPaused = true;
                }
            }
        }

        public void Resume()
        {
            if (isRunning && isPaused)
            {
                lock (lockObject)
                {
                    isPaused = false;
                    Monitor.Pulse(lockObject); // Thức tỉnh luồng sau khi tạm dừng
                }
            }
        }

        public void Reset()
        {
            duration = TimeSpan.Zero;
            isRunning = false;
            isPaused = false;
        }

        private void CountdownThread()
        {
            while (duration.TotalSeconds > 0)
            {
                lock (lockObject)
                {
                    // Kiểm tra xem luồng đã bị tạm dừng chưa
                    while (isPaused)
                    {
                        Monitor.Wait(lockObject); // Tạm dừng luồng
                    }
                }

                Thread.Sleep(1000);

                if (!isPaused)
                {                    
                    duration = duration.Subtract(TimeSpan.FromSeconds(1));

                    TimerTick?.Invoke(this, duration.ToString(@"hh\:mm\:ss"));
                }                
            }

            TimerElapsed?.Invoke(this, EventArgs.Empty);
            isRunning = false;
        }
    }
}
