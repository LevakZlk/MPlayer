using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.Interfaces
{
    public  interface IGenreManager: IDisposable
    {
        void Create(Genre item);
        void Update(Genre item);
        IEnumerable<Genre> GetAll();
        Genre Get(string id);
        void Delete(string id);

     
    }
}
