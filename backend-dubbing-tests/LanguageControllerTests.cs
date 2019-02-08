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

    [TestFixture]
    public class LanguageControllerTests
    {
        private Mock<IRepository<Language>> languageRepository = null;
        private LanguageController languageController = null;
        private IEnumerable<Language> languageTestData = null;
        private IEnumerable<Language> languageTestDataWithoutLanguages = null;

        public LanguageControllerTests()
        {
            this.languageTestData = new List<Language>
            {
            new Language { Id = 1, Name = "lang1" },
            new Language { Id = 2, Name = "lang2" },
            new Language { Id = 3, Name = "lang3" },
            };

            this.languageTestDataWithoutLanguages = new List<Language>
            {
            };
        }

        [SetUp]
        public void Setup()
        {
            this.languageRepository = new Mock<IRepository<Language>>();
            this.languageController = new LanguageController(this.languageRepository.Object);
        }

        [Test]
        public void Get_WhenCalled_ReturnAllItems()
        {
            this.languageRepository.Setup(repo => repo.GetAllItems(null)).Returns(this.languageTestData);

            var result = this.languageController.Get();

            Assert.That(result, Is.EquivalentTo(this.languageTestData));
        }

        [Test]
        public void Get_WhenCalled_ReturnNoLanguages()
        {
            this.languageRepository.Setup(repo => repo.GetAllItems(null)).Returns(this.languageTestDataWithoutLanguages);

            var result = this.languageController.Get();

            Assert.That(result, Is.EquivalentTo(this.languageTestDataWithoutLanguages));
        }

        [Test]
        public void Get_WhenCalled_ReturnIEnumerableResult()
        {
            var result = this.languageController.Get();

            Assert.IsInstanceOf(typeof(IEnumerable<Language>), result);
        }

        [Test]
        public void GetById_ValidId_ShouldReturnValidObject()
        {
            int id = 1;
            var expected = this.languageTestData.FirstOrDefault(p => p.Id == id);

            this.languageRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this.languageTestData);
            this.languageRepository.Setup(rep => rep.GetItem(id, null))
                .Returns(expected);

            var result = this.languageController.GetById(id);

            Assert.AreEqual(expected, result.Value);
        }

        [Test]
        public void GetById_InvalidId_ShouldReturnNotFoundResult()
        {
            int id = 4;
            this.languageRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this.languageTestData);

            var result = this.languageController.GetById(id);

            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }

        [Test]
        public void Create_ValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
        {
            var lang4 = new Language { Id = 0, Name = "lang4" };

            var result = this.languageController.Create(lang4).Result as OkObjectResult;

            Assert.AreEqual(lang4, result.Value);
            Assert.IsInstanceOf(typeof(OkObjectResult), result);
        }

        [Test]
        public void Create_NullObject_ShouldReturnBadRequest()
        {
            var result = this.languageController.Create(null);

            Assert.IsInstanceOf(typeof(BadRequestResult), result.Result);
        }

        [Test]
        public void Update_ValidObject_ShouldReturnUpdatedObject()
        {
            var lang4 = new Language { Id = 3, Name = "lang4" };
            this.languageRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this.languageTestData);

            var result = this.languageController.Update(lang4);

            Assert.AreEqual(lang4, result.Value);
        }

        [Test]
        public void Update_NotExistObject_ShouldReturnNotFound()
        {
            var lang4 = new Language { Id = 4, Name = "lang4" };
            this.languageRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this.languageTestData);

            var result = this.languageController.Update(lang4);

            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }

        /*[Test]
        public void Delete_ValidId_ShouldReturnDeletedObject()
        {
            int id = 1;
            var deletedLanguage = this.languageTestData.FirstOrDefault(per => per.Id == id);

            this.languageRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this.languageTestData);

            var result = this.languageController.Delete(id);

            Assert.AreEqual(deletedLanguage, result.Value);
        }*/

        [Test]
        public void Delete_NotExistId_ShouldReturnNotFoundResult()
        {
            var id = 4;
            this.languageRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this.languageTestData);

            var result = this.languageController.Delete(id);

            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
        }

        [TearDown]
        public void FreeResources()
        {
            this.languageController = null;
            this.languageRepository = null;
        }
    }
}