using AutoMapper;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace BirkanTuncer_PayCore_BitirmeProjesi
{
    [ApiController]
    [Route("nhb/v1/api/[controller]")]
    //Dummy controllers I created to perform controller test operations
    public class DummyAccountController : ControllerBase
    {
        private DummyAccount DummyAccount;
        private IHibernateRepository<DummyAccount> @object;


        public DummyAccountController(IHibernateRepository<DummyAccount> repository, IMapper mapper)
        {
            this.@object = repository;
        }


        public DummyAccountController(IHibernateRepository<DummyAccount> @object)
        {
            this.@object = @object;
        }

        [NonAction]
        [HttpGet]
        public List<DummyAccount> Get()
        {
            var categories = @object.GetAll();
            return categories;
        }
        [NonAction]
        [HttpGet("{id}")]
        public DummyAccount GetItem(int id)
        {
            var item = @object.GetById(id);

            if (item == null)
                return null;

            return item;
        }



    }
}
