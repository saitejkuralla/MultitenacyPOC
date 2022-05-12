
using IMM.Core.API;
var builder = WebApplication.CreateBuilder(args);
var startup = new IMMStartup(builder.Configuration);

// Add services to the container.

startup.ConfigureServices(builder.Services);

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

startup.Configure(app);

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();




//var builder = WebApplication.CreateBuilder(args);
//var startup = new IMMStartup(builder.Configuration);
//startup.ConfigureServices(builder.Services);
//var app = builder.Build();
//startup.Configure(app);
//app.Run();
