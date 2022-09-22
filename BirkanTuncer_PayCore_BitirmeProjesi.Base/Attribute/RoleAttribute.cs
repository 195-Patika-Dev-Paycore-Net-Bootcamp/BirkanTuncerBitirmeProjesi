using System;
using System.ComponentModel.DataAnnotations;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Base
{
    public class RoleAttribute : ValidationAttribute
    {
        /// <summary>
        /// 
        /// Only users with admin authority can perform some operations. This is the section where I keep this record
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (value is null)
                    return new ValidationResult("Invalid Role Name.");


                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid Role field.");
            }
        }
    }
}
