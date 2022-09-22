using AutoMapper;
using NHibernate;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using System.Collections.Generic;
using System.Linq;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public class UserOperationService : BaseService<UserOperationDto, UserOperation>, IUserOperationService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<UserOperation> hibernateRepository;

        public UserOperationService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<UserOperation>(session);
        }

        public override BaseResponse<IEnumerable<UserOperationDto>> GetAll()
        {
            return base.GetAll();
        }

        public override BaseResponse<UserOperationDto> GetById(int id)
        {
            return base.GetById(id);
        }

        public override BaseResponse<UserOperationDto> Insert(UserOperationDto insertResource)
        {
            return base.Insert(insertResource);
        }

        public override BaseResponse<UserOperationDto> Update(int id, UserOperationDto updateResource)
        {
            Product product = session.Query<Product>().Where(x => x.Id == id).FirstOrDefault(); //NULL KONTROLU YAP
            updateResource.Id = id;
            updateResource.ProductName = product.ProductName;
            updateResource.ProductDescription = product.ProductDescription;
            updateResource.ProductPrice = product.ProductPrice;
            updateResource.ProductBrand = product.ProductBrand;
            updateResource.ProductColor = product.ProductColor;
            updateResource.ProductOwnerAccountId = product.ProductOwnerAccountId;
            updateResource.CategoryId = product.CategoryId;



            return base.Update(id, updateResource);
        }



    }
}
