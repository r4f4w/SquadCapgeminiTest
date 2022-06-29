using SquadCapgeminiTest.Entities;

namespace SquadCapgeminiTest.Repositories
{
    public interface ICustomerCardRepository
    {
        Task<CustomerCardEntity> GetCustomerCardAsync(int id);
        Task<int> AddAsync(CustomerCardEntity item);
    }
}
