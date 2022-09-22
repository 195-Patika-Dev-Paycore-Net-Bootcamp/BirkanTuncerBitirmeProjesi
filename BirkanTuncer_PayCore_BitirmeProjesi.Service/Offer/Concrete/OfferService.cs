using AutoMapper;
using NHibernate;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using System.Linq;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public class OfferService : BaseService<OfferDto, Offer>, IOfferService
    {

        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Offer> hibernateRepository;

        public OfferService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Offer>(session);
        }

        public override BaseResponse<OfferDto> Insert(OfferDto insertResource)
        { 
            Offer offer = new Offer();    
            Account account = new Account();

            //offer.OfferPrice = insertResource.OfferPrice;
            //offer.ProductId = insertResource.ProductId;
            //offer.Offerer = account.Email;
            //offer.Status = "Waiting..";
            //insertResource
            return base.Insert(insertResource);
        }

        public override BaseResponse<OfferDto> Update(int id, OfferDto updateResource)
        {
            Offer offer = session.Query<Offer>().Where(x => x.Id == id).FirstOrDefault(); //NULL KONTROLU YAP
            updateResource.ProductId = offer.ProductId;
            updateResource.OfferPrice = offer.OfferPrice;
            updateResource.Offerer = offer.Offerer;



            return base.Update(id, updateResource);
        }






    }
}
