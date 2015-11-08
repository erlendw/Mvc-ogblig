using System;
using System.Web;
using nettButikkpls.Controllers;
using nettButikkpls.DAL;
using nettButikkpls.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using nettButikkpls.Models;
using System.Web.Mvc;
using System.Linq;
using MvcContrib.TestHelper;

namespace EnhetsTest
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void allProducts()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
            var expectedresult = new List<Product>();
            Product p = new Product()
            {
                productid = 1,
                productname = "Te",
                price = 69,
                category = "Te",
                description = "Dette er te",
            };
            expectedresult.Add(p);
            expectedresult.Add(p);
            expectedresult.Add(p);
            //Act
            var result = (ViewResult)controller.ListProducts();
            var resultlist = (List<Product>)result.Model;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
            for (int i = 0; i < resultlist.Count; i++)
            {
                Assert.AreEqual(expectedresult[i].productid, resultlist[i].productid);
                Assert.AreEqual(expectedresult[i].productname, resultlist[i].productname);
                Assert.AreEqual(expectedresult[i].price, resultlist[i].price);
                Assert.AreEqual(expectedresult[i].category, resultlist[i].category);
                Assert.AreEqual(expectedresult[i].description, resultlist[i].description);
            }
        }
        [TestMethod]
        public void RegProduct_Admin_OK()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
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
            var result = (ViewResult)controller.RegProduct();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        public void RegProduct_Admin_Fail()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
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
            var result = (RedirectToRouteResult)controller.RegProduct();
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "ListProducts");
        }
        [TestMethod]
        public void ShowProduct_ID_Fail()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
            
            //Act
            var result = (RedirectToRouteResult)controller.ShowProduct(null);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "ListProducts");
        }
        [TestMethod]
        public void ShowProduct_ID_OK()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
            int id = 1;
            //Act
            var result = (ViewResult)controller.ShowProduct(id);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void RegProduct_OK_Post()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
            Product p = new Product()
            {
                productid = 1,
                productname = "Te",
            };
            //Act
            var result = (RedirectToRouteResult)controller.RegProduct(p);
            //Assert
            Assert.AreEqual(result.RouteValues.Values.First(), "ListProducts");
        }
        [TestMethod]
        public void RegProduct_Fail_Post()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
            //Act
            var result = (ViewResult)controller.RegProduct(null);
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
        /*[TestMethod]
        public void SaveImagesToServer()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
            //Act
            //Assert
        }*/
        [TestMethod]
        public void FindProduct()
        {
            //Arrange
            var controller = new ProductController(new ProductLogic(new ProductRepoStub()));
            Product expected = new Product()
            {
                productid = 1,
                productname = "Te",
                price = 69,
                category = "Te",
                description = "Dette er te",
            };
            //Act
            var result = controller.FindProduct(1);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.productid, expected.productid);
            Assert.AreEqual(result.productname, expected.productname);
            Assert.AreEqual(result.price, expected.price);
            Assert.AreEqual(result.category, expected.category);
            Assert.AreEqual(result.description, expected.description);
        }
    }
}
