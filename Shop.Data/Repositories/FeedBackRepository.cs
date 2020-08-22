using Shop.Data.Infrastructure;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public interface IFeedBackRepository:IRepository<FeedBack>
    {

    }
    public class FeedBackRepository:RepositoryBase<FeedBack>,IFeedBackRepository
    {
        IDbFactory _dbFactory;
        IUnitOfWork _unitOfWork;

        public FeedBackRepository(IDbFactory dbFactory,IUnitOfWork unitOfWork):base(dbFactory)
        {
            _dbFactory = dbFactory;
            _unitOfWork = unitOfWork;
        }
    }
}
