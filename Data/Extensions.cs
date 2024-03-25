/* (3/24/24, 33) Adding-in this file per step #1 under "The `DbInitializer` class is ready to seed the database, but it 
needs to be called from Program.cs. The following steps create an extension method for `IHost` that calls 
`DbInitializer.Initialize`:" (available at 
https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data): */

namespace PAPI_Libs.Data;

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<PAPI_LibContext>();
                context.Database.EnsureCreated();
                DbInitializer.Initialize(context);
            }
        }
    }
}
