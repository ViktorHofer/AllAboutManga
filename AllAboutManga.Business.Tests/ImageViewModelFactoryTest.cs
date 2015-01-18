using System;
using AllAboutManga.Business.ViewModels;
using AllAboutManga.Webservice.Libs;
using MetroLog;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;

namespace AllAboutManga.Business.Tests
{
    [TestClass]
    public class ImageViewModelFactoryTest
    {
        [TestMethod]
        public void ImageViewModelFactory_Ctor_Null_ExceptionThrown()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.ThrowsException<ArgumentNullException>(() => new ImageViewModelFactory(null, 
                null));
        }

        [TestMethod]
        public void ImageViewModelFactory_Ctor_ValidParameter_Success()
        {
            var factory = new ImageViewModelFactory(new Mock<IImageService>().Object,
                new Mock<ILogManager>().Object);
            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void ImageViewModelFactory_Create_NullParameter_ExceptionThrown()
        {
            var factory = new ImageViewModelFactory(new Mock<IImageService>().Object,
                new Mock<ILogManager>().Object);
            Assert.ThrowsException<ArgumentNullException>(() => factory.Create(null));
        }

        [TestMethod]
        public void ImageViewModelFactory_Create_ValidParameter_Success()
        {
            var factory = new ImageViewModelFactory(new Mock<IImageService>().Object,
                new Mock<ILogManager>().Object);
            factory.Create("urdksfjsd");
        }
    }
}
