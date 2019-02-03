namespace SoftServe.ITAcademy.BackendDubbingProjectTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
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
        }

        [SetUp]
        public void Initialize()
        {
            this._performanceRepository = new Mock<IRepository<Performance>>();
            this._performanceController = new PerformanceController(this._performanceRepository.Object);
        }

        // Test Get Method
        [Test]
        public void Get_WhenCalled_ReturnIEnumerableResult()
        {
            // Act
            var response = this._performanceController.Get() as IEnumerable<Performance>;

            // Assert
            Assert.IsInstanceOf(typeof(IEnumerable<Performance>), response);
        }

        [Test]
        public void Get_WhenCalled_ReturnAllItems()
        {
            // Assert
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.Get();

            // Assert
            Assert.AreEqual(3, response.Count());
        }

        // Test GetById method
        [Test]
        public void GetById_InvalidIdPassed_ShouldReturnNotFoundResult()
        {
            // Arrange
            const int performanceId = 8;
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(new List<Performance> { new Performance() });

            // Act
            var response = this._performanceController.GetById(performanceId);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), response.Result);
        }

        [Test]
        public void GetById_ValidIdPassed_ShouldReturnValidObjectAndOkObjectResult()
        {
            // Arrange
            const int performanceId = 1;
            var expected = this._performanceTestData.FirstOrDefault(p => p.Id == performanceId);
            
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(new List<Performance>() { expected });
            this._performanceRepository.Setup(rep => rep.GetItem(performanceId, null))
                .Returns(expected);

            // Act
            var response = this._performanceController.GetById(performanceId);

            // Assert
            Assert.AreEqual(expected, response.Value);
        }

        // Test GetSpeeches method
        [Test]
        public void GetSpeeches_InvalidIdPassed_ShouldReturnNotFoundResult()
        {  
            // Arrange
            const int performanceId = 8;
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(new List<Performance> { new Performance() });

            // Act
            var response = this._performanceController.GetSpeeches(performanceId);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), response.Result);
        }

        [Test]
        public void GetSpeeches_ValidIdPassed_ShouldReturnValidSpeecheAndOkObjectResult()
        {
            // Arrange
            const int performanceId = 1;
            var performance = this._performanceTestData.FirstOrDefault(p => p.Id == performanceId);

            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(new List<Performance> { performance });
            this._performanceRepository
                .Setup(rep => rep.GetItem(performanceId, It.IsAny<Func<IQueryable<Performance>, IIncludableQueryable<Performance, object>>>()).Speeches)
                .Returns(performance.Speeches);
            
            // Act
            var response = this._performanceController.GetSpeeches(performanceId).Result as OkObjectResult;
            
            // Assert
            Assert.AreEqual(performance.Speeches, response.Value);
            Assert.IsInstanceOf(typeof(OkObjectResult), response);
        }

        // Test Create method
        [Test]
        public void Create_PassedInvalidObject_ShouldReturnBadRequest()
        {
            // Arrange
            var performance = new Performance()
            {
                Id = 1,
                Description = "Description 1"
            };
            this._performanceController.ModelState.AddModelError("Description", "The Description property required");

            // Act
            var response = this._performanceController.Create(performance);
            
            // Assert
            Assert.IsInstanceOf(typeof(BadRequestResult), response.Result);
        }

        [Test]
        public void Create_PassedValidObject_ShouldReturnValidObjectAndCreatedAtObjectResult()
        {
            // Arrange
            var performance = new Performance
            {
                Id = 1,
                Title = "Performance 1",
                Description = "Description 1",
                Speeches = null
            };

            // Act
            var response = this._performanceController.Create(performance).Result as CreatedAtActionResult;

            // Assert
            Assert.AreEqual(performance, response.Value);
            Assert.IsInstanceOf(typeof(CreatedAtActionResult), response);
        }

        // Test Update method
        [Test]
        public void Update_InvalidObjectPassed_ShouldReturnBadRequest()
        {
            // Arrange
            var performance = new Performance { Id = 1, Description = "Description 1" };
            this._performanceController.ModelState.AddModelError("Title", "The Title property required");

            // Act
            var response = this._performanceController.Update(performance);

            // Assert
            Assert.IsInstanceOf(typeof(BadRequestResult), response.Result);
        }

        [Test]
        public void Update_ValidObjectPassed_ShouldReturnUpdatedObject()
        {
            // Arrange
            var performance = new Performance { Id = 1, Title = "Title 1", Description = "Description 1" };
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.Update(performance);

            // Assert
            Assert.AreEqual(performance, response.Value);
        }

        [Test]
        public void Update_NotExistingObjectPassed_ShouldReturnNotFoundResult()
        {
            // Arrange
            var performance = new Performance { Id = 8, Title = "Title 8", Description = "Description 8" };
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);

            // Act
            var response = this._performanceController.Update(performance);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), response.Result);
        }

        // Test Delete method
        [Test]
        public void Delete_InvalidIdPassed_ShouldReturnNotFoundResult()
        {
            // Arrange
            var performanceId = 8;
            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
                .Returns(this._performanceTestData);
            
            // Act
            var response = this._performanceController.Delete(performanceId);

            // Assert
            Assert.IsInstanceOf(typeof(NotFoundResult), response.Result);
        }

        [Test]
        public void Delete_ValidIdPassed_ShouldReturnDeletedObject()
        {
            // Arrange
            var performanceId = 1;
            var deletedPerformance = this._performanceTestData.FirstOrDefault(per => per.Id == performanceId);

            this._performanceRepository.Setup(rep => rep.GetAllItems(null))
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