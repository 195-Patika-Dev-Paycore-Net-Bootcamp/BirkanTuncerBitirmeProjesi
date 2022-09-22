using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Dto
{
    public class ProductDto
    {
        [Required]        
        [Display(Name = "id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ProductOwnerAccountId")]
        public int ProductOwnerAccountId { get; set; }

        [Required]
        [Display(Name = "ProductName")]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "ProductDescription")]
        [MaxLength(500)]
        public string ProductDescription { get; set; }

        [Required]
        [Display(Name = "ProductColor")]
        public string ProductColor { get; set; }

        [Required]
        [Display(Name = "ProductBrand")]
        public string ProductBrand { get; set; }

        [Required]
        [Display(Name = "ProductPrice")]
        public double ProductPrice { get; set; }

        [Required]
        [Display(Name = "isOfferable")]
        public bool isOfferable { get; set; }

        [Required]
        [Display(Name = "isSold")]
        public bool isSold { get; set; }

        [Required]
        [Display(Name = "CategoryId")]
        public int CategoryId { get; set; }





    }
}
