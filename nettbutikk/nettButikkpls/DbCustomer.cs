using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Web.Mvc;

namespace nettButikkpls
{
    public class DbCustomer
    {
       NettbutikkContext bmx = new NettbutikkContext();
        public List<Customer> allCustomers()
        {
            using (var db = new NettbutikkContext())
            {
                List<Customer> allCustomers = db.Customers.Select(c => new Customer
                {
                    firstname = c.Firstname,
                    lastname = c.Lastname,
                    address = c.Address,
                    zipcode = c.Zipcode,
                    postalarea = c.Postalareas.Postalarea,
                    email = c.Mail,
                    password = c.Password,
                    salt = c.Salt
                }).ToList();
                return allCustomers;
            }
        }
        public bool saveCustomer (Customer inCustomer)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    String salt = GenerateSalt(32);

                    var newCustomerRow = new Customers();
                    newCustomerRow.Mail = inCustomer.email;
                    newCustomerRow.Password = HashPassword(inCustomer.password, salt);
                    newCustomerRow.Firstname = inCustomer.firstname;
                    newCustomerRow.Lastname = inCustomer.lastname;
                    newCustomerRow.Address = inCustomer.address;
                    newCustomerRow.Zipcode = inCustomer.zipcode;
                    newCustomerRow.Salt = salt;

                    var checkZipcode = db.PostalAreas.Find(inCustomer.zipcode);
                    if(checkZipcode==null)
                    {
                        var postalareaRow = new PostalAreas();
                        postalareaRow.Zipcode = inCustomer.zipcode;
                        postalareaRow.Postalarea = inCustomer.postalarea;
                        newCustomerRow.Postalareas = postalareaRow;
                    }
                    db.Customers.Add(newCustomerRow);
                    db.SaveChanges();
                    return true;
                }catch(Exception feil)
                {
                    return false;
                }                   
            }  
        }

        public bool EditCustomer(FormCollection inList)
        {
                try
                {
                    HttpContext context = HttpContext.Current;
                    Customers c = (Customers)context.Session["CurrentUser"];

                    Customers customer = FindCustomerByEmail(c.Mail);
                    if (!(String.IsNullOrEmpty(inList["Mail"])))
                    {
                        customer.Mail = inList["Mail"];
                    }
                    if (!(String.IsNullOrEmpty(inList["Firstname"])))
                    {
                        customer.Firstname = inList["Firstname"];
                    }
                    if (!(String.IsNullOrEmpty(inList["Lastname"])))
                    {
                        customer.Lastname = inList["Lastname"];
                    }
                    if (!(String.IsNullOrEmpty(inList["Address"])))
                    {
                        customer.Address = inList["Address"];
                    }
                    bmx.SaveChanges();
                    context.Session["CurrentUser"] = customer;
                    return true;
                }
                catch (Exception e)
                {
                    return false;   
                }
        }
        public bool Login()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["loggedin"] == null)
            {
                context.Session["loggedin"] = false;
            }
            else
            {
               return (bool)context.Session["loggedin"];
            }
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
        public Customers FindCustomerByEmail(string email)
        {
            List<Customers> GetAllCustomers = bmx.Customers.ToList();
            for (int i = 0; i < GetAllCustomers.Count; i++)
            {
                if (GetAllCustomers[i].Mail == email)
                {
                    return GetAllCustomers[i];
                }
            }
            return null;
        }
    }
}