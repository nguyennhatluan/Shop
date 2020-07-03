using Shop.Data.Infrastructure;
using Shop.Model.Models;

namespace Shop.Data.Repositories
{
    public interface IOrderDetailRepository { }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}