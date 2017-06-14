using Microsoft.EntityFrameworkCore;

namespace dotnetmvc.DataAccess
{
    public class SamplePostgresDbContext : DbContext
    {
        public SamplePostgresDbContext(DbContextOptions<SamplePostgresDbContext> options) : base(options)
        {

        }

        public DbSet<Attachment> Attachment { get; set; }
    }
}