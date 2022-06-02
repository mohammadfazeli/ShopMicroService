using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper
{

    public class DiscountMapper : ProfileAutoMapper
    {
        public DiscountMapper()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}