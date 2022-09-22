using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public interface IEmailRabbitMQService : IBaseService<EmailRabbitMQDto, EmailRabbitMQ>
    {
    }
}
