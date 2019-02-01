namespace SoftServe.ITAcademy.BackendDubbingProjectTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using SoftServe.ITAcademy.BackendDubbingProject.Controllers;
    using SoftServe.ITAcademy.BackendDubbingProject.Models;
    using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

    public class LanguageControllerTests
    {
        private List<Language> testData;
        private List<Language> nothingTestData;

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

        private List<Language> GetNothingTestLanguages()
        {
            List<Language> testLanguages = new List<Language>
            {
            };

            return testLanguages;
        }

        [SetUp]
        public void Setup()
        {
            this.testData = this.GetTestLanguages();
            this.nothingTestData = this.GetNothingTestLanguages();
        }

        [Test]
        public void GetLanguagesTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.testData);
            var controller = new LanguageController(mock.Object);

            IEnumerable<Language> result = controller.Get();

            Assert.AreEqual(this.testData, result);
        }

        [Test]
        public void GetNothingLanguagesTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.nothingTestData);
            var controller = new LanguageController(mock.Object);

            IEnumerable<Language> result = controller.Get();

            Assert.AreEqual(this.nothingTestData, result);
        }

        [Test]
        public void AddNewLanguageTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.testData);
            var controller = new LanguageController(mock.Object);

            var lang4 = new Language { Id = 0, Name = "lang4" };

            controller.Create(lang4);
            this.testData.Add(lang4);

            Assert.AreEqual(controller.Get(), this.testData);

        }

        [Test]
        public void AddNewLanguageWithoutNameTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.testData);
            var controller = new LanguageController(mock.Object);

            var lang4 = new Language { Id = 0, Name = "" };

            controller.Create(lang4);
            this.testData.Add(lang4);

            Assert.AreEqual(controller.Get(), this.testData);
        }

        [Test]
        public void AddTwoSameLanguageTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.testData);
            var controller = new LanguageController(mock.Object);

            var lang4 = new Language { Id = 0, Name = "lang4" };

            controller.Create(lang4);
            controller.Create(lang4);
            this.testData.Add(lang4);
            this.testData.Add(lang4);
            Assert.AreEqual(controller.Get(), this.testData);
        }

        [Test]
        public void RemoveExistingLanguageTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.testData);
            var controller = new LanguageController(mock.Object);

            int id = 1;
            controller.Delete(id);
            var language = this.testData.FirstOrDefault(x => x.Id == id);
            this.testData.Remove(language);

            Assert.AreEqual(controller.Get(), this.testData);
        }

        [Test]
        public void RemoveNotExistingLanguageTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.testData);
            var controller = new LanguageController(mock.Object);

            int id = 8;
            controller.Delete(id);
            var language = this.testData.FirstOrDefault(x => x.Id == id);
            this.testData.Remove(language);

            Assert.AreEqual(controller.Get(), this.testData);
        }

        [Test]
        public void UpdateNotExistingLanguageTest()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.testData);
            var controller = new LanguageController(mock.Object);

            var lang4 = new Language { Id = 4, Name = "lang4" };

            var testDataCopy = new List<Language>(this.testData);

            controller.Update(lang4);

            var language = testDataCopy.FirstOrDefault(x => x.Id == lang4.Id);
            if (language != null)
            {
                testDataCopy.Remove(language);
                testDataCopy.Add(lang4);
            }
            var a = controller.Get();

            Assert.AreEqual(controller.Get(), testDataCopy);
        }

        /*
        [Test]
        public void GetLanguagesById()
        {
            var mock = new Mock<IRepository<Language>>();
            mock.Setup(repo => repo.GetAllItems(null)).Returns(this.testData);
            var controller = new LanguageController(mock.Object);

            ActionResult<Language> result = controller.GetById(8);

            Assert.AreEqual(result.Result, this.testData[1]);
        }
        */
    }
}