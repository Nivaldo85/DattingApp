using API;
using Microsoft.EntityFrameworkCore;

namespace API;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }



    public DbSet<AppUser> Users { get; set; }
}
