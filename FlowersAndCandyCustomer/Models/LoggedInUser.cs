using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowersAndCandyCustomer.Models
{
    public class LoggedInUser
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int userId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string image { get; set; }
        public string userType { get; set; }
        public string country_code { get; set; }
        public string is_shop { get; set; }
        public string surName { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postCode { get; set; }


    }


    public class CartProductDetail
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string productId { get; set; }
        public string baseProductId { get; set; }
        public string productName { get; set; }
        public string productCategory { get; set; }
        public string productImage { get; set; }
        public string productQty { get; set; }
        public string productPrice { get; set; }
        public string typePrice { get; set; }
        public string shopAddress { get; set; }
        public int shopId { get; set; }
        public string note { get; set; }

    }

    public class CartProductAddonDetail
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string productId { get; set; }
        public string AddonId { get; set; }
        public string AddonCategory { get; set; }
        public string AddonName { get; set; }
        public string AddonQty { get; set; }
        public string AddonPrice { get; set; }
        public string PSQLId { get; set; }
        public string image { get; set; }

    }
    public class Language
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string LangKey { get; set; }


    }
}
