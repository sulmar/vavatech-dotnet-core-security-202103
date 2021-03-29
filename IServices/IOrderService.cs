using Models;
using System.Collections.Generic;

namespace IServices
{
    public interface IOrderService
    {
        IEnumerable<Order> Get();
        IEnumerable<Order> Get(string username);
    }

}
