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
    public class OrdersControllerTest
    {
        [TestMethod]
        public void addOrder_Null()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = null;
            //Act
            var result = (RedirectToRouteResult)controller.addOrder();
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListProducts", "Product");
        }
        [TestMethod]
        public void addOrder_OK()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            Customer c = new Customer();
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (ViewResult)controller.addOrder();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void AddOrder_Fail()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var user = new Customer()
            {
                customerId = 0,
            };
            List<int> pids = new List<int> { 1, 1, 1 };
            Cart c = new Cart()
            {
                customerid = 0,
                productids = pids,

            };
            SessionMock.InitializeController(controller);
            controller.Session["Cart"] = c;
            controller.Session["CurrentUser"] = user;
            //Act
            var result = (RedirectToRouteResult)controller.AddOrder();
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Customer", "List");
        }
        [TestMethod]
        public void AddOrder_OK()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            var user = new Customer()
            {
                customerId = 1,
            };
            List<int> pids = new List<int> { 1, 1, 1 };
            Cart c = new Cart()
            {
                customerid = 1,
                productids = pids,

            };
            SessionMock.InitializeController(controller);
            controller.Session["Cart"] = c;
            controller.Session["CurrentUser"] = user;
            //Act
            var result = (RedirectToRouteResult)controller.AddOrder();
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "OrderComplete", "Orders");
        }
        [TestMethod]
        public void OrderComplete()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            Cart c = new Cart();
            SessionMock.InitializeController(controller);
            controller.Session["Cart"] = c;
            //Act
            var result = (ViewResult)controller.OrderComplete();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void TotalPrice()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            List<int> pid = new List<int> { 1, 1, 1 };
            var expected = 69;
            //Act
            var result = (int)controller.TotalPrice(pid);
            //Assert
            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void ListOrders()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            //Act
            var result = (ViewResult)controller.ListOrders();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void CurrentCustomerId()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            int expected = 1;
            Customer c = new Customer()
            {
                customerId = 1,
            };
            SessionMock.InitializeController(controller);
            controller.Session["CurrentUser"] = c;
            //Act
            var result = (int)controller.CurrentCustomerId();
            //Assert
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void NullCart()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            Cart c = new Cart();
            SessionMock.InitializeController(controller);
            controller.Session["Cart"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.NullCart();
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "ListProducts", "Product");
        }
    }
}
