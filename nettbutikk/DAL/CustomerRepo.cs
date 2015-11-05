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
    public class CustomerRepo : ICustomerRepo
    {
        HttpContext context = HttpContext.Current;
        NettbutikkContext bmx = new NettbutikkContext();
        public List<Customer> allCustomers()
        {
            Customer cust = CurrentCustomer();
            if (cust.isadmin)
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
            return null;
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
                    
                    
                    //Start saving to Log
                    nettbutikkpls.Models.Log log = new nettbutikkpls.Models.Log();
                    log.ChangedTime = (DateTime.Now).ToString("yyyyMMddHHmmss");
                    log.EventType = "Create";
                    log.NewValue = newCustomerRow.ToString();
                    Customer changedby = (Customer)HttpContext.Current.Session["CurrentCustomer"]; ;
                    Debug.Print("Navn: " + changedby.firstname);
                    if (HttpContext.Current.Session["CurrentCustomer"] != null)
                    {
                        changedby = (Customer)HttpContext.Current.Session["CurrentCustomer"];
                        log.ChangedBy = changedby.firstname;
                    }
                    else
                    {
                        log.ChangedBy = "null";
                    }
                    SaveToLog(log.toString());
                    //End of saving to Log
                    return true;
                }catch(Exception e)
                {
                    string message = "Exception: " + e + " catched at DeleteOrder()";
                    SaveToErrorLog(message);
                    return false;
                }                   
            }  
        }

        public void SaveToLog(string log)
        {
            string path = "Log.txt";
            var _Path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/"), path);
            if (!File.Exists(_Path))
            {
                string createText = log + Environment.NewLine;
                File.WriteAllText(_Path, createText);
            }
            else
            {
                string appendText = log + Environment.NewLine;
                File.AppendAllText(_Path, appendText);
            }
        }

        public void SaveToErrorLog(string log)
        {
            string path = "ErrorLog.txt";
            var _Path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/"), path);
            if (!File.Exists(_Path))
            {
                string createText = log + Environment.NewLine;
                File.WriteAllText(_Path, createText);
            }
            else
            {
                string appendText = log + Environment.NewLine;
                File.AppendAllText(_Path, appendText);
            }
        }
        public bool EditCustomer(FormCollection inList)
        {
            try
                {
                    // HttpContext context = HttpContext.Current;
                    Customer c = (Customer)context.Session["CurrentUser"];
                    Customers customer = FindCustomersByEmail(c.email);
                string originalvalue = customer.ToString();
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

                //Start saving to log
                string newvalue = customer.ToString();
                nettbutikkpls.Models.Log log = new nettbutikkpls.Models.Log();
                log.ChangedTime = (DateTime.Now).ToString("yyyyMMddHHmmss");
                log.EventType = "Update";
                log.OriginalValue = originalvalue;
                log.NewValue = newvalue;
                if (HttpContext.Current.Session["CurrentCustomer"] != null)
                {
                    Customer changedby = (Customer)HttpContext.Current.Session["CurrentCustomer"];
                    log.ChangedBy = changedby.firstname;
                }
                else
                {
                    log.ChangedBy = "null";
                }
                SaveToLog(log.toString());
                //End saving to log
                c = FindCustomerByEmail(customer.Mail);
                    context.Session["CurrentUser"] = c;
                    return true;
                }
                catch (Exception e)
                {
                    string message = "Exception: " + e + " catched at EditCustomer()";
                    SaveToErrorLog(message);
                    return false;
                }
        }
        public bool Login()
        {
            if (context.Session["CurrentUser"] == null)
            {
                return false;
            }
            
            return true;
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
                }
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
        public Customer FindCustomerByEmail(string email)
        {
            Customer c = new Customer();
            List<Customers> GetAllCustomers = bmx.Customers.ToList();
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
            }
            return null;
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
            Customer c = (Customer)context.Session["CurrentUser"];
            return c.customerId;
        }
        public Customer CurrentCustomer()
        {
            return (Customer)context.Session["CurrentUser"];
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
                    c.email = customer.Mail;
                    c.firstname = customer.Firstname;
                    c.lastname = customer.Lastname;
                    c.address = customer.Address;
                    c.isadmin = customer.IsAdmin;
                    c.zipcode = customer.Zipcode;
                    c.password = customer.Password;
                    c.salt = customer.Salt;

                    return c;
                }
                catch (Exception e)
                {
                    string message = "Exception: " + e + " catched at FindCustomer()";
                    SaveToErrorLog(message);
                    return null;
                }
            }
        }
        public bool UpdateCustomer(FormCollection inList, int customerid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var customer = db.Customers.Single(b => (b.CustomerId == customerid));
                    Debug.Write("kundeid " + customer.CustomerId);

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
                    string message = "Exception: " + e + " catched at UpdateCustomer()";
                    SaveToErrorLog(message);
                    return false;
                }
            }
        }
    }
} 
