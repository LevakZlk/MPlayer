using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.TableConfigurations
{
    public class PlaylistMusicMap : EntityTypeConfiguration<PlaylistMusic>
    {
        public PlaylistMusicMap()
        {
            HasKey(x => x.Id);
            Property(x => x.MusicId).IsRequired();
            Property(x => x.PlaylistId).IsRequired();
            ToTable("PlaylistMusic");
        }
    }
}
