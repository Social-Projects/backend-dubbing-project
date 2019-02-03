namespace SoftServe.ITAcademy.BackendDubbingProjectTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using Moq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using SoftServe.ITAcademy.BackendDubbingProject.Controllers;
    using SoftServe.ITAcademy.BackendDubbingProject.Models;
    using SoftServe.ITAcademy.BackendDubbingProject.Utilities;
    using SoftServe.ITAcademy.BackendDubbingProject.Services;

    [TestFixture]
    public class StreamControllerTests
    {
        private Mock<IRepository<Performance>> _performanceRepositories;
        private Mock<IStreamService> _streamService;
        private StreamController _streamController;
        private IEnumerable<Performance> _performanceTestData;
        private ICollection<Audio> _audioTestData;

        public StreamControllerTests()
        {
            this._performanceTestData = new List<Performance>
            {
                new Performance { Id = 1, Title = "Performance 1", Description = "Description 1", 
                                    Speeches = new List<Speech>
                                    {
                                        new Speech { Id = 1, Text = "Text 1", PerformanceId = 1, Audios = null },
                                        new Speech { Id = 2, Text = "Text 2", PerformanceId = 1, Audios = null }
                                    } 
                },
                new Performance { Id = 2, Title = "Performance 2", Description = "Description 2",
                                    Speeches = new List<Speech>
                                    {
                                        new Speech { Id = 3, Text = "Text 3", PerformanceId = 2, Audios = null },
                                        new Speech { Id = 4, Text = "Text 4", PerformanceId = 2, Audios = null }
                                    }
                },
                new Performance { Id = 3, Title = "Performance 3", Description = "Description 3",
                                    Speeches = new List<Speech>
                                    {
                                        new Speech { Id = 5, Text = "Text 5", PerformanceId = 3, Audios = null },
                                        new Speech { Id = 6, Text = "Text 6", PerformanceId = 3, Audios = null }
                                    } 
                }
            };

            this._audioTestData = new List<Audio>
            {
                new Audio { Id = 1, LanguageId = 1, SpeechId = 1, FileName = "audio1.mp3" },
                new Audio { Id = 2, LanguageId = 2, SpeechId = 1, FileName = "audio2.mp3" },
                new Audio { Id = 3, LanguageId = 1, SpeechId = 2, FileName = "audio3.mp3" },
                new Audio { Id = 4, LanguageId = 2, SpeechId = 2, FileName = "audio4.mp3" }
            };
        }

        [SetUp]
        public void Initialize()
        {
            this._performanceRepositories = new Mock<IRepository<Performance>>();
            this._streamService = new Mock<IStreamService>();
            this._streamController = new StreamController(this._streamService.Object, this._performanceRepositories.Object);
        }

        // Testing LoadPerformance method
        [Test]
        public void LoadPerformance_InvalidPerformanceIdPassed_ShouldReturnNotFoundResult()
        {
            // Arrange
            const int performanceId = 8;
            this._performanceRepositories
                .Setup(rep => rep.GetItem(performanceId, It.IsAny<Func<IQueryable<Performance>, IIncludableQueryable<Performance, object>>>()))
                .Returns(null as Performance);

            // Act
            var response = this._streamController.LoadPerformance(performanceId);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), response);
        }

        [Test]
        public void LoadPerformance_ValidPerformanceIdPassed_ShouldReturnOkObjectResult()
        {
            // Arrange
            const int performanceId = 3;
            this._performanceRepositories.Setup(rep => rep.GetItem(performanceId, It.IsAny<Func<IQueryable<Performance>, IIncludableQueryable<Performance, object>>>()))
                .Returns(this._performanceTestData.FirstOrDefault(p => p.Id == performanceId));

            // Act
            var response = this._streamController.LoadPerformance(performanceId);

            // Assert
            Assert.IsInstanceOf(typeof(OkResult), response);
        }

        // Testing GetCurrentAudio method
        [Test]
        public void GetCurrentAudio_InvalidLangIdPassed_ShouldReturnNotFoundResult()
        {
            // Arrange
            const int languageId = 8;
            this._streamService.Setup(service => service.IsPaused)
                .Returns(false);
            this._streamService.Setup(service => service.CurrentSpeech.Audios)
                .Returns( new List<Audio> { 
                    new Audio { Id = 1, SpeechId = 1,  LanguageId = 1, FileName = "audio/au.mp3" },
                    new Audio { Id = 2, SpeechId = 1, LanguageId = 1, FileName = "audio/au1.mp3" }
                });
            
            // Act
            var response = this._streamController.GetCurrentAudio(languageId);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), response.Result);
        }

        [Test]
        public void GetCurrentAudio_ValidLanguageIdPassed_ShouldReturnValidPathToAudio()
        {
            // Arrange
            const int languageId = 1;
            this._streamService.Setup(service => service.IsPaused)
                .Returns(false);
            this._streamService.Setup(service => service.CurrentSpeech.Audios)
                .Returns(this._audioTestData);
            
            // Act
            var response = this._streamController.GetCurrentAudio(languageId);

            // Assert
            Assert.AreEqual("audio/" + this._audioTestData.First().FileName, response.Value);
        }

        [Test]
        public void GetCurrentAudio_StreamIsPaused_ShouldReturnStatusCode406()
        {
            // Arrange
            const int languageId = 1;
            this._streamService.Setup(service => service.IsPaused)
                .Returns(true);
            
            // Act
            var response = this._streamController.GetCurrentAudio(languageId).Result as StatusCodeResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status406NotAcceptable, response.StatusCode);
        }

        // Testing Play Method
        [Test]
        public void Play_InvalidIdPassed_ShouldReturnStatusCode406()
        {
            // Arrange
            const int speechIndex = 8;
            this._streamService.Setup(service => service.Play(speechIndex))
                .Returns(false);

            // Act
            var response = this._streamController.Play(speechIndex) as StatusCodeResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status406NotAcceptable, response.StatusCode);
        }

        [Test]
        public void Play_ValidIdPassed_ShouldReturnOkResult()
        {
            // Arrange
            const int speechIndex = 1;
            this._streamService.Setup(service => service.Play(speechIndex))
                .Returns(true);
            
            // Act
            var response = this._streamController.Play(speechIndex) as OkResult;

            // Assert
            Assert.IsInstanceOf(typeof(OkResult), response);
        }
    }
}