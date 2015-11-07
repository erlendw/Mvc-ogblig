using System;
using nettButikkpls.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace EnhetsTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
            var controller = new HomeController();
            //Act
            var result = (ViewResult)controller.Index();
            //Assert
            Assert.AreEqual(result.ViewName, "");
        }
    }
}
