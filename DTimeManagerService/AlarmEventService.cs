using DTimeManagerBO;
using DTimeManagerDAO;
using DTimeManagerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTimeManagerService
{
    public class AlarmEventService : IAlarmEventService
    {
        private IAlarmEventRepository alarmEventRepository = new AlarmEventRepository();
        public List<AlarmEvent> GetAll() => alarmEventRepository.GetAll();
        public AlarmEvent Get(int id) => alarmEventRepository.Get(id);
        public AlarmEvent Create(AlarmEvent item) => alarmEventRepository.Create(item);
        public AlarmEvent Update(AlarmEvent item) => alarmEventRepository.Update(item);
        public AlarmEvent Delete(int id) => alarmEventRepository.Delete(id);
    }
}
