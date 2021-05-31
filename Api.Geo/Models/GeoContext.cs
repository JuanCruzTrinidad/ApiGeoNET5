using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Geo.Models
{
    public class GeoContext : DbContext
    {
        #region Constructors
        public GeoContext() { }

        public GeoContext(DbContextOptions<GeoContext> options)
            : base(options)
        {
        }
        #endregion

        public DbSet<Location> Location { get; set; }
        public DbSet<Process> Processes { get; set; }
    }
}
