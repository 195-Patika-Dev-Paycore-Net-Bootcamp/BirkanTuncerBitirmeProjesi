using AutoMapper;
using NHibernate;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using System.Collections.Generic;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public class CategoryService : BaseService<CategoryDto, Category>, ICategoryService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Category> hibernateRepository;

        public CategoryService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Category>(session);
        }

        public override BaseResponse<IEnumerable<CategoryDto>> GetAll()
        {
            return base.GetAll();
        }

        public override BaseResponse<CategoryDto> GetById(int id)
        {
            return base.GetById(id);
        }

        public override BaseResponse<CategoryDto> Insert(CategoryDto insertResource)
        {
            return base.Insert(insertResource);
        }



    }
}
