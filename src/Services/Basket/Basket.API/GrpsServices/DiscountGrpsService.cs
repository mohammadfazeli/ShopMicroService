using Discount.Grpc.Protos;

namespace Basket.API.GrpsServices
{
    public class DiscountGrpsService
    {
        private readonly DiscountServiceProto.DiscountServiceProtoClient _discountServiceProto;

        public DiscountGrpsService(DiscountServiceProto.DiscountServiceProtoClient discountServiceProto)
        {
            _discountServiceProto = discountServiceProto;
        }

        public async Task<CouponModel> GetDiscount(string prodcutName)
        {
            var couponRequestModel = new GetDiscountRequest() { ProductName = prodcutName };
            return await _discountServiceProto.GetDiscountAsync(couponRequestModel);
        }
    }
}