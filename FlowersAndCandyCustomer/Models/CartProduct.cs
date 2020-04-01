using System;
namespace FlowersAndCandyCustomer.Models
{
    public class CartProduct
    {
        public string ProductId { get; set; }
        public string PSQLId { get; set; }
        public string Name { get; set; }
        public string AddonId { get; set; }
        public string AddonCategory { get; set; }
        public string AddonQty { get; set; }
        public string AddonPrice { get; set; }
        public string Note { get; set; }

    }
    public class ProductPageItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductPrice { get; set; }
        public string ShopAddress { get; set; }
        public string category_id { get; set; }
        public string user_id { get; set; }
        public string isvisible { get; set; }
    }
}
