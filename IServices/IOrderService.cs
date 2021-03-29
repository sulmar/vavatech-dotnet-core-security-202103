using Models;
using System.Collections.Generic;

namespace IServices
{
    public interface IOrderService
    {
        IEnumerable<Order> Get();
        Order Get(int id);
        IEnumerable<Order> Get(string username);
    }

}
