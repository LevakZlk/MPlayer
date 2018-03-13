using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPlayer.DAL.Entities
{
    public class Music
    {
        public Music()
        {
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string UserId { get; set; }
        public string GenreId { get; set; }
        public string SingerId { get; set; }
        public virtual User User { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Singer Singer { get; set; }
        public virtual ICollection<PlaylistMusic> PlaylistMusic { get; set; } = new List<PlaylistMusic>();
    }
}
