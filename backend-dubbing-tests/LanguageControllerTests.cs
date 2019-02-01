using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SoftServe.ITAcademy.BackendDubbingProject.Controllers;
using SoftServe.ITAcademy.BackendDubbingProject.Models;
using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

namespace SoftServe.ITAcademy.BackendDubbingProjectTests
{
    public class LanguageControllerTests
    {
        private List<Language> _testData;

        private List<Language> GetTestLanguages()
        {
            List<Language> testLanguages = new List<Language>
            {
            new Language { Id = 1, Name = "lang1" },
            new Language { Id = 2, Name = "lang2" },
            new Language { Id = 3, Name = "lang3" },
            };

            return testLanguages;
        }

        [SetUp]
        public void Setup()
        {
            _testData = GetTestLanguages();
        }

        [Test]
        public void GetLanguachesTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(_testData);
            var controller = new LanguageController(mock.Object);

            IEnumerable<Language> result = controller.Get();

            Assert.AreEqual(_testData, result);
        }

        [Test]
        public void Test()
        {
            Assert.AreEqual(1, 1);
        }
    }
}