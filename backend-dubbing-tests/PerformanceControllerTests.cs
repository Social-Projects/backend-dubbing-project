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
//    public class PerformanceControllerTests
//    {
//        private Mock<IRepository<Performance>> _performanceRepository;
//        private PerformanceController _performanceController;
//        private readonly List<Performance> _performanceTestData;
//
//        public PerformanceControllerTests()
//        {
//            _performanceTestData = new List<Performance>
//            {
//                new Performance
//                {
//                    Id = 1, Title = "Performance 1", Description = "Description 1",
//                    Speeches = new List<Speech>
//                    {
//                        new Speech
//                        {
//                            Id = 1,
//                            Text = "Text 1",
//                            PerformanceId = 1,
//                            Audios = new List<Audio>
//                            {
//                                new Audio {Id = 1, FileName = "audios/audio1.mp3", SpeechId = 1, LanguageId = 1},
//                                new Audio {Id = 2, FileName = "audios/audio2.mp3", SpeechId = 1, LanguageId = 2}
//                            }
//                        },
//                        new Speech
//                        {
//                            Id = 2,
//                            Text = "Text 2",
//                            PerformanceId = 1,
//                            Audios = new List<Audio>
//                            {
//                                new Audio {Id = 3, FileName = "audios/audio3.mp3", SpeechId = 2, LanguageId = 1},
//                                new Audio {Id = 4, FileName = "audios/audio4.mp3", SpeechId = 2, LanguageId = 2}
//                            }
//                        }
//                    }
//                },
//                new Performance
//                {
//                    Id = 2, Title = "Performance 2", Description = "Description 2",
//                    Speeches = new List<Speech>
//                    {
//                        new Speech
//                        {
//                            Id = 3,
//                            Text = "Text 3",
//                            PerformanceId = 2,
//                            Audios = new List<Audio>
//                            {
//                                new Audio {Id = 5, FileName = "audios/audio5.mp3", SpeechId = 3, LanguageId = 1},
//                                new Audio {Id = 6, FileName = "audios/audio6.mp3", SpeechId = 3, LanguageId = 2}
//                            }
//                        },
//                        new Speech
//                        {
//                            Id = 4,
//                            Text = "Text 4",
//                            PerformanceId = 2,
//                            Audios = new List<Audio>
//                            {
//                                new Audio {Id = 7, FileName = "audios/audio7.mp3", SpeechId = 4, LanguageId = 1},
//                                new Audio {Id = 8, FileName = "audios/audio8.mp3", SpeechId = 4, LanguageId = 2}
//                            }
//                        }
//                    }
//                },
//                new Performance
//                {
//                    Id = 3,
//                    Title = "Performance 3",
//                    Description = "Description 3",
//                    Speeches = new List<Speech>
//                    {
//                        new Speech
//                        {
//                            Id = 5,
//                            Text = "Text 5",
//                            PerformanceId = 3,
//                            Audios = new List<Audio>
//                            {
//                                new Audio {Id = 9, FileName = "audios/audio9.mp3", SpeechId = 5, LanguageId = 1},
//                                new Audio {Id = 10, FileName = "audios/audio10.mp3", SpeechId = 5, LanguageId = 2}
//                            }
//                        },
//                        new Speech
//                        {
//                            Id = 6,
//                            Text = "Text 6",
//                            PerformanceId = 3,
//                            Audios = new List<Audio>
//                            {
//                                new Audio {Id = 11, FileName = "audios/audio11.mp3", SpeechId = 6, LanguageId = 1},
//                                new Audio {Id = 12, FileName = "audios/audio12.mp3", SpeechId = 6, LanguageId = 2}
//                            }
//                        }
//                    }
//                }
//            };
//        }
//
//        [SetUp]
//        public void Initialize()
//        {
//            _performanceRepository = new Mock<IRepository<Performance>>();
//            _performanceController = new PerformanceController(_performanceRepository.Object);
//        }
//
//        // Testing Get Method
//        [Test]
//        public async Task Get_WhenCalled_ReturnAllItems()
//        {
//            // Assert
//            _performanceRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_performanceTestData);
//
//            // Act
//            var response = await _performanceController.Get();
//
//            // Assert
//            Assert.AreEqual(_performanceTestData.Count(), response.Value.Count);
//        }
//
//        // Testing GetById method
//        [Test]
//        public async Task GetById_InvalidIdPassed_ShouldReturnStatusCode404()
//        {
//            // Arrange
//            const int performanceId = 8;
//
//            _performanceRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_performanceTestData);
//
//            // Act
//            var response = await _performanceController.GetById(performanceId);
//
//            var result = response.Result as NotFoundResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
//        }
//
//        [Test]
//        public async Task GetById_ValidIdPassed_ShouldReturnValidObject()
//        {
//            // Arrange
//            const int performanceId = 1;
//            var expected = _performanceTestData.FirstOrDefault(p => p.Id == performanceId);
//
//            _performanceRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_performanceTestData);
//
//            _performanceRepository
//                .Setup(rep => rep.GetItemAsync(performanceId, null))
//                .ReturnsAsync(expected);
//
//            // Act
//            var response = await _performanceController.GetById(performanceId);
//
//            // Assert
//            Assert.AreEqual(expected, response.Value);
//        }
//
//        // Testing GetSpeeches method
//        [Test]
//        public async Task GetSpeeches_InvalidIdPassed_ShouldReturnStatusCode404()
//        {
//            // Arrange
//            const int performanceId = 8;
//            _performanceRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_performanceTestData);
//
//            // Act
//            var response = await _performanceController.GetSpeeches(performanceId);
//
//            var result = response.Result as NotFoundResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
//        }
//
////        [Test]
////        public async Task GetSpeeches_ValidIdPassed_ShouldReturnValidSpeechesAndStatusCode200()
////        {
////            // Arrange
////            const int performanceId = 1;
////
////            var performance = _performanceTestData.FirstOrDefault(p => p.Id == performanceId);
////
////            _performanceRepository
////                .Setup(rep => rep.GetAllItemsAsync(null))
////                .ReturnsAsync(_performanceTestData);
////
////            _performanceRepository
////                .Setup(rep => rep.GetItemAsync(
////                    performanceId,
////                    It.IsAny<Func<IQueryable<Performance>, IIncludableQueryable<Performance, object>>>()).Speeches)
////                .ReturnsAsync(performance.Speeches);
////
////            // Act
////            var response = _performanceController.GetSpeeches(performanceId).Result as OkObjectResult;
////
////            // Assert
////            Assert.AreEqual(performance.Speeches, response.Value);
////            Assert.AreEqual(StatusCodes.Status200OK, response.StatusCode);
////        }
//
//        // Testing Create method
////        [Test]
////        public void Create_PassedInvalidObject_ShouldReturnStatusCode400()
////        {
////            // Arrange
////            var performance = new Performance() { Id = 1, Description = "Description 1" };
////            this._performanceController.ModelState.AddModelError("Description", "The Description property required");
////
////            // Act
////            var response = this._performanceController.Create(performance).Result as BadRequestResult;
////            
////            // Assert
////            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
////        }
//
//        [Test]
//        public async Task Create_PassedValidObject_ShouldReturnValidObjectAndStatusCode201()
//        {
//            // Arrange
//            var performance = _performanceTestData.FirstOrDefault(p => p.Id == 1);
//
//            // Act
//            var response = await _performanceController.Create(performance);
//
//            var result = response.Result as CreatedAtActionResult;
//
//            // Assert
//            Assert.AreEqual(performance, response.Value);
//            Assert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
//        }
//
////        // Testing Update method
////        [Test]
////        public void Update_InvalidObjectPassed_ShouldReturnStatusCode400()
////        {
////            // Arrange
////            var performance = new Performance { Id = 1, Description = "Description 1" };
////            this._performanceController.ModelState.AddModelError("Title", "The Title property required");
////
////            // Act
////            var response = this._performanceController.Update(performance).Result as BadRequestResult;
////
////            // Assert
////            Assert.AreEqual(StatusCodes.Status400BadRequest, response.StatusCode);
////        }
//
//        [Test]
//        public async Task Update_ValidObjectPassed_ShouldReturnUpdatedObject()
//        {
//            // Arrange
//            var performance = _performanceTestData.FirstOrDefault(p => p.Id == 1);
//            _performanceRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_performanceTestData);
//
//            // Act
//            var response = await _performanceController.Update(performance);
//
//            // Assert
//            Assert.AreEqual(performance, response.Value);
//        }
//
//        [Test]
//        public async Task Update_NotExistingObjectPassed_ShouldReturnStatusCode404()
//        {
//            // Arrange
//            var performance = new Performance {Id = 8, Title = "Title 8", Description = "Description 8"};
//            _performanceRepository
//                .Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_performanceTestData);
//
//            // Act
//            var response = await _performanceController.Update(performance);
//
//            var result = response.Result as NotFoundResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
//        }
//
//        // Testing Delete method
//        [Test]
//        public async Task Delete_InvalidIdPassed_ShouldReturnStatusCode404()
//        {
//            // Arrange
//            var performanceId = 8;
//            _performanceRepository.Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(_performanceTestData);
//
//            // Act
//            var response = await _performanceController.Delete(performanceId);
//
//            var result = response.Result as NotFoundResult;
//
//            // Assert
//            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
//        }
//
//        /*[Test]
//        public void Delete_ValidIdPassed_ShouldReturnDeletedObject()
//        {
//            // Arrange
//            const int performanceId = 1;
//            var deletedPerformance = this._performanceTestData.FirstOrDefault(per => per.Id == performanceId);
//
//            this._performanceRepository.Setup(rep => rep.GetAllItems(It.IsAny<Func<IQueryable<Performance>, IIncludableQueryable<Performance, object>>>()))
//                .Returns(this._performanceTestData);
//
//            // Act
//            var response = this._performanceController.Delete(performanceId);
//
//            // Assert
//            Assert.AreEqual(deletedPerformance, response.Value);
//        }*/
//
//        [TearDown]
//        public void FreeResources()
//        {
//            _performanceController = null;
//            _performanceRepository = null;
//        }
//    }
//}