using NUnit.Framework;
using SSG.Interview;
using System.Collections.Generic;
using System.Linq;

namespace SSS.Interview.UnitTests
{
    [TestFixture]
    public class Tests
    {
        private Repository<Storeable> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Repository<Storeable>();
        }

        [Test]
        public void Repository_All_Returns_Success()
        {
            Storeable itemOne = new Storeable { Id = 1 };
            Storeable itemTwo = new Storeable { Id = 2 };

            _repository.Save(itemOne);
            _repository.Save(itemTwo);

            IEnumerable<IStoreable> expectedResult = _repository.All();

            Assert.IsInstanceOf<IEnumerable<Storeable>>(expectedResult);
            Assert.IsTrue(expectedResult.Count() == 2, "Returned count was not what was expected");
            Assert.IsTrue(((IEnumerable<Storeable>)expectedResult).Contains(itemOne), "Expected itemOne was not returned");
            Assert.IsTrue(((IEnumerable<Storeable>)expectedResult).Contains(itemTwo), "Expected itemTwo was not returned");
        }

        [Test]
        public void Repository_All_Returns_Fail()
        {
            Storeable itemOne = new Storeable { Id = 1 };

            _repository.Save(itemOne);

            IEnumerable<IStoreable> expectedResult = _repository.All();

            Assert.IsFalse(expectedResult.Count() == 2, "Returned count should not be 2");
        }

        [Test]
        public void Repository_Save_Returns_Success()
        {
            Storeable itemOne = new Storeable { Id = 1 };

            _repository.Save(itemOne);

            IEnumerable<IStoreable> expectedResult = _repository.All();

            Assert.IsInstanceOf<IEnumerable<Storeable>>(expectedResult);
            Assert.IsTrue(expectedResult.Count() == 1, "Returned count was not what was expected");
            Assert.IsTrue(((IEnumerable<Storeable>)expectedResult).Contains(itemOne), "Expected itemOne was not returned");
        }

        [Test]
        public void Repository_Delete_Returns_Success()
        {
            Storeable itemOne = new Storeable { Id = 1 };

            _repository.Save(itemOne);
            _repository.Delete(itemOne.Id);

            IEnumerable<IStoreable> expectedResult = _repository.All();

            Assert.IsInstanceOf<IEnumerable<Storeable>>(expectedResult);
            Assert.IsTrue(expectedResult.Count() == 0, "Returned count was not what was expected");
        }

        [Test]
        public void Repository_FindById_Returns_Success()
        {
            Storeable itemOne = new Storeable { Id = 1 };

            _repository.Save(itemOne);

            Storeable expectedResult = _repository.FindById(itemOne.Id);

            Assert.IsInstanceOf<IStoreable>(expectedResult);
            Assert.AreEqual(itemOne, expectedResult, "Returned item did not match what was expected");
        }

        [Test]
        public void Repository_FindById_Returns_Fail()
        {
            Storeable itemOne = new Storeable { Id = 1 };
            Storeable itemTwo = new Storeable { Id = 2 };

            _repository.Save(itemOne);

            Storeable expectedResult = _repository.FindById(itemTwo.Id);

            Assert.IsNull(expectedResult, "No returned item should be expected");
        }
    }
}