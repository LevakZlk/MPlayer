using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.BLL.DTO;
using MPlayer.BLL.Infrastructure;

namespace MPlayer.BLL.Interfaces
{
    public interface IGenreService : IDisposable
    {
        Task<OperationDetails> Create(GenreDTO genreDto);
        Task<OperationDetails> Delete(GenreDTO genreDto);
        Task<OperationDetails> Update(GenreDTO genreDto);

        IEnumerable<GenreDTO> GetAll();
        GenreDTO GetById(string id);
    }
}
