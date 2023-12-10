using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using Supabase;
using Supabase.Gotrue;
using System;
using System.Text;

/* http://localhost:5106 */
using Chatable.Api.Hubs;


var builder = WebApplication.CreateBuilder(args);

var url = "https://hpgymhgysqheiuoetdfu.supabase.co";
var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImhwZ3ltaGd5c3FoZWl1b2V0ZGZ1Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MDE4NzgxMzYsImV4cCI6MjAxNzQ1NDEzNn0.dEwbwSr_aZeFRuUht9fSE347DDsKv0F4LxboGl_isQA";

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Supabase.Client>(_ =>

	new Supabase.Client(
			url, key,
			new SupabaseOptions
			{
				AutoRefreshToken = true,
				AutoConnectRealtime = true,
			}
));

var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(opt =>
	{
		opt.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false,
			ValidateIssuer = false,

			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
			ClockSkew = TimeSpan.Zero
		};
	});

builder.Services.AddAuthorizationBuilder().AddPolicy("owner", p =>
{
	p.RequireClaim("UserName");
	//p.RequireClaim("TokenId", Guid.NewGuid().ToString());
	p.RequireRole("owner");
});

// Add SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<CallHub>("call-hub");
app.MapHub<RoomHub>("room-hub");

app.Run();
