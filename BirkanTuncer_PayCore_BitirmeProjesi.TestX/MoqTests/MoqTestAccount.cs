using BirkanTuncer_PayCore_BitirmeProjesi.Controllers;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BirkanTuncer_PayCore_BitirmeProjesi.TestX
{
    public class MoqTestAccount
    {
        private readonly Mock<IHibernateRepository<DummyAccount>> _mockRepo;
        private readonly DummyAccountController _controller;


        public MoqTestAccount()
        {
            _mockRepo = new Mock<IHibernateRepository<DummyAccount>>();
            _controller = new DummyAccountController(_mockRepo.Object);

            _mockRepo.Setup(repo => repo.GetAll()).Returns(new List<DummyAccount>() { new DummyAccount { Id = 1, Email = "birkan@example.com", Name = "Birkan Tuncer", Password = "12345678", Role = "admin", UserName = "Birkan" } });
        }


        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Get();
            Assert.IsType<List<DummyAccount>>(result);
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
            var viewResult = Assert.IsType<List<DummyAccount>>(result);
            Assert.Equal(1, viewResult.Count);
        }

    }
}
