using AutoMapper;
using NHibernate;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using System.Collections.Generic;
using System.Linq;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public class EmailRabbitMQService : BaseService<EmailRabbitMQDto, EmailRabbitMQ>, IEmailRabbitMQService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<EmailRabbitMQ> hibernateRepository;

        public EmailRabbitMQService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<EmailRabbitMQ>(session);
        }

        

        public override BaseResponse<EmailRabbitMQDto> Insert(EmailRabbitMQDto insertResource)
        {
            return base.Insert(insertResource);
        }

        
        

    }
}
