using Microsoft.EntityFrameworkCore;
using WeatherInfo.ApiClients;
using WeatherInfo.Data;
using WeatherInfo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WeatherContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IWeatherSearchHistoryService, WeatherSearchHistoryService>();
builder.Services.AddScoped<IWeatherForecastApiClient, WeatherForecastApiClient>(wf =>
{
    return new WeatherForecastApiClient(new HttpClient()
    {
        BaseAddress = new Uri(builder.Configuration.GetValue<string>("WeatherApi:BaseUrl")!)
    }, builder.Configuration.GetValue<string>("WeatherApi:ApiKey")!);
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WeatherContext>();
    await dbContext.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Weather}/{action=WeatherDetails}/{id?}");

app.Run();
