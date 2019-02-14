//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Query;
//using Moq;
//using NUnit.Framework;
//using SoftServe.ITAcademy.BackendDubbingProject.Controllers;
//using SoftServe.ITAcademy.BackendDubbingProject.Models;
//using SoftServe.ITAcademy.BackendDubbingProject.Utilities;
//
//namespace SoftServe.ITAcademy.BackendDubbingProjectTests
//{
//    [TestFixture]
//    public class SpeechControllerTests
//    {
//        private Mock<IRepository<Speech>> _speechRepository;
//        private SpeechController _speechController;
//        private readonly List<Speech> _speechTestData;
//
//        public SpeechControllerTests()
//        {
//            _speechTestData = new List<Speech>
//            {
//                new Speech
//                {
//                    Id = 1, Text = "Text 1", PerformanceId = 1,
//                    Audios = new List<Audio>
//                    {
//                        new Audio {Id = 1, FileName = "audios/audio1.mp3", SpeechId = 1, LanguageId = 1},
//                        new Audio {Id = 2, FileName = "audios/audio2.mp3", SpeechId = 1, LanguageId = 2}
//                    }
//                },
//                new Speech
//                {
//                    Id = 2, Text = "Text 2", PerformanceId = 1,
//                    Audios = new List<Audio>
//                    {
//                        new Audio {Id = 3, FileName = "audios/audio3.mp3", SpeechId = 2, LanguageId = 1},
//                        new Audio {Id = 4, FileName = "audios/audio4.mp3", SpeechId = 2, LanguageId = 2}
//                    }
//                },
//                new Speech
//                {
//                    Id = 3, Text = "Text 3", PerformanceId = 1,
//                    Audios = new List<Audio>
//                    {
//                        new Audio {Id = 5, FileName = "audios/audio5.mp3", SpeechId = 3, LanguageId = 1},
//                        new Audio {Id = 6, FileName = "audios/audio6.mp3", SpeechId = 3, LanguageId = 2}
//                    }
//                }
//            };
//        }
//
//        [SetUp]
//        public void Initialize()
//        {
//            _speechRepository = new Mock<IRepository<Speech>>();
//            _speechController = new SpeechController(_speechRepository.Object);
//        }
//
//        // Testing Get method
//        [Test]
//        public async Task Get_WhenCalled_ShouldReturnAllItems()
//        {
//            // Arrange
//            _speechRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_speechTestData);
//
//            // Act
//            var response = await _speechController.Get();
//
//            // Assert
//            Assert.AreEqual(_speechTestData.Count(), response.Value.Count);
//        }
//
//        // Testing GetById method
//        [Test]
//        public async Task GetById_ValidIdPassed_ShouldReturnValidObject()
//        {
//            // Arrange
//            const int speechId = 1;
//
//            var validSpeech = _speechTestData.FirstOrDefault(s => s.Id == speechId);
//
//            _speechRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_speechTestData);
//
//            _speechRepository
//                .Setup(rep => rep.GetItemAsync(speechId, null))
//                .ReturnsAsync(validSpeech);
//
//            // Act
//            var response = await _speechController.GetById(speechId);
//
//            // Assert
//            Assert.AreEqual(validSpeech, response.Value);
//        }
//
//        [Test]
//        public async Task GetById_InvalidIdPassed_ShouldReturnNotFoundResult()
//        {
//            // Arrange
//            const int speechId = 8;
//
//            _speechRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_speechTestData);
//
//            // Act
//            var response = await _speechController.GetById(speechId);
//
//            var result = response.Result as NotFoundResult;
//
//            // Assert
//            Assert.IsInstanceOf(typeof(NotFoundResult), result);
//        }
//
//        // Testing GetAllAudios method
//        [Test]
//        public async Task GetAudios_ValidIdPassed_ShouldReturnValidAudiosAndStatusCode200()
//        {
//            // Arrange
//            const int speechId = 1;
//
//            var validSpeech = _speechTestData.FirstOrDefault(s => s.Id == speechId);
//
//            _speechRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_speechTestData);
//
//            _speechRepository
//                .Setup(rep => rep.GetItemAsync(
//                    speechId, It.IsAny<Func<IQueryable<Speech>, IIncludableQueryable<Speech, object>>>()))
//                .ReturnsAsync(validSpeech);
//
//            // Act
//            var response = await _speechController.GetAudios(speechId);
//
//            var result = response.Result as OkObjectResult;
//
//            // Assert
//            Assert.AreEqual(validSpeech.Audios, response.Value);
//            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
//        }
//
//        [Test]
//        public async Task GetAudios_InvalidIdPassed_ShouldReturnStatusCode404()
//        {
//            // Arrange
//            const int speechId = 8;
//
//            _speechRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_speechTestData);
//
//            // Act
//            var response = await _speechController.GetAudios(speechId);
//
//            var result = response.Result as NotFoundResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
//        }
//
//        // Testing Create method
//        [Test]
//        public async Task Create_InvalidObjectPassed_ShouldReturnStatusCode400()
//        {
//            // Arrange
//            var invalidSpeech = new Speech {Text = "Some text"};
//            _speechController.ModelState.AddModelError("PerformanceId", "The PerformanceId property required");
//
//            // Act
////            var response = await _speechController.Create(invalidSpeech);
//
//            var result = response.Result as BadRequestResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
//        }
//
//        [Test]
//        public async Task Create_ValidObjectPassed_ShouldReturnCreatedObjectAndStatusCode201()
//        {
//            // Arrange
//            var validSpeech = new Speech {Text = "Some text", PerformanceId = 1};
//
//            // Act
//            var response = await _speechController.Create(validSpeech);
//
//            var result = response.Result as CreatedAtActionResult;
//
//            // Assert
//            Assert.AreEqual(validSpeech, response.Value);
//            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
//        }
//
//        // Testing Update method
//        [Test]
//        public async Task Update_InvalidObjectPassed_ShouldReturnStatusCode400()
//        {
//            // Arrange
//            var invalidSpeech = new Speech {Id = 1, Text = "Some text"};
//            _speechController.ModelState.AddModelError("PerformanceId", "The PerformanceId property required");
//
//            // Act
//            var response = await _speechController.Update(invalidSpeech);
//
//            var result = response.Result as BadRequestResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
//        }
//
//        [Test]
//        public async Task Update_InvalidObjectId_ShouldReturnStatusCode404()
//        {
//            // Arrange
//            var speech = new Speech {Id = 8, Text = "Text 1", PerformanceId = 1};
//
//            _speechRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_speechTestData);
//
//            // Act
//            var response = await _speechController.Update(speech);
//
//            var result = response.Result as NotFoundResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
//        }
//
//        [Test]
//        public async Task Update_ValidObjectPassed_ShouldReturnUpdatedObject()
//        {
//            // Arrange
//            var speech = _speechTestData.FirstOrDefault(s => s.Id == 1);
//
//            _speechRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_speechTestData);
//
//            // Act
//            var response = await _speechController.Update(speech);
//
//            // Assert
//            Assert.AreEqual(speech, response.Value);
//        }
//
//        // Testing Delete method
//        /*[Test]
//        public void Delete_InvalidIdPassed_ShouldReturnStatusCode404()
//        {
//            // Arrange
//            const int speechId = 8;
//            this._speechRepository.Setup(rep => rep.GetAllItems(null))
//                .Returns(this._speechTestData);
//
//            // Act
//            var response = this._speechController.Delete(speechId).Result as NotFoundResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
//        }*/
//
//        /*[Test]
//        public void Delete_ValidIdPassed_ShouldReturnDeletedObject()
//        {
//            // Arrange
//            const int speechId = 1;
//            var expectedObject = this._speechTestData.FirstOrDefault(s => s.Id == speechId);
//            this._speechRepository.Setup(rep => rep.GetAllItems(It.IsAny<Func<IQueryable<Speech>, IIncludableQueryable<Speech, object>>>()))
//                .Returns(this._speechTestData);
//            // Act
//            var response = this._speechController.Delete(speechId);
//
//            // Assert
//            Assert.AreEqual(expectedObject, response.Value);
//        }*/
//
//        [TearDown]
//        public void FreeResources()
//        {
//            _speechController = null;
//            _speechRepository = null;
//        }
//    }
//}