using System.ComponentModel.DataAnnotations;

namespace SquadCapgeminiTest.Dtos
{
    public class CreateCustomerCardDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public long CardNumber { get; set; }

        [Required]
        public int CVV { get; set; }
    }
}
