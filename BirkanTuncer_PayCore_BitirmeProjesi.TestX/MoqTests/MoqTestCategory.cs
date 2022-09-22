using BirkanTuncer_PayCore_BitirmeProjesi.Controllers;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BirkanTuncer_PayCore_BitirmeProjesi.TestX
{
    public class MoqTestCategory
    {
        private readonly Mock<IHibernateRepository<DummyCategory>> _mockRepo;
        private readonly DummyCategoryController _controller;


        public MoqTestCategory()
        {
            _mockRepo = new Mock<IHibernateRepository<DummyCategory>>();
            _controller = new DummyCategoryController(_mockRepo.Object);

            _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<DummyCategory>() { new DummyCategory { Id = 1, CategoryName = "Teknoloji" } });
        }


        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Get();
            Assert.IsType<List<DummyCategory>>(result);
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
            var viewResult = Assert.IsType<List<DummyCategory>>(result);
            Assert.Equal(1, viewResult.Count);
        }

    }
}
