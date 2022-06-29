using System.ComponentModel.DataAnnotations;

namespace SquadCapgeminiTest.Entities
{
    public class CustomerCardEntity
    {
        [Key]
        public int CardId { get; set; }
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
