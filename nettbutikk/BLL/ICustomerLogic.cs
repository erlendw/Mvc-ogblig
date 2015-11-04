using System.Collections.Generic;
using System.Web.Mvc;
using nettButikkpls.Models;
using nettButikkpls.DAL;

namespace nettButikkpls.BLL
{
    public interface ICustomerLogic
    {
        List<Customer> allCustomers();
        int CurrentCustomerId();
        bool EditCustomer(FormCollection inList);
        Customer FindCustomerByEmail(string email);
        Customers FindCustomersByEmail(string email);
        string GenerateSalt(int size);
        string HashPassword(string Password, string Salt);
        bool Login();
        bool saveCustomer(Customer inCustomer);
        bool ValidateUser(FormCollection inList);
        Customer FindCustomer(int customerid);
        public bool UpdateCustomer(FormCollection inList, int customerid);
    }
}