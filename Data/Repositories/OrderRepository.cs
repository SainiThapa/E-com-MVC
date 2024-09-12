using EcomMVC.Data;
using EcomMVC.Data.Infrastructure;
using EcomMVC.Models;
using EcomMVC.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomMVC.Data.Repositories
{
    public class OrderRepository : Repository <Order>, IOrderRepository
    {
        private ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
        public OrderViewModel GetOrderDetails(string Id)
        {
            var orderViewmodel = (from order in _context.Orders
                                    where order.Id ==Id
                                    select new OrderViewModel
                                    {
                                        Id=order.Id,
                                        UserId=order.UserId,
                                        CreatedDate=order.CreatedDate,
                                        Items=(from orderItem in _context.OrderItems
                                                join item in _context.Items
                                                on orderItem.ItemId equals item.Id
                                                where orderItem.OrderId==Id
                                                select new ItemViewModel
                                                {
                                                    Id=orderItem.Id,
                                                    Name=item.Name,
                                                    Description=item.Description,
                                                    ImagePath = item.ImagePath,
                                                    Quantity=orderItem.Quantity,
                                                    ItemId=item.Id,
                                                    Price=orderItem.Price,
                                                }).ToList()
                                    }).FirstOrDefault();
            return orderViewmodel;
        }

        public PagingList<OrderViewModel> GetOrderList (int page, int pageSize)
        {
            var pagingViewModel = new PagingList<OrderViewModel>();
            var data = (from order in _context.Orders
                        join payment in _context.PaymentDetails
                        on order.PaymentId equals payment.Id
                        select new OrderViewModel
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            PaymentId = payment.Id,
                            CreatedDate =  order.CreatedDate,
                            GrandTotal = payment.FinalTotal,
                            Locality = order.Locality
                        });
            int itemCounts = data.Count();
            var orders = data.Skip((page-1)*pageSize).Take(pageSize);

            pagingViewModel.Data=orders.ToList();
            pagingViewModel.PageNumber=page;
            pagingViewModel.PageSize=pageSize;
            pagingViewModel.TotalItems=itemCounts;
            return pagingViewModel;
        }

        public IEnumerable <Order> GetOrders (int UserId)
        {
            return _context.Orders
            .Include(o => o.OrderItems)
            .Where(x => x.UserId ==UserId).ToList();
        }
    }
}