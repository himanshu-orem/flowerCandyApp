using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlowersAndCandyCustomer.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace FlowersAndCandyCustomer.Repository
{
    public class CommonLib
    {
       
        public static string img_MainUrl = "http://flwercandy.com/flower/";
         public static string ws_MainUrl = "http://flwercandy.com/flower/api/apis/";
       // public static string img_MainUrl = "http://13.59.149.208/flower/";
       // public static string ws_MainUrl = "http://13.59.149.208/flower/api/apis/";
        public static bool checkconnection()
        {
            var con = CrossConnectivity.Current.IsConnected;
            return con == true ? true : false;
        }

        /// <summary>
        /// Checks the valid email.
        /// </summary>
        /// <returns><c>true</c>, if valid email was checked, <c>false</c> otherwise.</returns>
        /// <param name="Email">Email.</param>


        public static bool CheckValidEmail(string Email)
        {
            bool isEmail = Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            return isEmail;
        }

        public static List<PhoneCodes> LoadJson()
        {
            var fileName = "FlowersAndCandyCustomer.CountryCodes.json";
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(fileName);

            List<PhoneCodes> items = null;

            using (StreamReader r = new StreamReader(stream))
            {
                string json = r.ReadToEnd().ToString();
                items = JsonConvert.DeserializeObject<List<PhoneCodes>>(json);
            }
            return items;
        }

        public static async Task<DriverResult> GetUserDetails(string url)
        {
            DriverResult objData = new DriverResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<DriverResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<UserResult> Login(string url)
        {
            UserResult objData = new UserResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<UserResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<TokenUpdateData> UpdateToken(string url)
        {
            TokenUpdateData objData = new TokenUpdateData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<TokenUpdateData>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<UserResult> RegisterUser(string url)
        {
            UserResult objData = new UserResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<UserResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<UserResult> ForgotPassword(string url)
        {
            UserResult objData = new UserResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<UserResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static List<PloyLineRoutes> DecodePoly(string encodedPoints)
        {
            if (encodedPoints == null || encodedPoints == "") return null;
            List<PloyLineRoutes> poly = new List<PloyLineRoutes>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            try
            {
                while (index < polylinechars.Length)
                {
                    // calculate next latitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length)
                        break;

                    currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                    //calculate next longitude
                    sum = 0;
                    shifter = 0;
                    do
                    {
                        next5bits = (int)polylinechars[index++] - 63;
                        sum |= (next5bits & 31) << shifter;
                        shifter += 5;
                    } while (next5bits >= 32 && index < polylinechars.Length);

                    if (index >= polylinechars.Length && next5bits >= 32)
                        break;

                    currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                    PloyLineRoutes p = new PloyLineRoutes();
                    p.Latitude = Convert.ToDouble(currentLat) / 100000.0;
                    p.Longitude = Convert.ToDouble(currentLng) / 100000.0;
                    poly.Add(p);
                }
            }
            catch (Exception ex)
            {
                // logo it
            }
            return poly;
        }
        public static async Task<RootObjectPolyLine> GetPolyLine(double SLat, double SLng, double ELat, double ELng)
        {
            //AIzaSyCAW0qqikPYbOKLu_aobSw04z1Dnfhgpv4

            string url = "https://maps.googleapis.com/maps/api/directions/json?origin=" + SLat + "," + SLng + "&destination=" + ELat + "," + ELng + "&key=AIzaSyCAW0qqikPYbOKLu_aobSw04z1Dnfhgpv4";

            //   string url = "https://maps.googleapis.com/maps/api/directions/json?origin=30.706459,76.708265&destination=30.7024,76.8215&key=AIzaSyCAW0qqikPYbOKLu_aobSw04z1Dnfhgpv4";

            RootObjectPolyLine objData = new RootObjectPolyLine();
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url);

                    TimeSpan time = new TimeSpan(0, 0, 20);
                    client.Timeout = time;
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<RootObjectPolyLine>(await result.Content.ReadAsStringAsync());

                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<PageDataResult> PageTxt(string url)
        {
            PageDataResult objData = new PageDataResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<PageDataResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<UserResult> ChangePassword(string url)
        {
            UserResult objData = new UserResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<UserResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<HomeCategoryResult> HomePageCategory(string url)
        {
            HomeCategoryResult objData = new HomeCategoryResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<HomeCategoryResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<ProductDataResult> HomePageProduct(string url)
        {
            ProductDataResult objData = new ProductDataResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ProductDataResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<NewProductResult> GetProductInfo(string url)
        {
            NewProductResult objData = new NewProductResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<NewProductResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }


        public static async Task<ProductAddonResult> GetProductAddon(string url)
        {
            ProductAddonResult objData = new ProductAddonResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ProductAddonResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<CustomerAddressResult> GetCustomerAddress(string url)
        {
            CustomerAddressResult objData = new CustomerAddressResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CustomerAddressResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<customerAddressResult> DeleteCustomerAddress(string url)
        {
            customerAddressResult objData = new customerAddressResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<customerAddressResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<customerAddressResult> DefaultCustomerAddress(string url)
        {
            customerAddressResult objData = new customerAddressResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<customerAddressResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<wsGooglePlaces> getGooglePlaces(string url)
        {
            wsGooglePlaces objData = new wsGooglePlaces();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    TimeSpan time = new TimeSpan(0, 0, 20);
                    client.Timeout = time;
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<wsGooglePlaces>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<CustAddressResult> GetCustomerAddressData(string url)
        {
            CustAddressResult objData = new CustAddressResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CustAddressResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<CreateOrderResult> CreateOrder(string url)
        {
            CreateOrderResult objData = new CreateOrderResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CreateOrderResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<CustomerOrdersList> GetCustomerOrder(string url)
        {
            CustomerOrdersList objData = new CustomerOrdersList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CustomerOrdersList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<UserResult> RegisterPhone(string url)
        {
            UserResult objData = new UserResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<UserResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<DataPastDetailsResult> GetCustomerOrderInfo(string url)
        {
            DataPastDetailsResult objData = new DataPastDetailsResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<DataPastDetailsResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<TrackingOrderData> GetCustomerOrderInfo1(string url)
        {
            TrackingOrderData objData = new TrackingOrderData();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<TrackingOrderData>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<SettingResult> GetSettingResult(string url)
        {
            SettingResult objData = new SettingResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<SettingResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }


        public static async Task<ProductSellerList> GetSellerProducts(string url)
        {
            ProductSellerList objData = new ProductSellerList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ProductSellerList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<ProductEditResult> EditProductDetails(string url)
        {
            ProductEditResult objData = new ProductEditResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ProductEditResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<ShopClsList> GetShopList(string url)
        {
            ShopClsList objData = new ShopClsList();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ShopClsList>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<OrderSellerResult> GetSellerOrder(string url)
        {
            OrderSellerResult objData = new OrderSellerResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<OrderSellerResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<customerAddressResult> SellerOrderAcceptReject(string url)
         {
            customerAddressResult objData = new customerAddressResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<customerAddressResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<CountryISO> GetISO(string url)
        {
            CountryISO objData = new CountryISO();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CountryISO>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }


        public static async Task<ContactResult> GetContactUs(string url)
        {
            ContactResult objData = new ContactResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ContactResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }

        public static async Task<PhnLoginResult> LoginPhn(string url)
        {
            PhnLoginResult objData = new PhnLoginResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<PhnLoginResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<CardResponse> GetCards(string url)
        {
            CardResponse objData = new CardResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<CardResponse>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<DeleteCardResponse> DeleteCards(string url)
        {
            DeleteCardResponse objData = new DeleteCardResponse();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<DeleteCardResponse>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
        public static async Task<ShopDetailsResult> GetShop(string url)
        {
            ShopDetailsResult objData = new ShopDetailsResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ShopDetailsResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }


        public static async Task<ResultProductExpendable> GetExpendProduct(string url)
        {
            ResultProductExpendable objData = new ResultProductExpendable();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<ResultProductExpendable>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }


        public static async Task<TraderResult> GetSellerProduct(string url)
        {
            TraderResult objData = new TraderResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var result = await client.GetAsync(url);
                    var place = result.Content.ReadAsStringAsync().Result;
                    objData = JsonConvert.DeserializeObject<TraderResult>(await result.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
            }
            return objData;
        }
    }
}
