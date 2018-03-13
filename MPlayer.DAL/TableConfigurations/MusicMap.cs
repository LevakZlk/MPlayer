using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.TableConfigurations
{
    public class MusicMap : EntityTypeConfiguration<Music>
    {
        public MusicMap()
        {
            HasKey(x => x.Id);
            Property(s => s.Name).HasMaxLength(255).IsRequired();
            Property(s => s.Path).HasMaxLength(255).IsRequired();
            Property(x => x.UserId).IsRequired();
            Property(x => x.GenreId).IsRequired();
            Property(x => x.SingerId).IsRequired();
            HasMany(x => x.PlaylistMusic).WithRequired(x => x.Music).HasForeignKey(x => x.MusicId).WillCascadeOnDelete(true);
            ToTable("Music");
        }
    }
}
