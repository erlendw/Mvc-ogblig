using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_oblig.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult GetAllCustomers()
        {
            var db = new Models.CustomerContext();
            List<Models.Customer> GetAllCustomers = db.Customer.ToList();
            ViewData.Model = GetAllCustomers;
            //ViewBag.Message = "Click here " + GetAllCustomers[0].FirstName;
            ViewData["Message"] = "Do some stuff..and some shit: ";
            return View();
        }
        public ActionResult CreateCustomer()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomer(FormCollection inList)
        {
            try
            {
                using (var db = new Models.CustomerContext())
                {
                    var newCustomer = new Models.Customer();
                    newCustomer.FirstName = inList["FirstName"];
                    newCustomer.LastName = inList["LastName"];
                    newCustomer.Address = inList["Address"];
                    newCustomer.mail = inList["Mail"];
                    newCustomer.Password = inList["Password"];
                    // kan ikke bruke dette array i LINQ nedenfor
                    string inZip = inList["ZipCode"];

                    var foundPostalArea = db.PostalArea
                   .FirstOrDefault(p => p.ZipCode == inZip);
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
            }
            catch (Exception e)
            {
                return View();
            }
        }
    }
}