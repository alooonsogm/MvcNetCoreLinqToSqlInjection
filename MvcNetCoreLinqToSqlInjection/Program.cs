using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

Coche car = new Coche();
car.Marca = "Porche";
car.Modelo = "Blanco";
car.Imagen = "porsche.jpg";
car.Velocidad = 0;
car.VelocidadMax = 200;
builder.Services.AddSingleton<ICoche, Coche>(x => car);

//Resolvemos el servicio Coche para la inyeccion.
//builder.Services.AddTransient<Coche>(); --> Hace un new Coche por cada peticion a CocheController
//builder.Services.AddSingleton<Coche>(); --> Hace un new Coche solo la primera peticion a CocheController
//builder.Services.AddSingleton<ICoche, Deportivo>();

//Los repos suelen ir en Transient
builder.Services.AddTransient<IRepositoryDoctores, RepositoryDoctoresOracle>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
