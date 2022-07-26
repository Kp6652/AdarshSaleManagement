
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdarshSale.Models
{
    public class saleentry
    {
        public int id { set; get; }

        [Display(Name = "Client")]
        public int client_id { set; get; }


        [Display(Name = "Sale Date")]
        [Required(ErrorMessage = "Sale Date Required")]
        [DataType(DataType.Date)]
        public DateTime saleDate { set; get; } = DateTime.Now;


        [Required(ErrorMessage ="Please Enter Unit Price")]
        [Display(Name = "Unit Price")]
        [DefaultValue(0.00)]
        public decimal unitPrice { set; get; }

        [DefaultValue(0)]
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please Enter qty")]
        
        public int qty { set; get; }

        
        [Display(Name = "Total")]

        public decimal total{ set; get; }

        [Required(ErrorMessage = "Please Enter Remarks")]
        [Display(Name = "Remarks")]
        [StringLength(140)]
        public string remarks{ set; get; }
    }
}
