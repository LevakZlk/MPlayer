using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPlayer.Models
{
    public class PlaylistModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public List<MusicModel> Musics { get; set; }
    }
}