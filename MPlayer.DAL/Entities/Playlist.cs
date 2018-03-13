using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPlayer.DAL.Entities
{
    public class Playlist
    {
        public Playlist ()
        {
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<PlaylistMusic> PlaylistMusic { get; set; } = new HashSet<PlaylistMusic>();
    }
}
