using Microsoft.EntityFrameworkCore;
using Photo.Manufacture.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Photo.Manufacture.Infrastructure.Data
{
    public class EfProductRepository : EfRepository<Product>
    {
        public EfProductRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Task<Product> GetByIdAsync(int id)
        {
            return dbContext.Set<Product>()
                .Include(x => x.ProductType)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public override Task<List<Product>> ListAsync()
        {
            return dbContext.Set<Product>()
                .Include(x => x.ProductType)
                .ToListAsync();
        }
    }
}
