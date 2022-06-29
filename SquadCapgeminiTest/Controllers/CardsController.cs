using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SquadCapgeminiTest.Context;
using SquadCapgeminiTest.Dtos;
using SquadCapgeminiTest.Entities;
using SquadCapgeminiTest.Helpers;
using SquadCapgeminiTest.Repositories;

namespace SquadCapgeminiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : Controller
    {
        private ICustomerCardRepository _customerCardRepository;
        private readonly IMapper _mapper;

        public CardsController(ICustomerCardRepository customerCardRepository, IMapper mapper)
        {
            _customerCardRepository = customerCardRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomerCard([FromBody] CreateCustomerCardDto customerCardDto)
        {
            if (customerCardDto.CardNumber > 9999999999999999)
            {
                return BadRequest("Card number cannot be larger than 16 digits.");
            }
            else if (customerCardDto.CVV > 99999)
            {
                return BadRequest("CVV cannot be larger than 16 digits.");
            }

            if (customerCardDto.CardNumber == 0)
            {
                return BadRequest("Card number cannot be 0.");
            }
            else if(customerCardDto.CVV == 0)
            {
                return BadRequest("CVV cannot be 0.");
            }
            else if (customerCardDto.CustomerId == 0)
            {
                return BadRequest("CustomerId cannot be 0.");
            }


            CustomerCardEntity customerCardEntity = _mapper.Map<CustomerCardEntity>(customerCardDto);
            long token;

            try
            {
                token = new TokenGenerator().GenerateToken(customerCardDto.CardNumber, customerCardDto.CVV);
            }
            catch (Exception ex)
            {
                return BadRequest("Error while trying to generate token: " + ex);
            }

            customerCardEntity.CreationDate = DateTime.UtcNow;

            try
            {
                customerCardEntity.CardId = await _customerCardRepository.AddAsync(customerCardEntity);
            }
            catch (Exception ex)
            {
                return BadRequest("Error while trying to add customer card: " + ex);
            }
            

            return Ok(new
            {
                RegistrationDate = customerCardEntity.CreationDate,
                Token = token,
                CardId = customerCardEntity.CardId
            });
        }

        [HttpGet]
        public async Task<IActionResult> ValidateCardToken([FromQuery] ValidateCustomerCardDto customerCardDto)
        {
            if (customerCardDto.CVV > 99999)
            {
                return BadRequest("CVV cannot be larger than 16 digits.");
            } else if (customerCardDto.CVV == 0)
            {
                return BadRequest("CVV cannot be 0.");
            }

            if (customerCardDto.CardId == 0)
            {
                return BadRequest("CardId cannot be 0.");
            }

            if (customerCardDto.CustomerId == 0)
            {
                return BadRequest("CustomerId cannot be 0.");
            }

            bool tokenIsValid;
            CustomerCardEntity customerCardReturned;

            try
            {
                customerCardReturned = await _customerCardRepository.GetCustomerCardAsync(customerCardDto.CardId);
            }
            catch (Exception ex)
            {
                return BadRequest("Error while trying to retrieve customer card: " + ex);
            }

            try
            {
                tokenIsValid = new TokenValidator().ValidateToken(customerCardDto, customerCardReturned);
            }
            catch (Exception ex )
            {
                return BadRequest("Error while trying to validate token: " + ex);
            }



            return Ok(new
            {
                Validated = tokenIsValid
            });
        }
    }
}
