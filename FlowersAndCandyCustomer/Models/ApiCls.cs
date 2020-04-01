using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FlowersAndCandyCustomer.Models
{
    class ApiCls
    {
    }
    public class TokenUpdate
    {
    }

    public class TokenUpdateData
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public TokenUpdate data { get; set; }
    }

    //Users
    public class User
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string deviceType { get; set; }
        public string deviceToken { get; set; }
        public string userType { get; set; }
        public string image { get; set; }
        public string phone { get; set; }
        public string verify { get; set; }
        public string serverTime { get; set; }
        public string country_code { get; set; }
        public string otp { get; set; }
        public string is_shop { get; set; }


        public string surname { get; set; }
        public string streetaddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }

    }

    public class UserData
    {
        public User User { get; set; }
    }

    public class UserResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public UserData data { get; set; }
    }

    //dummy text
    public class PageData
    {
        public string content { get; set; }
    }

    public class PageDataResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public PageData data { get; set; }
    }

    //Home page product
    public class HomeProduct
    {
        public string id { get; set; }
        public int prepare_time { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string distance_in_km { get; set; }
        public string image { get; set; }
        public string shopaddress { get; set; }


    }
    public class ProductMedia
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }
    public class Shops
    {
        public string address { get; set; }
    }
    public class Products
    {
        public HomeProduct Product { get; set; }
        public Shops shops { get; set; }
        public List<ProductMedia> ProductMedia { get; set; }
    }

    public class ProductData
    {
        public List<Products> products { get; set; }
        public int total_pages { get; set; }
    }

    public class ProductDataResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public ProductData data { get; set; }
    }

    // Home page category
    public class HomeCategory
    {
        public string id { get; set; }
        public string name { get; set; }
        public string is_offer { get; set; }
        public string logo { get; set; }
        
    }

    public class HomeData
    {
        public List<HomeCategory> categories { get; set; }
    }

    public class HomeCategoryResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public HomeData data { get; set; }
    }

    // Product info

    public class ProductInfo
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string distance_in_km { get; set; }
    }

    public class ProductMediaInfo
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }
    public class ShopsCls
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string company_name { get; set; }
        public string license_number { get; set; }
        public string communication_number { get; set; }
        public string delivery_system { get; set; }
        public string logo { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created { get; set; }
        public string distance_in_km { get; set; }
        
    }
    public class ProductInfoData
    {
        public ProductInfo Product { get; set; }
        public ShopsCls shops { get; set; }
        public List<ProductMediaInfo> ProductMedia { get; set; }
    }

    public class Addon
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string qty { get; set; }
        public string checkbox { get; set; }
        public string image { get; set; }
    }

    public class DataProductInfo
    {
        public ProductInfoData product { get; set; }
        public List<Addon> addon { get; set; }
        public int addon_total_pages { get; set; }
    }

    public class ProductInfoResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataProductInfo data { get; set; }
    }
    //product addon
    public class ProductAddon
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class ProductMediaAddon
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }

    public class AddonData
    {
        public ProductAddon ProductAddon { get; set; }
        public List<ProductMediaAddon> ProductMedia { get; set; }
    }

    public class ProductAddonData
    {
        public List<AddonData> addon { get; set; }
        public int addon_total_pages { get; set; }
    }

    public class ProductAddonResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public ProductAddonData data { get; set; }
    }
    //





    //public class ProductAddon
    //{
    //    public string id { get; set; }
    //    public string user_id { get; set; }
    //    public string category_id { get; set; }
    //    public string name { get; set; }
    //    public string quantity { get; set; }
    //    public string price { get; set; }
    //    public string created { get; set; }
    //    public string modified { get; set; }
    //}

    //public class ProductAddonData
    //{
    //    public List<ProductAddon> addon { get; set; }
    //    public int addon_total_pages { get; set; }
    //}

    //public class ProductAddonResult
    //{
    //    public int status { get; set; }
    //    public string msg_en { get; set; }
    //    public string msg_ar { get; set; }
    //    public ProductAddonData data { get; set; }
    //}

    public class UsersAddress
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string full_name { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string full_address { get; set; }
        public string landmark { get; set; }
        public string zipcode { get; set; }
        public string is_default { get; set; }
        public string img { get; set; }
       
    }

    public class CustomerAddressData
    {
        public UsersAddress UsersAddress { get; set; }
    }

    public class CustomerAddressResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public List<CustomerAddressData> data { get; set; }
    }

    //delete customer address
    public class customerAddress
    {
    }

    public class customerAddressResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public customerAddress data { get; set; }
    }
    //geo location
    public class PredictionL
    {
        public string description { get; set; }
        public string id { get; set; }
        public List<MatchedSubstring> matched_substrings { get; set; }
        public string place_id { get; set; }
        public string reference { get; set; }
        public StructuredFormatting structured_formatting { get; set; }
        public List<Term> terms { get; set; }
        public List<string> types { get; set; }
    }

    public class wsGooglePlaces
    {
        public List<PredictionL> predictions { get; set; }
        public string status { get; set; }
    }
    public class MatchedSubstring
    {
        public int length { get; set; }
        public int offset { get; set; }
    }
    public class StructuredFormatting
    {
        public string main_text { get; set; }
        public List<MainTextMatchedSubstring> main_text_matched_substrings { get; set; }
        public string secondary_text { get; set; }
    }
    public class Term
    {
        public int offset { get; set; }
        public string value { get; set; }
    }
    public class MainTextMatchedSubstring
    {
        public int length { get; set; }
        public int offset { get; set; }
    }

    //get customer address
    public class CustAddress
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string full_name { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string full_address { get; set; }
        public string landmark { get; set; }
        public string zipcode { get; set; }
        public string is_default { get; set; }
    }

    public class CustAddressData
    {
        public CustAddress UsersAddress { get; set; }
    }

    public class CustAddressResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public CustAddressData data { get; set; }
    }
    //create order
    public class CreateOrder
    {
        public string checkout_id { get; set; }
    }

    public class CreateOrderResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public CreateOrder data { get; set; }
    }

    //customer order
    public class CustomerOrdersCls
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string driver_id { get; set; }
        public string total_cost { get; set; }
        public string payment_method { get; set; }
        public string transaction_id { get; set; }
        public string status { get; set; }
        public string payment_status { get; set; }
        public string success_time { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class CustomerOrdersData
    {
        public CustomerOrdersCls Order { get; set; }
    }

    public class CustomerOrdersResult
    {
        public List<CustomerOrdersData> orders { get; set; }
        public int total_pages { get; set; }
    }

    public class CustomerOrdersList
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public CustomerOrdersResult data { get; set; }
    }
    //get customer order info
    public class OrderInfo
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string driver_id { get; set; }
        public string total_cost { get; set; }
        public string payment_method { get; set; }
        public string transaction_id { get; set; }
        public string status { get; set; }
        public string payment_status { get; set; }
        public string success_time { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class OrderInfoAddon
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string name { get; set; }
        public string product_id { get; set; }
        public string addon_id { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string total_cost { get; set; }
    }
    public class MediaOrderInfo
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }
    public class OrderInfoProduct
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string name { get; set; }
        public string product_id { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string total_cost { get; set; }
        public List<OrderInfoAddon> Addon { get; set; }
        public List<MediaOrderInfo> ProductMedia { get; set; }
    }

    public class OrderInfoCls
    {
        public OrderInfo Order { get; set; }
        public List<OrderInfoProduct> OrderProduct { get; set; }
    }

    public class OrderInfoData
    {
        public OrderInfoCls order { get; set; }
    }
   
    public class OrderInforesult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public OrderInfoData data { get; set; }
    }
    //add seller product
    public class AddProduct
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }
    public class AddProductMedia
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }
    public class AddProducrData
    {
        public AddProduct Product { get; set; }
        public List<AddProductMedia> ProductMedia { get; set; }
    }

    public class AddProductResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public AddProducrData data { get; set; }
    }

    //setting
    public class Setting
    {
        public string delivery_fee { get; set; }
        public string waiting_charge { get; set; }
        public string distance_charge { get; set; }
        public string delivery_fee_radius { get; set; }
    }

    public class SettingData
    {
        public Setting Setting { get; set; }
    }

    public class SettingResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public SettingData data { get; set; }
    }
    //register shop
    public class ShopRegister
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string company_name { get; set; }
        public string license_number { get; set; }
        public string country_code { get; set; }
        public string communication_number { get; set; }
        public string delivery_system { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        public string country { get; set; }
        public string country_short { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created { get; set; }
    }

    public class DataShopRegister
    {
        public ShopRegister Shop { get; set; }
    }

    public class ShopRegisterResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataShopRegister data { get; set; }
    }
    public class addAddons
    {
        public int id { get; set; }
        public string AddonsId { get; set; }
        public string addButton { get; set; }
        public string deleteButton { get; set; }
        public string name { get; set; }
        public string priceL { get; set; }
        public string priceM { get; set; }
        public string priceS { get; set; }
        public string qty { get; set; }
        public ImageSource imgSource { get; set; }
        public byte[] img { get; set; }

        public string acdcImg { get; set; }
        public string acdcLbl { get; set; }
        public string status { get; set; }
    }
    //add seller addon
    public class SellerProductAddon
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string product_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string images { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string is_error { get; set; }
    }

    public class SellerAddonData
    {
        public SellerProductAddon ProductAddon { get; set; }
        public List<object> ProductMedia { get; set; }
    }

    public class SellerAddonDataResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public SellerAddonData data { get; set; }
    }
    //seller product list

    public class ProductSeller
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string status { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string img { get; set; }
    }

    public class MediaProductSeller
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }

    public class ProductSellerCls
    {
        public ProductSeller Product { get; set; }
        public List<MediaProductSeller> ProductMedia { get; set; }
    }

    public class DataProductSeller
    {
        public List<ProductSellerCls> products { get; set; }
        public int total_pages { get; set; }
    }

    public class ProductSellerList
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataProductSeller data { get; set; }
    }

    //get seller product
    public class ProductEdit
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string is_offer { get; set; }
        public string description { get; set; }
        public string prepare_time { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string status { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class ProductMediaEDit
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }
    public class AddonProductMedia
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }

    public class ProductEditCls
    {
        public ProductEdit Product { get; set; }
        public List<ProductMediaEDit> ProductMedia { get; set; }
    }

    public class ProductAddonEdit
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string product_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string images { get; set; }
        public string quantity { get; set; }
        public string price_medium { get; set; }
        public string price_large { get; set; }
        public string price_small { get; set; }
        public string deleted { get; set; }
        public string status { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class AddonEdit
    {
        public ProductAddonEdit ProductAddon { get; set; }
        public List<AddonProductMedia> ProductMedia { get; set; }
    }

    public class ProductEditData
    {
        public ProductEditCls product { get; set; }
        public List<AddonEdit> addon { get; set; }
    }

    public class ProductEditResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public ProductEditData data { get; set; }
    }
    //get shop list
    public class ShopList
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string company_name { get; set; }
        public string license_number { get; set; }
        public string country_code { get; set; }
        public string communication_number { get; set; }
        public string delivery_system { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        public string is_offer { get; set; }
        public string latitude { get; set; }
        public string is_close { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created { get; set; }
    }

    public class Usershop1
    {
        public string is_close { get; set; }
        public string is_busy { get; set; }
        public string distance_in_km { get; set; }
    }
    public class ShopClsData
    {
        public Usershop1 User { get; set; }
        public ShopList Shop { get; set; }
    }

    public class ShopClsDetails
    {
        public List<ShopClsData> shops { get; set; }
        public int total_pages { get; set; }
    }

    public class ShopClsList
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public ShopClsDetails data { get; set; }
    }
    //add notes
    public class Notes
    {
        public string id { get; set; }
        public string noteFor { get; set; }
        public string noteTxt { get; set; }
        public byte[] noteImg { get; set; }
    }

    //seller order list
    public class OrderSeller
    {
        public string id { get; set; }
        public string parent_order_id { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string driver_id { get; set; }
        public string total_cost { get; set; }
        public string delivery_fee { get; set; }
        public string waiting_charge { get; set; }
        public string distance_charge { get; set; }
        public string payment_method { get; set; }
        public string transaction_id { get; set; }
        public string note { get; set; }
        public string note_image { get; set; }
        public string status { get; set; }
        public string payment_status { get; set; }
        public string success_time { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class OrderSellerCls
    {
        public OrderSeller Order { get; set; }
    }

    public class DataOrderSellerCls
    {
        public List<OrderSellerCls> orders { get; set; }
        public int total_pages { get; set; }
    }

    public class OrderSellerResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataOrderSellerCls data { get; set; }
    }
    //map
    public class RootObjectPolyLine
    {
        public List<GeocodedWaypoint> geocoded_waypoints { get; set; }
        public List<Route> routes { get; set; }
        public string status { get; set; }
    }
    public class GeocodedWaypoint
    {
        public string geocoder_status { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }
    public class Route
    {
        public Bounds bounds { get; set; }
        public string copyrights { get; set; }
        public List<Leg> legs { get; set; }
        public OverviewPolyline overview_polyline { get; set; }
        public string summary { get; set; }
        public List<object> warnings { get; set; }
        public List<object> waypoint_order { get; set; }
    }
    public class Leg
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string end_address { get; set; }
        public EndLocation end_location { get; set; }
        public string start_address { get; set; }
        public StartLocation start_location { get; set; }
        public List<Step> steps { get; set; }
        public List<object> traffic_speed_entry { get; set; }
        public List<object> via_waypoint { get; set; }
    }
    public class Bounds
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }
    public class OverviewPolyline
    {
        public string points { get; set; }
    }
    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }
    public class EndLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class StartLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class Step
    {
        public Distance2 distance { get; set; }
        public Duration2 duration { get; set; }
        public EndLocation2 end_location { get; set; }
        public string html_instructions { get; set; }
        public Polyline polyline { get; set; }
        public StartLocation2 start_location { get; set; }
        public string travel_mode { get; set; }
        public string maneuver { get; set; }
    }
    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class Distance2
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration2
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class EndLocation2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class Polyline
    {
        public string points { get; set; }
    }

    public class StartLocation2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class PloyLineRoutes
    {

        public double Altitude { get; set; }
        public double Direction { get; set; }
        public double HorizontalAccuracy { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Speed { get; set; }
        public DateTime Timestamp { get; set; }
    }

    //tracking
    public class trackingOrder 
    {
        public string id { get; set; }
        public string parent_order_id { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string driver_id { get; set; }
        public string total_cost { get; set; }
        public string delivery_fee { get; set; }
        public string waiting_charge { get; set; }
        public string distance_charge { get; set; }
        public string payment_method { get; set; }
        public string card_type { get; set; }
        public string transaction_id { get; set; }
        public string note { get; set; }
        public string note_image { get; set; }
        public string status { get; set; }
        public string payment_status { get; set; }
        public string success_time { get; set; }
        public string waiting_start { get; set; }
        public string waiting_end { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class Customer
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }


    public class Driver
    {
        public string image { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class AddonTrack
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string name { get; set; }
        public string product_id { get; set; }
        public string addon_id { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string total_cost { get; set; }
    }

    public class ProductMediaTrack
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }
    public class OrderAddress
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string full_address { get; set; }
    }
    public class OrderProduct
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string name { get; set; }
        public string product_id { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string total_cost { get; set; }
        public List<AddonTrack> Addon { get; set; }
        public List<ProductMediaTrack> ProductMedia { get; set; }
    }
    public class ShopDetails1
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string company_name { get; set; }
        public string license_number { get; set; }
        public string country_code { get; set; }
        public string communication_number { get; set; }
        public string delivery_system { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        public string country { get; set; }
        public string country_short { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created { get; set; }
    }
    public class TrackOrder
    {
        public trackingOrder Order { get; set; }
        public ShopDetails1 Shop { get; set; }
        public OrderAddress OrderAddress { get; set; }
        public Customer Customer { get; set; }
        public Driver Driver { get; set; }
        public List<OrderProduct> OrderProduct { get; set; }
    }

    public class DataTracking
    {
        public TrackOrder order { get; set; }
        public string server_time { get; set; }
    }

    public class TrackingOrderData
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataTracking data { get; set; }
    }
    // login phn

    public class UserPhn
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string deviceType { get; set; }
        public string deviceToken { get; set; }
        public string userType { get; set; }
        public string image { get; set; }
        public string country_code { get; set; }
        public string phone { get; set; }
        public string verify { get; set; }
        public string is_shop { get; set; }
        public string otp { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string serverTime { get; set; }

        public string surname { get; set; }
        public string streetaddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postcode { get; set; }
    }

    public class CategoryPhn
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string logo { get; set; }
        public string title_ar { get; set; }
        public string is_offer { get; set; }
        public string status { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class ShopPhn
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string company_name { get; set; }
        public string license_number { get; set; }
        public string country_code { get; set; }
        public string communication_number { get; set; }
        public string delivery_system { get; set; }
        public string logo { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created { get; set; }
    }

    public class ShopsPhn
    {
        public CategoryPhn Category { get; set; }
        public ShopPhn Shop { get; set; }
    }

    public class PhnLoginData
    {
        public UserPhn User { get; set; }
        public ShopsPhn shop { get; set; }
    }

    public class PhnLoginResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public PhnLoginData data { get; set; }
    }
    //contact
    public class ContactData
    {
        public string name { get; set; }
        public string phone { get; set; }
    }

    public class ContactResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public ContactData data { get; set; }
    }
    //
    public class CountryIso
    {
        public string id { get; set; }
        public string iso { get; set; }
        public string name { get; set; }
        public string nicename { get; set; }
        public string iso3 { get; set; }
        public string numcode { get; set; }
        public string phonecode { get; set; }
    }

    public class Data
    {
        public List<CountryIso> countries { get; set; }
    }

    public class CountryISO
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public Data data { get; set; }
    }
    //shop details

    public class Shop2
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string company_name { get; set; }
        public string license_number { get; set; }
        public string country_code { get; set; }
        public string communication_number { get; set; }
        public string delivery_system { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        public string country { get; set; }
        public string country_short { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created { get; set; }
    }

    public class ShopDetails
    {
        public Shop2 Shop { get; set; }
    }

    public class ShopDetailsData
    {
        public ShopDetails shop { get; set; }
    }

    public class ShopDetailsResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public ShopDetailsData data { get; set; }
    }
    //





    public class ProductExpendable
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string prepare_time { get; set; }
        public string is_offer { get; set; }
        public string status { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string distance_in_km { get; set; }
    }

    public class ExpUsers
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class ExpShops
    {
        public string company_name { get; set; }
        public string logo { get; set; }
        public string address { get; set; }
        public string country_short { get; set; }
    }

    public class ProductAddonExp
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string product_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string images { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string price_large { get; set; }
        public string price_medium { get; set; }
        public string price_small { get; set; }
        public string deleted { get; set; }
        public string status { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class ProductMediaExp
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }

    public class ExpAddon
    {
        public ProductAddonExp ProductAddon { get; set; }
        public List<ProductMediaExp> ProductMedia { get; set; }
    }

    public class ExpProduct
    {
        public ProductExpendable Product { get; set; }
        public ExpUsers users { get; set; }
        public ExpShops shops { get; set; }
        public List<object> ProductMedia { get; set; }
        public List<ExpAddon> addons { get; set; }
    }

    public class DataProductExpendable
    {
        public List<ExpProduct> products { get; set; }
        public int total_pages { get; set; }
    }

    public class ResultProductExpendable
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataProductExpendable data { get; set; }
    }
    //





    public class ProductAddonNew
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string product_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string images { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string price_large { get; set; }
        public string price_small { get; set; }
        public string price_medium { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string distance_in_km { get; set; }
    }

    public class ShopsNew
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string company_name { get; set; }
        public string license_number { get; set; }
        public string country_code { get; set; }
        public string communication_number { get; set; }
        public string delivery_system { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        public string country { get; set; }
        public string country_short { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created { get; set; }
    }

    public class ProductMediaNew
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }

    public class AddonInfoNew
    {
        public ProductAddonNew ProductAddon { get; set; }
        public ShopsNew shops { get; set; }
        public List<ProductMediaNew> ProductMedia { get; set; }
    }

    public class DataNew
    {
        public AddonInfoNew addonInfo { get; set; }
    }

    public class NewProductResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataNew data { get; set; }
    }
    //


    public class OrderPastDetails
    {
        public string id { get; set; }
        public string parent_order_id { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string driver_id { get; set; }
        public string total_cost { get; set; }
        public string delivery_fee { get; set; }
        public string waiting_charge { get; set; }
        public string distance_charge { get; set; }
        public string payment_method { get; set; }
        public string card_type { get; set; }
        public string transaction_id { get; set; }
        public string note { get; set; }
        public string note_image { get; set; }
        public string status { get; set; }
        public string payment_status { get; set; }
        public string success_time { get; set; }
        public string waiting_start { get; set; }
        public string waiting_end { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
        public string self_pick { get; set; }
    }

    public class OrderAddressPastDetails
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string full_address { get; set; }
    }

    public class CustomerPastDetails
    {
        public string lat { get; set; }
        public string lng { get; set; }
        public string image { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }


    }

    public class DriverPastDetails
    {
        public string image { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class AddonMediaPastDetails
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }

    public class AddonPastDetails
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public string product_id { get; set; }
        public string addon_id { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string total_cost { get; set; }
        public AddonMediaPastDetails AddonMedia { get; set; }
    }

    public class OrderProductPastDetails
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string name { get; set; }
        public string product_id { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string total_cost { get; set; }
        public List<AddonPastDetails> Addon { get; set; }
    }
    public class ShopDetail1
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string category_id { get; set; }
        public string company_name { get; set; }
        public string license_number { get; set; }
        public string country_code { get; set; }
        public string communication_number { get; set; }
        public string delivery_system { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        public string country { get; set; }
        public string country_short { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string address { get; set; }
        public string created { get; set; }
    }
    public class OrderPastDetails1
    {
        public ShopDetail1 Shop { get; set; }
        public OrderPastDetails Order { get; set; }
        public OrderAddressPastDetails OrderAddress { get; set; }
        public CustomerPastDetails Customer { get; set; }
        public DriverPastDetails Driver { get; set; }
        public List<OrderProductPastDetails> OrderProduct { get; set; }
    }

    public class DataPastDetails
    {
        public OrderPastDetails1 order { get; set; }
        public string server_time { get; set; }
    }

    public class DataPastDetailsResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataPastDetails data { get; set; }
    }

    //driver 
    public class DriverUser
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string deviceType { get; set; }
        public string deviceToken { get; set; }
        public string userType { get; set; }
        public string admin_approve { get; set; }
        public string image { get; set; }
        public string country_code { get; set; }
        public string phone { get; set; }
        public string verify { get; set; }
        public string is_shop { get; set; }
        public string is_close { get; set; }
        public string is_busy { get; set; }
        public string otp { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string serverTime { get; set; }
    }

    public class Doc
    {
        public string copy_of_civil { get; set; }
        public string copy_of_vehicle { get; set; }
        public string vehicle_image { get; set; }
    }

    public class DriverData
    {
        public DriverUser User { get; set; }
        public Doc Doc { get; set; }
    }

    public class DriverResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DriverData data { get; set; }
    }

    //seller product list



    public class ProductTrader
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string prepare_time { get; set; }
        public string is_offer { get; set; }
        public string status { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class ProductAddonTrader
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string product_id { get; set; }
        public string shop_id { get; set; }
        public string category_id { get; set; }
        public string name { get; set; }
        public string images { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string price_small { get; set; }
        public string price_large { get; set; }
        public string price_medium { get; set; }
        public string status { get; set; }
        public string deleted { get; set; }
        public string created { get; set; }
        public string modified { get; set; }
    }

    public class ProductMediaTrader
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string media { get; set; }
        public string type { get; set; }
        public string media_type { get; set; }
    }

    public class AddonTrader
    {
        public ProductAddonTrader ProductAddon { get; set; }
        public List<ProductMediaTrader> ProductMedia { get; set; }
    }

    public class ProductTraders
    {
        public ProductTrader Product { get; set; }
        public List<object> ProductMedia { get; set; }
        public List<AddonTrader> addon { get; set; }
    }

    public class DataTrader
    {
        public List<ProductTraders> products { get; set; }
        public int total_pages { get; set; }
    }

    public class TraderResult
    {
        public int status { get; set; }
        public string msg_en { get; set; }
        public string msg_ar { get; set; }
        public DataTrader data { get; set; }
    }
}
