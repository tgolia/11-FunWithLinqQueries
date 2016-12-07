﻿using LinqExercises.Controllers;
using LinqExercises.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace LinqExercises.Test.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private ProductsController _productsController;

        [TestInitialize]
        public void Initialize()
        {
            // ARRANGE
            _productsController = new ProductsController();
        }

        [TestMethod]
        public void GetDiscontinuedProductsTest()
        {
            // ACT
            IHttpActionResult actionResult = _productsController.GetDiscontinuedCount();
            var contentResult = actionResult as OkNegotiatedContentResult<int>;

            // ASSERT
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(8, contentResult.Content);
        }

        [TestMethod]
        public void GetProductsInCategoryTest()
        {
            // ACT
            IHttpActionResult actionResult = _productsController.GetProductsInCategory("Condiments");
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<Product>>;

            // ASSERT
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content.Count(), 12);
        }

        [TestMethod]
        public void GetStockReportTest()
        {
            // ACT
            dynamic contentResult = _productsController.GetStockReport();

            var list = ((IEnumerable<dynamic>)contentResult.Content).ToList();

            // ASSERT
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(12, list.Count);
        }
    }
}
