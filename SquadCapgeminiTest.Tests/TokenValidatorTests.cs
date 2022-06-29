using NUnit.Framework;
using SquadCapgeminiTest.Dtos;
using SquadCapgeminiTest.Entities;
using SquadCapgeminiTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCapgeminiTest.Tests
{
    public class TokenValidatorTests
    {
        private readonly TokenValidator _tokenValidator;
        public TokenValidatorTests()
        {
            _tokenValidator = new TokenValidator();
        }

        [Test]
        public void ValidateToken_ReturnTrue()
        {
            //Arrange
            ValidateCustomerCardDto customerCardToValidate = new ValidateCustomerCardDto { CardId = 1, CustomerId = 1, CVV = 2, Token = 453 };
            CustomerCardEntity customerCardValid = new CustomerCardEntity { CardId = 1, CustomerId = 1, CVV = 2, CardNumber = 345, CreationDate = DateTime.UtcNow };

            //Act
            var act = _tokenValidator.ValidateToken(customerCardToValidate, customerCardValid);

            //Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void ValidateToken_ReturnFalse()
        {
            //Arrange
            ValidateCustomerCardDto customerCardToValidate = new ValidateCustomerCardDto { CardId = 1, CustomerId = 1, CVV = 3, Token = 1234 };
            CustomerCardEntity customerCardValid = new CustomerCardEntity { CardId = 1, CustomerId = 1, CVV = 3, CardNumber = 31251, CreationDate = DateTime.UtcNow };

            //Act
            var act = _tokenValidator.ValidateToken(customerCardToValidate, customerCardValid);

            //Assert
            Assert.IsFalse(act);
        }
    }
}
