using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;

namespace MPlayer.BLL.Interfaces
{
    public interface ISingerService:IDisposable
    {
        Task<OperationDetails> Create(SingerDTO genreDto);
        Task<OperationDetails> Delete(SingerDTO genreDto);
        Task<OperationDetails> Update(SingerDTO genreDto);

        IEnumerable<SingerDTO> GetAll();
        SingerDTO GetById(string id);
    }
}
