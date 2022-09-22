using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using System.ComponentModel.DataAnnotations;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Dto
{
    public class CategoryDto
    {
        [Required]
        [Display(Name = "CategoryName")]
        public string CategoryName { get; set; }

    }
}
