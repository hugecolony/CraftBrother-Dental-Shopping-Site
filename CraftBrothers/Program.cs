using CraftBrothers.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();


});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<ApplicationDbContext>();
//sessions
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromMinutes(10);
    Options.Cookie.HttpOnly = true;
    Options.Cookie.IsEssential = true;

});
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{   //pipelines
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//middlewares

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapControllerRoute(

    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

