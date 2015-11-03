using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Web.Mvc;
using System.IO;

namespace nettButikkpls.DAL
{
    public class CustomerDAL
    {
        HttpContext context = HttpContext.Current;
        NettbutikkContext bmx = new NettbutikkContext();
        public List<Customer> allCustomers()
        {
            using (var db = new NettbutikkContext())
            {
                List<Customer> allCustomers = db.Customers.Select(c => new Customer
                {
                    customerId = c.CustomerId,
                    firstname = c.Firstname,
                    lastname = c.Lastname,
                    address = c.Address,
                    zipcode = c.Zipcode,
                    isadmin = c.IsAdmin,
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
                    newCustomerRow.IsAdmin = inCustomer.isadmin;
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
                    try
                    {
                        nettbutikkpls.Models.Log log = new nettbutikkpls.Models.Log();
                        log.NewValue = newCustomerRow.ToString();
                        log.EventType = "Create";
                        log.ChangedTime = (DateTime.Now).ToString("yyyyMMddHHmmss");
                        Debug.Print(log.toString());
                        string logText = log.toString();
                        var _Path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data"), "/Log.txt");

                        using (System.IO.StreamWriter file =
                             new System.IO.StreamWriter(@"C:\Users\Mads Karlstad\Source\Repos\Mvc-ogblig6\nettbutikk\nettButikkpls\App_Data", true))
                        {
                            file.WriteLine(logText);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Mads, du suger balle");
                    }
                    

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
                    // HttpContext context = HttpContext.Current;
                    Customer c = (Customer)context.Session["CurrentUser"];
                    Customers customer = FindCustomersByEmail(c.email);
                    
                    
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
                    c = FindCustomerByEmail(customer.Mail);
                    context.Session["CurrentUser"] = c;
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
        }
        public bool Login()
        {
            //HttpContext context = HttpContext.Current;
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
        public bool ValidateUser(FormCollection inList)
        {
            Customer customer = FindCustomerByEmail(inList["Email"]);

            if (customer != null)
            {
                String OldHash = customer.password;
                String ReHash = HashPassword(inList["Password"], customer.salt);
                HttpContext context = HttpContext.Current;
                if (OldHash == ReHash)
                {
                    context.Session["loggedin"] = true;
                    context.Session["CurrentUser"] = customer;
                    Debug.WriteLine("Du er nå logget inn");
                    return true;
                    // return RedirectToAction("List");
                }
                else
                {
                    context.Session["loggedin"] = false;
                    context.Session["CurrentUser"] = null;
                    Debug.WriteLine("Kunne ikke logge inn");
                    return false;
                    //return RedirectToAction("Reg");
                }
            }
            return false;
            //return RedirectToAction("Login");
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
            using (var db = new NettbutikkContext())
            {
                try
                {
                    Customer c = new Customer();
                    var customer = db.Customers.Single(b => (b.Mail.Equals(email)));

                    c.customerId = customer.CustomerId;
                    c.email = email;
                    c.firstname = customer.Firstname;
                    c.lastname = customer.Lastname;
                    c.address = customer.Address;
                    c.isadmin = customer.IsAdmin;
                    c.zipcode = customer.Zipcode;
                    c.password = customer.Password;
                    c.salt = customer.Salt;
                    return c;
                    /*List<Customers> GetAllCustomers = bmx.Customers.ToList();
                    for (int i = 0; i < GetAllCustomers.Count; i++)
                    {
                        if (GetAllCustomers[i].Mail == email)
                        {
                    c.customerId = GetAllCustomers[i].CustomerId;
                    c.email = email;
                    c.firstname = GetAllCustomers[i].Firstname;
                    c.lastname = GetAllCustomers[i].Lastname;
                    c.address = GetAllCustomers[i].Address;
                    c.isadmin = GetAllCustomers[i].IsAdmin;
                    c.zipcode = GetAllCustomers[i].Zipcode;
                    c.postalarea = GetAllCustomers[i].Postalareas.ToString();
                    c.password = GetAllCustomers[i].Password;
                    c.salt = GetAllCustomers[i].Salt;
                    return c;
                    }
                 }*/

                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public Customers FindCustomersByEmail(string email)
        {
            List<Customers> GetAllCustomers = bmx.Customers.ToList();
            for(int i = 0; i<GetAllCustomers.Count; i++)
            {
                if(GetAllCustomers[i].Mail == email)
                {
                    return GetAllCustomers[i];
                }
            }
            return null;
        }

        public int CurrentCustomerId()
        {
            Customer c = (Customer)context.Session["CurrentUSer"];
            return c.customerId;
        }
        public bool UpdateCustomer(FormCollection inList, int customerid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var customer = db.Customers.Single(b => (b.CustomerId == customerid));

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
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public Customer FindCustomer(int customerid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    Customer c = new Customer();
                    var customer = db.Customers.Single(b => (b.CustomerId == customerid));
                    c.customerId = customerid;
                    c.firstname = customer.Firstname;
                    c.lastname = customer.Lastname;
                    c.address = customer.Address;
                    c.zipcode = customer.Zipcode;
                    c.password = customer.Password;
                    c.salt = customer.Salt;
                    c.isadmin = customer.IsAdmin;

                    return c;
                }
                catch
                {
                    return null;
                }
            }
          
        }
    }
}