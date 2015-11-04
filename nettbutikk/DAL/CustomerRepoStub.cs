using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Web.Mvc;

namespace nettButikkpls.DAL
{
    public class CustomerRepoStub : ICustomerRepo
    {
        public List<Customer> allCustomers()
        {
            using (var db = new NettbutikkContext())
            {
                List<Customer> allCustomers = new List<Customer>();
                Customer c = new Customer()
                {
                    email = "daniel@thoresen.no",
                    password = "Sommeren2015",
                    firstname = "Daniel",
                    lastname = "Thoresen",
                    address = "Hesselbergs gate 7A",
                    isadmin = false,
                    salt = "hejhejhallo",
                    zipcode = "0555",
                    postalarea = "Oslo",
                };
                allCustomers.Add(c);
                allCustomers.Add(c);
                allCustomers.Add(c);
                return allCustomers;
            }
        }
        public bool saveCustomer (Customer inCustomer)
        {
                if (inCustomer.firstname == null)
                    return false;
                else
                    return true;    
        }

        public bool EditCustomer(FormCollection inList)
        {
            if (!(String.IsNullOrEmpty(inList["Firstname"])))
                return false;
            else
                return true;
        }
        public bool Login()
        {
            return true;
        }
        public bool ValidateUser(FormCollection inList)
        {
            if (!(String.IsNullOrEmpty(inList["Firstname"])))
                return true;
            else
                return false;
        }
        public String GenerateSalt(int size)
        {
            var RandomNumberGenerator = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buffer = new byte[size];
            RandomNumberGenerator.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }
        public String HashPassword(String Password, string Salt)
        {

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Password + Salt);
            SHA256Managed SHA256String = new SHA256Managed();
            byte[] hash = SHA256String.ComputeHash(bytes);

            return Convert.ToBase64String(hash);

        }
        public Customer FindCustomerByEmail(string email)
        {
            Customer c = new Customer();
            c.email = email;
            return c;
        }
        public Customers FindCustomersByEmail(string email)
        {
            Customers c = new Customers();
            c.Mail = email;
            return c;
        }
        public int CurrentCustomerId()
        {
            int customerId = 1337;
            return customerId;
        }
        public Customer FindCustomer(int customerid)
        {
            Customer c = new Customer()
            {
                    email = "daniel@thoresen.no",
                    password = "Sommeren2015",
                    firstname = "Daniel",
                    lastname = "Thoresen",
                    address = "Hesselbergs gate 7A",
                    isadmin = false,
                    salt = "hejhejhallo",
                    zipcode = "0555",
                    postalarea = "Oslo",
            };
            return c;
        }
        public bool UpdateCustomer(FormCollection inList, int customerid)
        {
            if (!(String.IsNullOrEmpty(inList["Firstname"])))
                return true;
            else
                return false;
        }
    }
}