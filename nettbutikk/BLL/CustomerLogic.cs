using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Web.Mvc;
using nettButikkpls.DAL;
using nettButikkpls.Models;

namespace nettButikkpls.BLL
{
    public class CustomerLogic : ICustomerLogic
    {
        private ICustomerRepo _repo;

        public CustomerLogic()
        {
            _repo = new CustomerRepo();
        }
        public CustomerLogic(ICustomerRepo stub)
        {
            _repo = stub;
        }
        public List<Customer> allCustomers()
        {
            
            return _repo.allCustomers();
        }
        public bool saveCustomer(Customer inCustomer)
        {
            
            return _repo.saveCustomer(inCustomer);
        }

        public bool EditCustomer(FormCollection inList)
        {
            
            return _repo.EditCustomer(inList);
        }
        public bool Login()
        {
            
            return _repo.Login();
        }
        public bool ValidateUser(FormCollection inList)
        {
            
            return _repo.ValidateUser(inList);
        }
        public String GenerateSalt(int size)
        {
            
            return _repo.GenerateSalt(size);
        }
        public String HashPassword(String Password, string Salt)
        {
            
            return _repo.HashPassword(Password, Salt);

        }
        public Customer FindCustomerByEmail(string email)
        {
            
            return _repo.FindCustomerByEmail(email);
        }
        public Customers FindCustomersByEmail(string email)
        {
            
            return _repo.FindCustomersByEmail(email);
        }
        public int CurrentCustomerId()
        {
            
            return _repo.CurrentCustomerId();
        }
        public Customer FindCustomer(int customerid)
        {
            return _repo.FindCustomer(customerid);
        }
        public bool UpdateCustomer(FormCollection inList, int customerid)
        {
            return _repo.UpdateCustomer(inList, customerid);
        }
    }
}