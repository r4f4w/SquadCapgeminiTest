using SquadCapgeminiTest.Dtos;
using SquadCapgeminiTest.Entities;

namespace SquadCapgeminiTest.Helpers
{
    public class TokenValidator
    {
        public bool ValidateToken(ValidateCustomerCardDto customerCardToValidate, CustomerCardEntity customerCardValid)
        {
            //Check if creation date is more than 30 min
            //Check if customer is the owner of the card
            if (customerCardValid.CreationDate < DateTime.UtcNow.AddMinutes(-30) ||
                customerCardToValidate.CustomerId != customerCardValid.CustomerId)
            {
                return false;
            }

            Console.WriteLine("Card number: " + customerCardValid.CardNumber);

            //Generate token with located card number and received CVV
            var token = new TokenGenerator().GenerateToken(customerCardValid.CardNumber, customerCardToValidate.CVV);

            //Check if generated token is equal to received token
            if (token == customerCardToValidate.Token)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
