global using Microsoft.EntityFrameworkCore;
global using WebApp.Data;
global using WebApp.Interfaces;
global using WebApp.Models;
global using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UniversityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityConnection")));

builder.Services.AddScoped<ICourseService<Course>, CourseService>();
builder.Services.AddScoped<IGroupService<Group>, GroupService>();
builder.Services.AddScoped<IService<Student>, StudentService>();
builder.Services.AddScoped<ICancelable, CancelService>();

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