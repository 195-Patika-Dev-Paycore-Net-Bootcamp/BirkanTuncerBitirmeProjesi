using AutoMapper;
using NHibernate;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public class OrderService : BaseService<OrderDto, Order>, IOrderService
    {

        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Order> hibernateRepository;

        public OrderService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Order>(session);
        }

        public override BaseResponse<OrderDto> Insert(OrderDto insertResource)
        {
            
            


            return base.Insert(insertResource);
        }

        public override BaseResponse<OrderDto> Update(int id, OrderDto updateResource)
        {
            return base.Update(id, updateResource);
        }


    }
}
