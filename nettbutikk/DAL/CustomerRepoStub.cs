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
    public class CustomerRepoStub
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
            if ((String.IsNullOrEmpty(inList["Firstname"])))
                return false;
            else
                return true;
        }
        public bool Login()
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 5);
            if (rnd < 3)
                return false;
            else if (rnd > 3)
                return true;
            else
                return true;
        }
        public bool ValidateUser(FormCollection inList)
        {
            if ((String.IsNullOrEmpty(inList["Firstname"])))
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
            /*List<Customer> clist = allCustomers();
            Customer c = new Customer();
            email = "daniel@thoresen.no";
            for(int i = 0; i < clist.Count; i++)
            {
                if (clist[i].email == email)
                {
                    c.customerId = clist[i].customerId;
                    c.email = email;
                    c.firstname = clist[i].firstname;
                    c.lastname = clist[i].lastname;
                    c.address = clist[i].address;
                    c.isadmin = clist[i].isadmin;
                    c.zipcode = clist[i].zipcode;
                    c.postalarea = clist[i].postalarea.ToString();
                    c.password = clist[i].password;
                    c.salt = clist[i].salt;
                    return c;
                }
            }
            return null;*/
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
    }
}