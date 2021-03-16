using Microsoft.EntityFrameworkCore;
using Photo.Manufacture.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Photo.Manufacture.Infrastructure.Data
{
    public class EfOrderRepository : EfRepository<Order>
    {
        public EfOrderRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Task<Order> GetByIdAsync(int id)
        {
            return dbContext.Set<Order>()
                .Include(x => x.OrderItems)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public override Task<List<Order>> ListAsync()
        {
            return dbContext.Set<Order>()
                .Include(x => x.OrderItems)
                .ToListAsync();
        }
    }
}
