using DTimeManagerBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTimeManagerService
{
    public interface IAlarmEventService
    {
        public List<AlarmEvent> GetAll();
        public AlarmEvent Get(int id);
        public AlarmEvent Create(AlarmEvent item);
        public AlarmEvent Update(AlarmEvent item);
        public AlarmEvent Delete(int id);
    }
}
