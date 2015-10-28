using System;
using System.Web.Mvc;
using nettButikkpls;
using nettButikkpls.Models;
using nettButikkpls.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using System.Collections.Generic;

namespace EnhetsTest
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void List()
        {
            //Arrange
            var controller = new CustomerController();
            //Act
            var result = (ViewResult)controller.List();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }
        [TestMethod]
        public void Reg()
        {
            //Arrange
            var controller = new CustomerController();
            //Act
            var result = (ViewResult)controller.Reg();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
    }
}
