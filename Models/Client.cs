using System.ComponentModel.DataAnnotations;


namespace AdarshSale.Models
{
    public class LoginDet
    {
        [Display(Name = "User Id")]
        [Required(ErrorMessage = "User Id Required")]

        public string UserId { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
    public class Client
    {


        public int id { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage ="Name Required")]
        public string companyName { get; set; }

        [Required(ErrorMessage = "Contact Person Required")]
        [Display(Name = "Contact Person")] 
        public string contactPer { get; set; }



        [Required(ErrorMessage = "Please enter phone number")] 
        [Display(Name = "Phone Number")]
        [Phone] 
        public string contactMobile { get; set; }



        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string contactEmail { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required")]

        public string address { get; set; }



    }
}
