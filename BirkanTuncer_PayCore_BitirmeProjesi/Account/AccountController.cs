using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using BirkanTuncer_PayCore_BitirmeProjesi.Service;
using Microsoft.AspNetCore.Authorization;
using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using System.Collections.Generic;
using Hangfire;
using NHibernate;
using System.Linq;
using Hangfire.Server;

namespace BirkanTuncer_PayCore_BitirmeProjesi
{
    [ApiController]
    [Route("api/nhb/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IEmailRabbitMQService emailRabbitMQService;
        private readonly IAccountService accountService;
        private readonly ISession session;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// All methods except account creation require admin authority.
        /// In some methods, users were returned using the hangfire service.
        /// 
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="mapper"></param>
        /// <param name="session"></param>
        /// <param name="emailRabbitMQService"></param>
        public AccountController(IAccountService accountService, IMapper mapper, ISession session, IEmailRabbitMQService emailRabbitMQService)
        {
            this.session = session;
            this.mapper = mapper;
            this.accountService = accountService;
            this.emailRabbitMQService = emailRabbitMQService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetAllAccounts")]
        public BaseResponse<IEnumerable<AccountDto>> GetAll()
        {
            var response = accountService.GetAll();
            return response;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("FindAccountByID")]
        public BaseResponse<AccountDto> GetById(int id)
        {
            var response = accountService.GetById(id);
            return response;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteAccount")]
        public BaseResponse<AccountDto> Delete(int id)
        {
            var response = accountService.Remove(id);
            return response;
        }

        
        [HttpPost("CreateAccount")]
        public ActionResult Create([FromBody] AccountDto dto)
        {
            var listOfAccounts = session.QueryOver<Account>().List();
            foreach (var account in listOfAccounts)
            {
                if(account.Email == dto.Email)
                {
                    return BadRequest("This account already registered"); // You cannot register with the same email account.
                }
            }

            var response = accountService.Insert(dto);

            EmailRabbitMQDto emailRabbitMQ = new EmailRabbitMQDto(); // Apart from the account table, I keep an extra table to use in the rabbitmq service. In this table, I keep the e-mails of the users who have just registered to the system, together with the status as propery.
            emailRabbitMQ.Email = dto.Email;
            emailRabbitMQ.Status = "Ready For Queue";
            emailRabbitMQService.Insert(emailRabbitMQ);

            
            BackgroundJob.Enqueue(() => JobFireAndForget.Welcome(dto.Email)); //Message sent to user with Hangfire service

            return Ok("Signed up successfully !");

        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateAccount")]
        public BaseResponse<AccountDto> Update(int id, [FromBody] AccountDto dto)
        {
            var response = accountService.Update(id, dto);
            return response;
        }



    }
}
