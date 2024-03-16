using DTimeManagerBO;

namespace DTimeManagerDAO
{
    public class AlarmEventDAO
    {
        private static AlarmEventDAO instance = null;
        private static readonly object instanceLock = new object();
        private int idCounter = 0;
        private static List<AlarmEvent> data = new List<AlarmEvent>()
        {            
            new AlarmEvent()
            {
                Name = "PRN221 Assignment 3",
                Time = new TimeSpan(15, 0, 0),
                IsActived = true,
            },
            new AlarmEvent()
            {
                Name = "PRU212 Lab 3",
                Time = new TimeSpan(21, 30, 0),
                IsActived = true,
            },
        };

        private AlarmEventDAO() {}
        public static AlarmEventDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AlarmEventDAO();
                    }
                    return instance;
                }
            }
        }

        public List<AlarmEvent> GetAll()
        {
            return data;
        }

        public AlarmEvent Get(int id)
        {
            return data.FirstOrDefault(item => item.Id == id);
        }

        public AlarmEvent Create(AlarmEvent item)
        {
            item.Id = ++idCounter;
            data.Add(item);
            return item;
        }

        public AlarmEvent Update(AlarmEvent item)
        {
            var target = Get(item.Id);
            target.Name = item.Name;
            target.Time = item.Time;
            target.IsActived = item.IsActived;
            return target;
        }

        public AlarmEvent Delete(AlarmEvent item)
        {
            if (item != null)           
                data.Remove(item);
            return item;
        }

        public AlarmEvent Delete(int id)
        {
            var item = Get(id);
            if (item != null)
                data.Remove(item);
            return item;
        }
    }
}