using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Data
{
    public class Offer
    {
        public virtual int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual double OfferPrice { get; set; }
        public virtual string Offerer { get; set; }
        public virtual string Status { get; set; }
    }
}
