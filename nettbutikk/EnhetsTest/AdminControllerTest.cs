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
    public class AdminControllerTest
    {
        [TestMethod]
        public void AdminPanel_Admin_OK()
        {
            //Arrange
            var controller = new AdminController();
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (ViewResult)controller.AdminPanel();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
         [TestMethod]
         public void AdminPanel_Admin_False()
         {
            //Arrange
            var controller = new AdminController();
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
            var result = (RedirectToRouteResult)controller.AdminPanel();
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "redirect");
        }
        [TestMethod]
        public void UpdateProduct_Admin_OK_Product_OK()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
            var SessionMock = new TestControllerBuilder();
            FormCollection inList = new FormCollection();
            inList.Add("Name", "auraTe");
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.UpdateProduct(inList, 1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "AdminPanel");
        }
        [TestMethod]
        public void UpdateProduct_Admin_OK_Product_Fail()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
            var SessionMock = new TestControllerBuilder();
            FormCollection inList = new FormCollection();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.UpdateProduct(inList, 1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "ListProducts");
        }
        [TestMethod]
        public void UpdateProduct_Admin_Fail()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
            var SessionMock = new TestControllerBuilder();
            FormCollection inList = new FormCollection();
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
            var result = (RedirectToRouteResult)controller.UpdateProduct(inList, 1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }
        [TestMethod]
        public void ListProducts_Admin_OK()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (ViewResult)controller.ListProducts();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void ListProducts_Admin_Fail()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
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
            var result = (RedirectToRouteResult)controller.ListProducts();
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }
        [TestMethod]
        public void EditProduct_Admin_OK_ID_OK()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (ViewResult)controller.EditProduct(1);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void EditProduct_Admin_OK_ID_Null()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.EditProduct(null);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "EditProduct");
        }
        [TestMethod]
        public void EditProduct_Admin_Fail()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
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
            var result = (RedirectToRouteResult)controller.EditProduct(1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }
        [TestMethod]
        public void FindProduct()
        {
            //Arrange
            var controller = new AdminController(new ProductLogic(new ProductRepoStub()));
            Product expected = new Product()
            {
                productid = 1,
                productname = "Te",
                price = 69,
                category = "Te",
                description = "Dette er te",
            };
            //Act
            var result = (Product)controller.FindProduct(1);
            //Assert
            Assert.AreEqual(result.productid, expected.productid);
            Assert.AreEqual(result.productname, expected.productname);
            Assert.AreEqual(result.price, expected.price);
            Assert.AreEqual(result.category, expected.category);
            Assert.AreEqual(result.description, expected.description);
        }
        [TestMethod]
        public void ListCustomers_Admin_OK()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (ViewResult)controller.ListCustomers();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void ListCustomers_Admin_Fail()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
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
            var result = (RedirectToRouteResult)controller.ListCustomers();
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }
        [TestMethod]
        public void EditCustomer_Admin_OK_ID_OK()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (ViewResult)controller.EditCustomer(1);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void EditCustomer_Admin_OK_ID_Null()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.EditCustomer(null);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "EditCustomer");
        }
        [TestMethod]
        public void EditCustomer_Admin_Fail()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
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
            var result = (RedirectToRouteResult)controller.EditCustomer(1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }
        [TestMethod]
        public void FindCustomer()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
            Customer expected = new Customer()
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
            var result = (Customer)controller.FindCustomer(1);
            //Assert
            Assert.AreEqual(result.email, expected.email);
            Assert.AreEqual(result.password, expected.password);
            Assert.AreEqual(result.firstname, expected.firstname);
            Assert.AreEqual(result.lastname, expected.lastname);
            Assert.AreEqual(result.address, expected.address);
            Assert.AreEqual(result.isadmin, expected.isadmin);
            Assert.AreEqual(result.salt, expected.salt);
            Assert.AreEqual(result.zipcode, expected.zipcode);
            Assert.AreEqual(result.postalarea, expected.postalarea);
        }
        [TestMethod]
        public void UpdateCustomer_Admin_OK_Customer_OK()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
            var SessionMock = new TestControllerBuilder();
            FormCollection inList = new FormCollection();
            inList.Add("Firstname", "Egil");
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.UpdateCustomer(inList, 1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "ListCustomers");
        }
        [TestMethod]
        public void UpdateCustomer_Admin_OK_Customer_Fail()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
            var SessionMock = new TestControllerBuilder();
            FormCollection inList = new FormCollection();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.UpdateCustomer(inList, 1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "AdminPanel");
        }
        [TestMethod]
        public void UpdateCustomer_Admin_Fail()
        {
            //Arrange
            var controller = new AdminController(new CustomerLogic(new CustomerRepoStub()));
            var SessionMock = new TestControllerBuilder();
            FormCollection inList = new FormCollection();
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
            var result = (RedirectToRouteResult)controller.UpdateProduct(inList, 1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }
        /*[TestMethod]
        public void ListOrders_Admin_OK()
        {
            //TODO: Implementere expected
            //Arrange
            var controller = new AdminController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (ViewResult)controller.ListOrders();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void ListOrders_Admin_Fail()
        {
            //TODO: Implementere expected
            //Arrange
            var controller = new AdminController(new OrderLogic(new OrderRepoStub()));
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
            var result = (RedirectToRouteResult)controller.ListOrders();
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }*/
        [TestMethod]
        public void DeleteOrder_Admin_OK_ID_OK()
        {
            //Arrange
            var controller = new AdminController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.DeleteOrder(1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "ListOrders");
        }
        [TestMethod]
        public void DeleteOrder_Admin_OK_ID_Fail()
        {
            //Arrange
            var controller = new AdminController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true,
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.DeleteOrder(-1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "ListOrders");
        }
        [TestMethod]
        public void DeleteOrder_Admin_Fail()
        {
            //Arrange
            var controller = new AdminController(new OrderLogic(new OrderRepoStub()));
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
            var result = (RedirectToRouteResult)controller.DeleteOrder(1);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }
        [TestMethod]
        public void Redirect()
        {
            //Arrange
            var controller = new AdminController();
            //Act
            var result = (RedirectToRouteResult)controller.Redirect();
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Login", "Customer");
        }
        [TestMethod]
        public void AccessOk_Pass()
        {
            //Arrange
            var controller = new AdminController();
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true, //Expected value
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (bool)controller.AccessOk();
            //Assert
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void AccessOk_Fail()
        {
            //Arrange
            var controller = new AdminController();
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = false, //Expected value
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (bool)controller.AccessOk();
            //Assert
            Assert.AreEqual(result, false);
        }
        [TestMethod]
        public void ListOrders_Admin_OK()
        {
            //Arrange
            var controller = new AdminController();
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = true, //Expected value
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (ViewResult)controller.ListOrders();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
            [TestMethod]
            }
        public void ListOrders_Admin_Fail()
        {
            //Arrange
            var controller = new AdminController();
            var SessionMock = new TestControllerBuilder();
            var c = new Customer()
            {
                email = "daniel@thoresen.no",
                password = "Sommeren2015",
                firstname = "Daniel",
                lastname = "Thoresen",
                address = "Hesselbergs gate 7A",
                isadmin = false, //Expected value
                salt = "hejhejhallo",
                zipcode = "0555",
                postalarea = "Oslo",
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.ListOrders();
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "Redirect");
        }
    }
}
