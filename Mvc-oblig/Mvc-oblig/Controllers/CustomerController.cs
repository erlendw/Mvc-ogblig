using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Mvc_oblig.Models;
using System.Diagnostics;


//alt klikker ved endring i modellen, workaround var å droppe migration history fra databasen ... http://stackoverflow.com/questions/21852121/the-model-backing-the-context-context-has-changed-since-the-database-was-cre


namespace Mvc_oblig.Controllers
{
    
    public class CustomerController : Controller
    {
        private CustomerContext db = new CustomerContext();        // GET: Customer


        public ActionResult GetAllCustomers() 
        {
            
                List<Models.Customer> GetAllCustomers = db.Customer.ToList();
                ViewData.Model = GetAllCustomers;
                return View();
            

                
        }


        public ActionResult CreateCustomer()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Session["loggedin"] == null)
            {
                Session["loggedin"] = false;
            }
            else
            {
                ViewBag.loggedin = (bool)Session["loggedin"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateCustomer(FormCollection inList)
        {
            try
            {
                

                    String salt = GenerateSalt(32);

                    var newCustomer = new Models.Customer();
                    newCustomer.Mail = inList["Email"];
                    newCustomer.Password = HashPassword(inList["Password"], salt);//inList["Password"];
                    newCustomer.FirstName = inList["FirstName"];
                    newCustomer.LastName = inList["LastName"];
                    newCustomer.Address = inList["Address"];
                    newCustomer.Salt = salt;

                    // kan ikke bruke dette array i LINQ nedenfor
                    string inZip = inList["ZipCode"];

                    var foundPostalArea = db.PostalArea.FirstOrDefault(p => p.ZipCode == inZip);
                    if (foundPostalArea == null) // fant ikke poststed, må legge inn et nytt
                    {
                        var newPostalArea = new Models.PostalArea();
                        newPostalArea.ZipCode = inList["ZipCode"];
                        newPostalArea.PostalArea_ = inList["PostalArea"];
                        db.PostalArea.Add(newPostalArea);
                        // det nye poststedet legges i den nye brukeren
                        newCustomer.PostalArea = newPostalArea;
                    }
                    else
                    { // fant poststedet, legger det inn i den nye brukeren
                        newCustomer.PostalArea = foundPostalArea;
                    }

                    db.Customer.Add(newCustomer);
                    db.SaveChanges();
                    return RedirectToAction("GetAllCustomers");

                
            }

            catch (Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult UpdateCustomer(FormCollection inList)
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];

            try
            {

                Customer customer = FindCustomerByEmail(c.Mail);
                customer.FirstName = inList["FirstName"];
                customer.LastName = inList["LastName"];
                customer.Address = inList["Address"];
                customer.Mail = inList["Mail"];
                db.SaveChanges();
                HttpContext.Session["CurrentUser"] = customer;
                
                    return RedirectToAction("UserProfile");
            }
            catch (Exception e)
            {
                return RedirectToAction("UserProfile");
            }
           
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


        [HttpPost]
        public ActionResult ValidateUser(FormCollection inList)
        {
            Customer customer = FindCustomerByEmail(inList["Email"]);

            if (customer != null)
            {

                String OldHash = customer.Password;
                String ReHash = HashPassword(inList["Password"], customer.Salt);

                if(OldHash == ReHash)
                {

                    Session["loggedin"] = true;
                    Session["CurrentUser"] = customer;
                    Debug.WriteLine("get logged in son");
                    return RedirectToAction("GetAllCustomers");

                }
                else
                {
                    Session["loggedin"] = false;
                    Session["CurrentUser"] = null;
                    Debug.WriteLine("null funk");
                    
                }
            }
            return RedirectToAction("Login", "Login");
        }

        public Customer FindCustomerByEmail(string Email)
        {
            
                List<Models.Customer> GetAllCustomers = db.Customer.ToList();

                for (int i = 0; i < GetAllCustomers.Count; i++)
                {
                    if (GetAllCustomers[i].Mail == Email)
                    {
                        return GetAllCustomers[i];
                    }
                }
                return null;
                 
        }
    }
}