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
        /*[TestMethod]
        public void AddOrder()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            var SessionMock = new TestControllerBuilder();
            List<int> pids = new List<int> { 1, 2, 3 };
            Cart c = new Cart()
            {
                customerid = 1,
                productids = pids,

            };
            SessionMock.InitializeController(controller);
            controller.Session["Cart"] = c;
            //Act
            var result = (RedirectToRouteResult)controller.AddOrder();
            //Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Customer", "List");
        }*/
        [TestMethod]
        public void OrderComplete()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            //Act
            var result = (ViewResult)controller.OrderComplete();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        /*[TestMethod]
        public void TotalPrice()
        {
            //Arrange
            var controller = new OrdersController(new OrderLogic(new OrderRepoStub()));
            List<int> pid = new List<int> { 1, 1, 1 };
            var expected = 207;
            //Act
            var result = (int)controller.TotalPrice(pid);
            //Assert
            Assert.AreEqual(result, expected);
        }*/
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
    }
}
