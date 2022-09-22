using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Dto
{
    public class AccountDto
    {

        [Required]
        [MaxLength(125)]
        [UserNameAttribute]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(500)]
        public string Email { get; set; }

        
        [RoleAttribute]
        public string Role { get; set; }


        [Display(Name = "LastActivity")]
        public DateTime LastActivity { get; set; }
    }
}
