using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Mvc2NorthwindSortOrder.Models;

namespace Mvc2NorthwindSortOrder.Services
{
    public interface IOrderRepository
    {
        IQueryable<Orders> GetAll();
        Orders Get(int id);
    }

    class OrderRepository : IOrderRepository
    {
        private readonly NorthwindContext _context;

        public OrderRepository(NorthwindContext context)
        {
            _context = context;
        }

      

        public IQueryable<Orders> GetAll()
        {
            return _context.Orders;
        }

        public Orders Get(int id)
        {
            return _context.Orders.Include(p=>p.Customer).FirstOrDefault(r=>r.OrderId == id);
        }
    }


    class CachedOrderRepository : IOrderRepository
    {
        private readonly IOrderRepository _orderRepository;

        public CachedOrderRepository(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IQueryable<Orders> GetAll()
        {
            return _orderRepository.GetAll();
        }

        private static Dictionary<int, Orders> cache = new Dictionary<int, Orders>();
        public Orders Get(int id)
        {
            Orders o;
            if (cache.TryGetValue(id, out o) == true)
                return o;
            o = _orderRepository.Get(id);
            cache[id] = o;
            return o;
        }
    }

}