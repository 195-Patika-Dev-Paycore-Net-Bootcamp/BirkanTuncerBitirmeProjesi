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
    public class DummyCategoryController : ControllerBase
    {
        private DummyCategory DummyCategory;
        private IHibernateRepository<DummyCategory> @object;


        public DummyCategoryController(IHibernateRepository<DummyCategory> repository, IMapper mapper)
        {
            this.@object = repository;
        }


        public DummyCategoryController(IHibernateRepository<DummyCategory> @object)
        {
            this.@object = @object;
        }
        [NonAction]
        [HttpGet]
        public  List<DummyCategory> Get()
        {
            var categories = @object.GetAll();
            return categories;
        }
        [NonAction]
        [HttpGet("{id}")]
        public DummyCategory GetItem(int id)
        {
            var item = @object.GetById(id);

            if (item == null)
                return null;

            return item;
        }

      

    }
}
