using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using FluentValidation;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Dto
{
    public class EmailRabbitMQDto
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public string Status { get; set; }

    }
}
