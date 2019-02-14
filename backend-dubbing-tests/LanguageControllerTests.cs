//using System.Threading.Tasks;
//
//namespace SoftServe.ITAcademy.BackendDubbingProjectTests
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using BackendDubbingProject.Controllers;
//    using BackendDubbingProject.Models;
//    using BackendDubbingProject.Utilities;
//    using Microsoft.AspNetCore.Mvc;
//    using Moq;
//    using NUnit.Framework;
//
//    [TestFixture]
//    public class LanguageControllerTests
//    {
//        private Mock<IRepository<Language>> languageRepository;
//        private LanguageController languageController;
//        private readonly List<Language> languageTestData;
//        private readonly List<Language> languageTestDataWithoutLanguages;
//
//        public LanguageControllerTests()
//        {
//            languageTestData = new List<Language>
//            {
//                new Language {Id = 1, Name = "lang1"},
//                new Language {Id = 2, Name = "lang2"},
//                new Language {Id = 3, Name = "lang3"},
//            };
//
//            languageTestDataWithoutLanguages = new List<Language>();
//        }
//
//        [SetUp]
//        public void Setup()
//        {
//            languageRepository = new Mock<IRepository<Language>>();
//            languageController = new LanguageController(languageRepository.Object);
//        }
//
//        [Test]
//        public void Get_WhenCalled_ReturnAllItems()
//        {
//            languageRepository
//                .Setup(repo => repo.GetAllItemsAsync(null))
//                .ReturnsAsync(languageTestData);
//
//            var result = languageController.Get();
//
//            Assert.That(result, Is.EquivalentTo(languageTestData));
//        }
//
//        [Test]
//        public void Get_WhenCalled_ReturnNoLanguages()
//        {
//            languageRepository
//                .Setup(repo => repo.GetAllItemsAsync(null))
//                .ReturnsAsync(languageTestDataWithoutLanguages);
//
//            var result = languageController.Get();
//
//            Assert.That(result, Is.EquivalentTo(languageTestDataWithoutLanguages));
//        }
//
//        [Test]
//        public void Get_WhenCalled_ReturnIEnumerableResult()
//        {
//            var result = languageController.Get();
//
//            Assert.IsInstanceOf(typeof(IEnumerable<Language>), result);
//        }
//
//        [Test]
//        public async Task GetById_ValidId_ShouldReturnValidObject()
//        {
//            const int id = 1;
//
//            var expected = languageTestData.FirstOrDefault(p => p.Id == id);
//
//            languageRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(languageTestData);
//
//            languageRepository
//                .Setup(rep => rep.GetItemAsync(id, null))
//                .ReturnsAsync(expected);
//
//            var result = await languageController.GetById(id);
//
//            Assert.AreEqual(expected, result.Value);
//        }
//
//        [Test]
//        public void GetById_InvalidId_ShouldReturnNotFoundResult()
//        {
//            const int id = 4;
//
//            languageRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(languageTestData);
//
//            var result = languageController.GetById(id);
//
//            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
//        }
//
//        [Test]
//        public async Task Create_ValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
//        {
//            var lang4 = new Language { Id = 0, Name = "lang4" };
//
//            var response = await languageController.Create(lang4);
//
//            var result = response.Result as OkObjectResult;
//
//            Assert.AreEqual(lang4, result.Value);
//
//            Assert.IsInstanceOf(typeof(OkObjectResult), result);
//        }
//
//        [Test]
//        public void Create_NullObject_ShouldReturnBadRequest()
//        {
//            var result = languageController.Create(null);
//
//            Assert.IsInstanceOf(typeof(BadRequestResult), result.Result);
//        }
//
//        [Test]
//        public async Task Update_ValidObject_ShouldReturnUpdatedObject()
//        {
//            var lang4 = new Language { Id = 3, Name = "lang4" };
//            languageRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(languageTestData);
//
//            var result = await languageController.Update(lang4);
//
//            Assert.AreEqual(lang4, result.Value);
//        }
//
//        [Test]
//        public void Update_NotExistObject_ShouldReturnNotFound()
//        {
//            var lang4 = new Language { Id = 4, Name = "lang4" };
//            languageRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(languageTestData);
//
//            var result = languageController.Update(lang4);
//
//            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
//        }
//
//        /*[Test]
//        public void Delete_ValidId_ShouldReturnDeletedObject()
//        {
//            int id = 1;
//            var deletedLanguage = this.languageTestData.FirstOrDefault(per => per.Id == id);
//
//            this.languageRepository.Setup(rep => rep.GetAllItems(null))
//                .Returns(this.languageTestData);
//
//            var result = this.languageController.Delete(id);
//
//            Assert.AreEqual(deletedLanguage, result.Value);
//        }*/
//
//        [Test]
//        public void Delete_NotExistId_ShouldReturnNotFoundResult()
//        {
//            const int id = 4;
//
//            languageRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(languageTestData);
//
//            var result = languageController.Delete(id);
//
//            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
//        }
//
//        [TearDown]
//        public void FreeResources()
//        {
//            languageController = null;
//            languageRepository = null;
//        }
//    }
//}