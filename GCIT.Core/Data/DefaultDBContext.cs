using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCIT.Core.Data
{
    public class DefaultDBContext: DbContext
    {
        public DefaultDBContext(DbContextOptions<DefaultDBContext> options) : base(options)
        {
        }

        public DefaultDBContext(): base()
        {

        }

        // Permite que contextos derivados pasen DbContextOptions<TDerived>
        public DefaultDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aplica configuraciones (IEntityTypeConfiguration<>) definidas en GCIT.Core
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultDBContext).Assembly);
        }
    }
}
