namespace DTimeManagerBO
{
    public class AlarmEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        private bool isActived;
        private AlarmTimerManager manager;
        public Action<string, string> ShowAlarmDelegate { get; set; }
        public bool IsActived
        {
            get { return isActived; }
            set
            {
                isActived = value;
                

                if (value)
                {
                    Manager.Start(Time);
                }
                else
                {
                    Manager.Stop();
                }
            }
        }
        public AlarmTimerManager Manager {
            get
            {
                if (manager == null)
                {
                    manager = new AlarmTimerManager(Time);
                    manager.AlarmTriggered += (object sender, EventArgs e) =>
                    {
                        ShowAlarmDelegate("Alarmingggggg!!!", "Time for " + Name + ".");
                    };
                    if (isActived)
                    {
                        manager.Start(Time);
                    }
                    else
                    {
                        manager.Stop();
                    }
                }
                return manager;
            }

            set
            {
                manager = value;
            }
        }
    }
}