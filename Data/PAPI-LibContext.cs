/* Establishing the schema for the application's database context (i.e., declaring that the database will have two tables 
("PAPI_Libs" and "PAPI_LibTemplates"): */

using Microsoft.EntityFrameworkCore;
using PAPI_Libs.Models;

namespace PAPI_Libs.Data;

public class PAPI_LibContext : DbContext
{
    public PAPI_LibContext (DbContextOptions<PAPI_LibContext> options) : base(options)
    {
    }

    /* Creating `DbSet<T>`s for each table desired/needed in the database: */
    public DbSet<PAPI_Lib> PAPI_Libs => Set<PAPI_Lib>();
    public DbSet<PAPI_LibTemplate> PAPI_LibTemplates => Set<PAPI_LibTemplate>();
}
