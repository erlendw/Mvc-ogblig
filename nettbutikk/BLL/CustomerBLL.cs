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
    public class CustomerBLL
    {
        public List<Customer> allCustomers()
        {
            var customerDal = new CustomerDAL();
            return customerDal.allCustomers();
        }
        public bool saveCustomer(Customer inCustomer)
        {
            var customerDal = new CustomerDAL();
            return customerDal.saveCustomer(inCustomer);
        }

        public bool EditCustomer(FormCollection inList)
        {
            var customerDal = new CustomerDAL();
            return customerDal.EditCustomer(inList);
        }
        public bool Login()
        {
            var customerDal = new CustomerDAL();
            return customerDal.Login();
        }
        public bool ValidateUser(FormCollection inList)
        {
            var customerDal = new CustomerDAL();
            return customerDal.ValidateUser(inList);
        }
        public String GenerateSalt(int size)
        {
            var customerDal = new CustomerDAL();
            return customerDal.GenerateSalt(size);
        }
        public String HashPassword(String Password, string Salt)
        {
            var customerDal = new CustomerDAL();
            return customerDal.HashPassword(Password, Salt);

        }
        public Customer FindCustomerByEmail(string email)
        {
            var customerDal = new CustomerDAL();
            return customerDal.FindCustomerByEmail(email);
        }
        public int CurrentCustomerId()
        {
            var customerDal = new CustomerDAL();
            return customerDal.CurrentCustomerId();
        }
      /* public Customers CurrentCustomerObj()
        {
            var customerDal = new CustomerDAL();
            return customerDal.CurrentCustomerObj();
        }*/
    }
}