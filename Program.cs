/* (3/24/24, 2) Adding-in as many of the "as-is" contents of the "Program.cs" file available at 
https://github.com/MicrosoftDocs/mslearn-persist-data-ef-core/blob/main/ContosoPizza/Program.cs 
so as to ensure that the "template" I am working with as I ATTEMPT to get EF Core set-up in 
"PAPI-Libs" is AS CLOSE AS POSSIBLE to the version of "Program.cs" that is used/built-upon 
throughout the Microsoft tutorial I am following: */
using PAPI_Libs.Data;
using PAPI_Libs.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* (3/24/24, 6) Adding-in this code per step #3 under the "Scaffold models and DbContext" header of the content available at 
https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/3-migrations: */
builder.Services.AddSqlite<PAPI_LibContext>("Data Source=PAPI-Libs.db");

/* (3/24/24, 2) Adding-in as many of the "as-is" contents of the "Program.cs" file available at 
https://github.com/MicrosoftDocs/mslearn-persist-data-ef-core/blob/main/ContosoPizza/Program.cs 
so as to ensure that the "template" I am working with as I ATTEMPT to get EF Core set-up in 
"PAPI-Libs" is AS CLOSE AS POSSIBLE to the version of "Program.cs" that is used/built-upon 
throughout the Microsoft tutorial I am following: */
builder.Services.AddScoped<PAPI_LibService>(); /* (3/24/24, 3) Note that IF you take the `static` modifier 
out of the top-level "signature" for the `PAPI_LibService` class (i.e., see where it says `public class PAPI_LibService` 
in "PAPI_LibService.cs", the error line underneath `.AddScoped<PAPI_LibService>()` disappears. This MAY work since in 
the tutorial you are following, "PizzaService.cs" is NOT defined as a `static` class, it would seem (see 
https://github.com/MicrosoftDocs/mslearn-persist-data-ef-core/blob/main/ContosoPizza/Services/PizzaService.cs). */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* (3/24/24, 2) Should the following line of code be commented-out so that this file's contents are more in-accord with 
what can be seen at https://github.com/MicrosoftDocs/mslearn-persist-data-ef-core/blob/main/ContosoPizza/Program.cs (?): */
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

/* (3/24/24, 34) Added this line of code per step #3 under "The `DbInitializer` class is ready to seed the database, but it 
needs to be called from Program.cs. The following steps create an extension method for `IHost` that calls 
`DbInitializer.Initialize`:" (available at 
https://learn.microsoft.com/en-us/training/modules/persist-data-ef-core/4-interacting-data): */
app.CreateDbIfNotExists();

/* (3/24/24, 2) The following lines of code were present in "Program.cs" at 
https://github.com/MicrosoftDocs/mslearn-persist-data-ef-core/blob/main/ContosoPizza/Program.cs. They can probably be 
"commented-in" if/as necessary as you progress with your efforts: */
// Add the CreateDbIfNotExists method call
//app.MapGet("/", () => @"Contoso Pizza management API. Navigate to /swagger to open the Swagger test UI.");

app.Run();
