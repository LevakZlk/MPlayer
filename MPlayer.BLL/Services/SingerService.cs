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
    internal class SingerService:ISingerService
    {
        IUnitOfWork Database { get; set; }

        public SingerService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<OperationDetails> Create(SingerDTO SingerDto)
        {
            var Singers = Database.SingerManager.GetAll().ToList();
            var Singer = Singers.Find(x => x.Name == SingerDto.Name);
            if (Singer == null && SingerDto.Name != null)
            {
                Singer = new Singer { Id = Guid.NewGuid().ToString(), Name = SingerDto.Name };
                Database.SingerManager.Create(Singer);
                await Database.SaveAsync();
                return new OperationDetails(true, "Singer added successfully", "");
            }
            return new OperationDetails(false, "Singer is exist or empty", "Name");
        }
        public async Task<OperationDetails> Delete(SingerDTO SingerDto)
        {
            var Singer = Database.SingerManager.Get(SingerDto.Id);
            if (Singer != null)
            {
                Database.SingerManager.Delete(Singer.Id);
                await Database.SaveAsync();
                return new OperationDetails(true, "Singer deleted successfully", "");
            }
            return new OperationDetails(false, "Singer is not exist", "Name");
        }
        public async Task<OperationDetails> Update(SingerDTO SingerDto)
        {
            var Singer = Database.SingerManager.Get(SingerDto.Id);
            if (Singer != null)
            {
                Singer = new Singer { Id = SingerDto.Id, Name = SingerDto.Name };
                Database.SingerManager.Update(Singer);
                await Database.SaveAsync();
                return new OperationDetails(true, "Singer update successfully", "");
            }
            return new OperationDetails(false, "Singer is not exist", "Name");
        }

        public IEnumerable<SingerDTO> GetAll()
        {
            return Database.SingerManager.GetAll().Select(Singer => new SingerDTO() { Id = Singer.Id, Name = Singer.Name }).OrderBy(x => x.Name).ToList();
        }

        public SingerDTO GetById(string id)
        {
            var g = Database.SingerManager.Get(id);
            return new SingerDTO() { Id = g.Id, Name = g.Name };
        }
    }
}
