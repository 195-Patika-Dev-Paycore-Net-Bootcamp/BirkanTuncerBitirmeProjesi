using AutoMapper;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using BirkanTuncer_PayCore_BitirmeProjesi.Service;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;



namespace BirkanTuncer_PayCore_BitirmeProjesi.Controllers
{



    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        //The class structure I opened for validation purposes because users should only be able to interfere with their own products.
        private bool Identify(int id)
        {
            var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
            int userId = int.Parse(accountId);

            if (userId != id)
            {
                return false;
            }

            return true;
        }



        private readonly ISession session;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductController(IProductService productService, IMapper mapper, ISession session)
        {
            this.productService = productService; 
            this.mapper = mapper;
            this.session = session;
            

        }

        
        
        [HttpGet]
        public BaseResponse<IEnumerable<ProductDto>> GetAll()
        {
            

            var response = productService.GetAll();
            return response;
        }

        
        [HttpGet("{id}")]
        public BaseResponse<ProductDto> GetById(int id)
        {
            var response = productService.GetById(id);
            return response;
        }


        

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) //Users can delete their own products. After deletion, a notification message is sent to the user.
        {
            string userEmail = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;
            Product product = session.Query<Product>().Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return BadRequest("There is no product with this id");
            }


            if (Identify(product.ProductOwnerAccountId) == false)
            {
                return BadRequest("The product you want to delete does not belong to you");
            }
            var response = productService.Remove(id);

            BackgroundJob.Enqueue(() => JobFireAndForget.DeleteProduct(userEmail));
            return Ok(response);

        }





        [HttpPost]
        public ActionResult Create([FromBody] ProductDto dto) //Users can only create products for themselves. The created product cannot be shown as someone else's. In addition, a message is sent to the user for each product created.
        {
            var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;            
            dto.ProductOwnerAccountId = int.Parse(accountId);

            string userEmail = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;

            var listOfCategories = session.QueryOver<Category>().List();
            foreach (var item in listOfCategories)
            {
                if (item.Id == dto.CategoryId)
                {
                    var response = productService.Insert(dto);
                    BackgroundJob.Enqueue(() => JobFireAndForget.CreateProduct(userEmail));
                    return Ok(response);
                }

            }

            return BadRequest("There is no such category.");

        }


        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ProductDto dto) //Users can only make changes to their own products.
        {
            Product product = session.Query<Product>().Where(x => x.Id == id).FirstOrDefault();
            if(product == null)
            {
                return BadRequest("There is no product with this id");
            }
            var listOfCategories = session.QueryOver<Category>().List();

            

            if (Identify(product.ProductOwnerAccountId) == false)
            {
                return BadRequest("This product doesnt belong to you");
            }


            foreach (var category in listOfCategories)
            {
                if (category.Id == dto.CategoryId)
                {
                    dto.ProductOwnerAccountId = product.ProductOwnerAccountId;
                    var response = productService.Update(id, dto);
                    return Ok(response);
                }
            }

            return BadRequest("There is no such category id");


        }




    }







}
