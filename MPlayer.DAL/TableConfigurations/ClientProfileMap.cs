using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPlayer.DAL.Entities;

namespace MPlayer.DAL.TableConfigurations
{
    public class ClientProfileMap: EntityTypeConfiguration<ClientProfile>
    {
        public ClientProfileMap()
        {
            HasKey(x => x.Id);

            ToTable("ClientProfile");
        }
    }
}
