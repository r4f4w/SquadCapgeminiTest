using Microsoft.EntityFrameworkCore;
using SquadCapgeminiTest.Entities;

namespace SquadCapgeminiTest.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<CustomerCardEntity> CustomerCard { get; set; }
    }
}
