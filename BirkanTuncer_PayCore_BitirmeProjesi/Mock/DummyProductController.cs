using AutoMapper;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using Microsoft.AspNetCore.Mvc;


using System.Collections.Generic;
using System.Threading.Tasks;


namespace BirkanTuncer_PayCore_BitirmeProjesi
{
    [ApiController]
    [Route("nhb/v1/api/[controller]")]
    //Dummy controllers I created to perform controller test operations
    public class DummyProductController : ControllerBase
    {
        private DummyProduct DummyProduct;
        private IHibernateRepository<DummyProduct> @object;


        public DummyProductController(IHibernateRepository<DummyProduct> repository, IMapper mapper)
        {
            this.@object = repository;
        }


        public DummyProductController(IHibernateRepository<DummyProduct> @object)
        {
            this.@object = @object;
        }
        [NonAction]
        [HttpGet]
        public List<DummyProduct> Get()
        {
            var products = @object.GetAll();
            return products;
        }
        [NonAction]
        [HttpGet("{id}")]
        public DummyProduct GetItem(int id)
        {
            var item = @object.GetById(id);

            if (item == null)
                return null;

            return item;
        }



    }
}
