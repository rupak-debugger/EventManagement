using EventManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace EventManagement.Tests
{
    public class HomeControllerTest
    {
        [Fact]
        public void TestIndexView()
        {
            var mock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> logger = mock.Object;

            //or use this short equivalent 
            logger = Mock.Of<ILogger<HomeController>>();
            var controller = new HomeController(logger);
            var result = controller.Index() as ViewResult;
            Assert.Equal("Index", result.ViewName);

        }
        [Fact]
        public void TestAboutUsView()
        {
            var mock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> logger = mock.Object;

            //or use this short equivalent 
            logger = Mock.Of<ILogger<HomeController>>();
            var controller = new HomeController(logger);
            var result = controller.AboutUs() as ViewResult;
            Assert.Equal("AboutUs", result.ViewName);

        }
    }
}