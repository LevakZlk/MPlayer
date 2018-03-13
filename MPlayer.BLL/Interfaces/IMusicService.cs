using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;

namespace MPlayer.BLL.Interfaces
{
    public interface IMusicService : IDisposable
    {
        Task<OperationDetails> Create(MusicDTO musicDTO);
        Task<OperationDetails> Delete(MusicDTO musicDTO);
        Task<OperationDetails> Update(MusicDTO musicDTO);

        IEnumerable<MusicDTO> GetAll();
        IEnumerable<MusicDTO> GetAllById(string id);
        MusicDTO GetById(string id);
    }
}
