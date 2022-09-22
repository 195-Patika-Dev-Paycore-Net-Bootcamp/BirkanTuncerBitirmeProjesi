using BirkanTuncer_PayCore_BitirmeProjesi.Controllers;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BirkanTuncer_PayCore_BitirmeProjesi.TestX
{
    public class MoqTestProduct
    {
        private readonly Mock<IHibernateRepository<DummyProduct>> _mockRepo;
        private readonly DummyProductController _controller;


        public MoqTestProduct()
        {
            _mockRepo = new Mock<IHibernateRepository<DummyProduct>>();
            _controller = new DummyProductController(_mockRepo.Object);

            _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<DummyProduct>() { new DummyProduct { Id = 1, ProductOwnerAccountId = 1, ProductName = "klavye", ProductDescription = "description", ProductColor = "Beyaz", ProductBrand = "Algida", ProductPrice = 22, isOfferable = true, isSold = false, CategoryId = 3 } });
        }


        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Get();
            Assert.IsType<List<DummyProduct>>(result);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex_Detail()
        {
            var result = _controller.GetItem(1);
            Assert.Null(result);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsExactNumberOfCategories()
        {
            var result = _controller.Get();
            var viewResult = Assert.IsType<List<DummyProduct>>(result);
            Assert.Equal(1, viewResult.Count);
        }

    }
}
