using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.TableConfigurations
{
    public class GenreMap : EntityTypeConfiguration<Genre>
    {
        public GenreMap()
        {
            HasKey(x => x.Id);
            
            Property(s => s.Name).HasMaxLength(255).IsRequired();
            HasMany(x => x.Musics).WithRequired(x => x.Genre).HasForeignKey(x => x.GenreId).WillCascadeOnDelete(true);
            ToTable("Genre");
        }
    }
}
