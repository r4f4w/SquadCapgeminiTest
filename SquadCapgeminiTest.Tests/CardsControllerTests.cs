using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SquadCapgeminiTest.Controllers;
using SquadCapgeminiTest.Dtos;
using SquadCapgeminiTest.Entities;
using SquadCapgeminiTest.MappingsProfiles;
using SquadCapgeminiTest.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadCapgeminiTest.Tests
{
    public class CardsControllerTests
    {
        private readonly CardsController _cardsController;
        private Mock<ICustomerCardRepository> _repository;
        private readonly IMapper _mapper;

        public CardsControllerTests()
        {
            _repository = new Mock<ICustomerCardRepository>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CustomerCardMappings());
            });
            _mapper = mapperConfig.CreateMapper();
            _cardsController = new CardsController(_repository.Object, _mapper);
        }

        [Test]
        public async Task RegisterCustomerCard_Return200()
        {
            //Arrange
            var createCustomerCardDto = new CreateCustomerCardDto{ CardNumber = 1234, CustomerId = 1, CVV = 2 };
            var returnCardDto = new
            {
                RegistrationDate = DateTime.UtcNow,
                Token = 3412,
                CardId = 1
            };

            //Act
            var actionResult = await _cardsController.RegisterCustomerCard(createCustomerCardDto);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test]
        [TestCaseSource("CreateCustomerCardDto_MultipleInvalidFields")]
        public async Task RegisterCustomerCard_InvalidFields_Return400(CreateCustomerCardDto createCustomerCardDto)
        {
            //Act
            var actionResult = await _cardsController.RegisterCustomerCard(createCustomerCardDto);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }

        [Test]
        public async Task ValidateCardToken_Return200()
        {
            //Arrange
            var validateCustomerCardDto = new ValidateCustomerCardDto { CustomerId = 1, CardId = 1, Token = 1234, CVV = 2 };
            var returnCardDto = new
            {
                Validated = true
            };
            var customerCardEntity = new CustomerCardEntity { CardId = 1, CardNumber = 1234, CustomerId = 1, CreationDate = DateTime.UtcNow, CVV = 2 };
            _repository.Setup(x => x.GetCustomerCardAsync(1)).ReturnsAsync(customerCardEntity);

            //Act
            var actionResult = await _cardsController.ValidateCardToken(validateCustomerCardDto);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult);
        }

        [Test]
        [TestCaseSource("ValidateCustomerCardDto_MultipleInvalidFields")]
        public async Task ValidateCardToken_InvalidFields_Return400(ValidateCustomerCardDto validateCustomerCardDto)
        {
            //Act
            var actionResult = await _cardsController.ValidateCardToken(validateCustomerCardDto);

            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);
        }

        public static IEnumerable<TestCaseData> CreateCustomerCardDto_MultipleInvalidFields
        {
            get
            {
                yield return new TestCaseData(new CreateCustomerCardDto { CardNumber = 12345678901234567, CustomerId = 1, CVV = 2 });
                yield return new TestCaseData(new CreateCustomerCardDto { CardNumber = 1234, CustomerId = 1, CVV = 123456 });
                yield return new TestCaseData(new CreateCustomerCardDto { CardNumber = 0, CustomerId = 1, CVV = 1234 });
                yield return new TestCaseData(new CreateCustomerCardDto { CardNumber = 1234, CustomerId = 0, CVV = 1234 });
                yield return new TestCaseData(new CreateCustomerCardDto { CardNumber = 1234, CustomerId = 1, CVV = 0 });
            }
        }

        public static IEnumerable<TestCaseData> ValidateCustomerCardDto_MultipleInvalidFields
        {
            get
            {
                yield return new TestCaseData(new ValidateCustomerCardDto { CustomerId = 1, CardId = 1, Token = 1, CVV = 123456 });
                yield return new TestCaseData(new ValidateCustomerCardDto { CustomerId = 0, CardId = 1, Token = 1, CVV = 1 });
                yield return new TestCaseData(new ValidateCustomerCardDto { CustomerId = 1, CardId = 0, Token = 1, CVV = 1 });
                yield return new TestCaseData(new ValidateCustomerCardDto { CustomerId = 1, CardId = 1, Token = 1, CVV = 0 });
            }
        }
    }
}
