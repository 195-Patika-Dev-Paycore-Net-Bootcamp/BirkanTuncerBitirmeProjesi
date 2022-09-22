using AutoMapper;
using NHibernate;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public class AccountService : BaseService<AccountDto, Account>, IAccountService
    {

        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Account> hibernateRepository;

        public AccountService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Account>(session);
        }

        public override BaseResponse<AccountDto> Insert(AccountDto insertResource)
        {

            Encryption enc = new Encryption();
            insertResource.Password = enc.MD5Encryption(insertResource.Password);
            return base.Insert(insertResource);
        }

        public override BaseResponse<AccountDto> Update(int id, AccountDto updateResource)
        {
            return base.Update(id, updateResource);
        }


    }
}
