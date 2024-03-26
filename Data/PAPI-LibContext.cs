using Microsoft.EntityFrameworkCore;
using PAPI_Libs.Models;

namespace PAPI_Libs.Data;

public class PAPI_LibContext : DbContext
{
    public PAPI_LibContext (DbContextOptions<PAPI_LibContext> options) : base(options)
    {
    }

    /* (3/24/24, 5) We create `DbSet<T>`s within our "context" file for each TABLE that we want in our database (!): */
    public DbSet<PAPI_Lib> PAPI_Libs => Set<PAPI_Lib>();
    public DbSet<PAPI_LibTemplate> PAPI_LibTemplates => Set<PAPI_LibTemplate>();
}
