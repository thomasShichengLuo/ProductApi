using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;
using Product.Core.Domain;

namespace Product.Data
{
    public class ProductDbContext
        : DbContext
    {

        #region Constructor

            public ProductDbContext(DbContextOptions<ProductDbContext> options)
                : base(options)
            {
                if (s_migrated[0])
                {
                    return;
                }

                lock (s_migrated)
                {
                    if (s_migrated[0] == false)
                    {
                        var memoryOptions = options.FindExtension<InMemoryOptionsExtension>();
                        if (memoryOptions == null)
                        {
                            this.Database.Migrate();
                        }

                        s_migrated[0] = true;
                    }
                }
            }

        #endregion


        #region Public Properties

            public DbSet<Core.Domain.Product> Products { get; set; }


        #endregion


        #region Private Properties

        private static readonly bool[] s_migrated = { false };

        #endregion


        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
               
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }
                

        #endregion
    }
}
