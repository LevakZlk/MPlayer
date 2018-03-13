using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPlayer.DAL.Entities
{
    public class Singer
    {
        public Singer()
        {
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Music> Musics { get; set; } = new HashSet<Music>();
    }
}
