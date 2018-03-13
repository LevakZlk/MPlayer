using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;

namespace MPlayer.BLL.Interfaces
{
    public interface IPlaylistService : IDisposable
    {
        Task<OperationDetails> Create(PlaylistDTO playlistDTO);
        Task<OperationDetails> Delete(PlaylistDTO playlistDTO);
        Task<OperationDetails> Update(PlaylistDTO playlistDTO);

        IEnumerable<PlaylistDTO> GetAll();
        IEnumerable<PlaylistDTO> GetAllById(string id);
        PlaylistDTO GetById(string id);
    }
}
