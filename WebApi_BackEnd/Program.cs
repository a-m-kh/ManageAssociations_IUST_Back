using DataBase.Configuration.DBContexts;
using DataBase.Configuration.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Logic.Service.Services.Interface;
using Utility.Customize;
using Logic.Service.Services;
using Microsoft.OpenApi.Models;
using DataBase.Repository.Repositories.Interface;
using DataBase.Repository.Repositories;
using Repository;
using Logic.Service.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, MainContext>();

builder.Services.AddScoped<IAssociationRepository, AssociationRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAssociationService, AssociationService>();
builder.Services.AddScoped<IEventService, EventService>();
//builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddAutoMapper(typeof(Logic.Service.Mapper.AutoMapper));

builder.Services.AddTransient<IUserValidator<User>, OptionalEmailUserValidator<User>>();
builder.Services.AddDbContext<UserDBContext>(opt =>
{
	opt.UseSqlServer(connectionStr/*, b=>b.MigrationsAssembly("WebApi_BackEnd")*/);
});
builder.Services.AddDbContext<MainContext>(opt =>
{
	opt.UseSqlServer(connectionStr);
});
builder.Services.AddIdentity<User, IdentityRole<string>>().AddEntityFrameworkStores<UserDBContext>();
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
		ValidAudience = jwtSettings.GetSection("validAudience").Value,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
	};
});

builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});



builder.Services.AddCors(options => {
	options.AddPolicy("test",
		builder => {
			builder
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
		});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("test");

app.MapControllers();

app.Run();
