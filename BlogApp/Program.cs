using BlogApp.Business.Abstract;
using BlogApp.Business.Concrete;
using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Concrete.EntityFramework;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<BlogAppContext>(options =>
    options.UseSqlServer(connectionString));





//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<AppUser, AppRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BlogAppContext>()
    .AddDefaultTokenProviders(); ;

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

    options.LoginPath = "/Login/SignIn";
    options.AccessDeniedPath = "/Login/SignInd";
    options.SlidingExpiration = true;
});


builder.Services.AddScoped<IBlogDal, EfBlogDal>();
builder.Services.AddScoped<ILogDal, EfLogDal>();
builder.Services.AddScoped<ICommentDal, EfCommentDal>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<IAppUserDal, EfAppUserDal>();



builder.Services.AddScoped<IBlogService, BlogManager>();
builder.Services.AddScoped<ILogService, LogManager>();
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IAppUserService, AppUserManager>();


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
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=SignIn}/{id?}");

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();

