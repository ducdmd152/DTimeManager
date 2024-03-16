using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTimeManagerBO
{
    public class AlarmTimerManager
    {
        private Thread timerThread;
        private TimeSpan alarmTime;
        private bool isRunning;

        public event EventHandler AlarmTriggered;

        public bool IsRunning => isRunning;

        public AlarmTimerManager(TimeSpan alarmTime)
        {
            this.alarmTime = alarmTime;
        }

        public void Start()
        {
            if (!isRunning)
            {
                timerThread = new Thread(AlarmThread);
                timerThread.IsBackground = true;
                timerThread.Start();
                isRunning = true;
            }
        }

        public void Start(TimeSpan alarmTime)
        {
            this.alarmTime = alarmTime;
            if (!isRunning)
            {
                timerThread = new Thread(AlarmThread);
                timerThread.IsBackground = true;
                timerThread.Start();
                isRunning = true;
            }
        }

        public void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
            }
        }

        private void AlarmThread()
        {
            while (isRunning)
            {
                DateTime now = DateTime.Now;
                TimeSpan current = new TimeSpan(now.Hour, now.Minute, 0);

                if (current == alarmTime)
                {
                    AlarmTriggered?.Invoke(this, EventArgs.Empty);
                    Stop();
                }

                Thread.Sleep(1000);
            }
        }
    }
}
