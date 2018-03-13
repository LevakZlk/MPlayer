using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.TableConfigurations
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(x => x.Id);
            Property(s => s.Name).HasMaxLength(256).IsRequired();
            ToTable("Role");
        }
    }
}
