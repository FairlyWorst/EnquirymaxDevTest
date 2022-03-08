namespace EMaxDevTest.Core
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Address
    {
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; } = Guid.NewGuid();
        [RegularExpression("[0-9]")]
        public int HouseNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("(GIR 0AA)|((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY])))) [0-9][A-Z-[CIKMOV]]{2})", ErrorMessage = "Please enter a valid UK Postcode")]
        public string PostCode { get; set; }
    }
}
