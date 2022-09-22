using System.ComponentModel.DataAnnotations;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Base
{
    public class TokenRequest
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
