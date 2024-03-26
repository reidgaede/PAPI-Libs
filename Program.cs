using PAPI_Libs.Data;
using PAPI_Libs.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlite<PAPI_LibContext>("Data Source=PAPI-Libs.db");

builder.Services.AddScoped<PAPI_LibService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.CreateDbIfNotExists();

/* (3/24/24, 2) The following lines of code were present in "Program.cs" at 
https://github.com/MicrosoftDocs/mslearn-persist-data-ef-core/blob/main/ContosoPizza/Program.cs. They can probably be 
"commented-in" if/as necessary as you progress with your efforts: */
// Add the CreateDbIfNotExists method call
//app.MapGet("/", () => @"Contoso Pizza management API. Navigate to /swagger to open the Swagger test UI.");

app.Run();
