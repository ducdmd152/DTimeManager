using DTimeManagerBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTimeManagerRepository
{
    public interface IAlarmEventRepository
    {
        List<AlarmEvent> GetAll();
        AlarmEvent Get(int id);
        AlarmEvent Create(AlarmEvent item);
        AlarmEvent Update(AlarmEvent item);
        AlarmEvent Delete(int id);
    }
}
