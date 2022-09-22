using AutoMapper;
using NHibernate;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using System.Collections.Generic;
using System.Linq;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public class ProductService : BaseService<ProductDto, Product>, IProductService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Product> hibernateRepository;

        public ProductService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Product>(session);
        }

        public override BaseResponse<IEnumerable<ProductDto>> GetAll()
        {
            return base.GetAll();
        }

        public override BaseResponse<ProductDto> GetById(int id)
        {
            return base.GetById(id);
        }

        public override BaseResponse<ProductDto> Insert(ProductDto insertResource)
        {
            return base.Insert(insertResource);
        }

        public override BaseResponse<ProductDto> Update(int id, ProductDto updateResource)
        {
            
            return base.Update(id, updateResource);
        }
        public BaseResponse<ProductDto> UpdateOperation(int id, ProductDto updateResource) 
        {
            Product product = session.Query<Product>().Where(x => x.Id == id).FirstOrDefault(); 
            updateResource.Id = id;
            updateResource.ProductName = product.ProductName;
            updateResource.ProductDescription = product.ProductDescription;
            updateResource.ProductBrand = product.ProductBrand;
            updateResource.ProductColor = product.ProductColor;
            updateResource.ProductOwnerAccountId = product.ProductOwnerAccountId;
            updateResource.CategoryId = product.CategoryId;
            return base.Update(id, updateResource);
        }

    }
}
