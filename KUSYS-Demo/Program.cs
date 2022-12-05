using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Repositories.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



/// Add DI for DBContext
builder.Services.AddDbContext<DatabaseContext>(options=>options.UseSqlServer( builder.Configuration.GetConnectionString("conn")));
/// Add DI for Identity
 builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(Op => Op.LoginPath = "/UserAuthetication/Login");
/// Add DI for UserAuthenticationService
builder.Services.AddScoped<KUSYS_Demo.Repositories.Abstract.IUserAuthenticationService, UserAuthenticationService>();


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


///Add Authentication
app.UseAuthentication();

app.UseAuthorization();

/// Add Controller Address
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAuthentication}/{action=Login}/{id?}");

app.Run();
