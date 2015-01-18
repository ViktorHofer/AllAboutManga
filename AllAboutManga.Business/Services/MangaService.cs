using AllAboutManga.DataAccess.Libs;
using AutoMapper;
using System;
using AllAboutManga.Business.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using AllAboutManga.Webservice.Libs;

namespace AllAboutManga.Business.Services
{
    public class MangaService : Libs.Services.IMangaService
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly IMangaCollectionService _mangaCollectionService;
        private readonly IMappingEngine _mappingEngine;

        public MangaService(IMangaRepository mangaRepository,
            IMangaCollectionService mangaCollectionService,
            IMappingEngine mappingEngine)
        {
            if (mangaRepository == null) throw new ArgumentNullException(nameof(mangaRepository));
            if (mangaCollectionService == null) throw new ArgumentNullException(nameof(mangaCollectionService));
            if (mappingEngine == null) throw new ArgumentNullException(nameof(mappingEngine));
            _mangaRepository = mangaRepository;
            _mangaCollectionService = mangaCollectionService;
            _mappingEngine = mappingEngine;
        }

        public async Task LoadAsync()
        {
            // Retrieve
            var webserviceMangas = await _mangaCollectionService.GetAllAsync();

            // Save in db
            await _mangaRepository.Clear();
            var dataAccessMangas = _mappingEngine.Map<DataAccess.Libs.Models.MangaCollection>(webserviceMangas);
            await _mangaRepository.AddAll(dataAccessMangas.Mangas);
        }

        public async Task<IReadOnlyCollection<Manga>> GetAllAsync()
        {
            var dataAccessMangas = await _mangaRepository.GetAll();

            return new ReadOnlyCollection<Manga>(dataAccessMangas
                .Select(manga => _mappingEngine.Map<Manga>(manga))
                .ToList());
        }

        public async Task<IReadOnlyCollection<Manga>> GetFavoritesAsync()
        {
            // TODO: Retrieve favorites
            var favoriteIdList = new List<string>();
            //var dataAccessMangas = await _mangaRepository.Query(manga => favoriteIdList.Contains(manga.Id));
            var dataAccessMangas = (await _mangaRepository.GetAll()).Take(12);

            return new ReadOnlyCollection<Manga>(dataAccessMangas
                .Select(manga => _mappingEngine.Map<Manga>(manga))
                .ToList());
        }
    }
}
