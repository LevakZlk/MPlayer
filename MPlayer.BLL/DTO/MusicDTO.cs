using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPlayer.BLL.DTO
{
    public class MusicDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string UserId { get; set; }
        public string GenreId { get; set; }
        public string SingerId { get; set; }
    }
}
