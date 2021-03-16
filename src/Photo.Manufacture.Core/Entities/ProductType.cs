using Photo.Manufacture.SharedKernel;
using System.Collections.Generic;

namespace Photo.Manufacture.Core.Entities
{
    public class ProductType : BaseEnumeration
    {
        public static ProductType PhotoBook = new(1, nameof(PhotoBook));
        public static ProductType Calendar = new(2, nameof(Calendar));
        public static ProductType Canvas = new(3, nameof(Canvas));
        public static ProductType Cards = new(4, nameof(Cards));
        public static ProductType Mug = new(5, nameof(Mug));

        public ProductType(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<ProductType> List() =>
            new[] { PhotoBook, Calendar, Canvas, Cards, Mug };
    }
}
