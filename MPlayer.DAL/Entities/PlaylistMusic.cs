using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPlayer.DAL.Entities
{
    public class PlaylistMusic
    {
        public string Id { get; set; }
        public string MusicId { get; set; }
        public string PlaylistId { get; set; }
        public virtual Music Music { get; set; }
        public  virtual  Playlist Playlist { get; set; }
    }
}
