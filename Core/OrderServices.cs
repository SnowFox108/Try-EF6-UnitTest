using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Model;

namespace Core
{
    public class OrderServices : IOrderServices
    {

        private readonly IContentContext _context;

        public OrderServices(IContentContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(Guid orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Attach(order);
            _context.SaveChanges();
        }
    }
}
