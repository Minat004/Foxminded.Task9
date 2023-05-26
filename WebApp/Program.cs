global using Microsoft.EntityFrameworkCore;
global using WebApp.Data;
global using WebApp.Interfaces;
global using WebApp.Models;
global using WebApp.Repositories;
global using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IReadable<Course>, CourseRepository>();
builder.Services.AddScoped<IReadable<Course, Group>, CourseService>();

builder.Services.AddScoped<IRepository<Group>, GroupRepository>();
builder.Services.AddScoped<IService<Group, Student>, GroupService>();

builder.Services.AddScoped<IRepository<Student>, StudentRepository>();
builder.Services.AddScoped<IService<Student>, StudentService>();

var app = builder.Build();

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
    pattern: "{controller=Courses}/{action=Index}/{id?}");

app.Run();