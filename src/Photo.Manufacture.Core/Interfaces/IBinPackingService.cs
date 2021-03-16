using Photo.Manufacture.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Photo.Manufacture.Core.Interfaces
{
    public interface IBinPackingService
    {
        Task<float> CalculateRequiredBinWidth(IEnumerable<OrderItem> orderItems);
    }
}
