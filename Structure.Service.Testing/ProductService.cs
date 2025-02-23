using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Structure.Service.Testing
{
    [TestClass]
    public class ProductService
    {
        [TestMethod]
        public void CreateroductTest()
        {
            var service = new ProductsServices();
            service.CreateroductTest();
        }
    }
}
