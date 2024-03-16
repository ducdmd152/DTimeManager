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
    public class AlarmEventService
    {
        private IAlarmEventRepository alarmEventRepository;
        List<AlarmEvent> GetAll() => alarmEventRepository.GetAll();
        AlarmEvent Get(int id) => alarmEventRepository.Get(id);
        AlarmEvent Create(AlarmEvent item) => alarmEventRepository.Create(item);
        AlarmEvent Update(AlarmEvent item) => alarmEventRepository.Update(item);
        AlarmEvent Delete(int id) => alarmEventRepository.Delete(id);
    }
}
