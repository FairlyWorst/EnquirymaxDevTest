using System.ComponentModel.DataAnnotations;

namespace EMaxDevTest.Core
{
    public class Customer
    {
        public Guid CustomerId{ get; set; }

        public string Forename { get; set; }

        [Required(ErrorMessage = "Surname is Required")]
        [MinLength(3, ErrorMessage = "A Minimum of Three Letters is Required")]
        [RegularExpression("[^0-9]", ErrorMessage = "No numbers are allowed")]
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public IEnumerable<ContactInformation> ContactInformation { get; set; }

    }
}