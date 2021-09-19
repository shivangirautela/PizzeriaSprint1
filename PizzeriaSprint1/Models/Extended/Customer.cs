using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaSprint1.Models
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
        public string ConfirmPassword { get; set; }
    }

    public class CustomerMetadata
    {
        [Display(Name = "First Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name = "Email ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address required")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City name required")]
        public string City { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimumn 6 characters required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        [MinLength(6, ErrorMessage = "Minimumn 6 characters required")]
        public string ConfirmPassword { get; set; }


    }
}