using Microsoft.EntityFrameworkCore;
using WuyiDAL.IReponsitory;
using WuyiDAL.Repository;
using WuyiDAL.Models;
using WuyiServices.IServices;
using WuyiServices.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Data Source=MSI\\SQLEXPRESS01;Initial Catalog=DB_WuyiMusic;Persist Security Info=True;User ID=sa;Password=hathanh2304;Trust Server Certificate=True"));


// Cấu hình xác thực JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});


// Đăng ký Repository và Service
builder.Services.AddTransient<IAuthenticateRepo, AuthenticateRepo>();
builder.Services.AddTransient<IAuthenticateService, AuthenticateService>();

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseRouting();
app.MapControllers();
app.UseFileServer();
app.Run();
