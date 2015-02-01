using AllAboutManga.Data.Libs;
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
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMangaCollectionService _mangaCollectionService;
        private readonly IMappingEngine _mappingEngine;

        public MangaService(IMangaRepository mangaRepository,
            IFavoriteRepository favoriteRepository,
            IMangaCollectionService mangaCollectionService,
            IMappingEngine mappingEngine)
        {
            if (mangaRepository == null) throw new ArgumentNullException(nameof(mangaRepository));
            if (favoriteRepository == null) throw new ArgumentNullException(nameof(favoriteRepository));
            if (mangaCollectionService == null) throw new ArgumentNullException(nameof(mangaCollectionService));
            if (mappingEngine == null) throw new ArgumentNullException(nameof(mappingEngine));

            _mangaRepository = mangaRepository;
            _favoriteRepository = favoriteRepository;
            _mangaCollectionService = mangaCollectionService;
            _mappingEngine = mappingEngine;
        }

        public async Task LoadAsync(bool loadOnlyIfNotExisting = true)
        {
            // If mangas already retrieved skip pulling from webservice
            if (loadOnlyIfNotExisting && await _mangaRepository.AnyAsync()) return;

            // Retrieve
            var webserviceMangas = await _mangaCollectionService.GetAllAsync();

            // Save in db
            var dataAccessMangas = webserviceMangas.Mangas
                .Select(manga => _mappingEngine.Map<Data.Libs.Models.Manga>(manga))
                .ToArray();
            await _mangaRepository.CreateAsync(dataAccessMangas);
        }

        public async Task<IReadOnlyCollection<Manga>> GetAllAsync()
        {
            var dataAccessMangas = await _mangaRepository.GetAllAsync();

            return new ReadOnlyCollection<Manga>(dataAccessMangas
                .Select(manga => _mappingEngine.Map<Manga>(manga))
                .ToList());
        }

        public async Task<IReadOnlyCollection<Manga>> GetFavoritesAsync()
        {
            var favorites = await _favoriteRepository.GetFavoritesAsync();

            List<Manga> mangas = new List<Manga>(favorites.Count);
            foreach (var favorite in favorites)
            {
                var manga = _mangaRepository.GetByIdAsync(favorite.MangaId);
                mangas.Add(_mappingEngine.Map<Manga>(manga));
            }

            return mangas;
        }
    }
}
