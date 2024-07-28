using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using WuyiDAL.IReponsitory;
using WuyiDAL.Models;
using WuyiDAL.Repository;
using WuyiServices.IServices;
using WuyiServices.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Đăng ký constraint
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Data Source=MSI\\SQLEXPRESS01;Initial Catalog=DB_WuyiMusic;Persist Security Info=True;User ID=sa;Password=hathanh2304;Trust Server Certificate=True"));

// Đăng ký Repository và Service
builder.Services.AddTransient<IAuthenticateRepo, AuthenticateRepo>();

builder.Services.AddScoped(typeof(IAllReponsitories<User>), typeof(AllReponsitories<User>));
builder.Services.AddScoped(typeof(IServices<User>), typeof(Services<User>));
builder.Services.AddScoped(typeof(IAllReponsitories<Album>), typeof(AllReponsitories<Album>));
builder.Services.AddScoped(typeof(IServices<Album>), typeof(Services<Album>));
builder.Services.AddScoped(typeof(IAllReponsitories<Song>), typeof(AllReponsitories<Song>));
builder.Services.AddScoped(typeof(IServices<Song>), typeof(Services<Song>));
builder.Services.AddScoped(typeof(IAllReponsitories<UserFollowArtist>), typeof(AllReponsitories<UserFollowArtist>));
builder.Services.AddScoped(typeof(IServices<UserFollowArtist>), typeof(Services<UserFollowArtist>));
builder.Services.AddScoped(typeof(IAllReponsitories<Genre>), typeof(AllReponsitories<Genre>));
builder.Services.AddScoped(typeof(IServices<Genre>), typeof(Services<Genre>));
builder.Services.AddScoped(typeof(IAllReponsitories<Notification>), typeof(AllReponsitories<Notification>));
builder.Services.AddScoped(typeof(IServices<Notification>), typeof(Services<Notification>));
builder.Services.AddScoped(typeof(IAllReponsitories<Artist>), typeof(AllReponsitories<Artist>));
builder.Services.AddScoped(typeof(IServices<Artist>), typeof(Services<Artist>));
builder.Services.AddScoped(typeof(IAllReponsitories<Playlist>), typeof(AllReponsitories<Playlist>));
builder.Services.AddScoped(typeof(IServices<Playlist>), typeof(Services<Playlist>));
builder.Services.AddScoped(typeof(IAllReponsitories<PlaylistSong>), typeof(AllReponsitories<PlaylistSong>));
builder.Services.AddScoped(typeof(IServices<PlaylistSong>), typeof(Services<PlaylistSong>));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký xác thực Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });
builder.Services.AddHttpClient();

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
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();
app.UseCookiePolicy();
// Đăng ký routes ở cấp cao nhất

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Users}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ListSong}/{action=ListSong}/{id?}");

app.Run();
