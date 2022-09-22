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
using System.Text.Json.Serialization;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationController : ControllerBase
    {
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
        private readonly IOfferService offerService;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly IUserOperationService userOperationService;

        public UserOperationController(IOfferService offerService, IMapper mapper, ISession session, IProductService productService, IUserOperationService userOperationService, IOrderService orderService)
        {
            this.offerService = offerService;
            this.productService = productService;
            this.userOperationService = userOperationService;
            this.orderService = orderService;
            this.mapper = mapper;
            this.session = session;
            
            
        }

        [HttpGet("List_My_Offers")]
        public ActionResult GetAllOffers() //A method where I can list the offers I have made for someone else's product
        {
            string userEmail = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;

            var data = session.QueryOver<Offer>().Where(x => x.Offerer == userEmail).List();

            return Ok(data);
        }

        [HttpGet("List_My_Orders")] // A method where I can see the products I have purchased
        public ActionResult GetAllOrders()
        {
            string userEmail = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;

            var data = session.QueryOver<Order>().Where(x => x.Buyer == userEmail).List();

            return Ok(data);
        }



        [HttpGet("List_My_Offered_Products")] //A method where I can list all offers made to my products
        public ActionResult GetAllOfferedProducts()
        {
            List<Offer> offeredProducts = new List<Offer>();
            var accountId = (User.Identity as ClaimsIdentity).FindFirst("AccountId").Value;
            int UserId = int.Parse(accountId);

            var listOfOffers = session.QueryOver<Offer>().List();
            var listOfUserProducts = session.QueryOver<Product>().Where(x => x.ProductOwnerAccountId == UserId).List();
            foreach (var product in listOfUserProducts)
            {
                
                foreach (var offer in listOfOffers)
                {
                    if(product.Id == offer.ProductId)
                    {
                        offeredProducts.Add(offer);
                    }
                }              
            }

            return Ok(offeredProducts);
        }


        [HttpPost("Make_An_Offer")]
        public ActionResult Offer([FromBody] OfferDto dto)
        {
            var listOfProducts = session.QueryOver<Product>().List();
            foreach (var item in listOfProducts)
            {
                if (dto.ProductId == item.Id)
                {
                    if (item.isOfferable == false)
                    {
                        return BadRequest("Product is not offerable !");
                    }

                    dto.Offerer = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;
                    dto.Status = "Waiting..";
                    var response = offerService.Insert(dto);
                    return Ok(response);
                }
            }

            return BadRequest("Can't find the product");

        }



        [HttpPut("{id}/Buy")]
        public ActionResult Buy(int id) //The method by which I made the purchase. No one can buy that item again after the purchase has been made.
        {
            string userEmail = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;


            Product product = session.Query<Product>().Where(x => x.Id == id).FirstOrDefault();

            if (product == null || product.isSold == true)
            {
                return BadRequest("There is no sellable product with this id");
            }
            if (Identify(product.ProductOwnerAccountId) == true)
            {
                return BadRequest("You cannot buy your own product");
            }




            var listOfOffers = session.QueryOver<Offer>().List();


            
            ProductDto pdto = new ProductDto();

            pdto.isSold = true;
            pdto.isOfferable = false;
            productService.UpdateOperation(id, pdto);

            OfferDto dto = new OfferDto();

            foreach (var item in listOfOffers)
            {
                if (item.ProductId == id)
                {
                    dto.Status = "Rejected";
                    dto.ProductId = item.ProductId;
                    dto.OfferPrice = item.OfferPrice;
                    dto.Offerer = item.Offerer;
                    offerService.Update(item.Id, dto);
                }
            }


            OrderDto orderdto = new OrderDto();
            orderdto.Id = 1;
            orderdto.ProductName = product.ProductName;
            orderdto.ProductId = product.Id;
            orderdto.Buyer = userEmail;
            orderService.Insert(orderdto);


            BackgroundJob.Enqueue(() => JobFireAndForget.Buy(userEmail));
            return Ok("The product has been bought");

        }






        [HttpPut("{id}/Accept")]
        public ActionResult Accept(int id) //The method I accepted the offer made. Also, once an offer is accepted, all other offers for that product are automatically rejected.
        {
            string userEmail = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;



            Offer offer = session.Query<Offer>().Where(x => x.Id == id).FirstOrDefault();
            if (offer == null)
            {
                return BadRequest("There is no acceptable product with this id");
            }
            Product product = session.Query<Product>().Where(x => x.Id == offer.ProductId).FirstOrDefault();

            if (Identify(product.ProductOwnerAccountId) == false)
            {
                return BadRequest("No product belonging to you was found in this id");
            }


            var listOfOffers = session.QueryOver<Offer>().List();


            if (offer.Status.Equals("Accepted"))
            {
                return BadRequest("The product has already been sold");
            }


            OfferDto dto = new OfferDto();
            dto.Status = "Accepted";
            var response = offerService.Update(id, dto);

            

            ProductDto pdto = new ProductDto();
            

            pdto.isSold = false;
            pdto.isOfferable = true;
            pdto.ProductPrice = offer.OfferPrice;
            productService.UpdateOperation(offer.ProductId, pdto);

            foreach (var item in listOfOffers)
            {
                if (item.ProductId == offer.ProductId && !item.Status.Equals("Accepted")) 
                {
                    dto.Status = "Rejected";
                    dto.ProductId = item.ProductId;
                    dto.OfferPrice = item.OfferPrice;
                    dto.Offerer = item.Offerer;
                    offerService.Update(item.Id, dto);
                }
            }

            BackgroundJob.Enqueue(() => JobFireAndForget.OfferAccept(userEmail));
            return Ok("The offer has been accepted");
        }





        [HttpPut("{id}/Reject")]
        public ActionResult Reject(int id) //The method by which I perform the Reject operation
        {
            string userEmail = (User.Identity as ClaimsIdentity).FindFirst("Email").Value;

            Offer offer = session.Query<Offer>().Where(x => x.Id == id).FirstOrDefault();
            if (offer == null)
            {
                return BadRequest("There is no rejectable product with this id");
            }
            Product product = session.Query<Product>().Where(x => x.Id == offer.ProductId).FirstOrDefault();

            if(Identify(product.ProductOwnerAccountId) == false)
            {
                return BadRequest("No product belonging to you was found in this id");
            }


            OfferDto dto = new OfferDto();
            dto.Status = "Rejected";
            var response = offerService.Update(id, dto);

            ProductDto pdto = new ProductDto();
            pdto.isSold = false;
            pdto.isOfferable = true;
            pdto.ProductPrice = product.ProductPrice;
            productService.UpdateOperation(offer.ProductId, pdto);

            BackgroundJob.Enqueue(() => JobFireAndForget.OfferReject(userEmail));
            return Ok("Offer has been rejected");
        }



    }
}
