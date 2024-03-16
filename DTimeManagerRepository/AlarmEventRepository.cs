using DTimeManagerBO;
using DTimeManagerDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTimeManagerRepository
{
    public class AlarmEventRepository : IAlarmEventRepository
    {
        List<AlarmEvent> IAlarmEventRepository.GetAll() => AlarmEventDAO.Instance.GetAll();
        AlarmEvent IAlarmEventRepository.Get(int id) => AlarmEventDAO.Instance.Get(id);
        AlarmEvent IAlarmEventRepository.Create(AlarmEvent item) => AlarmEventDAO.Instance.Create(item);
        AlarmEvent IAlarmEventRepository.Update(AlarmEvent item) => AlarmEventDAO.Instance.Update(item);
        AlarmEvent IAlarmEventRepository.Delete(int id) => AlarmEventDAO.Instance.Delete(id);
    }
}
