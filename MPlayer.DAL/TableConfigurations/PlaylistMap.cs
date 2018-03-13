using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.TableConfigurations
{
    public class PlaylistMap : EntityTypeConfiguration<Playlist>
    {
        public PlaylistMap()
        {
            HasKey(x => x.Id);
            Property(s => s.Name).HasMaxLength(255).IsRequired();
            Property(x => x.UserId).IsRequired();
            HasMany(x => x.PlaylistMusic).WithRequired(x => x.Playlist).HasForeignKey(x => x.PlaylistId).WillCascadeOnDelete(true);
            ToTable("Playlist");
        }
    }
}
