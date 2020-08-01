using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public interface ISlideService
    {
        IEnumerable<Slide> GetAll();
    }
    public class SlideService:ISlideService
    {
        IErrorRepository _errorRepository;
        ISlideRepository _slideRepository;

        public SlideService(IErrorRepository errorRepository, ISlideRepository slideRepository)
        {
            _errorRepository = errorRepository;
            _slideRepository = slideRepository;
        }
        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }
    }
}
