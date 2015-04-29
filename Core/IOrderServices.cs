using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;

namespace Core
{
    public interface IOrderServices
    {
        IEnumerable<Order> GetAll();
        Order GetById(Guid orderId);
        void Create(Order order);
        void Update(Order order);
    }
}
