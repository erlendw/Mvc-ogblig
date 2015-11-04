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
            var customerDal = new CustomerRepo();
            return _repo.allCustomers();
        }
        public bool saveCustomer(Customer inCustomer)
        {
            var customerDal = new CustomerRepo();
            return _repo.saveCustomer(inCustomer);
        }

        public bool EditCustomer(FormCollection inList)
        {
            var customerDal = new CustomerRepo();
            return _repo.EditCustomer(inList);
        }
        public bool Login()
        {
            var customerDal = new CustomerRepo();
            return _repo.Login();
        }
        public bool ValidateUser(FormCollection inList)
        {
            var customerDal = new CustomerRepo();
            return _repo.ValidateUser(inList);
        }
        public String GenerateSalt(int size)
        {
            var customerDal = new CustomerRepo();
            return _repo.GenerateSalt(size);
        }
        public String HashPassword(String Password, string Salt)
        {
            var customerDal = new CustomerRepo();
            return _repo.HashPassword(Password, Salt);

        }
        public Customer FindCustomerByEmail(string email)
        {
            var customerDal = new CustomerRepo();
            return _repo.FindCustomerByEmail(email);
        }
        public Customers FindCustomersByEmail(string email)
        {
            var customerDal = new CustomerRepo();
            return _repo.FindCustomersByEmail(email);
        }
        public int CurrentCustomerId()
        {
            var customerDal = new CustomerRepo();
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