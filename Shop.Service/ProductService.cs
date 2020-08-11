using Shop.Common;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Shop.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        void Delete(int id);

        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAll(string keyword);
        IEnumerable<Product> GetLatestProduct(int top);
        IEnumerable<Product> GetHotProduct(int top);
        IEnumerable<Product> GetAll(string[] includes, string keyWord);
        IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, out int totalRow, string sort);
        Product GetById(int id);
        void Save();
        IEnumerable<string> GetListProductByName(string name);
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository,IProductTagRepository productTagRepository,ITagRepository tagRepository,IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _productTagRepository = productTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }
        public Product Add(Product product)
        {
            _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                foreach(var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);

                    if (_tagRepository.Count(x => x.ID.Equals(tagId)) == 0)
                    {
                        Tag newTag = new Tag()
                        {
                            ID = tagId,
                            Name = tag,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(newTag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        ProductID = product.ID,
                        TagID = tagId
                       
                    };

                    _productTagRepository.Add(productTag);
                }
                

            }
            _unitOfWork.Commit();
            return product;
        }

        public void Delete(int id)
        {
            var product = _productRepository.GetSingleById(id);
            
            if (!string.IsNullOrEmpty(product.Tags)){
                string[] tags = product.Tags.Split(',');
                foreach(var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID && x.TagID.Equals(tagId));
                }
            }
            _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
        
        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return _productRepository.GetAll();
            }
        }

        

        public IEnumerable<Product> GetAll(string[] includes, string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
            {
                return _productRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord), includes);
            }
            else
            {
                return _productRepository.GetAll(includes);
            }
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLatestProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, out int totalRow,string sort)
        {
            var query = _productRepository.GetMulti(x => x.Status == true && x.CategoryID == categoryId);
            switch (sort)
            {
                case "popular":
                    query = query.OrderBy(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderBy(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                
                case "new":
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return _productRepository.GetMulti(x => x.Status == true && x.Name.Contains(name)).Select(y=>y.Name);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                foreach(var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    if (_tagRepository.Count(x => x.ID.Equals(tagId)) == 0)
                    {
                        Tag newTag = new Tag()
                        {
                            ID = tagId,
                            Name = tag,
                            Type = CommonConstants.ProductTag
                        };

                    }

                    if(_productTagRepository.Count(x=>x.ProductID==product.ID && x.TagID == tagId) == 0)
                    {
                        ProductTag productTag = new ProductTag()
                        {
                            ProductID = product.ID,
                            TagID=tagId
                        };
                    }
                }
            }
        }
    }
}