namespace Basket.API.Entites
{
    public class ShoppingCard
    {
        public ShoppingCard()
        {
            ShoppingCardItems = new List<ShoppingCardItem>();
        }

        public string UserName { get; set; }
        public List<ShoppingCardItem> ShoppingCardItems { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal sum = 0;

                foreach (var item in ShoppingCardItems)
                {
                    sum += item.Qty * item.Price;
                }
                return sum;
            }
        }
    }
}