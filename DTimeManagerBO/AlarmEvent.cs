namespace DTimeManagerBO
{
    public class AlarmEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsActived { get; set; }
        public AlarmTimerManager Manager { get; set; }
    }
}