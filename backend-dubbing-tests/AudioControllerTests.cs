namespace SoftServe.ITAcademy.BackendDubbingProjectTests
{
    using System.Collections.Generic;
    using System.Linq;
    using BackendDubbingProject.Controllers;
    using BackendDubbingProject.Models;
    using BackendDubbingProject.Utilities;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class AudioControllerTests
    {
        private Mock<IRepository<Audio>> audioRepository = null;
        private Mock<IRepository<Speech>> speechRepository = null;
        private AudioController audioController = null;
        private readonly List<Audio> audioTestData = null;
        private readonly List<Audio> audiTestDataWithoutAudio = null;
        private readonly List<Speech> speechTestData = null;

        public AudioControllerTests()
        {
            speechTestData = new List<Speech>()
            {
                new Speech() {PerformanceId = 1, Text = "Text", Id = 2},
                new Speech() {PerformanceId = 1, Text = "Text2", Id = 1}
            };
            audioTestData = new List<Audio>
            {
                new Audio
                {
                    Id = 1, LanguageId = 1, SpeechId = 1, FileName = "audio1.mp3",
                    Speech = speechTestData.ElementAt(1)
                },
                new Audio
                {
                    Id = 2, LanguageId = 2, SpeechId = 1, FileName = "audio2.mp3",
                    Speech = speechTestData.ElementAt(1)
                },
                new Audio
                {
                    Id = 3, LanguageId = 1, SpeechId = 2, FileName = "audio3.mp3",
                    Speech = speechTestData.ElementAt(0)
                },
                new Audio
                {
                    Id = 4, LanguageId = 2, SpeechId = 2, FileName = "audio4.mp3",
                    Speech = speechTestData.ElementAt(0)
                },
            };

            audiTestDataWithoutAudio = new List<Audio>
            {
            };
        }

        [SetUp]
        public void Setup()
        {
            audioRepository = new Mock<IRepository<Audio>>();
            speechRepository = new Mock<IRepository<Speech>>();
//            audioController = new AudioController(audioRepository.Object, speechRepository.Object);
        }


        [Test]
        public void DummyTest()
        {
            Assert.AreEqual(1, 1);
        }

//        [Test]
//        public void Get_WhenCalled_ReturnAllItems()
//        {
//            audioRepository
//                .Setup(repo => repo.GetAllItemsAsync(null))
//                .ReturnsAsync(audioTestData);
//
//            var result = audioController.Get();
//
//            Assert.That(result, Is.EquivalentTo(audioTestData));
//        }

//
//        [Test]
//        public void Get_WhenCalled_ReturnNoAudio()
//        {
//            audioRepository
//                .Setup(repo => repo.GetAllItemsAsync(null))
//                .ReturnsAsync(audiTestDataWithoutAudio);
//
//            var result = audioController.Get();
//
//            Assert.That(result, Is.EquivalentTo(audiTestDataWithoutAudio));
//        }
//
//        [Test]
//        public void Get_WhenCalled_ReturnIEnumerableResult()
//        {
//            var result = audioController.Get();
//
//            Assert.IsInstanceOf(typeof(IEnumerable<Audio>), result);
//        }
//
//        [Test]
//        public void GetById_ValidId_ShouldReturnValidObject()
//        {
//            const int id = 1;
//
//            var expected = audioTestData.FirstOrDefault(p => p.Id == id);
//
//            audioRepository.Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(audioTestData);
//
//            audioRepository.Setup(rep => rep.GetItemAsync(id, null))
//                .ReturnsAsync(expected);
//
//            var result = audioController.GetById(id);
//
//            Assert.AreEqual(expected, result.Result.Value);
//        }
//
//        [Test]
//        public async Task GetById_InvalidId_ShouldReturnNotFoundResult()
//        {
//            const int id = 10;
//
//            audioRepository.Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(audioTestData);
//
//            var result = await audioController.GetById(id);
//
//            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
//        }
//
//        // TODO: create test for Upload
//        // Cannot creted IFromFileAudio
//        /* [Test]
//        public void Upload_ValidObject_ShouldReturnOkResult()
//        {
//            var fileMock = new Mock<IFormFile>();
//            var physicalFile = new FileInfo("test.mp3");
//            var ms = new MemoryStream();
//            var writer = new StreamWriter(ms);
//            writer.Write(physicalFile.OpenRead());
//            writer.Flush();
//            ms.Position = 0;
//            var fileName = physicalFile.Name;
//            fileMock.Setup(_ => _.FileName).Returns(fileName);
//            fileMock.Setup(_ => _.Length).Returns(ms.Length);
//            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
//            fileMock.Setup(_ => _.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));
//            var file = fileMock.Object;
//
//            var newAudio = new AudioDTO { AudioFile = file, LanguageId = 1, SpeechId = 3 };
//
//            var result = this.audioController.Upload(newAudio).Result as OkResult;
//
//            Assert.IsInstanceOf(typeof(OkResult), result);
//        }*/
//
//        /*[Test]
//        public void Create_ValidObject_ShouldReturnCreatedAtAction()
//        {
//            var audio = new Audio { Id = 5, LanguageId = 1, SpeechId = 3, FileName = "audio5.mp3" };
//
//            var result = this.audioController.Create(audio).Result as CreatedAtActionResult;
//
//            Assert.AreEqual(audio, result.Value);
//            Assert.IsInstanceOf(typeof(CreatedAtActionResult), result);
//        }*/
//
////        [Test]
////        public void Create_NullObject_ShouldReturnBadRequest()
////        {
////            var result = this.audioController.Create(null);
////
////            Assert.IsInstanceOf(typeof(BadRequestResult), result.Result);
////        }
//
////        [Test] 
////        public void Update_ValidObject_ShouldReturnUpdatedObject()
////        {
////            this.speechRepository.Setup(rep => rep.GetAllItems(null))
////                .Returns(this.speechTestData);
////            var audio = new Audio { Id = 4, LanguageId = 1, SpeechId = 2, FileName = "audio5.mp3", Speech = this.speechTestData.ElementAt(0) };
////            this.audioRepository.Setup(rep => rep.GetAllItems(null))
////                .Returns(this.audioTestData);
////
////            var result = this.audioController.Update(audio);
////
////            Assert.AreEqual(audio, result.Value);
////        }
//
////        [Test]
////        public void Update_NullObject_ShouldReturnBadRequest()
////        {
////            this.audioRepository.Setup(rep => rep.GetAllItems(null))
////                .Returns(this.audioTestData);
////
////            var result = this.audioController.Update(null);
////
////            Assert.IsInstanceOf(typeof(BadRequestResult), result.Result);
////        }
//
//        [Test]
//        public void Update_NotExistObject_ShouldReturnNotFound()
//        {
//            var audio = new Audio { Id = 10, LanguageId = 1, SpeechId = 3, FileName = "audio5.mp3" };
//
//            audioRepository.Setup(rep => rep.GetAllItemsAsync(null))
//                .ReturnsAsync(audioTestData);
//
//            var result = audioController.Update(audio);
//
//            Assert.IsInstanceOf(typeof(NotFoundResult), result.Result);
//        }
//
//        [TearDown]
//        public void FreeResources()
//        {
//            audioController = null;
//            audioRepository = null;
//        }
    }
}