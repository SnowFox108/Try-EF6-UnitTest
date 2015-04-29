using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Data.Infrastructure;
using Data.Model;

namespace FrontTest
{
    public class OrdersWork
    {

        private IOrderServices _orderServices;

        public OrdersWork()
        {
            _orderServices = new OrderServices(new ContentContext());

            CreateJunk();
            CreateJunk();
            CreateJunk();

            var result = _orderServices.GetAll();

            foreach (var order in result)
            {
                Console.WriteLine("Id: {0} \t Create Date: {1}", order.Id, order.CreatedDate);
            }
        }

        private void CreateJunk()
        {
            _orderServices.Create(new Order
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now
            });            
        }
    }
}
