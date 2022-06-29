using NUnit.Framework;
using SquadCapgeminiTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCapgeminiTest.Tests
{
    public class TokenGeneratorTests
    {
        private readonly TokenGenerator _tokenGenerator;
        public TokenGeneratorTests()
        {
            _tokenGenerator = new TokenGenerator();
        }

        [Test]
        public void GenerateToken_ReturnCorrectValue()
        {
            //Arrange
            long cardNumber = 345;
            int cvv = 2;

            //Act
            var act = _tokenGenerator.GenerateToken(cardNumber, cvv);

            //Assert
            Assert.AreEqual(453, act);
        }

        [Test]
        public void GenerateToken_ReturnCorrectValue_2()
        {
            //Arrange
            long cardNumber = 123;
            int cvv = 2;

            //Act
            var act = _tokenGenerator.GenerateToken(cardNumber, cvv);

            //Assert
            Assert.AreEqual(231, act);
        }
    }
}
