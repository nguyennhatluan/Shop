using Shop.Data.Infrastructure;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        IEnumerable<Product> GetListProductByTag(string tagID, int page, int pageSize, out int totalRow);
        IEnumerable<Tag> GetListTagByProductID(int id);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetListProductByTag(string tagID, int page, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join
                        pt in DbContext.ProductTags on
                        p.ID equals pt.ProductID
                        where pt.TagID.Equals(tagID)
                        select p;
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Tag> GetListTagByProductID(int id)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        join t in DbContext.Tags on pt.TagID equals t.ID
                        where p.ID == id
                        select t;
            return query;
        }
    }
}
