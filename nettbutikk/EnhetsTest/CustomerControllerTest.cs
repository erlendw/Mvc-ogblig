using System;
using System.Web;
using System.Web.Mvc;
using nettButikkpls;
using nettButikkpls.Models;
using nettButikkpls.BLL;
using nettButikkpls.DAL;
using nettButikkpls.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using System.Collections.Generic;
using System.Linq;

namespace EnhetsTest
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void List()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            var expectedResult = new List<Customer>();
            var customer = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = false,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            expectedResult.Add(customer);
            expectedResult.Add(customer);
            expectedResult.Add(customer);

            //Act
            var result = (ViewResult)controller.List();
            var resultlist = (List<Customer>)result.Model;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
            for(int i = 0; i < resultlist.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].email, resultlist[i].email);
                Assert.AreEqual(expectedResult[i].password, resultlist[i].password);
                Assert.AreEqual(expectedResult[i].firstname, resultlist[i].firstname);
                Assert.AreEqual(expectedResult[i].lastname, resultlist[i].lastname);
                Assert.AreEqual(expectedResult[i].address, resultlist[i].address);
                Assert.AreEqual(expectedResult[i].isadmin, resultlist[i].isadmin);
                Assert.AreEqual(expectedResult[i].salt, resultlist[i].salt);
                Assert.AreEqual(expectedResult[i].zipcode, resultlist[i].zipcode);
                Assert.AreEqual(expectedResult[i].postalarea, resultlist[i].postalarea);
            }
        }
        [TestMethod]
        public void Reg()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            //Act
            var result = (ViewResult)controller.Reg();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void Reg_OK_Post()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            var inCustomer = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = false,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };

            //Act
            var result = (RedirectToRouteResult)controller.Reg(inCustomer);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "List");
        }
        [TestMethod]
        public void Reg_Fail_Post()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            var inCustomer = new Customer();
            controller.ViewData.ModelState.AddModelError("firstname", "No firstname provided");

            //Act
            var result = (ViewResult)controller.Reg(inCustomer);

            //Assert
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void UserProfile()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            //Act
            var result = (ViewResult)controller.UserProfile();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void Edit()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            //Act
            var result = (ViewResult)controller.Edit();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void UpdateCustomer_OK_Post()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            FormCollection inList = new FormCollection();
            inList.Add("Firstname", "Egil");

            //Act
            var result = (RedirectToRouteResult)controller.UpdateCustomer(inList);
            
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "List");
        }
        [TestMethod]
        public void UpdateCustomer_Fail_Post()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            FormCollection inList = new FormCollection();

            //Act
            var result = (RedirectToRouteResult)controller.UpdateCustomer(inList);

            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "List");
        }
        [TestMethod]
        public void FindCustomerByEmail_Email_OK()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            var email = "daniel@thoresen.no";

            //Act
            var result = (Customer)controller.FindCustomerByEmail(email);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.email, email);
        }
        [TestMethod]
        public void FindCustomerByEmail_Email_Fail()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            var email = "";

            //Act
            var result = (Customer)controller.FindCustomerByEmail(email);

            //Assert
            Assert.AreEqual(null, result);
        }
        [TestMethod]
        public void Login_OK()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = false,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.Login();
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "List");
        }
        [TestMethod]
        public void Login_Fail()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = null;
            //Act
            var result = (ViewResult)controller.Login();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void ValidateUser_OK()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            FormCollection inList = new FormCollection();
            inList.Add("Firstname", "Egil");
            //Act
            var result = (RedirectToRouteResult)controller.ValidateUser(inList);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListProducts", "Product");
        }
        [TestMethod]
        public void ValidateUser_Fail()
        {
            //Arrange
            var controller = new CustomerController(new CustomerLogic(new CustomerRepoStub()));
            FormCollection inList = new FormCollection();
            //Act
            var result = (RedirectToRouteResult)controller.ValidateUser(inList);
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Login");
        }
    }
}
