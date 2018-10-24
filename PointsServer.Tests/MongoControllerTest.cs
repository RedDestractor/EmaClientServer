using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointsServer.Interfaces.Mongo;
using Moq;
using PointsServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using System;
using PointsServer.Controllers;
using MongoDB.Driver;

namespace PointsServer.MongoDb.Tests
{
    [TestClass]
    public class MongoControllerTest
    {
        private MongoController _sut;

        private Mock<IMongoAdapter> _mongoAdapterMock;

        private Mock<IMongoWrapper<Point>> _mongoWrapperMock;

        private List<Point> _collection;

        [TestInitialize]
        public void TestInit()
        {
            _collection = new List<Point>
            {
                new Point { _id = "58bb62440bd0d13c103cfa93", Time = new DateTime(2018, 10, 21, 11, 46, 47, 956)},
                new Point { _id = "58bb61a10bd0d120fcd33881", Time = new DateTime(2018, 10, 21, 11, 46, 48, 956)},
                new Point { _id = "58bb62ab0bd0d12c647ffc11", Time = new DateTime(2018, 10, 21, 11, 46, 49, 956)},
                new Point { _id = "58bb62ab0bd0d12c647ffc12", Time = new DateTime(2018, 10, 21, 11, 46, 50, 956)},
                new Point { _id = "58bb62ab0bd0d12c647ffc13", Time = new DateTime(2018, 10, 21, 11, 46, 51, 956)},
                new Point { _id = "58bb62ab0bd0d12c647ffc14", Time = new DateTime(2018, 10, 21, 11, 46, 52, 956)},
            };

            _mongoWrapperMock = new Mock<IMongoWrapper<Point>>();
            _mongoAdapterMock = new Mock<IMongoAdapter>(MockBehavior.Strict);
            _mongoAdapterMock.Setup(adapter => adapter.GetWrapper()).Returns(_mongoWrapperMock.Object);
            _sut = new MongoController(_mongoAdapterMock.Object);
        }

        [TestMethod]
        public async Task GetAllPoints()
        {
            _mongoWrapperMock.Setup(wrapper => wrapper.GetAll()).Returns(Task.FromResult(_collection));
            var points = await _sut.GetAll();
            Assert.AreEqual(6, points.Count);
        }
    }
}
