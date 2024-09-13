using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcomMVC.Models;
using EcomMVC.ViewModel;

namespace EcomMVC.Data.Infrastructure
{
    public interface IOrderRepository:IRepository<Order>
    {
        IEnumerable<Order> GetOrders(string UserId);
        OrderViewModel GetOrderDetails(string id);
        PagingList<OrderViewModel> GetOrderList(int page, int pageSize);
        IEnumerable<Order> GetAllOrders();

    }
}