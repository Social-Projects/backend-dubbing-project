namespace SoftServe.ITAcademy.BackendDubbingProjectTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using NUnit.Framework;
    using NUnit.Framework.Constraints;
    using SoftServe.ITAcademy.BackendDubbingProject.Controllers;
    using SoftServe.ITAcademy.BackendDubbingProject.Models;
    using SoftServe.ITAcademy.BackendDubbingProject.Utilities;

    [TestFixture]
    public class PerformanceControllerTests
    {
        private Mock<IRepository<Performance>> _performanceRepository = null;
        private PerformanceController _performanceController = null;
        private IEnumerable<Performance> _performanceTestData = null;

        public PerformanceControllerTests()
        {
            this._performanceTestData = new List<Performance>
            {
                new Performance { Id = 1, Title = "Performance 1", Description = "Description 1", 
                    Speeches = new List<Speech>
                    {
                        new Speech
                        {
                            Id = 1,
                            Text = "Text 1",
                            PerformanceId = 1,
                            Audios = new List<Audio>
                            {
                                new Audio { Id = 1, FileName = "audios/audio1.mp3", SpeechId = 1, LanguageId = 1 },
                                new Audio { Id = 2, FileName = "audios/audio2.mp3", SpeechId = 1, LanguageId = 2 }
                            }
                        },
                        new Speech
                        {
                            Id = 2,
                            Text = "Text 2",
                            PerformanceId = 1,
                            Audios = new List<Audio>
                            {
                                new Audio { Id = 3, FileName = "audios/audio3.mp3", SpeechId = 2, LanguageId = 1 },
                                new Audio { Id = 4, FileName = "audios/audio4.mp3", SpeechId = 2, LanguageId = 2 }
                            }
                        }
                    } 
                },
                new Performance { Id = 2, Title = "Performance 2", Description = "Description 2",
                    Speeches = new List<Speech>
                    {
                        new Speech
                        {
                            Id = 3,
                            Text = "Text 3",
                            PerformanceId = 2,
                            Audios = new List<Audio>
                            {
                                new Audio { Id = 5, FileName = "audios/audio5.mp3", SpeechId = 3, LanguageId = 1 },
                                new Audio { Id = 6, FileName = "audios/audio6.mp3", SpeechId = 3, LanguageId = 2 }
                            }
                        },
                        new Speech
                        {
                            Id = 4,
                            Text = "Text 4",
                            PerformanceId = 2,
                            Audios = new List<Audio>
                            {
                                new Audio { Id = 7, FileName = "audios/audio7.mp3", SpeechId = 4, LanguageId = 1 },
                                new Audio { Id = 8, FileName = "audios/audio8.mp3", SpeechId = 4, LanguageId = 2 }
                            }
                        }
                    }
                },
                new Performance 
                { 
                    Id = 3,
                    Title = "Performance 3",
                    Description = "Description 3",
                    Speeches = new List<Speech>
                    {
                        new Speech
                        { 
                            Id = 5,
                            Text = "Text 5",
                            PerformanceId = 3,
                            Audios = new List<Audio>
                            {
                                new Audio { Id = 9, FileName = "audios/audio9.mp3", SpeechId = 5, LanguageId = 1 },
                                new Audio { Id = 10, FileName = "audios/audio10.mp3", SpeechId = 5, LanguageId = 2 }
                            }
                        },
                        new Speech
                        {
                            Id = 6,
                            Text = "Text 6",
                            PerformanceId = 3,
                            Audios = new List<Audio>
                            {
                                new Audio { Id = 11, FileName = "audios/audio11.mp3", SpeechId = 6, LanguageId = 1 },
                                new Audio { Id = 12, FileName = "audios/audio12.mp3", SpeechId = 6, LanguageId = 2 }
                            }
                        }
                    } 
                }
            };
        }

        [SetUp]
        public void Initialize()
        {
            this._performanceRepository = new Mock<IRepository<Performance>>();
            this._performanceController = new PerformanceController(this._performanceRepository.Object);
        }

        // Testing Get Method
        [Test]
        public void Get_WhenCalled_ReturnAllItems()
        {
            // Assert
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.Get();

            // Assert
            Assert.AreEqual(this._performanceTestData.Count(), response.Count());
        }

        // Testing GetById method
        [Test]
        public void GetById_InvalidIdPassed_ShouldReturnStatusCode404()
        {
            // Arrange
            const int performanceId = 8;
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.GetById(performanceId).Result as NotFoundResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Test]
        public void GetById_ValidIdPassed_ShouldReturnValidObject()
        {
            // Arrange
            const int performanceId = 1;
            var expected = this._performanceTestData.FirstOrDefault(p => p.Id == performanceId);
            
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);
            this._performanceRepository.Setup(rep => rep.GetItem(performanceId, null))
                .Returns(expected);

            // Act
            var response = this._performanceController.GetById(performanceId);

            // Assert
            Assert.AreEqual(expected, response.Value);
        }

        // Testing GetSpeeches method
        [Test]
        public void GetSpeeches_InvalidIdPassed_ShouldReturnStatusCode404()
        {  
            // Arrange
            const int performanceId = 8;
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.GetSpeeches(performanceId).Result as NotFoundResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Test]
        public void GetSpeeches_ValidIdPassed_ShouldReturnValidSpeecheAndStatusCode200()
        {
            // Arrange
            const int performanceId = 1;
            var performance = this._performanceTestData.FirstOrDefault(p => p.Id == performanceId);

            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);
            this._performanceRepository
                .Setup(rep => rep.GetItem(performanceId, It.IsAny<Func<IQueryable<Performance>, IIncludableQueryable<Performance, object>>>()).Speeches)
                .Returns(performance.Speeches);
            
            // Act
            var response = this._performanceController.GetSpeeches(performanceId).Result as OkObjectResult;
            
            // Assert
            Assert.AreEqual(performance.Speeches, response.Value);
            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
        }

        // Testing Create method
        [Test]
        public void Create_PassedInvalidObject_ShouldReturnStatusCode400()
        {
            // Arrange
            var performance = new Performance() { Id = 1, Description = "Description 1" };
            this._performanceController.ModelState.AddModelError("Description", "The Description property required");

            // Act
            var response = this._performanceController.Create(performance).Result as BadRequestResult;
            
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Test]
        public void Create_PassedValidObject_ShouldReturnValidObjectAndStatusCode201()
        {
            // Arrange
            var performance = this._performanceTestData.FirstOrDefault(p => p.Id == 1);

            // Act
            var response = this._performanceController.Create(performance).Result as CreatedAtActionResult;

            // Assert
            Assert.AreEqual(performance, response.Value);
            Assert.AreEqual(StatusCodes.Status201Created, response.StatusCode);
        }

        // Testing Update method
        [Test]
        public void Update_InvalidObjectPassed_ShouldReturnStatusCode400()
        {
            // Arrange
            var performance = new Performance { Id = 1, Description = "Description 1" };
            this._performanceController.ModelState.AddModelError("Title", "The Title property required");

            // Act
            var response = this._performanceController.Update(performance).Result as BadRequestResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Test]
        public void Update_ValidObjectPassed_ShouldReturnUpdatedObject()
        {
            // Arrange
            var performance = this._performanceTestData.FirstOrDefault(p => p.Id == 1);
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.Update(performance);

            // Assert
            Assert.AreEqual(performance, response.Value);
        }

        [Test]
        public void Update_NotExistingObjectPassed_ShouldReturnStatusCode404()
        {
            // Arrange
            var performance = new Performance { Id = 8, Title = "Title 8", Description = "Description 8" };
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.Update(performance).Result as NotFoundResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        // Testing Delete method
        [Test]
        public void Delete_InvalidIdPassed_ShouldReturnStatusCode404()
        {
            // Arrange
            var performanceId = 8;
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);
            
            // Act
            var response = this._performanceController.Delete(performanceId).Result as NotFoundResult;

            // Assert
            Assert.AreEqual(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Test]
        public void Delete_ValidIdPassed_ShouldReturnDeletedObject()
        {
            // Arrange
            const int performanceId = 1;
            var deletedPerformance = this._performanceTestData.FirstOrDefault(per => per.Id == performanceId);

            this._performanceRepository.Setup(rep => rep.GetAllItems(It.IsAny<Func<IQueryable<Performance>, IIncludableQueryable<Performance, object>>>()))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.Delete(performanceId);

            // Assert
            Assert.AreEqual(deletedPerformance, response.Value);
        }

        [TearDown]
        public void FreeResources()
        {
            this._performanceController = null;
            this._performanceRepository = null;
        }
    }
}