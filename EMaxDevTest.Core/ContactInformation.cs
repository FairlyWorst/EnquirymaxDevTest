using System.ComponentModel.DataAnnotations.Schema;

namespace EMaxDevTest.Core
{
    public class ContactInformation
    {
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public Guid ContactInformationId { get; set; } = Guid.NewGuid();
        public ContactType Type { get; set; }
        [ContactValueValidator(ErrorMessage = "Please enter a valid Email address, or phone number. i.e. email@domain.com OR 07794476206")]
        public string Value { get; set; }

    }
}
