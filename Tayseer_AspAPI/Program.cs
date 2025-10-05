
//using Microsoft.EntityFrameworkCore;
//using System.Text.Json.Serialization;
//using Tayseer_AspMVC.Data;
//using Tayseer_AspMVC.Repository;
//using Tayseer_AspMVC.Repository.Base;


//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//options.UseLazyLoadingProxies().
//    UseSqlServer(conectionString));

//builder.Services.AddScoped(typeof(IRepository<>), typeof(MainRepository<>));

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//builder.Services.AddControllersWithViews().AddJsonOptions(o =>
//{
//    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//});




//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
