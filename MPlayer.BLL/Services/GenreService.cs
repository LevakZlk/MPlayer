using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;
using MPlayer.BLL.Interfaces;
using MPlayer.DAL.Entities;
using MPlayer.DAL.Interfaces;

namespace MPlayer.BLL.Services
{
    internal class GenreService:IGenreService
    {
        IUnitOfWork Database { get; set; }

        public GenreService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose() => Database.Dispose();

        public async Task<OperationDetails> Create(GenreDTO genreDto)
        {
            var genres = Database.GenreManager.GetAll().ToList();
            var genre = genres.Find(x => x.Name == genreDto.Name);
            if (genre==null && genreDto.Name!=null)
            {
                genre = new Genre { Id=Guid.NewGuid().ToString(),Name = genreDto.Name };
                Database.GenreManager.Create(genre);
                await Database.SaveAsync();
                return new OperationDetails(true, "Genre added successfully", "");
            }
            return new OperationDetails(false, "Genre is exist or empty", "Name");
        }
        public async Task<OperationDetails> Delete(GenreDTO genreDto)
        {
            var genre = Database.GenreManager.Get(genreDto.Id);
            if (genre != null)
            {
                Database.GenreManager.Delete(genre.Id);
                await Database.SaveAsync();
                return new OperationDetails(true, "Genre deleted successfully", "");
            }
            return new OperationDetails(false, "Genre is not exist", "Name");
        }
        public async Task<OperationDetails> Update(GenreDTO genreDto)
        {
            var genre = Database.GenreManager.Get(genreDto.Id);
            if (genre != null)
            {
                genre = new Genre { Id = genreDto.Id, Name = genreDto.Name };
                Database.GenreManager.Update(genre);
                await Database.SaveAsync();
                return new OperationDetails(true, "Genre update successfully", "");
            }
            return new OperationDetails(false, "Genre is not exist", "Name");
        }

        public IEnumerable<GenreDTO> GetAll()
        {
            return Database.GenreManager.GetAll().Select(genre => new GenreDTO() {Id = genre.Id, Name = genre.Name}).OrderBy(x=>x.Name).ToList();
        }

        public GenreDTO GetById(string id)
        {
            var g= Database.GenreManager.Get(id);
            return new GenreDTO() {Id = g.Id, Name = g.Name};
        }
    }
}
