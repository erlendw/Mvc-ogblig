using System.Collections.Generic;
using System.Web.Mvc;
using nettButikkpls.Models;

namespace nettButikkpls.DAL
{
    public interface ICustomerRepo
    {
        List<Customer> allCustomers();
        int CurrentCustomerId();
        bool EditCustomer(FormCollection inList);
        Customer FindCustomerByEmail(string email);
        Customers FindCustomersByEmail(string email);
        string GenerateSalt(int size);
        string HashPassword(string Password, string Salt);
        bool saveCustomer(Customer inCustomer);
        bool ValidateUser(FormCollection inList);
        Customer FindCustomer(int customerid);
        bool UpdateCustomer(FormCollection inList, int customerid);
    }
}