using SquadCapgeminiTest.Context;
using SquadCapgeminiTest.Entities;

namespace SquadCapgeminiTest.Repositories
{
    public class CustomerCardRepository : ICustomerCardRepository
    {
        private readonly ApiContext _dbContext;

        public CustomerCardRepository(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(CustomerCardEntity custumerCard)
        {
            await _dbContext.CustomerCard.AddAsync(custumerCard);
            await _dbContext.SaveChangesAsync();

            return custumerCard.CardId;
        }

        public async Task<CustomerCardEntity> GetCustomerCardAsync(int id)
        {
            return await _dbContext.CustomerCard.FindAsync(id);
        }
    }
}
