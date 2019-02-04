namespace SoftServe.ITAcademy.BackendDubbingProjectTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using Moq;
    using Microsoft.EntityFrameworkCore.Query;
    using SoftServe.ITAcademy.BackendDubbingProject.Models;
    using SoftServe.ITAcademy.BackendDubbingProject.Utilities;
    using SoftServe.ITAcademy.BackendDubbingProject.Controllers;

    [TestFixture]
    public class SpeechControllerTests
    {
        private Mock<IRepository<Speech>> _speechRepository;
        private SpeechController _speechController;
        private IEnumerable<Speech> _speechTestData;

        public SpeechControllerTests()
        {
            this._speechTestData = new List<Speech>
            {
                new Speech { Id = 1, Text = "Text 1", PerformanceId = 1,
                    Audios = new List<Audio>
                    {
                        new Audio {Id = 1, FileName = "audios/audio1.mp3", SpeechId = 1, LanguageId = 1},
                        new Audio {Id = 2, FileName = "audios/audio2.mp3", SpeechId = 1, LanguageId = 2}
                    }},
                new Speech { Id = 2, Text = "Text 2", PerformanceId = 1,
                    Audios = new List<Audio>
                    {
                        new Audio {Id = 3, FileName = "audios/audio3.mp3", SpeechId = 2, LanguageId = 1},
                        new Audio {Id = 4, FileName = "audios/audio4.mp3", SpeechId = 2, LanguageId = 2}
                    }},
                new Speech { Id = 3, Text = "Text 3", PerformanceId = 1,
                    Audios = new List<Audio>
                    {
                        new Audio {Id = 5, FileName = "audios/audio5.mp3", SpeechId = 3, LanguageId = 1},
                        new Audio {Id = 6, FileName = "audios/audio6.mp3", SpeechId = 3, LanguageId = 2}
                    }}
            };
        }

        [SetUp]
        public void Initialize()
        {
            this._speechRepository = new Mock<IRepository<Speech>>();
            this._speechController = new SpeechController(this._speechRepository.Object);
        }

        // Testing Get method
        [Test]
        public void Get_WhenCalled_ShouldReturnAllItems()
        {
            // Arrange
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);

            // Act
            var response = this._speechController.Get();

            // Assert
            Assert.AreEqual(this._speechTestData.Count(), response.Count());
        }

        // Testing GetById method
        [Test]
        public void GetById_ValidIdPassed_ShouldReturnValidObject()
        {
            // Arrange
            const int speechId = 1;
            var validSpeech = this._speechTestData.FirstOrDefault(s => s.Id == speechId);
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);
            this._speechRepository.Setup(rep => rep.GetItem(speechId, null))
                .Returns(validSpeech);

            // Act
            var response = this._speechController.GetById(speechId);

            // Assert
            Assert.AreEqual(validSpeech, response.Value);
        }

        [Test]
        public void GetById_InvalidIdPassed_ShouldReturnNotFoundResult()
        {
            // Arrange
            const int speechId = 8;
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);
            
            // Act
            var response = this._speechController.GetById(speechId).Result as NotFoundResult;

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), response);
        }

        // Testing GetAllAudios method
        [Test]
        public void GetAudios_ValidIdPassed_ShouldReturnValidAudiosAndStatusCode200()
        {
            // Arrange
            var speechId = 1;
            var validSpeech = this._speechTestData.FirstOrDefault(s => s.Id == speechId);
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);
            this._speechRepository.Setup(rep => rep.GetItem(speechId, It.IsAny<Func<IQueryable<Speech>, IIncludableQueryable<Speech, object>>>()))
                .Returns(validSpeech);

            // Act
            var response = this._speechController.GetAudios(speechId).Result as OkObjectResult;

            // Assert
            Assert.AreEqual(validSpeech.Audios, response.Value);
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        [Test]
        public void GetAudios_InvalidIdPassed_ShouldReturnStatusCode404()
        {
            // Arrange
            var speechId = 8;
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);

            // Act
            var response = this._speechController.GetAudios(speechId).Result as NotFoundResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        // Testing Create method
        [Test]
        public void Create_InvalidObjectPassed_ShouldReturnStatusCode400()
        {
            // Arrange
            var invalidSpeech = new Speech { Text = "Some text" };
            this._speechController.ModelState.AddModelError("PerformanceId", "The PerformanceId property required");
            
            // Act
            var response = this._speechController.Create(invalidSpeech).Result as BadRequestResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Test]
        public void Create_ValidObjectPassed_ShouldReturnCreatedObjectAndStatusCode201()
        {
            // Arrange
            var validSpeech = new Speech { Text = "Some text", PerformanceId = 1 };

            // Act
            var response = this._speechController.Create(validSpeech).Result as CreatedAtActionResult;

            // Assert
            Assert.AreEqual(validSpeech, response.Value);
            Assert.AreEqual(StatusCodes.Status201Created, response.StatusCode);
        }

        // Testing Update method
        [Test]
        public void Update_InvalidObjectPassed_ShouldReturnStatusCode400()
        {
            // Arrange
            var invalidSpeech = new Speech {Id = 1, Text = "Some text" };
            this._speechController.ModelState.AddModelError("PerformanceId", "The PerformanceId property required");

            // Act
            var response = this._speechController.Update(invalidSpeech).Result as BadRequestResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Test]
        public void Update_InvalidObjectId_ShouldReturnStatusCode404()
        {
            // Arrange
            var speech = new Speech { Id = 8, Text = "Text 1", PerformanceId = 1 };
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);

            // Act
            var response = this._speechController.Update(speech).Result as NotFoundResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Test]
        public void Update_ValidObjectPassed_ShouldReturnUpdatedObject()
        {
            // Arrange
            var speech = this._speechTestData.FirstOrDefault(s => s.Id == 1);
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);

            // Act
            var response = this._speechController.Update(speech);

            // Assert
            Assert.AreEqual(speech, response.Value);
        }

        // Testing Delete method
        [Test]
        public void Delete_InvalidIdPassed_ShouldReturnStatusCode404()
        {
            // Arrange
            const int speechId = 8;
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);

            // Act
            var response = this._speechController.Delete(speechId).Result as NotFoundResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Test]
        public void Delete_ValidIdPassed_ShouldReturnDeletedObject()
        {
            // Arrange
            const int speechId = 1;
            var expectedObject = this._speechTestData.FirstOrDefault(s => s.Id == speechId);
            this._speechRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._speechTestData);
            
            // Act
            var response = this._speechController.Delete(speechId);

            // Assert
            Assert.AreEqual(expectedObject, response.Value);
        }

        [TearDown]
        public void FreeResources()
        {
            this._speechController = null;
            this._speechRepository = null;
        }
    }    
}