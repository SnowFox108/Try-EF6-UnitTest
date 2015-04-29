using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Infrastructure;
using Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;

namespace Core.UnitTests
{
    [TestClass]
    public class OrderServiceTests
    {
        private readonly Fixture _fix = new Fixture();

        public OrderServiceTests()
        {
            _fix.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TestMethod]
        public void CanGetAllOrder()
        {
            var id = Guid.NewGuid();

            var data = new List<Order>
            {
                _fix.Build<Order>().With(e => e.Id, id).Create(),
                _fix.Create<Order>(),
                _fix.Create<Order>(),
            }.AsQueryable();

            var mockSet = CreateMockDbSet(data);
            var mockContext = new Mock<ContentContext>();
            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            var service = new OrderServices(mockContext.Object);
            var orders = service.GetAll();

            Assert.AreEqual(3, orders.Count());
            Assert.AreEqual(id, orders.ElementAt(0).Id);
        }

        [TestMethod]
        public void CanGetSingleOrder()
        {
            var id = Guid.NewGuid();

            var data = new List<Order>
            {
                _fix.Build<Order>().With(e => e.Id, id).Create(),
                _fix.Create<Order>(),
                _fix.Create<Order>(),
            }.AsQueryable();

            var mockSet = CreateMockDbSet(data);
            var mockContext = new Mock<ContentContext>();
            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            var service = new OrderServices(mockContext.Object);
            var order = service.GetById(id);

            Assert.IsNotNull(order);
            Assert.AreEqual(order.Id, id);        
        }

        [TestMethod]
        public void CanCreateOrder()
        {
            var mockSet = CreateMockDbSet<Order>();
            var mockContext = new Mock<ContentContext>();
            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            var service = new OrderServices(mockContext.Object);
            service.Create(_fix.Create<Order>());

            mockSet.Verify(m => m.Add(It.IsAny<Order>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void CanUpdateOrder()
        {
            var mockSet = CreateMockDbSet<Order>();
            var mockContext = new Mock<ContentContext>();
            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);

            var service = new OrderServices(mockContext.Object);
            service.Update(_fix.Create<Order>());

            mockSet.Verify(m => m.Attach(It.IsAny<Order>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        private Mock<DbSet<T>> CreateMockDbSet<T>() where T : Entity
        {
            return new Mock<DbSet<T>>();
        }

        private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : Entity
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

    }
}
