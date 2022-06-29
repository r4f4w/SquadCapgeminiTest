using System.ComponentModel.DataAnnotations;

namespace SquadCapgeminiTest.Dtos
{
    public class ValidateCustomerCardDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int CardId { get; set; }
        [Required]
        public long Token { get; set; }
        [Required]
        public int CVV { get; set; }
    }
}
