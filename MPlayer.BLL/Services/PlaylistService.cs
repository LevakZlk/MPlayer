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
    internal class PlaylistService:IPlaylistService
    {
        IUnitOfWork Database { get; set; }
        public PlaylistService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<OperationDetails> Create(PlaylistDTO playlistDTO)
        {
            var playlists = Database.PlaylistManager.GetAll().ToList();
            var playlist = playlists.Find(x => x.Name == playlistDTO.Name);
            if (playlist == null && playlistDTO.Name != null && playlistDTO.UserId != null)
            {

                playlist = new Playlist
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = playlistDTO.Name,
                    UserId = playlistDTO.UserId
                };
                List<PlaylistMusic> playlistMusics = playlistDTO.Musics.Select(x=>
                    new PlaylistMusic()
                    {
                        Id=Guid.NewGuid().ToString(),
                        PlaylistId = playlist.Id,
                        MusicId=x.Id
                    }).ToList();
                Database.PlaylistManager.Create(playlist);
                foreach (var pm in playlistMusics)
                {
                    Database.PlaylistMusicManager.Create(pm);
                }       
                await Database.SaveAsync();
                return new OperationDetails(true, "Playlist added successfully", "");
            }
            return new OperationDetails(false, "Playlist is exist or empty", "Name");
        }

        public async Task<OperationDetails> Delete(PlaylistDTO playlistDTO)
        {
            var playlist = Database.PlaylistManager.Get(playlistDTO.Id);
            if (playlist != null)
            {
                Database.PlaylistManager.Delete(playlist.Id);
                await Database.SaveAsync();
                return new OperationDetails(true, "Playlist deleted successfully", "");
            }
            return new OperationDetails(false, "Playlist is not exist", "Name");
        }

        public async Task<OperationDetails> Update(PlaylistDTO playlistDTO)
        {
            var playlist = Database.PlaylistManager.Get(playlistDTO.Id);
            if (playlist != null)
            {
                playlist = new Playlist
                {
                    Id = playlistDTO.Id,
                    Name = playlistDTO.Name,
                    UserId = playlistDTO.UserId
                };
                List<PlaylistMusic> playlistMusics = playlistDTO.Musics.Select(x =>
                   new PlaylistMusic()
                   {
                       Id = Guid.NewGuid().ToString(),
                       PlaylistId = playlist.Id,
                       MusicId = x.Id
                   }).ToList();
                Database.PlaylistManager.Update(playlist);
                Database.PlaylistMusicManager.DeleteByPlaylistId(playlist.Id);
                foreach (var pm in playlistMusics)
                {
                    Database.PlaylistMusicManager.Create(pm);
                }
                await Database.SaveAsync();
                return new OperationDetails(true, "Playlist update successfully", "");
            }
            return new OperationDetails(false, "Playlist is not exist", "Name");
        }

        public IEnumerable<PlaylistDTO> GetAll()
        {
            return Database.PlaylistManager.GetAll().
               Select(playlist => new PlaylistDTO()
               {
                   Id = playlist.Id,
                   Name = playlist.Name,
                   UserId = playlist.UserId,
                   Musics = playlist.PlaylistMusic.Select(x => new MusicDTO()
                   {
                       Id = x.MusicId
                   }).ToList()
               }).OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<PlaylistDTO> GetAllById(string id)
        {
            return Database.PlaylistManager.GetAllById(id).
              Select(playlist => new PlaylistDTO()
              {
                  Id = playlist.Id,
                  Name = playlist.Name,
                  UserId = playlist.UserId,
                  Musics = playlist.PlaylistMusic.Select(x => new MusicDTO()
                  {
                      Id = x.MusicId
                  }).ToList()
              }).OrderBy(x => x.Name).ToList();
        }

        public PlaylistDTO GetById(string id)
        {
            var playlist = Database.PlaylistManager.Get(id);
            return new PlaylistDTO()
            {
                Id = playlist.Id,
                Name = playlist.Name,
                UserId = playlist.UserId,
                Musics = playlist.PlaylistMusic.Select(x => new MusicDTO()
                {
                    Id = x.MusicId
                }).ToList()
            };
        }
    }
}
