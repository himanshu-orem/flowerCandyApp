using FlowersAndCandyCustomer.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowersAndCandyCustomer.Data
{
    public class DbCls
    {
        readonly SQLiteConnection database;

        public DbCls(string dbPath)
        {
            try
            {
                database = new SQLiteConnection(dbPath);
                database.CreateTable<LoggedInUser>();
                database.CreateTable<CartProductDetail>();
                database.CreateTable<CartProductAddonDetail>();
                database.CreateTable<Language>();
            }
            catch (Exception ex)
            {

            }
        }
        public Language GetLanguage()
        {
            return database.Table<Language>().FirstOrDefault();
        }

        public int SaveLanguage(Language item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        //
        public int SaveCartProduct(CartProductDetail item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        public CartProductDetail GetLastProduct()
        {
            return database.Table<CartProductDetail>().OrderByDescending(x=>x.ID).FirstOrDefault();
        }

        public int GetAllCount()
        {
            return database.Table<CartProductDetail>().Count();
        }
        public List<CartProductDetail> GetAllProduct()
        {
            return database.Table<CartProductDetail>().ToList();
        }
        public int GetProductShop(int Id)
        {
            return database.Table<CartProductDetail>().Where(x => x.shopId == Id).Count();
        }
        public CartProductDetail GetProduct(int Id)
        {
            return database.Table<CartProductDetail>().Where(x => x.ID==Id).FirstOrDefault();
        }
        public int DeleteProduct(int Id)
        {
            var status = 0;
            try
            {
                var data = database.Table<CartProductDetail>().Where(x=>x.ID==Id).FirstOrDefault();
                
                status = database.Delete(data);
                 

            }
            catch (Exception ex)
            {
            }

            return status;
        }
        public int ClearProduc()
        {
            var status = 0;
            try
            {
                var data = database.Table<CartProductDetail>().ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }

            }
            catch (Exception ex)
            {

            }

            return status;
        }



        public int SaveCartProductAddon(CartProductAddonDetail item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        public List<CartProductAddonDetail> GetAllProductAddon(string ProductId)
        {
            return database.Table<CartProductAddonDetail>().Where(x=>x.PSQLId==ProductId).ToList();
        }
        public int DeleteCartProductAddon(string Id)
        {
            var status = 0;
            try
            {
                var data = database.Table<CartProductAddonDetail>().Where(x=>x.PSQLId==Id).ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }

            }
            catch (Exception ex)
            {

            }

            return status;
        }
        public int ClearAddon()
        {
            var status = 0;
            try
            {
                var data = database.Table<CartProductAddonDetail>().ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }

            }
            catch (Exception ex)
            {

            }

            return status;
        }
        //




        public LoggedInUser GetLoggedInUser()
        {
            return database.Table<LoggedInUser>().FirstOrDefault();
        }

        public int SaveLoggedInUser(LoggedInUser item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int ClearLoginDetails()
        {
            var status = 0;
            try
            {
                var data = database.Table<LoggedInUser>().ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }

            }
            catch (Exception ex)
            {

            }

            return status;
        }

    }
}
