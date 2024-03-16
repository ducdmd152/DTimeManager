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
        public List<AlarmEvent> GetAll() => AlarmEventDAO.Instance.GetAll();
        public AlarmEvent Get(int id) => AlarmEventDAO.Instance.Get(id);
        public AlarmEvent Create(AlarmEvent item) => AlarmEventDAO.Instance.Create(item);
        public AlarmEvent Update(AlarmEvent item) => AlarmEventDAO.Instance.Update(item);
        public AlarmEvent Delete(int id) => AlarmEventDAO.Instance.Delete(id);
    }
}
