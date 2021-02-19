using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        // : base means we are using base constructor
        public OrdersWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }
        
        //this constructor will be used for our individual order
        public OrdersWithItemsAndOrderingSpecification(int id, string email) 
            : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}