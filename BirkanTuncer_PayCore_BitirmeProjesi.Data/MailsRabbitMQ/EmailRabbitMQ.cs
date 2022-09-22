using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Data
{
    public class EmailRabbitMQ
    {

        public virtual int Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Status { get; set; }
    }
}
