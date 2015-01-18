using System;
using System.Threading.Tasks;
using AllAboutManga.Webservice.Libs;
using AllAboutManga.Webservice.MangaEden.Profiles;
using AutoMapper;
using ExceptionHelper;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace AllAboutManga.Webservice.MangaEden.Tests
{
    [TestClass]
    public class MangaCollectionServiceTest
    {
        private readonly IMangaCollectionService _mangaCollectionService;

        public MangaCollectionServiceTest()
        {
            Mapper.Initialize(mapperConfiguration => mapperConfiguration.AddProfile(new MangaCollectionProfile()));
            _mangaCollectionService = new MangaCollectionService(Mapper.Engine);
        }

        [TestMethod]
        public async Task MangaCollectionService_GetAllAsync_NoParameter_Success()
        {
            var mangaCollection = await _mangaCollectionService.GetAllAsync();

            Assert.IsNotNull(mangaCollection);
            Assert.AreEqual(-1, mangaCollection.End);
            Assert.AreEqual(-1, mangaCollection.Page);
            Assert.AreEqual(0, mangaCollection.Start);
            Assert.AreEqual(mangaCollection.Total, mangaCollection.Mangas.Count);
        }

        [TestMethod]
        public async Task MangaCollectionService_GetPageAsync_InvalidCollectionLength_ExceptionThrown()
        {
            await AsyncAssertion.ThrowsAsync<ArgumentOutOfRangeException>(() => _mangaCollectionService.GetSetAsync(0, 24));
            await AsyncAssertion.ThrowsAsync<ArgumentOutOfRangeException>(() => _mangaCollectionService.GetSetAsync(0, 2001));
        }

        [TestMethod]
        public async Task MangaCollectionService_GetPageAsync_Page0_Success()
        {
            var mangaCollection = await _mangaCollectionService.GetSetAsync(0, 25);

            Assert.IsNotNull(mangaCollection);
            Assert.AreEqual(25, mangaCollection.End);
            Assert.AreEqual(0, mangaCollection.Page);
            Assert.AreEqual(0, mangaCollection.Start);
        }
    }
}
