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
    internal class MusicService:IMusicService
    {
        IUnitOfWork Database { get; set; }
        public MusicService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<OperationDetails> Create(MusicDTO musicDTO)
        {
            var musics = Database.MusicManager.GetAll().ToList();
            var music = musics.Find(x => x.Name == musicDTO.Name);
            if (music == null && musicDTO.Name != null && musicDTO.UserId!=null)
            {
                music = new Music
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = musicDTO.Name,
                    Path = musicDTO.Path,
                    GenreId = musicDTO.GenreId,
                    SingerId = musicDTO.SingerId,
                    UserId = musicDTO.UserId
                };
                Database.MusicManager.Create(music);
                await Database.SaveAsync();
                return new OperationDetails(true, "Music added successfully", "");
            }
            return new OperationDetails(false, "Music is exist or empty", "Name");
        }

        public async Task<OperationDetails> Delete(MusicDTO musicDTO)
        {
            var music = Database.MusicManager.Get(musicDTO.Id);
            if (music != null)
            {
                Database.MusicManager.Delete(music.Id);
                await Database.SaveAsync();
                return new OperationDetails(true, "Music deleted successfully", "");
            }
            return new OperationDetails(false, "Music is not exist", "Name");
        }

        public async Task<OperationDetails> Update(MusicDTO musicDTO)
        {
            var music = Database.MusicManager.Get(musicDTO.Id);
            if (music != null)
            {
                music = new Music
                {
                    Id = musicDTO.Id,
                    Name = musicDTO.Name,
                    Path = musicDTO.Path,
                    GenreId = musicDTO.GenreId,
                    SingerId = musicDTO.SingerId,
                    UserId = musicDTO.UserId

                };
                Database.MusicManager.Update(music);
                await Database.SaveAsync();
                return new OperationDetails(true, "Music update successfully", "");
            }
            return new OperationDetails(false, "Music is not exist", "Name");
        }

        public IEnumerable<MusicDTO> GetAll()
        {
            return Database.MusicManager.GetAll().
                Select(music => new MusicDTO()
                {
                    Id = music.Id,
                    Name = music.Name,
                    Path = music.Path,
                    GenreId = music.GenreId,
                    SingerId = music.SingerId,
                    UserId = music.UserId
                }).OrderBy(x => x.Name).ToList();

        }

        public IEnumerable<MusicDTO> GetAllById(string id)
        {
            return Database.MusicManager.GetAllById(id).
               Select(music => new MusicDTO()
               {
                   Id = music.Id,
                   Name = music.Name,
                   Path = music.Path,
                   GenreId = music.GenreId,
                   SingerId = music.SingerId,
                   UserId = music.UserId
               }).OrderBy(x => x.Name).ToList();
        }

        public MusicDTO GetById(string id)
        {
            var music = Database.MusicManager.Get(id);
            return new MusicDTO() {
                Id = music.Id,
                Name = music.Name,
                Path = music.Path,
                GenreId = music.GenreId,
                SingerId = music.SingerId,
                UserId = music.UserId
            };
        }
    }
}
