using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Data
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string Buyer { get; set; }

    }
}
