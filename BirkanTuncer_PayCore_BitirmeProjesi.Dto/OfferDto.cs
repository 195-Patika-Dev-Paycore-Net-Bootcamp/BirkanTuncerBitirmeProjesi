using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Dto
{
    public class OfferDto
    {
        [Required]       
        public int ProductId { get; set; }

        [Required]
        public double OfferPrice { get; set; }
        [Required]
        public string Offerer { get; set; }

        public string Status { get; set; }

    }
}
